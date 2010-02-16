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
using System.Threading;


namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represent TABLE HTML tag.
	/// A simple HTML table consists of the table element and one or more tr, th, and td elements.
	/// A more complex HTML table may also include caption, col, colgroup, thead, tfoot, and tbody elements.
	/// </summary>
	[DisplayName("Table")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class HTMLTable : WebControl
	{
		public HTMLTable(IHTMLElement element)
			: base(element)
		{
		  
		}
		/// <summary>
		/// Retrieve the Rows in the current Table (HTMLTr [] )
		/// </summary>
		/// <example>
		/// <code>
		///   //on iGoogle (http://www.google.com/ig?hl=en)
		///   HTMLTable iGoogleSetup = Browser.This.CurrentPage.FindByID("DIV", "nhdrwrapsizer")["DIV", 5]["DIV", 2]["TABLE", 1] as HTMLTable;
		///   HTMLTr row2 = iGoogleSetup.Rows[1];
		///   string res = row2.Cell(0).InnerText;
		/// </code>
		/// </example>
		[Category("HTMLTable")]
		public HTMLTr [] Rows
		{
			
			get 
			{
				IHTMLElementCollection tblRows = ((IHTMLTable)htmlElement).rows;
				HTMLTr[] rowsArr = new HTMLTr[tblRows.length];
				int idx = 0;
				foreach (IHTMLElement row in tblRows)
				{
					rowsArr[idx++] = new HTMLTr(row);
				}
				return rowsArr ; 
			}
		}
		/// <summary>
		/// Retrieve a Row in the current Table (HTMLTr)
		/// </summary>
		/// <example>
		/// <code>
		///   //on iGoogle (http://www.google.com/ig?hl=en)
		///   HTMLTable iGoogleSetup = Browser.This.CurrentPage.FindByID("DIV", "nhdrwrapsizer")["DIV", 5]["DIV", 2]["TABLE", 1] as HTMLTable;
		///   HTMLTr row2 = iGoogleSetup.row(1);
		///   string res = row2.Cell(0).InnerText;
		/// </code>
		/// </example>
		public HTMLTr Row(int index)
		{
			IHTMLElementCollection tblRows = ((IHTMLTable)htmlElement).rows;
			return new HTMLTr((IHTMLElement)tblRows.item(null, index));
		}

	   
	}
}
