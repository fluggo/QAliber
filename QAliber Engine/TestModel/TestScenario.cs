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
using System.Runtime.Serialization.Formatters.Binary;
using QAliber.TestModel.Variables;
using System.Collections;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace QAliber.TestModel
{
	[Serializable]
	[XmlType("Scenario", Namespace=Util.XmlNamespace)]
	public class TestScenario
	{
		public TestScenario()
		{
			variables = new BindingVariableList<string>();
			lists = new BindingVariableList<string[]>();
			tables = new BindingVariableList<DataTable>();
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
		[XmlIgnore]
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
		private BindingVariableList<string> variables;

		[Browsable(false)]
		public BindingVariableList<string> Variables
		{
			get { return variables; }
			set { variables = value; }
		}

		private BindingVariableList<string[]> lists;

		[Browsable(false)]
		public BindingVariableList<string[]> Lists
		{
			get { return lists; }
			set { lists = value; }
		}

		private BindingVariableList<DataTable> tables;

		[Browsable(false)]
		public BindingVariableList<DataTable> Tables
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

		static XmlSerializer CreateXmlSerializer() {
			// Pull in all the additional types we need
			List<Type> extraTypes = new List<Type>();
			extraTypes.AddRange( TestController.Default.SupportedTypes );
			extraTypes.Add( typeof(DataTable) );

			// Assign System.Windows.Size a different namespace
			XmlAttributeOverrides attributeOverrides = new XmlAttributeOverrides();

			attributeOverrides.Add( typeof(System.Windows.Size), new XmlAttributes() {
				XmlType = new XmlTypeAttribute() { TypeName = "Size", Namespace = "assembly:WindowsBase" }
			} );

			attributeOverrides.Add( typeof(System.Windows.Point), new XmlAttributes() {
				XmlType = new XmlTypeAttribute() { TypeName = "Point", Namespace = "assembly:WindowsBase" }
			} );

			attributeOverrides.Add( typeof(ScenarioVariable<string>), new XmlAttributes() {
				XmlType = new XmlTypeAttribute() { TypeName = "StringVariable", Namespace = Util.XmlNamespace }
			} );

			attributeOverrides.Add( typeof(ScenarioVariable<string[]>), new XmlAttributes() {
				XmlType = new XmlTypeAttribute() { TypeName = "ListVariable", Namespace = Util.XmlNamespace }
			} );

			attributeOverrides.Add( typeof(ScenarioVariable<DataTable>), new XmlAttributes() {
				XmlType = new XmlTypeAttribute() { TypeName = "TableVariable", Namespace = Util.XmlNamespace }
			} );

			return new XmlSerializer( typeof(TestScenario), attributeOverrides,
				extraTypes.ToArray(), new XmlRootAttribute() { Namespace=Util.XmlNamespace }, Util.XmlNamespace );
		}

		public void Save() {
			XmlSerializer serializer = CreateXmlSerializer();
			XmlWriterSettings settings = new XmlWriterSettings() {
				Indent = true,
				CloseOutput = true,
				Encoding = Encoding.UTF8
			};

			// Scan the assemblies for PreferredXmlPrefix attributes
			List<XmlQualifiedName> namespaceList = new List<XmlQualifiedName>();
			namespaceList.Add( new XmlQualifiedName( "xsi", "http://www.w3.org/2001/XMLSchema-instance" ) );

			var attrs = TestController.Default.SupportedTypes
				.Select( type => type.Assembly )
				.Distinct()
				.SelectMany( assem =>
					assem.GetCustomAttributes( typeof(PreferredXmlPrefixAttribute), false )
						.Cast<PreferredXmlPrefixAttribute>() )
				.OrderBy( attr => attr.Namespace );

			// Now select some prefixes for them; we prefer to give them the one they asked for,
			// but we can substitute a numbered suffix if some collide
			HashSet<string> prefixesInUse = new HashSet<string>( new string[] { "xsi" } );

			foreach( PreferredXmlPrefixAttribute attr in attrs ) {
				if( namespaceList.Any( name => name.Namespace == attr.Namespace ) )
					break;

				string prefix = attr.Prefix;
				int suffix = 1;

				while( prefixesInUse.Contains( prefix ) )
					prefix = attr.Prefix + (suffix++).ToString();

				namespaceList.Add( new XmlQualifiedName( prefix, attr.Namespace ) );
				prefixesInUse.Add( prefix );
			}

			XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces( namespaceList.ToArray() );
			XmlWriter writer = XmlWriter.Create( Filename, settings );

			try {
				serializer.Serialize( writer, this, namespaces );
			}
			finally {
				writer.Close();
			}
		}

		public static TestScenario Load( string filename ) {
			XmlSerializer serializer = CreateXmlSerializer();
			XmlReader reader = XmlReader.Create( filename );
			TestScenario result;

			try {
				result = (TestScenario) serializer.Deserialize( reader );
			}
			finally {
				reader.Close();
			}

			if( result != null ) {
				result.Filename = filename;
				result.RootTestCase.Parent = null;
				result.RootTestCase.Scenario = result;
				result.SetDependantsRecursively( result.RootTestCase );
			}

			return result;
		}

		private void SetDependantsRecursively(TestCase testcase)
		{
			testcase.ID = TestCase.maxID++;
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
			variablesCopy = new BindingVariableList<string>();
			listsCopy = new BindingVariableList<string[]>();
			tablesCopy = new BindingVariableList<DataTable>();

			foreach (ScenarioVariable<string> var in variables)
			{
				variablesCopy.Add(new ScenarioVariable<string>(
					var.Name, var.Value.ToString(), var.TestStep));
			}

			foreach (ScenarioVariable<string[]> var in lists)
			{
				ICollection vals = var.Value as ICollection;
				string[] copyList= new string[vals.Count];
				vals.CopyTo(copyList, 0);
				listsCopy.Add(new ScenarioVariable<string[]>(
					var.Name, copyList, var.TestStep));
			}

			foreach (ScenarioVariable<DataTable> var in tables)
			{
				DataTable table = var.Value as DataTable;
				DataTable copyTable = table.Copy();
				tablesCopy.Add(new ScenarioVariable<DataTable>(
					var.Name, copyTable, var.TestStep));
			}
		}

		private void ReloadUserVariables()
		{
			ReloadUserVariables( variables, variablesCopy );
			ReloadUserVariables( lists, listsCopy );
			ReloadUserVariables( tables, tablesCopy );
		}

		private void ReloadUserVariables<T>( BindingVariableList<T> list, BindingVariableList<T> savedList ) {
			list.RaiseListChangedEvents = false;
			list.Clear();

			foreach( var variable in savedList )
				list.Add( variable );

			list.RaiseListChangedEvents = true;
			list.ResetBindings();
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
			var var = variables[key];
			if (var != null)
				return var.Value.ToString();
			else
				return match.Value;
		}

		private string ReplaceListForMatch(Match match)
		{
			string key = match.Groups[1].Value;
			var list = lists[key];
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
			var list = lists[key];
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
			var table = tables[key];
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
			ScenarioVariable<DataTable> table = tables[key];
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

		private BindingVariableList<string> variablesCopy;
		private BindingVariableList<string[]> listsCopy;
		private BindingVariableList<DataTable> tablesCopy;
	}

	sealed class DeserializationBinder : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			try
			{
				
				Type t = Type.GetType(String.Format("{0}, {1}",
				typeName, assemblyName));
				if (t != null)
				{
					return t;
				}
				errorTypes.Add(typeName);
				return typeof(QAliber.TestModel.TestCases.LoadErrorTestCase);
			}
			catch
			{
				return typeof(QAliber.TestModel.TestCases.LoadErrorTestCase);
			}
		}

		internal static List<string> errorTypes = new List<string>();
		internal static int errorIndex = 0;

	}
}
