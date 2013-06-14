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

namespace QAliber.TestModel
{
	/// <summary>
	/// Iterates on a given list, item by item.
	/// <preconditions>List should exist</preconditions>
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Loops")]
	[XmlType("ForEach", Namespace=Util.XmlNamespace)]
	public class ForEachTestCase : FolderTestCase
	{
		public ForEachTestCase() : base( "For Each Item In List" )
		{
			Icon = Properties.Resources.Loop;
		}

		private string listName = string.Empty;


		/// <summary>
		/// The list to iterate on (excluding $)
		/// </summary>
		[Category("List")]
		[DisplayName("List Name")]
		[Description("The list to iterate on (excluding $)")]
		[TypeConverter(typeof(ListVariableNameTypeConverter))]
		public string ListName
		{
			get { return listName; }
			set { listName = value; }
		}

		public override void Body( TestRun run )
		{
			Log log = Log.Current;
			ScenarioVariable<string[]> list = run.Lists[listName];

			if (list == null)
				throw new ArgumentException("List '" + listName + "' is not recognized");

			string[] vals = list.Value;
			foreach (string obj in vals)
			{
				run.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable<string>(listName + ".Current", obj, this));

				if( log != null )
					log.StartFolder( "Iteration on item '" + obj.ToString() + "'", null );

				base.Body( run );

				if( log != null )
					log.EndFolder();

				if (run.Canceled)
					break;
				if (run.BranchesToBreak > 0)
				{
					run.BranchesToBreak--;
					break;
				}
			}
		}

		public override string Description
		{
			get
			{
				return "For Each Item In '" + listName + "'";
			}
		}
	}
	
}
