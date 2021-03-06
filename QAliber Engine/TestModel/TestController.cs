/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using QAliber.Logger;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using QAliber.RemotingModel;

namespace QAliber.TestModel
{

	/// <summary>
	/// Provides a driver for a test scenario, you can load, save, run pause and stop from this class
	/// </summary>
	public class TestController : MarshalByRefObject, IController
	{
		private TestController()
		{
			assemblyDir = Environment.CurrentDirectory;
			//RaiseExecutionStateChanged(ExecutionState.NotExecuted);
		}

		/// <summary>
		/// Singleton, the access point to the only TestControler instance
		/// </summary>
		public static TestController Default
		{
			get
			{
				if (instance == null)
				{
					instance = new TestController();
				}
				return instance;
			}
		}

		/// <summary>
		/// The test scenario that is loaded
		/// </summary>
		public TestScenario Scenario
		{
			get
			{
				return scenario;
			}
		}

		/// <summary>
		/// The central directory location from which to update the assemblies containing the test cases
		/// </summary>
		public string RemoteAssemblyDirectory
		{
			get { return assemblyDir; }
			set { assemblyDir = value; }
		}

		/// <summary>
		/// Indicates whether to copy the assemblies defined in RemoteAssemblyDirectory, only if newer (if set to false, copies the entire directory)
		/// </summary>
		public bool CopyAssembliesIfNewer
		{
			get { return copyAssembliesIfNewer; }
			set { copyAssembliesIfNewer = value; }
		}
	
		/// <summary>
		/// The path to log the run into
		/// </summary>
		public string LogPath
		{
			get { return logPath; }
			set { logPath = value; }
		}

		/// <summary>
		/// The log directory structure that will be created on top of the LogPath
		/// </summary>
		public string LogDirectoryStructure
		{
			get { return logStructure; }
			set { logStructure = value; }
		}

		/// <summary>
		/// Provides a list of user defined assembly locations
		/// </summary>
		public string[] UserFiles
		{
			get { return userFiles; }
			set { userFiles = value; }
		}

		/// <summary>
		/// Gets a list of all the supported test case types
		/// </summary>
		/// <remarks>The list is constructed by reflecting all the dlls in the LocalAssemblyPath directory</remarks>
		[System.ComponentModel.Browsable(false)]
		public Type[] SupportedTypes
		{
			get
			{
				if (types == null || types.Count == 0)
				{
					RetrieveSupportedTypes();
				}
				return types.ToArray();
			}
		}

		/// <summary>
		/// The directory where all the test case types are being read from, this is also the directory where the remote assemblies are being copied to
		/// </summary>
		public string LocalAssemblyPath
		{
			get
			{
				try
				{
					string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\User Assemblies\";
					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);
					return path;
				}
				catch (NullReferenceException)
				{
					//Designer workaround
					return Environment.CurrentDirectory;
				}
			}
		}

		/// <summary>
		/// Runs an entire test scenario by a given file
		/// </summary>
		/// <param name="scenarioFile">the filename of the scenario to run</param>
		public void Run(string scenarioFile)
		{
			RetrieveSupportedTypes();
			scenario = TestScenario.Load(scenarioFile);
			executionWorker = new Thread(new ThreadStart(ExecutionWorker));
			executionWorker.SetApartmentState(ApartmentState.STA);
			executionWorker.Start();
			new Thread(new ThreadStart(RunAsync)).Start();
		}

		/// <summary>
		/// Runs a single test case and all its children
		/// </summary>
		/// <param name="testcase">The test case to run, must inherit from TestCase</param>
		public void Run(object testcase)
		{
			TestCase tc = testcase as TestCase;
			if (tc != null)
			{
				scenario = tc.Scenario;
				if (tc.Scenario.RootTestCase.Equals(tc))
				{
					
					executionWorker = new Thread(new ThreadStart(ExecutionWorker));
					executionWorker.SetApartmentState(ApartmentState.STA);
					executionWorker.Start();
				}
				else
				{
					executionWorker = new Thread(new ParameterizedThreadStart(TestExecutionWorker));
					executionWorker.SetApartmentState(ApartmentState.STA);
					executionWorker.Start(testcase);
				}
				new Thread(new ThreadStart(RunAsync)).Start();
			}
		}

