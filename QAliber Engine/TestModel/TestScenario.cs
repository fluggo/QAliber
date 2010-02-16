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
using QAliber.Logger;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using QAliber.TestModel.Variables;
using System.Collections;
using System.Data;

namespace QAliber.TestModel
{
	[Serializable]
	public class TestScenario
	{
		public TestScenario()
		{
			variables = new BindingVariableList<ScenarioVariable>();
			lists = new BindingVariableList<ScenarioList>();
			tables = new BindingVariableList<ScenarioTable>();
			rootTestCase = new FolderTestCase();
			rootTestCase.Scenario = this;
			rootTestCase.Name = "Unknown Scenario";
			filename = "Unknown Scenario.scn";
		}

		#region Test Scenario Descriptors
		private string name;

		[Category("Test Scenario Details")]
		[Description("The name of the test scenario")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private string description;

		[Category("Test Scenario Details")]
		[Description("The description of the test scenario")]
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		private string filename;

		[Category("Test Scenario Details")]
		[Description("The filename that contains the test scenario")]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		private bool hasChanged;

		[Browsable(false)]
		public bool HasChanged
		{
			get { return hasChanged; }
			set { hasChanged = value; }
		}

		private FolderTestCase rootTestCase;

		[Browsable(false)]
		public FolderTestCase RootTestCase
		{
			get { return rootTestCase; }
			set { rootTestCase = value; }
		}

		#endregion

		#region Global Variables
		private BindingVariableList<ScenarioVariable> variables;

		[Browsable(false)]
		[XmlIgnore]
		public BindingVariableList<ScenarioVariable> Variables
		{
			get { return variables; }
			set { variables = value; }
		}

		private BindingVariableList<ScenarioList> lists;

		[Browsable(false)]
		[XmlIgnore]
		public BindingVariableList<ScenarioList> Lists
		{
			get { return lists; }
			set { lists = value; }
		}

		private BindingVariableList<ScenarioTable> tables;

		[Browsable(false)]
		[XmlIgnore]
		public BindingVariableList<ScenarioTable> Tables
		{
			get { return tables; }
			set { tables = value; }
		}
		#endregion

		#region Methods

		public void Run()
		{
			SaveUserVariables();
			//Log.Default.IndentIn("Scenario '" + name + "'");
			rootTestCase.Run();
			//Log.Default.IndentOut();
			ReloadUserVariables();
		}

		public void Save()
		{
			BinaryFormatter binFormatter = new BinaryFormatter();
			using (StreamWriter writer = new StreamWriter(filename))
			{
				binFormatter.Serialize(writer.BaseStream, this);
			}
		}

		public void SaveAs(string newFilename)
		{
			BinaryFormatter binFormatter = new BinaryFormatter();
			
			using (StreamWriter writer = new StreamWriter(newFilename))
			{
				binFormatter.Serialize(writer.BaseStream, this);
			}
		}

		public static TestScenario Load(string filename)
		{
			TestScenario testScenario = null;
			BinaryFormatter binFormatter = new BinaryFormatter();
			using (StreamReader reader = new StreamReader(filename))
			{
				testScenario = binFormatter.Deserialize(reader.BaseStream) as TestScenario;
			}
			if (testScenario != null)
			{
				testScenario.Filename = filename;
				testScenario.RootTestCase.Parent = null;
				testScenario.RootTestCase.Scenario = testScenario;
				testScenario.SetDependantsRecursively(testScenario.RootTestCase);
			}
			return testScenario;
		}

		private void SetDependantsRecursively(TestCase testcase)
		{
			TestCase.maxID = TestCase.maxID > testcase.ID ? TestCase.maxID : testcase.ID;
			FolderTestCase folder = testcase as FolderTestCase;
			if (folder != null && folder.Children != null)
			{
				foreach (TestCase child in folder.Children)
				{
					child.Parent = folder;
					child.Scenario = this;
					SetDependantsRecursively(child);
				}
			}
		}

		#endregion

		#region Variables Parsing Methods

		private void SaveUserVariables()
		{
			variablesCopy = new BindingVariableList<ScenarioVariable>();
			listsCopy = new BindingVariableList<ScenarioList>();
			tablesCopy = new BindingVariableList<ScenarioTable>();

			foreach (ScenarioVariable var in variables)
			{
				//if (var.DefinedBy == "User")
					variablesCopy.Add(new ScenarioVariable(
						var.Name, var.Value.ToString(), null));
			}

			foreach (ScenarioList var in lists)
			{
				//if (var.DefinedBy == "User")
				//{
					ICollection vals = var.Value as ICollection;
					string[] copyList= new string[vals.Count];
					vals.CopyTo(copyList, 0);
					listsCopy.Add(new ScenarioList(
						var.Name, copyList, null));
				//}
			}

			foreach (ScenarioTable var in tables)
			{
				//if (var.DefinedBy == "User")
				//{
					DataTable table = var.Value as DataTable;
					DataTable copyTable = table.Copy();
					tablesCopy.Add(new ScenarioTable(
						var.Name, copyTable, null));
				//}
			}
		}

		private void ReloadUserVariables()
		{
			variables = variablesCopy;
			variables.ResetBindings();
			lists = listsCopy;
			lists.ResetBindings();
			tables = tablesCopy;
			tables.ResetBindings();
		}

		internal string ReplaceAllVars(string input)
		{
			Regex regex = new Regex(@"\$([^\$\[\]]+)\$");
			string res = regex.Replace(input, new MatchEvaluator(ReplaceVarForMatch));
			regex = new Regex(@"\$([^\$]+)\[([0-9]+)\]\$");
			res = regex.Replace(res, new MatchEvaluator(ReplaceListForMatch));
			regex = new Regex(@"\$([^\$]+)\[([0-9]+),([0-9]+)\]\$");
			res = regex.Replace(res, new MatchEvaluator(ReplaceTableForMatch));
			regex = new Regex(@"\$([^\$]+)\.([a-zA-Z]+)\$");
			res = regex.Replace(res, new MatchEvaluator(ReplacePropertyForMatch));

			return res;
		}

		private string ReplaceVarForMatch(Match match)
		{
			string key = match.Value.Trim('$');
			ScenarioVariable var = variables[key];
			if (var != null)
				return var.Value.ToString();
			else
				return match.Value;
		}

		private string ReplaceListForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			ScenarioList list = lists[key];
			if (lists != null)
			{
				IList val = list.Value as IList;
				if (val != null)
				{
					int index = val.Count;
					if (int.TryParse(match.Groups[2].Value, out index))
					{
						if (index < val.Count)
							return val[index].ToString();
					}
				}
			}
			return match.Value;
		}

