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
			variables = new BindingVariableList<ScenarioVariable<string>, string>();
			lists = new BindingVariableList<ScenarioVariable<string[]>, string[]>();
			tables = new BindingVariableList<ScenarioTable, DataTable>();
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
		private BindingVariableList<ScenarioVariable<string>, string> variables;

		[Browsable(false)]
		public BindingVariableList<ScenarioVariable<string>, string> Variables
		{
			get { return variables; }
			set { variables = value; }
		}

		private BindingVariableList<ScenarioVariable<string[]>, string[]> lists;

		[Browsable(false)]
		public BindingVariableList<ScenarioVariable<string[]>, string[]> Lists
		{
			get { return lists; }
			set { lists = value; }
		}

		private BindingVariableList<ScenarioTable, DataTable> tables;

		[Browsable(false)]
		public BindingVariableList<ScenarioTable, DataTable> Tables
		{
			get { return tables; }
			set { tables = value; }
		}
		#endregion

		#region Methods

		public void Run( TestRun run )
		{
			//Log.Default.IndentIn("Scenario '" + name + "'");
			rootTestCase.Run( run );
			//Log.Default.IndentOut();
		}

		static XmlSerializer CreateXmlSerializer() {
			// Pull in all the additional types we need
			List<Type> extraTypes = new List<Type>();
			extraTypes.AddRange( TestController.SupportedTypes );
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

/*			attributeOverrides.Add( typeof(ScenarioTable), new XmlAttributes() {
				XmlType = new XmlTypeAttribute() { TypeName = "TableVariable", Namespace = Util.XmlNamespace }
			} );*/

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

			var attrs = TestController.SupportedTypes
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

		[Obsolete("Should be moved to TestRun")]
		private void ReloadUserVariables<TVar, TValue>( BindingVariableList<TVar, TValue> list, BindingVariableList<TVar, TValue> savedList )
				where TVar : ScenarioVariable<TValue> {
			list.RaiseListChangedEvents = false;
			list.Clear();

			foreach( var variable in savedList )
				list.Add( variable );

			list.RaiseListChangedEvents = true;
			list.ResetBindings();
		}

		#endregion
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
