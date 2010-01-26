using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace Easy.Automation.WebControls
{
	public class HTMLStyle
	{
		public HTMLStyle(IHTMLElement element)
		{
			this.element = element;
		}


		IHTMLElement element;

		public IHTMLElement Element
		{
			get { return element; }
			set { element = value; }
		}
	}
}
