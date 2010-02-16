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
	/// Represents HTML element DIV tag.
	/// </summary>
	public class HTMLDiv : WebControl
	{
		public HTMLDiv(IHTMLElement element)
			: base(element)
		{ }
		/// <summary>
		/// Retrieve control align state (deprecated)
		/// </summary>
		/// <example>
		/// <code>
		///  HTMLDiv gmailDiv = Browser.This.CurrentPage.FindByID("DIV", "t_1");
		///    if (gmailDiv.DontWrapText)
		///    {
		///    }
		/// </code>
		/// </example>
		/// <returns>
		/// string: left,right, center,justify
		/// </returns>
		/// <remarks>Deprecated, it is suggested to use Styles instead. </remarks>
		[Category("HTMLDiv properties:")]
		public string Align
		{
			get
			{
				return ((IHTMLDivElement)htmlElement).align;
			}

			set
			{
				((IHTMLDivElement)htmlElement).align = value;
			}
		}
		/// <summary>
		/// Verify if text on control should be wraped.
		/// </summary>
		/// <example>
		/// <code>
		///  HTMLDiv gmailDiv = Browser.This.CurrentPage.FindByID("DIV", "t_1");
		///   if (gmailDiv.DontWrapText)
		///    {
		///    }
		/// </code>
		/// </example>
		/// <returns>true if text is should not be wraped, else return false</returns>
		[Category("HTMLDiv properties:")]
		public bool DontWrapText
		{
			get
			{
				return ((IHTMLDivElement)htmlElement).noWrap;
			}

			set
			{
				((IHTMLDivElement)htmlElement).noWrap = value;
			}
		}
	}
}
