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
	[XmlType("Try", Namespace=Util.XmlNamespace)]
	public class TryTestCase : FolderTestCase
	{
		public TryTestCase() : base( "Try" )
		{
			icon = Properties.Resources.Try;
		}

		private void SetExitOnErrorRec(FolderTestCase testcase)
		{
			foreach (TestCase child in testcase.Children)
			{
				child.ExitOnError = true;
				if (child is FolderTestCase)
					SetExitOnErrorRec((FolderTestCase)child);
			}
		}

		public override void Body()
		{
			lastError = string.Empty;
			SetExitOnErrorRec(this);
			Log.Default.BeforeErrorIsPosted += new EventHandler<LogEventArgs>(BeforeErrorIsPosted);
			base.Body();
			exitTotally = false;
			if (actualResult == TestCaseResult.Failed)
				lastError = errListener;
			Log.Default.BeforeErrorIsPosted -= new EventHandler<LogEventArgs>(BeforeErrorIsPosted);
		}

		private void BeforeErrorIsPosted(object sender, LogEventArgs e)
		{
			errListener += e.LogEntryProperties.Message;
		}

		internal static string lastError = string.Empty;
		private string errListener = string.Empty;
	}
	
}
