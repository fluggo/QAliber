using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using QAliber.TestModel.TypeEditors;

namespace QAliber.Repository.CommonTestCases.UI.Windows
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	public class GetPropertyFromControl : TestCase
	{
		public GetPropertyFromControl()
		{
			name = "Get Property From Control";
			icon = Properties.Resources.Window;
			list = new MultipleSelectionList();
			list.Items.AddRange(
				new string[] { "Layout.X", "Layout.Y", "Layout.Width", "Layout.Height",
				"Name", "HelpText",
				"ClassName", "ID",
				"Enabled", "Visible"});
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

		private MultipleSelectionList list;

		[Category("Control")]
		[DisplayName("2) Properties")]
		[Description("The properties to retrieve")]
		[Editor(typeof(MultipleSelectionTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public MultipleSelectionList List
		{
			get { return list; }
			set { list = value; }
		}


		private string vals;

		[Category("Control")]
		[DisplayName("3) Values")]
		[Description("The values to retrieve")]
		public string Values
		{
			get { return vals; }
		}
		
	
	
		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			string code = "UIControlBase c = " + control + ";\n";
			foreach (string item in list.SelectedItems)
			{
				string execCode = code + "return c." + item + ".ToString();";
				string res = (string)QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(execCode);
				Log.Default.Info("Property '" + item + "' = '" + res + "'");
				vals += res + ",";
			}
			vals = vals.Trim(',');
			
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
