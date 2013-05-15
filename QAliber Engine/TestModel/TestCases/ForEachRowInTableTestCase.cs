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

		private string _tableName = string.Empty;

		/// <summary>
		/// The table to iterate on (excluding $)
		/// </summary>
		[Category("Behavior")]
		[DisplayName("Table Name")]
		[Description("The table to iterate on (excluding $)")]
		[TypeConverter(typeof(TableVariableNameTypeConverter))]
		public string TableName
		{
			get { return _tableName; }
			set { _tableName = value; }
		}

		private bool _prefixTableName = true;

		/// <summary>
		/// The table to iterate on (excluding $)
		/// </summary>
		[Category("Behavior")]
		[DisplayName("Prefix Table Name")]
		[Description("True to prefix each column name with the name of the table, or false otherwise.")]
		[DefaultValue(true)]
		public bool PrefixTableName
		{
			get { return _prefixTableName; }
			set { _prefixTableName = value; }
		}

		public override void Body( TestRun run )
		{
			Log log = Log.Current;
			ScenarioVariable<DataTable> table = Scenario.Tables[_tableName];

			if (table == null)
				throw new ArgumentException("Table '" + _tableName + "' is not recognized");

			DataTable dataTable = table.Value as DataTable;
			int j = 0;
			foreach (DataRow row in dataTable.Rows)
			{
				StringBuilder extra = new StringBuilder();
				extra.AppendLine( "Variables:" );

				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					string name = dataTable.Columns[i].ColumnName;

					if( _prefixTableName )
						name = _tableName + "." + name;

					Scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable<string>(name, row[i].ToString(), this));

					extra.AppendFormat( "   {0}: {1}", name, row[i].ToString() ).AppendLine();
				}

				if( log != null )
					log.StartFolder( "Iteration on row '" + j + "'", extra.ToString() );

				base.Body( run );

				if( log != null )
					log.EndFolder();

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
				return "For Each Item In '" + _tableName + "'";
			}
		}
	}
}
