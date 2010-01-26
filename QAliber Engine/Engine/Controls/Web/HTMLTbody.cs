using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents TBODY HTML tag. 
	/// TBody contains the table rows.
	/// but has not effect GUI and has no additional functionality or information to WebControl
	/// </summary>
	public class HTMLTbody : WebControl
	{
		public HTMLTbody(IHTMLElement element)
			: base(element)
		{ }
	}
}
