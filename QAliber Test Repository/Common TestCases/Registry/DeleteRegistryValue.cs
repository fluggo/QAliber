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
	/// Deletes a registry key
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Registry")]
	public class DeleteRegistryValue : global::QAliber.TestModel.TestCase
	{
		public DeleteRegistryValue()
		{
			name = "Delete Registry Value";
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
			set
			{
				base.Description = value;
			}
		}



	}
}
