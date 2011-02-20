using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace QAliber.AgentService
{
	public partial class QAliberAgentService : Component
	{
		public QAliberAgentService()
		{
			InitializeComponent();
			InitService();
			notifyIcon.Visible = true;
			Application.ApplicationExit += new EventHandler(OnApplicationExit);
			OnStart(this, EventArgs.Empty);
				
		}


		protected void OnStop(object sender, EventArgs e)
		{
			if (listening)
			{
				QAliber.DAL.Data.Current.ScheduleData.StopListen();
				QAliber.DAL.Data.Current.ChangeAgentStatus(QAliber.DAL.AgentStatusType.Down);
				scheduleTimer.Stop();
				stopStripMenuItem.Enabled = false;
				startStripMenuItem.Enabled = true;
				listening = false;
			}
		}


		protected void OnStart(object sender, EventArgs e)
		{
			if (!listening)
			{
				if (QAliber.DAL.Data.Current.FindAgentByIP(QAliber.DAL.Data.Current.AgentData.IP) == 0)
					QAliber.DAL.Data.Current.RegisterAgent();
				QAliber.DAL.Data.Current.ChangeAgentStatus(QAliber.DAL.AgentStatusType.Up);
				QAliber.DAL.Data.Current.ScheduleData.StartListen();
				QAliber.DAL.Data.Current.ScheduleData.GetClosestSchedule();
				scheduleTimer.Start();
				stopStripMenuItem.Enabled = true;
				startStripMenuItem.Enabled = false;
				listening = true;
			}
		}

		protected void OnExit(object sender, EventArgs e)
		{
			OnStop(this, EventArgs.Empty);
			Application.Exit();
		}

		private void InitService()
		{
			if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\QAlibers") != null)
			{
				execPath = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\QAlibers").GetValue("RunnerPath").ToString();
			}
			else
			{
				execPath = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\QAlibers").GetValue("RunnerPath").ToString();
			}
			
			QAliber.DAL.Data.Current.ScheduleData.ScheduleChanged += new EventHandler<QAliber.DAL.ScheduleChangedEventArgs>(ScheduleData_ScheduleChanged);
			scheduleTimer.Elapsed += new System.Timers.ElapsedEventHandler(scheduleTimer_Elapsed);
			
		}

		private void ScheduleData_ScheduleChanged(object sender, QAliber.DAL.ScheduleChangedEventArgs e)
		{
			QAliber.DAL.Data.Current.ScheduleData.GetClosestSchedule();
		}

		private void scheduleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				scheduleTimer.Enabled = false;
				if (DateTime.Now > QAliber.DAL.Data.Current.ScheduleData.NextSchedule && QAliber.DAL.Data.Current.ScheduleData.ScenarioID > 0)
				{
					ProcessStartInfo startInfo = new ProcessStartInfo(execPath + @"\QAliber Test Runner.exe", string.Format("-schid={0} -exit=true", QAliber.DAL.Data.Current.ScheduleData.ScheduleID));
					startInfo.WorkingDirectory = execPath;
					Process.Start(startInfo);
					QAliber.DAL.Data.Current.ScheduleData.GetClosestSchedule();
				}
			}
			finally
			{
				scheduleTimer.Enabled = true;
			}
		}

		private void OnApplicationExit(object sender, EventArgs e)
		{
			OnStop(this, EventArgs.Empty);
		}

		private bool listening;
		private string execPath;

	   
	}
}
