using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using QAliber.Recorder;
using EnvDTE;

namespace QAliber.VS2005.Plugin
{
	public class Statics
	{
		public static LLRecorder Recorder
		{
			get { return recorder; }
		}

		public static DTE2 DTE
		{

			get
			{
				if (dte == null)
					dte = IDEDetector.SeekDTE2InstanceFromROT("!VisualStudio.DTE.8.0:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString());
				return dte;
			}
			set { dte = value; }
		}

		public static QAliber.VS2005.Plugin.Commands.Command[] Commands
		{
			get { return commands; }
		}

		public static ProjectLanguage Language
		{
			get
			{
				object[] projects = (object[])DTE.ActiveSolutionProjects;
				if (projects.Length > 0)
				{
					string lang = ((Project)projects[0]).FileName;
					if (lang.EndsWith("csproj"))
						return ProjectLanguage.CSharp;
					else if (lang.EndsWith("vbproj"))
						return ProjectLanguage.VB;
				}
				return ProjectLanguage.None;
			}
		}

		private static QAliber.VS2005.Plugin.Commands.Command[] commands = new QAliber.VS2005.Plugin.Commands.Command[] { new QAliber.VS2005.Plugin.Commands.RecordCommand(), new QAliber.VS2005.Plugin.Commands.StopRecordCommand() };

		private static LLRecorder recorder = new LLRecorder();
		private static DTE2 dte;
	}

	public enum ProjectLanguage
	{
		None,
		CSharp,
		VB
	}
}
