using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;

namespace QAliber.DAL
{
	public class Schedule
	{

		private int scenarioID;
		private int scheduleID;

		

		public event EventHandler<ScheduleChangedEventArgs> ScheduleChanged;

		public int ScheduleID
		{
			get { return scheduleID; }
			set { scheduleID = value; }
		}

		public int ScenarioID
		{
			get { return scenarioID; }
			set { scenarioID = value; }
		}

		private DateTime nextSchedule;

		public DateTime NextSchedule
		{
			get { return nextSchedule; }
			set { nextSchedule = value; }
		}

		public void StartListen()
		{
			SqlDependency.Stop(Settings.Default.AutomationConnectionString);
			SqlDependency.Start(Settings.Default.AutomationConnectionString);
			
		}

		public void StopListen()
		{
			SqlDependency.Stop(Settings.Default.AutomationConnectionString);

		}

		public void GetClosestSchedule()
		{
			try
			{
				scenarioID = -1;
				nextSchedule = DateTime.MaxValue;
				using (SqlConnection conn = new SqlConnection(Settings.Default.AutomationConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand("dbo.GetClosestSchedule"))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Clear();
						cmd.Parameters.Add(new SqlParameter("@date", DateTime.Now));
						cmd.Parameters.Add(new SqlParameter("@agentip", QAliber.DAL.Data.Current.AgentData.IP));
						cmd.Notification = null;
						cmd.Connection = conn;

						SqlDependency dependency = new SqlDependency(cmd);
						dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
						
						conn.Open();
						using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
						{
							if (rdr.HasRows)
							{
								rdr.Read();
								scenarioID = rdr.GetInt32(rdr.GetOrdinal("ScenarioID"));
								scheduleID = rdr.GetInt32(rdr.GetOrdinal("ScheduleID"));
								nextSchedule = rdr.GetDateTime(rdr.GetOrdinal("StartTime"));
							}
							rdr.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}

		protected virtual void OnScheduleChanged()
		{
			if (ScheduleChanged != null)
				ScheduleChanged(this, new ScheduleChangedEventArgs(nextSchedule, scheduleID));
		}

		private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
		{
			SqlDependency dependency = sender as SqlDependency;

			// Notices are only a one shot deal
			// so remove the existing one so a new 
			// one can be added
			dependency.OnChange -= OnDependencyChange;
			OnScheduleChanged();
		}


	}

	public class ScheduleChangedEventArgs : EventArgs
	{
		private DateTime newTime;
		private int scheduleID;

		public ScheduleChangedEventArgs(DateTime newTime, int scheduleID)
		{
			this.newTime = newTime;
			this.scheduleID = scheduleID;
		}

		public DateTime NewTime
		{
			get { return newTime; }
			set { newTime = value; }
		}

		public int ScheduleID
		{
			get { return scheduleID; }
			set { scheduleID = value; }
		}
	}
}
