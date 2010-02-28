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
using QAliber.Recorder;

namespace QAliber.VS2005.Plugin
{
	public class Statics
	{
		public static LLRecorder Recorder
		{
			get { return recorder; }
		}

		public static QAliber.VS2005.Plugin.Commands.Command[] Commands
		{
			get { return commands; }
		}

		public static ProjectLanguage Language
		{
			get
			{
				return language;
			}
			set
			{
				language = value;
			}
		}

		public static SpyControl SpyControl
		{
			get { return spyControl; }
			set { spyControl = value; }
		}

		private static QAliber.VS2005.Plugin.Commands.Command[] commands = new QAliber.VS2005.Plugin.Commands.Command[] { new QAliber.VS2005.Plugin.Commands.RecordCommand(), new QAliber.VS2005.Plugin.Commands.StopRecordCommand() };

		private static LLRecorder recorder = new LLRecorder();
		private static SpyControl spyControl;
		private static ProjectLanguage language = ProjectLanguage.CSharp;
	}

	public enum ProjectLanguage
	{
		None,
		CSharp,
		VB
	}
}
