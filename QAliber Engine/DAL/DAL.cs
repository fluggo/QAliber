using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QAliber.DAL
{
	public class Data
	{

		public static Data Current
		{
			get
			{
				if (current == null)
					current = new Data();
				return current;
			}
		}

		public Agent AgentData
		{
			get { return agent; }
		}

		public Scenario ScenarioData
		{
			get { return scenario; }
		}

		public Schedule ScheduleData
		{
			get { return schedule; }
		}

		public Run RunData
		{
			get { return run; }
		}

		public Stack<TestCase> TestCasesData
		{
			get { return testcases; }
		}

		public string ConnectionString
		{
			get { return connString; }
			set { connString = value; }

		}

		#region Agent Methods

		public void RegisterAgent()
		{
			agent.ID = (int)storedProcedures.RegisterAgent(agent.IP, agent.Name, agent.CPU, agent.Memory, agent.Model, agent.OS, (int)agent.Status);
		}

		public void ChangeAgentStatus(AgentStatusType status)
		{
			agent.Status = status;
			storedProcedures.ChangeAgentStatus(agent.ID, (int?)status);
		}

		public int FindAgentByIP(string ip)
		{
			int? id = (int?)storedProcedures.FindAgent(ip, String.Empty);
			if (id.HasValue)
			{
				agent.ID = id.Value;
				return agent.ID;
			}
			return 0;
		}

		public int FindAgentByName(string name)
		{
			int? id = (int?)storedProcedures.FindAgent(String.Empty, name);
			if (id.HasValue)
			{
				agent.ID = id.Value;
				return agent.ID;
			}
			return 0;
		}

		#endregion

		#region Scenario Methods

		public bool SetScenarioByID(int id)
		{
			AutomationDataSetTableAdapters.ScenariosTableAdapter sTA = new QAliber.DAL.AutomationDataSetTableAdapters.ScenariosTableAdapter();
			AutomationDataSet.ScenariosDataTable table = sTA.GetDataBy(id);
			if (table.Rows.Count > 0)
			{
				scenario.ID = table[0].ID;
				scenario.Name = table[0].Name;
				scenario.Filename = table[0].Filename;
				scenario.Description = table[0].Description;
				scenario.Attachments = table[0].Attachments;
				return true;
			}
			return false;
		}

		public int GetScenarioByScheduleID(int scheduleID)
		{
			AutomationDataSetTableAdapters.SchedulesTableAdapter sTA = new QAliber.DAL.AutomationDataSetTableAdapters.SchedulesTableAdapter();
			AutomationDataSet.SchedulesDataTable table = sTA.GetDataByScheduleID(scheduleID);
			if (table.Rows.Count > 0)
			{
			   SetScenarioByID((int)table[0].ScenarioID);
			}
			return ScenarioData.ID;
		}

		public void AddScenario(string name, string filename, DateTime lastModified, string description, string attachments)
		{
			object res = storedProcedures.AddScenario(name, filename, lastModified, description, attachments);
			if (res != null)
			{
				scenario.ID = (int)res;
				scenario.Name = name;
				scenario.Filename = filename;
				scenario.Description = description;
				scenario.Attachments = attachments;
			}

		}

		public void AddScenario(string name, string filename, DateTime lastModified, string description)
		{
			AddScenario(name, filename, lastModified, description, "");
		}

		public void RemoveScenario(int scenarioID)
		{
		   storedProcedures.RemoveScenario(scenarioID);
		}

		
		#endregion

		#region Run Methods

		public void AddRun(int scheduleID, int scenarioID, string logFile)
		{
			run.ID = (int)storedProcedures.AddRun(scheduleID, scenarioID, agent.ID, DateTime.Now, logFile);
			run.LogFilename = logFile;
		}

		public void UpdateRun()
		{
			storedProcedures.UpdateRun(run.ID, DateTime.Now, (int)run.Status, run.PercentsPassed, 0, run.PercentsFailed);
		}
		#endregion

		#region Test Case Methods
		public void InsertTestCase(bool passed)
		{
			int status = passed ? 1 : 0;
			TestCase testcase = TestCasesData.Pop();
			storedProcedures.InsertOrGetTestCase(testcase.Name, "", "", testcase.AssemblyName);
			storedProcedures.InsertTestCaseResult(run.ID, testcase.Name, testcase.StartTime, status, testcase.Errors, testcase.Warnings, DateTime.Now);
		}
		#endregion

		private AutomationDataSetTableAdapters.AutomationStoredProcedures storedProcedures = new QAliber.DAL.AutomationDataSetTableAdapters.AutomationStoredProcedures();
		private string connString;
		private Agent agent = new Agent();
		private Scenario scenario = new Scenario();
		private Run run = new Run();
		private Schedule schedule = new Schedule();
		
		private Stack<TestCase> testcases = new Stack<TestCase>();

		private static Data current = null;




		
	}

}
