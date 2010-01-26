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
	public class ForEachTestCase : FolderTestCase
	{
		public ForEachTestCase()
		{
			name = "For Each Item In List";
			icon = Properties.Resources.Loop;
		}

		private ListVariableDropDownList listName = new ListVariableDropDownList();


		/// <summary>
		/// The list to iterate on (excluding $)
		/// </summary>
		[Editor(typeof(QAliber.TestModel.TypeEditors.ComboDropDownTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Test Case Flow Control")]
		[DisplayName("List Name")]
		[Description("The list to iterate on")]
		public ListVariableDropDownList ListName
		{
			get { return listName; }
			set { listName = value; }
		}

		public override void Setup()
		{
			base.Setup();
			list = scenario.Lists[listName.Selected];
			if (list == null)
				throw new ArgumentException("List '" + listName + "' is not recognized");
		}

		public override void Body()
		{
			string[] vals = list.Value as string[];
			foreach (string obj in vals)
			{
				scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable(listName + ".Current", obj, this));
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
			set
			{
				base.Description = value;
			}
		}

		protected ScenarioList list;
	
	}
	
}
