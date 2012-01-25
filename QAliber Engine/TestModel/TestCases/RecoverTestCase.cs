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
	[Serializable]
	[VisualPath(@"Flow Control\Recovery")]
	[XmlType("Recover", Namespace=Util.XmlNamespace)]
	public class RecoverTestCase : FolderTestCase
	{
		/// <summary>
		/// Try to recover from a general/specific error
		/// <preconditions>A previous 'Try' test case was performed</preconditions>
		/// </summary>
		public RecoverTestCase() : base( "Recover" )
		{
			icon = Properties.Resources.Recover;
		}

		private string errorToCatch = string.Empty;

		/// <summary>
		/// If the errors in the log inside a 'Try' folder contains this parameter, this folder will be executed. To execute on any error, leave it blank
		/// </summary>
		[Category(" Recovery")]
		[DisplayName("Error To Catch")]
		[Description("If the errors in the log inside a 'Try' folder contains this parameter, this folder will be executed.\nTo execute on any error, leave it blank")]
		public string ErrorToCatch
		{
			get { return errorToCatch; }
			set { errorToCatch = value; }
		}

		public override void Body()
		{
			actualResult = TestCaseResult.Passed;
			if (!string.IsNullOrEmpty(TryTestCase.lastError) && TryTestCase.lastError.Contains(errorToCatch))
				base.Body();
			else
				Log.Default.Info("Recovery is not needed");
		}
		
	}
	
}
