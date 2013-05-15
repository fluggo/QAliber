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
using QAliber.TestModel.TypeEditors;
using QAliber.Engine.Controls;
using QAliber.ImageHandling;
using System.Xml.Serialization;
using QAliber.Engine.Patterns;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("ReadTextFromControl", Namespace=Util.XmlNamespace)]
	public class ReadTextFromControl : TestCase
	{
		public ReadTextFromControl() : base( "Read Text from Control" )
		{
			Icon = Properties.Resources.Window;
		}

		private string control = "";

		
		[Category("Behavior")]
		[DisplayName("Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string text;

		[Category("Results")]
		[DisplayName("Text Read")]
		[Description("The text read from the control.")]
		[XmlIgnore]
		public string TextRead
		{
			get { return text; }
		}


		public override void Body( TestRun run )
		{
			ActualResult = TestCaseResult.Passed;
			text = string.Empty;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				Logger.Log.Error( "Couldn't find control", control );
				ActualResult = TestCaseResult.Failed;
				return;
			}

			IText textPattern = c.GetControlInterface<IText>();

			if( textPattern != null ) {
				text = textPattern.Text;
				Logger.Log.Info("Found text property", text);
				return;
			}

			if (c is QAliber.Engine.Controls.Web.WebControl)
			{
				text = ((QAliber.Engine.Controls.Web.WebControl)c).InnerText;
				Logger.Log.Info("Found inner text property of web control", text);
				return;
			}

			Logger.Log.Info("Couldn't find a text property, trying to do optical character recognition");
			OCRItem ocrItem = new OCRItem(c.GetImage());
			text = ocrItem.ProcessImage();
			Logger.Log.Info("OCR result = " + text);
			
		}

		public override string Description
		{
			get
			{
				return "Getting properties for control " + control;
			}
		}

		public override object Clone() {
			ReadTextFromControl result = (ReadTextFromControl) base.Clone();

			result.text = null;

			return result;
		}
	}


}
