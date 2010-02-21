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
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using QAliber.TestModel.Variables;
using QAliber.TestModel;



namespace QAliber.Repository.CommonTestCases.Randomizers
{
	/// <summary>
	/// Choose random item in a given list
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"Variables\Randomizers")]
	public class GetRandomItemInListTestCase : global::QAliber.TestModel.TestCase
	{
		public GetRandomItemInListTestCase()
		{
			name = "Get Random Item In List";
			icon = null;
			listName = new ListVariableDropDownList();
		}

		public override void Body()
		{
			ICollection collection = Scenario.Lists[listName.Selected].Value as ICollection;
			int curIndex = 0;
			int index = new Random().Next(collection.Count);
			IEnumerator i = collection.GetEnumerator();

			while (curIndex <= index)
			{
				i.MoveNext();
				curIndex++;
			}
			generatedItem = i.Current.ToString();
			Log.Default.Info("Item picked = '" + generatedItem + "'");
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private ListVariableDropDownList listName;

		/// <summary>
		/// The list name (without $)
		/// </summary>
		[Category(" Random Generator")]
		[Editor(typeof(QAliber.TestModel.TypeEditors.ComboDropDownTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("List Name")]
		public ListVariableDropDownList ListName
		{
			get { return listName; }
			set { listName = value; }
		}

		private string generatedItem;

		/// <summary>
		/// Read-only - The item that was chosen randomly
		/// </summary>
		[Category(" Random Generator")]
		[DisplayName("Chosen Item")]
		public string GeneratedItem
		{
			get { return generatedItem; }
		}
	
	

		public override string Description
		{
			get
			{
				return string.Format("Picking item from '{0}'", listName);
			}
			set
			{
				base.Description = value;
			}
		}
	}

	

	
}
