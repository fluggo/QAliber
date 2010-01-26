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