		private string ReplacePropertyForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			ScenarioList list = lists[key];
			if (lists != null)
			{
				IList val = list.Value as IList;
				if (val != null)
				{
					switch (match.Groups[2].Value.ToLower())
					{
						case "length":
							return val.Count.ToString();
						default:
							break;
					}
					
				}
			}
			ScenarioTable table = tables[key];
			if (table != null)
			{
				DataTable val = table.Value as DataTable;
				if (val != null)
				{
					switch (match.Groups[2].Value.ToLower())
					{
						case "numberofrows":
							return val.Rows.Count.ToString();
						case "numberofcolumns":
							return val.Columns.Count.ToString();
						default:
							break;
					}

				}
			}
			return match.Value;
		}

		private string ReplaceTableForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			ScenarioTable table = tables[key];
			if (table != null)
			{
				DataTable dataTable = table.Value as DataTable;
				if (dataTable != null)
				{
					int rowIndex = dataTable.Rows.Count, colIndex = dataTable.Columns.Count;
					if (int.TryParse(match.Groups[2].Value, out rowIndex)
						&& int.TryParse(match.Groups[3].Value, out colIndex))
					{
						if (rowIndex < dataTable.Rows.Count && colIndex < dataTable.Columns.Count)
							return dataTable.Rows[rowIndex][colIndex].ToString();
					}
				}
			}
			return match.Value;
		}
		#endregion

		private BindingVariableList<ScenarioVariable> variablesCopy;
		private BindingVariableList<ScenarioList> listsCopy;
		private BindingVariableList<ScenarioTable> tablesCopy;
	}
}
