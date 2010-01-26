using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	public class SaveImageFromControl : TestCase
	{
		public SaveImageFromControl()
		{
			name = "Save Image From Control";
			icon = Properties.Resources.Bitmap;
		}

		private string control = "";


		[Category("Image")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string file;

		[Category("Image")]
		[DisplayName("2) File To Save")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description("The file to save the image to (jpg format)")]
		public string File
		{
			get { return file; }
			set { file = value; }
		}

	
		public override void Body()
		{
			string code = "UIControlBase c = " + control + ";\n";
			code += "c.GetImage().Save(@\"" + file + "\");\n";
			code += "return null;\n";
			QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

		}

		public override string Description
		{
			get
			{
				return "Saving image for path '" + control + "' to file '" + file + "'";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
