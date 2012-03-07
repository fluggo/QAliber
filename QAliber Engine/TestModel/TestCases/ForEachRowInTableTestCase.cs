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

	/// <summary>
	/// Iterates on a given table, row by row.
	/// <preconditions>Table should exist</preconditions>
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Loops")]
	[XmlType("ForEachRowInTable", Namespace=Util.XmlNamespace)]
	public class ForEachRowInTableTestCase : FolderTestCase
	{
		public ForEachRowInTableTestCase() : base( "For Each Row In Table" )
		{
			Icon = Properties.Resources.Loop;
		}

		private string tableName = string.Empty;

		/// <summary>
		/// The table to iterate on (excluding $)
		/// </summary>
		[Category(" Table")]
		[DisplayName("Table Name")]
		[Description("The table to iterate on (excluding $)")]
		[TypeConverter(typeof(TableVariableNameTypeConverter))]
		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}

		public override void Body()
		{
			ScenarioVariable<DataTable> table = Scenario.Tables[tableName];

			if (table == null)
				throw new ArgumentException("Table '" + tableName + "' is not recognized");

			DataTable dataTable = table.Value as DataTable;
			int j = 0;
			foreach (DataRow row in dataTable.Rows)
			{
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					Scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable<string>(tableName + ".CurrentRow." + dataTable.Columns[i].ColumnName, row[i].ToString(), this));
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
		}
	}
}
