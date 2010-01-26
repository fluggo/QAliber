using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents HTML element CENTER or BLOCK tags.
	/// No additional functionality or information to WebControl
	/// </summary>
	public class HTMLBlock : WebControl
	{
		public HTMLBlock(IHTMLElement element)
			: base(element)
		{ }
	}
}
