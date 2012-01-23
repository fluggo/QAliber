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
	/// Executes its children if the last evaluated if was false
	/// <preconditions>A previous if was evaluated</preconditions>
	/// <seealso cref="T:QAliber.TestModel.IfTestCase"/>
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Conditions")]
	public class ElseTestCase : FolderTestCase
	{
		public ElseTestCase()
		{
			name = "Else";
			icon = Properties.Resources.If;
			
		}

		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			Log.Default.Info("Last If result = " + IfTestCase.ifConditionValue);
			if (!IfTestCase.ifConditionValue)
			{
				base.Body();
			}
		}

		public override string Description
		{
			get
			{
				return "Performs else on last if's result";
			}
		}
	}
	
}
