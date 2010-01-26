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
