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
using System.ComponentModel;
using QAliber.Engine.Controls;

namespace QAliber.Recorder
{
	public class RecorderConfig //: Microsoft.VisualStudio.Shell.DialogPage
	{
		public RecorderConfig()
		{
			filteredProcesses = new string[] { "devenv" };
		}

		private UIControlBase rootControl;

		[Browsable(false)]
		public UIControlBase RootControl
		{
			get { return rootControl; }
			set { rootControl = value; }
		}

		private string webStartURL = "";

		[Category("Recorder")]
		[Description("The URL you want to start with when recording web tests")]
		[DisplayName("Web Recording Base URL")]
		public string WebStartURL
		{
			get { return webStartURL; }
			set { webStartURL = value; }
		}
	
		private string[] filteredProcesses;

		[Category("Recorder")]
		[DisplayName("Filtered Processes")]
		[Description("A list of processes you don't want to record your actions for")]
		public string[] FilteredProcesses
		{
			get { return filteredProcesses; }
			set { filteredProcesses = value; }
		}

		private bool keepRecordedTimings;

		[Category("Recorder")]
		[DisplayName("Keep Original Record Timings")]
		[Description("Inserts delays in the generated code according to what was originally recorded")]
		public bool KeepRecordedTimings
		{
			get { return keepRecordedTimings; }
			set { keepRecordedTimings = value; }
		}

		public static RecorderConfig Default
		{
			get
			{
				if (instance == null)
					instance = new RecorderConfig();
				return instance;
			}
			set { instance = value; }
		}

		private static RecorderConfig instance;
	}
}
