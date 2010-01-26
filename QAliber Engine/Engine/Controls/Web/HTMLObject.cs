using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents HTML element OBJECT tags.
	/// OBJECT is used to embed activeX,Flash ,Images and more into
	/// web page. No additional functionality over WebControl is added here.
	/// </summary>
	public class HTMLObject : WebControl
	{
		public HTMLObject(IHTMLElement element) : base(element) 
		{
			
		}

	}
}
