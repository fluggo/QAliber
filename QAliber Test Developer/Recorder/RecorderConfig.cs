using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using QAliber.Engine.Controls;

namespace QAliber.Recorder
{
	public class RecorderConfig : Microsoft.VisualStudio.Shell.DialogPage
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
