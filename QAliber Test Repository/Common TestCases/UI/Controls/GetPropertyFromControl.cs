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
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using QAliber.TestModel.TypeEditors;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
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
			vals = "";
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
		}

	}


}
