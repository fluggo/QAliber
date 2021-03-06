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
using System.Windows.Forms;
using QAliber.TestModel;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using QAliber.RemotingModel;
using QAliber.Builder.Presentation;
using System.Xml;
using System.IO;
using System.Deployment.Application;

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

			if (Properties.Settings.Default.TestCasesAssemblyDir == "User Assemblies")
			{
				Properties.Settings.Default.TestCasesAssemblyDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\QAliber\\User Assemblies";
				if (!Directory.Exists(Properties.Settings.Default.TestCasesAssemblyDir))
					Directory.CreateDirectory(Properties.Settings.Default.TestCasesAssemblyDir);
			}

			if (!Directory.Exists(Properties.Settings.Default.LogLocation))
			{
				Properties.Settings.Default.LogLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\QAliber\Log";
			}

			TestController.Default.RemoteAssemblyDirectory = Properties.Settings.Default.TestCasesAssemblyDir;
			TestController.Default.LogDirectoryStructure = Properties.Settings.Default.LogStructure;
			TestController.Default.LogPath = Properties.Settings.Default.LogLocation;


			try {
				new TestModel.AssembliesRetriever(true).CopyRemoteToLocal();
			}
			catch( Exception ex ) {
				MessageBox.Show( "Could not read from \"" + Properties.Settings.Default.TestCasesAssemblyDir + "\". Check that the path is correct in the settings.\r\n\r\nThe error message was: " + ex.Message, "QAliber", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}
	}
}