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
namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Reperesents The li tag, a list item.
	///  Found under both ordered (ol) and unordered (ul) lists.
	///<example>
	/// <code>
	///		 //ON yahoo homepage
	///		HTMLOl searchList = Browser.This.CurrentPage.FindByID("DIV", "default-p_17416020_bea-bd")["DIV", 1]["DIV", 2]["OL", 2] as HTMLOl;
	///		HTMLLi litsItem = null;
	///		foreach (UIControl item in searchList.Children)
	///		{
	///			if (((WebControl)item).InnerText == "NASA")
	///				 litsItem = item as HTMLLi;
	///		}
	///		if (litsItem != null)
	///			litsItem.Click();
	/// </code>
	/// </example>
	/// </summary>
	public class HTMLLi : WebControl
	{
		public HTMLLi(IHTMLElement element) : base(element) { }
	   
	}
}
