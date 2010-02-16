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
					dte = IDEDetector.SeekDTE2InstanceFromROT("!VisualStudio.DTE.9.0:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString());
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
