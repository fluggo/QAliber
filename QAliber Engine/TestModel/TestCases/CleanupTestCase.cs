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
using QAliber.RemotingModel;

namespace QAliber.TestModel
{
	/// <summary>
	/// A folder which by default runs regardless of the containing folder's outcome.
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control")]
	public class CleanupTestCase : FolderTestCase
	{
		public CleanupTestCase() : base( "Cleanup" )
		{
			expectedResult = TestCaseResult.Passed;
			icon = Properties.Resources.Edit_UndoHS;
		}

		protected override bool AlwaysRunDefaultValue {
			get { return true; }
		}
	}

}
