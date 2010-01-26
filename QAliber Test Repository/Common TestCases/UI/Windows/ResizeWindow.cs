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
	public class ResizeWindow : TestCase
	{
		public ResizeWindow()
		{
			name = "Resize Window";
			icon = Properties.Resources.Window;
		}

		private string control = "";

		
		[Category("Window")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private Size size;

		[Category("Window")]
		[Description("The size in pixels to set the window")]
		public Size Size
		{
			get { return size; }
			set { size = value; }
		}
	
	
		public override void Body()
		{
			string code = "UIAWindow w = " + control + " as UIAWindow;\n";
			code += "w.Resize(" + size.Width + ", " + size.Height + ");\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return"Resizing window '" + control + "' to size " + size;
			}
			set
			{
				base.Description = value;
			}
		}

	}
}
