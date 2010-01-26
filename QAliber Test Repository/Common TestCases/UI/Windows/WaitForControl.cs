using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;

namespace QAliber.Repository.CommonTestCases.UI.Windows
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	public class ClickMouse : TestCase
	{
		public ClickMouse()
		{
			name = "Wait For Control";
			icon = Properties.Resources.Window;
		}

		private string control = "";

		
		[Category("Control")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private int timeout;

		[Category("Control")]
		[DisplayName("2) Timeout")]
		[Description("The timeout in miliseconds to wait for the control")]
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		
	
	
		public override void Body()
		{
			string cName, cId, code;

			cName = ExtractNameFromCodePath();
			if (cName != "")
				code = "UIControlBase c = " + cName + ";\n";
			else
			{
				cId = ExtractIDFromCodePath();
				if (cId != "")
				{
					code = "UIControlBase c = " + cId + ";\n";
				}
				throw new ArgumentException("Could not understand control '" + control + "', please try to grab it with the control locator");
			}
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Waiting for control " + control;
			}
			set
			{
				base.Description = value;
			}
		}

		private string ExtractNameFromCodePath()
		{
			int index = control.LastIndexOf('[');
			if (index < 0)
				return "";
			string lastIndexer = control.Substring(index).Trim('[', ']');
			string parentControl = control.Substring(0, index);
			string[] lastIndexerFields = lastIndexer.Split(',');
			return string.Format("{0}.WaitForControlByName({1}, {2})",
				parentControl, lastIndexerFields[0].Trim(), timeout.ToString());
		}

		private string ExtractIDFromCodePath()
		{
			int index = control.LastIndexOf('[');
			if (index < 0)
				return "";
			string lastIndexer = control.Substring(index).Trim('[', ']');
			string parentControl = control.Substring(0, index);
			string[] lastIndexerFields = lastIndexer.Split(',');
			if (lastIndexerFields.Length == 3)
				return string.Format("{0}.WaitForControlByID({1}, {2})",
					parentControl, lastIndexerFields[2].Trim(), timeout.ToString());
			return "";
		}

	}


}
