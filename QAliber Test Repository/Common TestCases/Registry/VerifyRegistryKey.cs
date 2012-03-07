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
using System.Xml.Serialization;



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
	[XmlType("VerifyRegistryKey", Namespace=Util.XmlNamespace)]
	public class VerifyRegistryKey : global::QAliber.TestModel.TestCase
	{
		public VerifyRegistryKey() : base( "Verify Registry Key" )
		{
			Icon = Properties.Resources.Registry;
		}

		public override void Setup()
		{
			base.Setup();
			ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
		}

		public override void Body()
		{
			Microsoft.Win32.RegistryKey key = null;
			switch (regHive)
			{
				case Microsoft.Win32.RegistryHive.ClassesRoot:
					key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkey);
					break;
				case Microsoft.Win32.RegistryHive.CurrentConfig:
					key = Microsoft.Win32.Registry.CurrentConfig.OpenSubKey(regkey);
					break;
				case Microsoft.Win32.RegistryHive.CurrentUser:
					key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(regkey);
					break;
				case Microsoft.Win32.RegistryHive.DynData:
					key = Microsoft.Win32.Registry.PerformanceData.OpenSubKey(regkey);
					break;
				case Microsoft.Win32.RegistryHive.LocalMachine:
					key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regkey);
					break;
				case Microsoft.Win32.RegistryHive.PerformanceData:
					key = Microsoft.Win32.Registry.PerformanceData.OpenSubKey(regkey);
					break;
				case Microsoft.Win32.RegistryHive.Users:
					key = Microsoft.Win32.Registry.Users.OpenSubKey(regkey);
					break;
				default:
					break;
			}
			if (key != null)
			{
				ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
				LogPassedByExpectedResult("Key found in registry", "");
			}
			else
			{
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				LogFailedByExpectedResult("Key was not found in registry", "");
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
				return "Expecting registry key at '" + regkey + "'";
			}
		}



	}
}
