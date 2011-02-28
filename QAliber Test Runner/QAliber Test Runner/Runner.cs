using System;
using System.Collections.Generic;
using System.Text;
using QAliber.RemotingModel;
using System.Windows.Forms;
using QAliber.TestModel;

namespace QAliber.Runner
{
	public class Runner
	{
		public Runner(Notifier notifier)
		{
			
			sink = new NotifySink();
			sink.Control = notifier;
			TestController.Default.OnExecutionStateChanged += new ExecutionStateChangedCallback(sink.FireExecutionStateChangedCallback);
			TestController.Default.OnStepStarted += new StepStartedCallback(sink.FireStepStartedCallback);
			TestController.Default.OnStepResultArrived += new StepResultArrivedCallback(sink.FireStepResultArrivedCallback);
			TestController.Default.OnLogResultArrived += new LogResultArrivedCallback(sink.FireLogResultArrivedCallback);
			TestController.Default.OnBreakPointReached += new BreakPointReachedCallback(sink.FireBreakPointReachedCallback);


		}

		internal static bool DBAlive
		{
			get { return dbAlive && Properties.Settings.Default.UseDatabase; }
			set { dbAlive = value; }
		}

		public void Run(int scheduleID, int scenarioID, string assemblyDir, string filename)
		{
			sink.lastErrors = null;
			sink.lastWarnings = null;
			try
			{
				try
				{
					if (DBAlive)
					{
						if (DAL.Data.Current.FindAgentByIP(DAL.Data.Current.AgentData.IP) == 0)
							DAL.Data.Current.RegisterAgent();
						QAliber.DAL.Data.Current.ChangeAgentStatus(QAliber.DAL.AgentStatusType.Running);
						if (scheduleID > 0)
						{
							scenarioID = DAL.Data.Current.GetScenarioByScheduleID(scheduleID);
						}
						else if (scenarioID > 0)
						{
							DAL.Data.Current.SetScenarioByID(scenarioID);
						}
						else
						{
							DAL.Data.Current.AddScenario(System.IO.Path.GetFileNameWithoutExtension(filename), filename,
														 new System.IO.FileInfo(filename).LastWriteTime, "");
							scenarioID = DAL.Data.Current.ScenarioData.ID;
						}
						DAL.Data.Current.AddRun(scheduleID, scenarioID,
												TestController.Default.CreateLogDirectory() + @"\Run.qlog");
					}
				}
				catch (System.Data.SqlClient.SqlException)
				{
					DBAlive = false;
				}
				if (DBAlive)
				{
					System.Net.WebClient webClient = new System.Net.WebClient();
					webClient.DownloadFile(DAL.Data.Current.ScenarioData.Filename, "DownloadedScenario.scn");
					TestController.Default.Run("DownloadedScenario.scn");
				}
				else
				{
					TestController.Default.Run(filename);
				}
			}
			catch (Exception ex)
			{

				sink.Control.notifyIcon.ShowBalloonTip(int.MaxValue, "Error Running Scenario", ex.Message, ToolTipIcon.Error);
				if (Program.cmdArgs.ExitAfterRun)
					Application.Exit();
			}
			finally
			{
				if (DBAlive)
				{
					QAliber.DAL.Data.Current.ChangeAgentStatus(QAliber.DAL.AgentStatusType.Up);
				}

			}
		}

		

		private static bool dbAlive;
		private NotifySink sink;
	}

	class NotifySink : NotifyCallbackSink
	{

		public NotifySink()
		{
			Logger.Log.Default.BeforeWarningIsPosted += new EventHandler<QAliber.Logger.LogEventArgs>(BeforeWarningIsPosted);
			Logger.Log.Default.BeforeErrorIsPosted += new EventHandler<QAliber.Logger.LogEventArgs>(BeforeErrorIsPosted);
		}

		public Notifier Control
		{
			get { return notifier; }
			set { notifier = value; }
		}
	
		protected override void OnExecutionStateChanged(ExecutionState state)
		{
			
			switch (state)
			{
				case ExecutionState.InProgress:
					if (Runner.DBAlive)
					{
						try
						{
							DAL.Data.Current.ChangeAgentStatus(QAliber.DAL.AgentStatusType.Running);
						}
						catch (System.Data.SqlClient.SqlException)
						{
						}
					}
					break;
				case ExecutionState.Executed:
					if (Runner.DBAlive)
					{
						try
						{
							DAL.Data.Current.ChangeAgentStatus(QAliber.DAL.AgentStatusType.Idle);
						}
						catch (System.Data.SqlClient.SqlException)
						{
						}
					}
						
					if (Program.cmdArgs.ExitAfterRun)
						Application.Exit();
					break;
				case ExecutionState.Paused:
					break;
				default:
					break;
			}
			notifier.ChangeEnabledState(state);
		}

