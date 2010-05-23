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

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;



namespace QAliber.Repository.CommonTestCases.Registry
{
	/// <summary>
	/// Verifies a value in registry
	/// <workflow>
	/// <verification>
	/// Registry value exists
	/// </verification>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Registry")]
	public class DeleteRegistryKey : global::QAliber.TestModel.TestCase
	{
		public DeleteRegistryKey()
		{
			name = "Delete Registry Key";
			icon = Properties.Resources.Registry;
		}

		public override void Setup()
		{
			base.Setup();
			actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
		}

		public override void Body()
		{
		   
			switch (regHive)
			{
				case Microsoft.Win32.RegistryHive.ClassesRoot:
					Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(regkey);
					break;
				case Microsoft.Win32.RegistryHive.CurrentConfig:
					Microsoft.Win32.Registry.CurrentConfig.DeleteSubKeyTree(regkey);
					break;
				case Microsoft.Win32.RegistryHive.CurrentUser:
					Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(regkey);
					break;
				case Microsoft.Win32.RegistryHive.DynData:
					Microsoft.Win32.Registry.DynData.DeleteSubKeyTree(regkey);
					break;
				case Microsoft.Win32.RegistryHive.LocalMachine:
					Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(regkey);
					break;
				case Microsoft.Win32.RegistryHive.PerformanceData:
					Microsoft.Win32.Registry.PerformanceData.DeleteSubKeyTree(regkey);
					break;
				case Microsoft.Win32.RegistryHive.Users:
					Microsoft.Win32.Registry.Users.DeleteSubKeyTree(regkey);
					break;
				default:
					break;
			}
			

		}

		private string regkey = "";

		[Category("Registry")]
		[Description(@"The registry path without the hive(e.g. 'SOFTWARE\Microsoft')")]
		[DisplayName("1) Registry Key")]
		public string RegKey
		{
			get { return regkey; }
			set { regkey = value; }
		}

		private Microsoft.Win32.RegistryHive regHive = Microsoft.Win32.RegistryHive.CurrentUser;

		[Category("Registry")]
		[Description(@"The registry hive (e.g. 'SOFTWARE\Microsoft')")]
		[DisplayName("2) Registry Hive")]
		public Microsoft.Win32.RegistryHive RegHive
		{
			get { return regHive; }
			set { regHive = value; }
		}


		public override string Description
		{
			get
			{
				return "Deleting registry key at '" + regkey + "'";
			}
			set
			{
				base.Description = value;
			}
		}



	}
}
