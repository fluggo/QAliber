using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using QAliber.Logger;
using System.Drawing;
using QAliber.TestModel.Attributes;
using System.Collections;
using QAliber.TestModel.Variables;
using System.Data;

namespace QAliber.TestModel
{
	[Serializable]
	[VisualPath(@"Flow Control\Loops")]
	public class ForEachRowInTableTestCase : FolderTestCase
	{
		public ForEachRowInTableTestCase()
		{
			name = "For Each Row In Table";
			icon = Properties.Resources.Loop;
		}

		private TableVariableDropDownList tableName = new TableVariableDropDownList();

		[Editor(typeof(QAliber.TestModel.TypeEditors.ComboDropDownTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Test Case Flow Control")]
		[DisplayName("Table Name")]
		[Description("The table to iterate on")]
		public TableVariableDropDownList TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}

		public override void Setup()
		{
			base.Setup();
			table = scenario.Tables[tableName.Selected];
			if (table == null)
				throw new ArgumentException("Table '" + tableName + "' is not recognized");
		}

		public override void Body()
		{
			DataTable dataTable = table.Value as DataTable;
			int j = 0;
			foreach (DataRow row in dataTable.Rows)
			{
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable(tableName + ".CurrentRow." + dataTable.Columns[i].ColumnName, row[i].ToString(), this));
				}
				Log.Default.IndentIn("Iteration on row '" + j + "'");
				base.Body();
				Log.Default.IndentOut();
				if (exitTotally)
					break;
				if (branchesToBreak > 0)
				{
					branchesToBreak--;
					break;
				}
				j++;
			}
		}

		public override string Description
		{
			get
			{
				return "For Each Item In '" + tableName + "'";
			}
			set
			{
				base.Description = value;
			}
		}

		protected ScenarioTable table;
	
	}
	
}
