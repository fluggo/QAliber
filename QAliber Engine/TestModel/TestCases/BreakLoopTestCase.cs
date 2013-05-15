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
using System.Drawing;

using QAliber.Logger;
using QAliber.TestModel.Attributes;

namespace QAliber.TestModel
{
	/// <summary>
	/// Breaks N levels up from folders (including loops, if's and any test case that has children)
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control")]
	[XmlType("BreakLoop", Namespace=Util.XmlNamespace)]
	public class BreakLoopTestCase : TestCase
	{
		public BreakLoopTestCase() : base( "Cut Branches" )
		{
			Icon = Properties.Resources.BreakFolder;
		}

		protected uint numOfLoopsToBreak = 1;

		/// <summary>
		/// The number of levels up the tree you want to go back
		/// <example>1 - will execute the next sibling's of the test case's parent</example>
		/// <example>2 - will execute the next sibling's of the test case's grand parent, and so on</example>
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("Number Of Branches To Cut")]
		[Description("How many levels up the tree do you want to break to ?\n e.g. '1' will exit the current branch")]
		public uint NumOfLoopsToBreak
		{
			get { return numOfLoopsToBreak; }
			set { numOfLoopsToBreak = value; }
		}

		public override void Body( TestRun run )
		{
			branchesToBreak = numOfLoopsToBreak;
			ActualResult = TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Breaking " + numOfLoopsToBreak + " Levels Up The Tree";
			}
		}

	}

}
