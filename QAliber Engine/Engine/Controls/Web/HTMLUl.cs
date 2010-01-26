using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	///  Represents HTML element UL tags.
	///  The ul tag is used to create an ordered numerical or alphabetical lists.
	///  No additional functionality over WebControl is added here.
	/// </summary>
	public class HTMLUl: WebControl
	{
		public HTMLUl(IHTMLElement element) : base(element) { }
	}
}
