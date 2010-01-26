using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	public class ChangeWindowState : TestCase
	{
		public ChangeWindowState()
		{
			name = "Change Window State";
			icon = QAliber.Repository.CommonTestCases.Properties.Resources.Window;
		}

		private string control = "";

		
		[Category("Window")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private WindowOperationType opType;

		[Category("Window")]
		[Description("Select the operation you want to perform on the window")]
		public WindowOperationType Operation
		{
			get { return opType; }
			set { opType = value; }
		}
	
	
		public override void Body()
		{
			string code = "UIAWindow w = " + control + " as UIAWindow;\n";
			code += "w." + opType + "();\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return opType + " is performed on window '" + control + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}

	public enum WindowOperationType
	{
		SetFocus,
		Close,
		Minimize,
		Maximize,
		Restore
	}


}
