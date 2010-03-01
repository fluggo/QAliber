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
using mshtml;
using System.ComponentModel;
namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents HTML INPUT element.
	/// An input field can vary , depending on the type attribute (text,button,etc').
	/// </summary>
	public class HTMLTextArea : WebControl
	{
		public HTMLTextArea(IHTMLElement element)
			: base(element)
		{

		}

		#region props
		/// <summary>
		/// Get or Set (text,password inputs) value in the control
		/// </summary>
		/// <example>
		/// <code>
		///  
		///    HTMLTextArea txtBox = Browser.This.CurrentPage.FindByName("textarea", "txt) as HTMLTextArea;
		///    txtBox.Value = "Search me";
		///    string searchText = txtBox.Value;
		/// </code>
		/// </example>
		[Category("HTMLTextArea")]
		public string Value
		{
			get { return ((IHTMLTextAreaElement) htmlElement).value; }
			set 
			{
				((IHTMLTextAreaElement)htmlElement).value = value;	   
			}
		}
		
		/// <summary>
		/// Retrieve disabled /enabled state of the INPUT control
		/// </summary>
		/// <example>
		/// HTMLTextArea txt = Browser.This.CurrentPage.FindByName("textarea", "txt) as HTMLTextArea;
		///    if (txt.Enabled)
		///    {
		///			txt.Value= "bla";
		///    }
		/// </example>
		/// <retruns> true if the control is enabled, false if disabled</retruns>
		[Category("HTMLTextArea")]
		public override bool Enabled
		{
			get
			{
				bool res = ((IHTMLTextAreaElement)htmlElement).disabled;

				if (res)
					return false;

				return true;
			}
		}
		#endregion
	}
}
