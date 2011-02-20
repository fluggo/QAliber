using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QAliber.TestModel;
using System.IO;

namespace QAliber.Runner
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			cmdArgs = new CmdArgs(args);
			if (!cmdArgs.ParsedSuccussfully)
			{
				return;
			}
			LoadSettings();
			if (cmdArgs.AssemblyDir != null)
				TestModel.TestController.Default.RemoteAssemblyDirectory = cmdArgs.AssemblyDir;
			
  
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Notifier notifier = new Notifier();
			if (cmdArgs.ScenarioFile != null)
			{
				notifier.runner.Run(cmdArgs.ScheduleID, 0, TestModel.TestController.Default.RemoteAssemblyDirectory, cmdArgs.ScenarioFile);
			}
			else if (cmdArgs.ScenarioID > 0 || cmdArgs.ScheduleID > 0)
			{
				notifier.runner.Run(cmdArgs.ScheduleID, cmdArgs.ScenarioID, TestModel.TestController.Default.RemoteAssemblyDirectory, "");
			}
			Application.Run();
		}

		private static void LoadSettings()
		{
			QAliber.Engine.PlayerConfig.Default.AnimateMouseCursor = Properties.Settings.Default.AnimateCursor;
			QAliber.Engine.PlayerConfig.Default.AutoWaitForControl = (int)Properties.Settings.Default.ControlAutoWaitTimeout;
			QAliber.Engine.PlayerConfig.Default.DelayAfterAction = (int)Properties.Settings.Default.DelayAfterAction;
			QAliber.Engine.PlayerConfig.Default.BlockUserInput = Properties.Settings.Default.BlockUserInput;
			QAliber.DAL.Data.Current.ConnectionString = Properties.Settings.Default.QAliberCentrerConnection;

			if (!Directory.Exists(Properties.Settings.Default.TestCasesAssemblyDir))
			{
				Properties.Settings.Default.TestCasesAssemblyDir = Environment.CurrentDirectory;
			}
			if (!Directory.Exists(Properties.Settings.Default.LogLocation))
			{
				Properties.Settings.Default.LogLocation = Environment.CurrentDirectory + @"\Log";
			}

			TestController.Default.RemoteAssemblyDirectory = Properties.Settings.Default.TestCasesAssemblyDir;
			TestController.Default.LogDirectoryStructure = Properties.Settings.Default.LogStructure;
			TestController.Default.LogPath = Properties.Settings.Default.LogLocation;
		}

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.Contains("Test"))
			{
				string file = TestModel.TestController.Default.RemoteAssemblyDirectory + @"\" + args.Name.Split(',')[0] + ".dll";
				if (File.Exists(file))
					return System.Reflection.Assembly.LoadFrom(file);
			}
			return null;
		}

		internal static CmdArgs cmdArgs = null;
	}

	public class CmdArgs
	{
		public CmdArgs(string[] args)
		{
			ParseArgs(args);
		}

		public static void DisplayUsage()
		{
			MessageBox.Show("QAliber Test Runner.exe -file=<filename> [-asm=<assemblydir>] [-schid=<scheduleid>] [-scnid=<scenarioid>] [-exit=<true|false>]\n\n"
				+ "file - The scenario file to run, this will be ignored if a valid scenario ID will be given\n"
				+ "asm - The assembly directory containing all the test cases that are needed for the scenario\n"
				+ "schid - The schedule id from 'QAliber Test Center' that is planned for this run (should be used for debugging purposes)\n"
				+ "scnid - The scenario id from 'QAliber Test Center' of the scenario to run if used filename is not needed and will be ignored\n"
				+ "exit - Should the runner exit immediately after run\n",
				"QAliber Test Runner Arguments");
		}

		private void ParseArgs(string[] args)
		{
			try
			{
				foreach (string arg in args)
				{
					string[] argFields = arg.Split('=');
					switch (argFields[0])
					{
						case "-file":
							scenarioFile = argFields[1];
							break;
						case "-asm":
							assemblyDir = argFields[1];
							break;
						case "-schid":
							scheduleID = int.Parse(argFields[1]);
							break;
						case "-scnid":
							scenarioID = int.Parse(argFields[1]);
							break;
						case "-exit":
							exitAfterRun = bool.Parse(argFields[1]);
							break;
						default:
							DisplayUsage();
							return;
					}
				}
				parsed = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Couldn't parse arguments, "	+ ex.Message);
				DisplayUsage();
			}
		}

		private int scheduleID;

		public int ScheduleID
		{
			get { return scheduleID; }
			set { scheduleID = value; }
		}

		private int scenarioID;

		public int ScenarioID
		{
			get { return scenarioID; }
			set { scenarioID = value; }
		}

		private string scenarioFile;

		public string ScenarioFile
		{
			get { return scenarioFile; }
			set { scenarioFile = value; }
		}

		private string assemblyDir;

		public string AssemblyDir
		{
			get { return assemblyDir; }
			set { assemblyDir = value; }
		}

		private bool exitAfterRun;

		public bool ExitAfterRun
		{
			get { return exitAfterRun; }
			set { exitAfterRun = value; }
		}


		public bool ParsedSuccussfully
		{
			get { return parsed; }
		}

		private bool parsed = false;
	
	
	}
}