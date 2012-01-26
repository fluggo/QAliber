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
using System.ComponentModel;
using System.Drawing;
using QAliber.Logger;
using System.Xml.Serialization;
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;

namespace QAliber.Repository.CommonTestCases.UI.Mouse
{
	/// <summary>
	/// Resizes a top level window to the specified size
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Windows")]
	[XmlType("ResizeWindow", Namespace=Util.XmlNamespace)]
	public class ResizeWindow : TestCase
	{
		public ResizeWindow() : base( "Resize Window" )
		{
			icon = Properties.Resources.Window;
		}

		private string control = "";

		/// <summary>
		/// The window to resize, make sure the 'UIType' in the locator dialog is 'UIAWindow'
		/// </summary>
		[Category(" Window")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description("The window to resize, make sure the 'UIType' is 'UIAWindow'")] 
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private Size size;

		/// <summary>
		/// The size in pixels to set the window"
		/// </summary>
		[Category(" Window")]
		[Description("The size in pixels to set the window")]
		public Size Size
		{
			get { return size; }
			set { size = value; }
		}
	
	
		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new InvalidOperationException("Control not found");
			}

			UIAWindow window = c as UIAWindow;

			if( window == null ) {
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new InvalidOperationException( "Control doesn't appear to be a window" );
			}

			window.Resize( size.Width, size.Height );
		}

		public override string Description
		{
			get
			{
				return "Resizing window '" + control + "' to size " + size;
			}
		}

	}
}
