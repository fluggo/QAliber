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
			
			DataTable dataTable = new DataTable();
			conn.Open();

			adapter.Fill(dataTable);
			dataTable.TableName = "Table from Excel (" + sourceFile + ")";
			QAliber.TestModel.Variables.ScenarioTable table = new QAliber.TestModel.Variables.ScenarioTable(rangeName, dataTable, this);
			
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