		/// <summary>
		/// Instructs the controller to wake up from a break point
		/// </summary>
		public void ContinueFromBreakPoint()
		{
			if (executionWorker != null)
			{
				if (executionWorker.IsAlive)
				{
					executionWorker.Interrupt();
				}
			}
		}

		/// <summary>
		/// Pauses a scenario that is being played
		/// </summary>
		public void Pause()
		{
			if (executionWorker != null)
			{
				if (executionWorker.IsAlive)
				{
					new Thread(new ThreadStart(PauseAsync)).Start();

				}
			}
		}

		/// <summary>
		/// Resumes a scenarioo that was paused
		/// </summary>
		public void Resume()
		{
			if (executionWorker != null)
			{
				if ((executionWorker.ThreadState & ThreadState.Suspended) > 0)
				{
					new Thread(new ThreadStart(ResumeAsync)).Start();
				}
			}
		}

		/// <summary>
		/// Stops the execution of a scenario
		/// </summary>
		public void Stop()
		{
			Log.Warning("Automatic run was aborted by the user", "", EntryVerbosity.Internal);
			TestCase.ExitTotally = true;
			new Thread(new ThreadStart(StopAsync)).Start();
		}

		public event ExecutionStateChangedCallback OnExecutionStateChanged
		{
			add { onExecutionStateChanged = value; }
			remove { onExecutionStateChanged = null; }
		}

		public event StepResultArrivedCallback OnStepResultArrived
		{
			add { onStepResultArrived = value; }
			remove { onStepResultArrived = null; }
		}

		public event StepStartedCallback OnStepStarted
		{
			add { onStepStarted = value; }
			remove { onStepStarted = null; }
		}

		public event LogResultArrivedCallback OnLogResultArrived
		{
			add { onLogResultArrived = value; }
			remove { onLogResultArrived = null; }
		}

		public event BreakPointReachedCallback OnBreakPointReached
		{
			add { onBreakPointReached = value; }
			remove { onBreakPointReached = null; }
		}

		public void RaiseExecutionStateChanged(ExecutionState state)
		{
			try
			{
				if (onExecutionStateChanged != null)
					onExecutionStateChanged(state);
			}
			catch (System.Net.Sockets.SocketException)
			{
				//Log.Default.Error(ex.Message, "", EntryVerbosity.Internal);
			}
		}

		public void RaiseStepResultArrived(TestCaseResult result)
		{
			try
			{
				if (onStepResultArrived != null)
					onStepResultArrived(result);
			}
			catch (System.Net.Sockets.SocketException)
			{
				//Log.Default.Error(ex.Message, "", EntryVerbosity.Internal);
			}
		}

		public void RaiseStepStarted(int id)
		{
			try
			{
				if (onStepStarted != null)
					onStepStarted(id);
			}
			catch (System.Net.Sockets.SocketException)
			{
				//Log.Default.Error(ex.Message, "", EntryVerbosity.Internal);
			}
		}

		public void RaiseLogResultArrived(string logFile)
		{
			try
			{
				if (onLogResultArrived != null)
					onLogResultArrived(logFile);
			}
			catch (System.Net.Sockets.SocketException)
			{
				//Log.Default.Error(ex.Message, "", EntryVerbosity.Internal);
			}
		}

		public void RaiseBreakPointReached()
		{
			try
			{
				if (onBreakPointReached != null)
					onBreakPointReached();
			}
			catch (System.Net.Sockets.SocketException)
			{
				//Log.Default.Error(ex.Message, "", EntryVerbosity.Internal);
			}
		}

		public override object InitializeLifetimeService()
		{
			//TODO : Handle lease time
			return null;
		}

		public void RetrieveSupportedTypes()
		{
			types = new List<Type>();
			List<string> allFiles = new List<string>();
			allFiles.AddRange(Directory.GetFiles(LocalAssemblyPath, "*.dll"));
			allFiles.AddRange(Directory.GetFiles(LocalAssemblyPath, "*.exe"));
			if (userFiles != null)
				allFiles.AddRange(userFiles);
			foreach (string file in allFiles)
			{
				try
				{
					Type[] assemblyTypes = Assembly.LoadFrom(file).GetTypes();
					foreach (Type type in assemblyTypes)
					{
						if (type.IsSubclassOf(typeof(TestCase)) && !type.IsAbstract)
						{
							types.Add(type);
						}
					}
				}
				catch
				{
				}
			}


			Type[] currentTypes = Assembly.GetExecutingAssembly().GetTypes();
			foreach (Type type in currentTypes)
			{
				if (type.IsSubclassOf(typeof(TestCase)) && !type.IsAbstract)
				{
					if (!types.Contains(type))
						types.Add(type);
				}
			}
		}

