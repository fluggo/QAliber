using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Security.Permissions;
using System.Diagnostics;


namespace QAliber.Developer.PostInstaller
{
	[RunInstaller(true)]
	public partial class PluginPostInstaller : Installer
	{
		public PluginPostInstaller()
		{
			InitializeComponent();
		}

		[SecurityPermission(SecurityAction.Demand)]
		public override void Commit(IDictionary savedState)
		{
			
			base.Commit(savedState);
			string vsType = this.Context.Parameters["vs"];
			string vsDir = string.Empty;
			switch (vsType)
			{
				case "2005":
					vsDir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\8.0", "InstallDir", "");
					break;
				case "2008":
					vsDir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0", "InstallDir", "");
					break;
				default:
					break;
			}
			if (string.IsNullOrEmpty(vsDir))
			{
				System.Windows.Forms.MessageBox.Show("It seems like Visual Studio " + vsType + " is not installed on your machine, you must install it before you install this plug-in", "Pre-requisite is not met");
				base.Uninstall(savedState);
			}
			Process.Start(vsDir + "devenv.exe", "/setup").WaitForExit(30000);
		}
	}
}
