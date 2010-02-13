using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using QAliber.TestModel.TypeEditors;
using QAliber.Engine.Controls;
using QAliber.ImageHandling;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	public class ReadTextFromControl : TestCase
	{
		public ReadTextFromControl()
		{
			name = "Read Text From Control";
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

		private string text;

		[Category("Control")]
		[DisplayName("2) Text Read")]
		[Description("The text read from the control")]
		public string TextRead
		{
			get { return text; }
		}


		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			StringBuilder code = new StringBuilder();
			code.Append("UIControlBase c = " + control + ";\n");
			code.Append("return c;\n");
			UIControlBase c = (UIControlBase)QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code.ToString());
			if (c == null)
				throw new ArgumentException("Couldn't retrieve control " + control);

			if (c is QAliber.Engine.Patterns.IText)
			{
			   
				text = ((QAliber.Engine.Patterns.IText)c).Text;
				Logger.Log.Default.Info("Found text property", text);
				return;
			}
			if (c is QAliber.Engine.Controls.Web.WebControl)
			{
				text = ((QAliber.Engine.Controls.Web.WebControl)c).InnerText;
				Logger.Log.Default.Info("Found inner text property of web control", text);
				return;
			}
			Logger.Log.Default.Info("Couldn't find a text property, trying to do optical character recognition");
			OCRItem ocrItem = new OCRItem(c.GetImage());
			text = ocrItem.ProcessImage();
			Logger.Log.Default.Info("OCR result = " + text);
			
		}

		public override string Description
		{
			get
			{
				return "Getting properties for control " + control;
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
