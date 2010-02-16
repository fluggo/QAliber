/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
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
