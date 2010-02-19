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
					if (string.IsNullOrEmpty(vsDir))
						vsDir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\8.0", "InstallDir", "");
					break;
				case "2008":
					vsDir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0", "InstallDir", "");
					if (string.IsNullOrEmpty(vsDir))
						vsDir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\9.0", "InstallDir", "");
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
