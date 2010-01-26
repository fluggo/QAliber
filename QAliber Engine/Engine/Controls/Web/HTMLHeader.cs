using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.ComponentModel;
namespace QAliber.Engine.Controls.Web
{
	public class HTMLHeader : WebControl
	{
		public HTMLHeader(IHTMLElement element)
			: base(element)
		{
		}

		[Category("HTMLHeader properties:")]
		public string Align
		{
			get
			{
			   return ((IHTMLHeaderElement)htmlElement).align;
			}

			set
			{
				((IHTMLHeaderElement)htmlElement).align = value;
			}
		}
	}
}