		public string CreateLogDirectory()
		{
			string res = logStructure;
			string path = "";
			try
			{
				if (!Directory.Exists(logPath))
					Directory.CreateDirectory(logPath);
				if (logStructure.Contains("machine"))
					res = res.Replace("machine", EscapeString(Environment.MachineName));
				if (logStructure.Contains("user"))
					res = res.Replace("user", EscapeString(Environment.UserName));
				if (logStructure.Contains("domain"))
					res = res.Replace("domain", EscapeString(Environment.UserDomainName));
				if (logStructure.Contains("scenario"))
				{
					if (scenario != null)
						res = res.Replace("scenario", EscapeString(scenario.RootTestCase.Name));
				}
				res = DateTime.Now.ToString(res);
				path = Path.Combine(Path.GetFullPath(logPath), res);
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
			}
			catch
			{
			}
			return path;
		}

		private void ExecutionWorker()
		{
			string logfile = CreateLogDirectory() + @"\Run.qlog";

			try {
				using( Log log = new Log( logfile, scenario.Filename, false ) ) {
					Log.Current = log;
					TestCase.ExitTotally = false;
					TestCase.BranchesToBreak = 0;
					scenario.Run();
				}
			}
			finally {
				Log.Current = null;

				RaiseExecutionStateChanged( ExecutionState.Executed );
				RaiseLogResultArrived( logfile );
			}
		}

		private void TestExecutionWorker(object testcase)
		{
			bool oldEnabled = ((TestCase) testcase).MarkedForExecution;
			string logfile = CreateLogDirectory() + @"\Testcase.qlog";

			try {
				using( Log log = new Log( logfile, scenario.Filename, true ) ) {
					Log.Current = log;
					TestCase.ExitTotally = false;
					TestCase.BranchesToBreak = 0;
					((TestCase) testcase).MarkedForExecution = true;
					((TestCase)testcase).Run();
				}
			}
			finally {
				Log.Current = null;
				((TestCase) testcase).MarkedForExecution = oldEnabled;
				RaiseExecutionStateChanged(ExecutionState.Executed);
				RaiseLogResultArrived(logfile);
			}
		}

		private void RunAsync()
		{
			RaiseExecutionStateChanged(ExecutionState.InProgress);
		}

		private void PauseAsync()
		{
			executionWorker.Suspend();
			RaiseExecutionStateChanged(ExecutionState.Paused);
		}

		private void ResumeAsync()
		{
			executionWorker.Resume();
			RaiseExecutionStateChanged(ExecutionState.InProgress);
		}

		private void StopAsync()
		{
			if ((executionWorker.ThreadState & (ThreadState.Suspended | ThreadState.SuspendRequested)) > 0)
			{
				executionWorker.Resume();
			}
			if (!executionWorker.Join(30000))
			{
				executionWorker.Abort();
				executionWorker.Join(30000);
			}
		}

		private string EscapeString(string s)
		{
			string res = "";
			foreach (char c in s)
			{
				res += "\\" + c.ToString();
			}
			return res;
		}

		private Thread executionWorker;
		private TestScenario scenario;
		private bool copyAssembliesIfNewer;
		private string assemblyDir = string.Empty;
		private string[] userFiles;
		private string logPath = Environment.CurrentDirectory + "\\Logs";
		private string logStructure = @"machine\dd-MM-yy HH_mm";
		private List<Type> types;
		private static TestController instance;
		private static event ExecutionStateChangedCallback onExecutionStateChanged;
		private static event StepStartedCallback onStepStarted;
		private static event StepResultArrivedCallback onStepResultArrived;
		private static event LogResultArrivedCallback onLogResultArrived;
		private static event BreakPointReachedCallback onBreakPointReached;
	}
}
