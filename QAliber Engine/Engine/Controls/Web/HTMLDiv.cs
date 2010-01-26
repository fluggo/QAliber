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
