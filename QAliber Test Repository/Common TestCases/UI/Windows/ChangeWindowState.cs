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
using System.Xml.Serialization;
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Patterns;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	[XmlType("ChangeWindowState", Namespace=Util.XmlNamespace)]
	public class ChangeWindowState : TestCase
	{
		/// <summary>
		/// Activate, minimize, maximize, restore or close the window 
		/// </summary>
		public ChangeWindowState() : base( "Change Window State" )
		{
			Icon = QAliber.Repository.CommonTestCases.Properties.Resources.Window;
		}

		private string control = "";

		/// <summary>
		/// The window to change, make sure the 'UIType' in the locator dialog is 'UIAWindow'
		/// </summary>
		[Category("Behavior")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private WindowOperationType opType = WindowOperationType.SetFocus;

		/// <summary>
		/// The operation you want to perform on the window
		/// </summary>
		[Category("Behavior")]
		[Description("The operation you want to perform on the window.")]
		[DefaultValue(WindowOperationType.SetFocus)]
		public WindowOperationType Operation
		{
			get { return opType; }
			set { opType = value; }
		}
	
	
		public override void Body()
		{
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new InvalidOperationException("Control not found");
			}

			IWindowPattern window = c.GetControlInterface<IWindowPattern>();

			if( window == null ) {
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new InvalidOperationException( "Control doesn't appear to be a window" );
			}

			switch( opType ) {
				case WindowOperationType.Close:
					window.Close();
					break;

				case WindowOperationType.Maximize:
					if( !window.CanMaximize ) {
						Log.Default.Error( "Window does not support maximizing.", string.Empty, EntryVerbosity.Internal );
						return;
					}

					window.SetState( WindowVisualState.Maximized );
					break;

				case WindowOperationType.Minimize:
					if( !window.CanMinimize ) {
						Log.Default.Error( "Window does not support maximizing.", string.Empty, EntryVerbosity.Internal );
						return;
					}

					window.SetState( WindowVisualState.Minimized );
					break;

				case WindowOperationType.Restore:
					window.SetState( WindowVisualState.Normal );
					break;

				case WindowOperationType.SetFocus:
					window.SetState( WindowVisualState.Normal );
					c.SetFocus();
					break;
			}
		}

		public override string Description
		{
			get
			{
				return opType + " is performed on window '" + control + "'";
			}
		}

	}

	public enum WindowOperationType
	{
		SetFocus,
		Close,
		Minimize,
		Maximize,
		Restore
	}


}
