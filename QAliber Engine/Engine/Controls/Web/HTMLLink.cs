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
	/// Represents the a html tag.
	/// Either link to another document, by using the href attribute
	/// or a bookmark inside a document, by using the name attribute
	/// </summary>
	public class HTMLLink : WebControl
	{
		public HTMLLink(IHTMLElement element )
			: base(element)
		{
		}
		/// <summary>
		/// Retrieve the specifieds destination of a link.
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLLink link = Browser.This.CurrentPage.Body["CENTER", 1]["FONT", 1]["A", 3] as HTMLLink;
		///    Browser.This.CurrentPage.Navigate(link.LinkTo);
		/// </code>
		/// </example>
		/// <returns>string destination of link</returns>
		[Category("Web Link")]
		[DisplayName("Link To")]
		public string LinkTo
		{
			get 
			{
				return ((IHTMLAnchorElement)htmlElement).href;
			}
		}
		 
	}
}
