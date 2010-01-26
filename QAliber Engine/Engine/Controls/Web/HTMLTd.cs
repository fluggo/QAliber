using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.Threading;
using System.ComponentModel;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represent TD HTML tag.
	/// TD elements are thml table cells resides under table -> row
	/// No additional functionality or information to WebControl.
	/// Usually you'll be intersted in the control inside or the InnerText property
	/// </summary>
	[DisplayName("Cell")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class HTMLTd : WebControl
	{
		public HTMLTd(IHTMLElement element)
			: base(element)
		{

		}
		//[Category("HTMLTd")]
		//public override string InnerText
		//{
		//	  get { return htmlElement.innerHTML; }
		//}

	}
}
