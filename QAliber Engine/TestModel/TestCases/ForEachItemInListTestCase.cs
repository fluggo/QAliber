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

		private ListVariableDropDownList listName = new ListVariableDropDownList();


		/// <summary>
		/// The list to iterate on (excluding $)
		/// </summary>
		[Editor(typeof(QAliber.TestModel.TypeEditors.ComboDropDownTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("List")]
		[DisplayName("List Name")]
		[Description("The list to iterate on (excluding $)")]
		public ListVariableDropDownList ListName
		{
			get { return listName; }
			set { listName = value; }
		}

		public override void Setup()
		{
			base.Setup();
			list = Scenario.Lists[listName.Selected];
			if (list == null)
				throw new ArgumentException("List '" + listName + "' is not recognized");
		}

		public override void Body()
		{
			string[] vals = list.Value as string[];
			foreach (string obj in vals)
			{
				Scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable<string>(listName + ".Current", obj, this));
				Log.Default.IndentIn("Iteration on item '" + obj.ToString() + "'");
				base.Body();
				Log.Default.IndentOut();
				if (exitTotally)
					break;
				if (branchesToBreak > 0)
				{
					branchesToBreak--;
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

		protected ScenarioVariable<string[]> list;
	
	}
	
}
