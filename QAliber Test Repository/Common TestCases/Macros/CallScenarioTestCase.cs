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
using QAliber.TestModel;

namespace QAliber.Repository.CommonTestCases.Macros
{
	[Serializable]
	[VisualPath(@"Flow Control")]
	[XmlType(TypeName="CallScenario", Namespace=Util.XmlNamespace)]
	public class CallScenarioTestCase : TestCase
	{
		public CallScenarioTestCase() : base( "Run Another Scenario" ) {
		}

		private string sourceFile = "";

		/// <summary>
		/// The 1st file to compare
		/// </summary>
		[DisplayName("Scenario File")]
		[Category(" Scenario")]
		[Description("The scenario file to run")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		public override void Body( TestRun run )
		{
			ActualResult = TestCaseResult.Passed;
			TestScenario scenario = TestScenario.Load(sourceFile);

			TestRun newRun = new TestRun( scenario );
			scenario.Run( newRun );
		}
	}
	
}
