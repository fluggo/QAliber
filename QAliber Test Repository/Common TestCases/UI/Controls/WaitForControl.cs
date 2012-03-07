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
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using QAliber.Engine.Controls;
using System.Diagnostics;
using System.Xml.Serialization;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("WaitForControl", Namespace=Util.XmlNamespace)]
	public class WaitForControl : TestCase
	{
		public WaitForControl() : base( "Wait for Control" )
		{
			Icon = Properties.Resources.Window;
		}

		private string control = "";

		[Category("Behavior")]
		[DisplayName("Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private int timeout = 1000;

		[Category("Behavior")]
		[DisplayName("Timeout")]
		[Description("The timeout in milliseconds to wait for the control.")]
		[DefaultValue(1000)]
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		public override void Body()
		{
			string code;
			bool res = false;
			
			code = "UIControlBase c = " + control + ";return c;\n";
			Stopwatch watch = new Stopwatch();
			string lastException = string.Empty;
			watch.Start();
			while (watch.ElapsedMilliseconds < timeout + 10)
			{
				try
				{
					UIControlBase c = UIControlBase.FindControlByPath( control );

					if (c.Exists)
					{
						res = true;
						break;
					}
				}
				catch (Exception ex)
				{
					lastException = ex.Message;
				}
			}
			if (res)
				ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			else
			{
				LogFailedByExpectedResult("Control not found after " + timeout + " miliseconds",control);
				if (lastException != string.Empty)
				{
					Log.Default.Warning("Exception caught", lastException, EntryVerbosity.Debug);
				}
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}

		}

		public override string Description
		{
			get
			{
				return "Waiting for control " + control;
			}
		}

	}


}
