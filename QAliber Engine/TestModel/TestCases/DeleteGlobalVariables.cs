﻿/*	 
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
using QAliber.TestModel.Attributes;
using QAliber.TestModel.Variables;
using System.Xml.Serialization;
using QAliber.Logger;



namespace QAliber.TestModel
{
	/// <summary>
	/// Delete element from the variable \ tables \ lists
	/// <preconditions>element name exists</preconditions>
	/// <workflow>
	/// <action></action>
	/// </workflow>
	/// </summary>
	[Serializable]
	[VisualPath(@"Variables")]
	[XmlType("DeleteVariable", Namespace=Util.XmlNamespace)]
	public class DeleteGlobalVariables : TestCase
	{
		public DeleteGlobalVariables() : base( "Delete Variable" ) {
		}

		public override void Body( TestRun run )
		{
			switch (variableType)
			{
				case varTypes.Variables:
					for (int idx = 0; idx < run.Variables.Count; idx++)
					{
						if (run.Variables[idx].Name == variableName)
							run.Variables.RemoveAt(idx);
					}
					break;
				case varTypes.Lists:
					for (int idx = 0; idx < run.Lists.Count; idx++)
					{
						if (run.Lists[idx].Name == variableName)
							run.Lists.RemoveAt(idx);
					}
					break;
				case varTypes.Tables:
					for (int idx = 0; idx < run.Tables.Count; idx++)
					{
						if (run.Tables[idx].Name == variableName)
							run.Tables.RemoveAt(idx);
					}
					break;
			}
			ActualResult = TestCaseResult.Passed;
		}

		private varTypes variableType;

		public varTypes VariableType
		{
			get { return variableType; }
			set { variableType = value; }
		}

		private string variableName;

		public string VariableName
		{
			get
			{
				return variableName;
			}
			set
			{
				variableName = value;
			}
		}


	}

	public enum varTypes
	{
		Variables,
		Lists,
		Tables
	}


}
