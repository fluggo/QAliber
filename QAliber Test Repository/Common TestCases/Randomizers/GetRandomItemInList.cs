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



namespace QAliber.Repository.CommonTestCases.Registry
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
