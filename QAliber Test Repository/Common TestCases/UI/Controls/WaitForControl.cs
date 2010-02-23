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

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	public class WaitForControl : TestCase
	{
		public WaitForControl()
		{
			name = "Wait For Control";
			icon = Properties.Resources.Window;
		}

		private string control = "";

		
		[Category("Control")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private int timeout = 1000;

		[Category("Control")]
		[DisplayName("2) Timeout")]
		[Description("The timeout in miliseconds to wait for the control")]
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
					UIControlBase c = (UIControlBase)QAliber.Repository.CommonTestCases.Eval.CodeEvaluator.Evaluate(code);

					if (c != null)
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
				actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			else
			{
				LogFailedByExpectedResult("Control not found after " + timeout + " miliseconds",control);
				if (lastException != string.Empty)
				{
					Log.Default.Warning("Exception caught", lastException, EntryVerbosity.Debug);
				}
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}

		}

		public override string Description
		{
			get
			{
				return "Waiting for control " + control;
			}
			set
			{
				base.Description = value;
			}
		}

	}


}
