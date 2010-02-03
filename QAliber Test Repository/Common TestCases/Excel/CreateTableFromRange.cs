using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Gets data from an excel range and fill the scenario's table using the excel column's headers
	/// <preconditions>Excel file should exist. Excel range in file should be named</preconditions>
	/// <workflow>
	/// <action>Create a table according to the named range</action>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"Excel")]
	public class CreateTableFromRange : global::QAliber.TestModel.TestCase
	{
		public CreateTableFromRange()
		{
			name = "Create Table From Range";
			icon = Properties.Resources.Excel;
		}

		public override void Body()
		{
			OleDbConnection conn = new OleDbConnection(
				"Provider=Microsoft.Jet.OLEDB.4.0;" +
				"Data Source=" + sourceFile +
				";Extended Properties=Excel 8.0;");
			OleDbCommand cmd = new OleDbCommand("SELECT * FROM " + rangeName, conn);
			OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
			QAliber.TestModel.Variables.ScenarioTable table = new QAliber.TestModel.Variables.ScenarioTable();
			DataTable dataTable = new DataTable();
			conn.Open();

			adapter.Fill(dataTable);
			table.Name = rangeName;
			table.Value = dataTable;
			Scenario.Tables.Add(table);
			conn.Close();

			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			

		}

		private string sourceFile = "";

		/// <summary>
		/// The excel file that contains the range
		/// </summary>
		[DisplayName("1) Source File")]
		[Category("Excel")]
		[Description("The excel file")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		private string rangeName = "";

		/// <summary>
		/// The name of the range
		/// <remarks>In order to name a range select the range in excel, right click and select Name a range...</remarks>
		/// </summary>
		[DisplayName("2) Range Name")]
		[Category("Excel")]
		[Description("The range to retrieve the columns from")]
		public string RangeName
		{
			get { return rangeName; }
			set { rangeName = value; }
		}

		public override string Description
		{
			get
			{
				return "Retrieveing columns from '" + sourceFile + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}
