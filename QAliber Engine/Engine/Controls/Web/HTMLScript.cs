using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents a SCRIPT HTML tag.
	/// This is not a GUI element and does not require interaction.
	/// </summary>
	public class HTMLScript : WebControl
	{
		public HTMLScript(IHTMLElement element) : base(element) { }
	}
}
