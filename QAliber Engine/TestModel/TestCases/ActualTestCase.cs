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

namespace QAliber.TestModel
{
	/// <summary>
	/// A simple folder for more organized and hierarchic test scenario, you can add test cases for this folder
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control")]
	[XmlType("TestCase", Namespace=Util.XmlNamespace)]
	public class ActualTestCase : FolderTestCase
	{
		public ActualTestCase() : base( "Test Case" )
		{
			Icon = Properties.Resources.ApproveReject;
		}

		protected override bool ExitBranchOnErrorDefaultValue {
			get { return false; }
		}
	}
	
}
