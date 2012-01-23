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
	/// Adds a key value pair to the registry hive
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Registry")]
	public class AddRegistryValue : global::QAliber.TestModel.TestCase
	{
		public AddRegistryValue()
		{
			name = "Add Registry Value";
			icon = Properties.Resources.Registry;
		}

		public override void Body()
		{
			int index = regkey.LastIndexOf('\\');
			string key = regkey.Substring(0, index);
			string val = regkey.Substring(index + 1);

			Microsoft.Win32.Registry.SetValue(key, val, regVal);

			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string regkey = "";

		/// <summary>
		/// The registry key path to add
		/// </summary>
		[Category("Registry")]
		[Description(@"The registry path (e.g. 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft')")]
		[DisplayName("1) Registry Key")]
		public string RegKey
		{
			get { return regkey; }
			set { regkey = value; }
		}

		private string regVal = "";

		/// <summary>
		/// The registry value to set
		/// </summary>
		[Category("Registry")]
		[Description("The registry value")]
		[DisplayName("2) Registry Value")]
		public string RegVal
		{
			get { return regVal; }
			set { regVal = value; }
		}

		public override string Description
		{
			get
			{
				return "Adding registry value '" + regVal + "' to path '" + regkey + "'";
			}
		}
	
	
	
	}
}
