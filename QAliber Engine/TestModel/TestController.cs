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
				string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\User Assemblies\";
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				return path;
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
				if (tc.Scenario.RootTestCase.Equals(tc))
				{
					scenario = tc.Scenario;
					executionWorker = new Thread(new ThreadStart(ExecutionWorker));
					executionWorker.Start();
				}
				else
				{
					executionWorker = new Thread(new ParameterizedThreadStart(TestExecutionWorker));
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
			TestCase.ExitTotally = true;
			Log.Default.Warning("Automatic run was aborted by the user", "", EntryVerbosity.Internal);
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
			try
			{
				Log.Default.Filename = CreateLogDirectory() + @"\Run.qlog";
				TestCase.ExitTotally = false;
				TestCase.BranchesToBreak = 0;
				scenario.Run();
			}
			finally
			{
				Log.Default.Dispose();
				RaiseExecutionStateChanged(ExecutionState.Executed);
				RaiseLogResultArrived(Log.Default.Filename);
			}
		}

		private void TestExecutionWorker(object testcase)
		{
			try
			{
			   
				Log.Default.Filename = CreateLogDirectory() + @"\Testcase.qlog";
				TestCase.ExitTotally = false;
				TestCase.BranchesToBreak = 0;
				((TestCase)testcase).Run();
			}
			finally
			{
				Log.Default.Dispose();
				RaiseExecutionStateChanged(ExecutionState.Executed);
				RaiseLogResultArrived(Log.Default.Filename);
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
