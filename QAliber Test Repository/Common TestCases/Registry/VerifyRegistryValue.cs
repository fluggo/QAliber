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
	public class VerifyRegistryValue : global::QAliber.TestModel.TestCase
	{
		public VerifyRegistryValue()
		{
			name = "Verify Registry Value";
			icon = Properties.Resources.Registry;
		}

		public override void Setup()
		{
			base.Setup();
			actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
		}

		public override void Body()
		{
			int index = regkey.LastIndexOf('\\');
			string key = regkey.Substring(0, index);
			string val = regkey.Substring(index + 1);
			string realVal = Microsoft.Win32.Registry.GetValue(key, val, "").ToString();
			if (string.Compare(realVal, regVal, true) == 0)
			{
			   actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			   LogPassedByExpectedResult("Registry value is as expected", "");
			}
			else
			{
				LogFailedByExpectedResult("Registry values are different", "Actual : " + realVal + "\nExpected : " + regVal);
				
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
		[Description("The registry value to expect")]
		[DisplayName("2) Expected Registry Value")]
		public string RegVal
		{
			get { return regVal; }
			set { regVal = value; }
		}

		public override string Description
		{
			get
			{
				return "Expecting registry value at '" + regkey + "' to be '" + regVal + "'";
			}
			set
			{
				base.Description = value;
			}
		}



	}
}