		protected override void OnStepResultArrived(TestCaseResult result)
		{
			try
			{
				bool passed = result == TestCaseResult.Passed ? true : false;
				if (passed)
					DAL.Data.Current.RunData.NumPassed++;
				else
					DAL.Data.Current.RunData.NumFailed++;
				if (Runner.DBAlive)
					DAL.Data.Current.InsertTestCase(passed);
			}
			catch (System.Data.SqlClient.SqlException)
			{
			}
		}

		protected override void OnStepStarted(int id)
		{
			DAL.TestCase tc = new QAliber.DAL.TestCase();
			tc.Name = TestCase.Current.GetType().ToString();
			tc.AssemblyName = TestCase.Current.GetType().AssemblyQualifiedName;
			tc.StartTime = DateTime.Now;
			tc.Errors = string.Empty;
			tc.Warnings = string.Empty;
			DAL.Data.Current.TestCasesData.Push(tc);
			string balloonText = BuildBalloonString(out balloonIcon);
			notifier.notifyIcon.BalloonTipText = balloonText;
			notifier.notifyIcon.BalloonTipIcon = balloonIcon;
			notifier.notifyIcon.ShowBalloonTip(int.MaxValue);
		}

		protected override void OnLogResultArrived(string logFile)
		{
			if (Runner.DBAlive)
			{
				try
				{
					DAL.Data.Current.UpdateRun();
				}
				catch (System.Data.SqlClient.SqlException ex)
				{
					MessageBox.Show(ex.Message, "Test");
				}
			}
			notifier.notifyIcon.ShowBalloonTip(int.MaxValue, "Run Ended", "Click the balloon to view the log", balloonIcon);
		}

		protected override void OnBreakPointReached()
		{
			
		}

		private void BeforeErrorIsPosted(object sender, QAliber.Logger.LogEventArgs e)
		{
			DAL.Data.Current.TestCasesData.Peek().Errors += e.LogEntryProperties.Message + "\n";
			if (string.IsNullOrEmpty(lastErrors))
			{
				lastErrors = e.LogEntryProperties.Message;
			}
			else
			{
				string[] lines = lastErrors.Split('\n');
				if (lines.Length < 3)
					lastErrors += "\n" + e.LogEntryProperties.Message;
				else
				{
					lastErrors = lines[1] + "\n" + lines[2] + "\n" + e.LogEntryProperties.Message;
				}
			}
		}

		private void BeforeWarningIsPosted(object sender, QAliber.Logger.LogEventArgs e)
		{
			DAL.Data.Current.TestCasesData.Peek().Warnings += e.LogEntryProperties.Message + "\n";
			if (string.IsNullOrEmpty(lastWarnings))
			{
				lastWarnings = e.LogEntryProperties.Message;
			}
			else
			{
				string[] lines = lastWarnings.Split('\n');
				if (lines.Length < 3)
					lastWarnings += "\n" + e.LogEntryProperties.Message;
				else
				{
					lastWarnings = lines[1] + "\n" + lines[2] + "\n" + e.LogEntryProperties.Message;
				}
			}
		}

		private string BuildBalloonString(out ToolTipIcon icon)
		{
			icon = ToolTipIcon.Info;
			StringBuilder sb = new StringBuilder();
			sb.Append(TestCase.Current.Name + "\n" + TestCase.Current.Description);
			if (!string.IsNullOrEmpty(lastErrors))
			{
				icon = ToolTipIcon.Error;
				sb.Append("\nLast Errors\n" + lastErrors);
			}
			else if (!string.IsNullOrEmpty(lastWarnings))
			{
				icon = ToolTipIcon.Warning;
				sb.Append("\nLast Warnings\n" + lastWarnings);
			}
			return sb.ToString();

		}

		private ToolTipIcon balloonIcon = ToolTipIcon.Info;
		internal string lastWarnings;
		internal string lastErrors;
		
	   private Notifier notifier;
	}

}
