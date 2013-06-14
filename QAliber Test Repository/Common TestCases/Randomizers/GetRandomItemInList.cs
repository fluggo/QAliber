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
using System.Xml.Serialization;



namespace QAliber.Repository.CommonTestCases.Randomizers
{
	/// <summary>
	/// Choose random item in a given list
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"Variables\Randomizers")]
	[XmlType("GetRandomItemInList", Namespace=Util.XmlNamespace)]
	public class GetRandomItemInListTestCase : global::QAliber.TestModel.TestCase
	{
		public GetRandomItemInListTestCase() : base( "Get Random Item in List" )
		{
			Icon = null;
		}

		public override void Body( TestRun run )
		{
			string[] collection = run.Lists[listName].Value;
			int index = new Random().Next(collection.Length);
			generatedItem = collection[index];
			Log.Info("Item picked = '" + generatedItem + "'");
			ActualResult = TestCaseResult.Passed;
		}

		private string listName = string.Empty;

		/// <summary>
		/// The list name (without $)
		/// </summary>
		[Category(" Random Generator")]
		[DisplayName("List Name")]
		[TypeConverter(typeof(ListVariableNameTypeConverter))]
		public string ListName
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
		[XmlIgnore]
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
		}

		public override object Clone() {
			GetRandomItemInListTestCase result = (GetRandomItemInListTestCase) base.Clone();

			result.generatedItem = null;

			return result;
		}
	}
}
