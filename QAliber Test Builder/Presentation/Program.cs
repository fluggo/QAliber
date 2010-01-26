using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QAliber.TestModel;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using QAliber.RemotingModel;
using QAliber.Builder.Presentation;
using System.Xml;
using System.IO;

namespace QAliber.Builder.Presentation
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
			LoadSettings(args);
			
			Control.CheckForIllegalCrossThreadCalls = false;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm form = new MainForm();
			foreach (string arg in args)
			{
				
				if (File.Exists(arg))
				{
					form.LoadFile(arg);
				}

			}
			Application.Run(form);

			

		}

		public static void AddStylesheetDeclaration(string filename)
		{
			using (StreamReader reader = new StreamReader(filename))
			{
				string decl = reader.ReadLine();
				string line = reader.ReadLine();
				if (!line.Contains("xml-stylesheet"))
				{
					using (StreamWriter writer = new StreamWriter(filename + ".new"))
					{
						writer.WriteLine(decl);
						writer.WriteLine(@"<?xml-stylesheet type=""text/xsl"" href=""HelpTranslator.xsl""?>");
						while (line != null)
						{
							writer.WriteLine(line);
							line = reader.ReadLine();
						}
					}
					reader.Close();
					File.Delete(filename);
					File.Move(filename + ".new", filename);
				}

			}
			
		}

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			
				string file = TestModel.TestController.Default.LocalAssemblyPath + @"\" + args.Name.Split(',')[0] + ".dll";
				if (File.Exists(file))
				{
					try
					{
						return System.Reflection.Assembly.LoadFrom(file);
					}
					catch
					{
					}
				}
				else
				{
					file = TestModel.TestController.Default.LocalAssemblyPath + @"\" + args.Name.Split(',')[0] + ".exe";
					if (File.Exists(file))
					{
						try
						{
							return System.Reflection.Assembly.LoadFrom(file);
						}
						catch
						{
						}
					}
				}
			
			return null;
		}

		private static void LoadSettings(string[] args)
		{
			QAliber.Engine.PlayerConfig.Default.AnimateMouseCursor = Properties.Settings.Default.AnimateCursor;
			QAliber.Engine.PlayerConfig.Default.AutoWaitForControl = (int)Properties.Settings.Default.ControlAutoWaitTimeout;
			QAliber.Engine.PlayerConfig.Default.DelayAfterAction = (int)Properties.Settings.Default.DelayAfterAction;
			QAliber.Engine.PlayerConfig.Default.BlockUserInput = Properties.Settings.Default.BlockUserInput;

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


			foreach (string arg in args)
			{
				if (arg.StartsWith("-"))
				{
					switch (arg.ToLower())
					{
						case "-update":
							new TestModel.AssembliesRetriever(true).CopyRemoteToLocal();
							break;
						default:
							break;
					}
				}
			}
			
		}
	}
}