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
	/// Deletes a registry key
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Registry")]
	[XmlType("DeleteRegistryValue", Namespace=Util.XmlNamespace)]
	public class DeleteRegistryValue : global::QAliber.TestModel.TestCase
	{
		public DeleteRegistryValue() : base( "Delete Registry Value" )
		{
			icon = Properties.Resources.Registry;
		}

		public override void Body()
		{
			using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(regkey))
			{
				if (key == null)
					Log.Default.Warning("Key does not exist in registry");
				else
					key.DeleteValue(regVal);
			}
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			
		}

		private string regkey = "";

		[Category("Registry")]
		[Description(@"The registry path (e.g. 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft')")]
		[DisplayName("1) Registry Key")]
		public string RegKey
		{
			get { return regkey; }
			set { regkey = value; }
		}

		private string regVal = "";

		[Category("Registry")]
		[Description("The registry value to delete")]
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
				return "Deleting registry value '" + regVal + "' from path '" + regkey + "'";
			}
		}



	}
}
