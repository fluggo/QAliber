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
using System.Drawing;
using QAliber.ImageHandling;
using System.Xml.Serialization;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	[XmlType("ClickOnImage", Namespace=Util.XmlNamespace)]
	public class ClickOnImage : OperateOnImage
	{
		public ClickOnImage() : this( "Click on Image" ) {
		}

		protected ClickOnImage( string name ) : base( name ) {
			icon = Properties.Resources.Mouse;
			actionType = QAliber.Engine.ControlActionType.Click;
			button = MouseButtons.Left;
		}

		[Category("Mouse")]
		[DisplayName("Button")]
		[Description("The mouse button to click")]
		public MouseButtons Button
		{
			get { return button; }
			set { button = value; }
		}
	
	}


}
