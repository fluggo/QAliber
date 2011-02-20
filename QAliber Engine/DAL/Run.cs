using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using Microsoft.Win32;

namespace QAliber.DAL
{
	public class Run
	{
		public int ID
		{
			get { return id; }
			internal set { id = value; }
		}

		public DateTime StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		public DateTime EndTime
		{
			get { return endTime; }
			set { endTime = value; }
		}


		public string LogFilename
		{
			get { return logFilename; }
			set { logFilename = value; }
		}

		public RunStatusType Status
		{
			get 
			{
				if (numFailed > 0)
					return RunStatusType.Failed;
				else
					return RunStatusType.Passed;
			}
		}

		public double PercentsPassed
		{
			get 
			{
				if (numPassed + numFailed == 0)
					return 0;
				return numPassed / (double)(numPassed + numFailed); 
			}
		}

		public double PercentsFailed
		{
			get 
			{
				if (numPassed + numFailed == 0)
					return 0;
				return numFailed / (double)(numPassed + numFailed); 
			}
		}

		private int numPassed;

		public int NumPassed
		{
			get { return numPassed; }
			set { numPassed = value; }
		}

		private int numFailed;

		public int NumFailed
		{
			get { return numFailed; }
			set { numFailed = value; }
		}
	
	

	  
		private int id;
	   

		private string logFilename;
		private DateTime startTime;
		private DateTime endTime;
		

	}

	public enum RunStatusType
	{
		InProgress,
		Passed,
		Failed
	}
}
