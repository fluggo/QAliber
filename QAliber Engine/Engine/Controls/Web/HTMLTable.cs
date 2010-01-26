using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.ComponentModel;
using System.Threading;


namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represent TABLE HTML tag.
	/// A simple HTML table consists of the table element and one or more tr, th, and td elements.
	/// A more complex HTML table may also include caption, col, colgroup, thead, tfoot, and tbody elements.
	/// </summary>
	[DisplayName("Table")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class HTMLTable : WebControl
	{
		public HTMLTable(IHTMLElement element)
			: base(element)
		{
		  
		}
		/// <summary>
		/// Retrieve the Rows in the current Table (HTMLTr [] )
		/// </summary>
		/// <example>
		/// <code>
		///   //on iGoogle (http://www.google.com/ig?hl=en)
		///   HTMLTable iGoogleSetup = Browser.This.CurrentPage.FindByID("DIV", "nhdrwrapsizer")["DIV", 5]["DIV", 2]["TABLE", 1] as HTMLTable;
		///   HTMLTr row2 = iGoogleSetup.Rows[1];
		///   string res = row2.Cell(0).InnerText;
		/// </code>
		/// </example>
		[Category("HTMLTable")]
		public HTMLTr [] Rows
		{
			
			get 
			{
				IHTMLElementCollection tblRows = ((IHTMLTable)htmlElement).rows;
				HTMLTr[] rowsArr = new HTMLTr[tblRows.length];
				int idx = 0;
				foreach (IHTMLElement row in tblRows)
				{
					rowsArr[idx++] = new HTMLTr(row);
				}
				return rowsArr ; 
			}
		}
		/// <summary>
		/// Retrieve a Row in the current Table (HTMLTr)
		/// </summary>
		/// <example>
		/// <code>
		///   //on iGoogle (http://www.google.com/ig?hl=en)
		///   HTMLTable iGoogleSetup = Browser.This.CurrentPage.FindByID("DIV", "nhdrwrapsizer")["DIV", 5]["DIV", 2]["TABLE", 1] as HTMLTable;
		///   HTMLTr row2 = iGoogleSetup.row(1);
		///   string res = row2.Cell(0).InnerText;
		/// </code>
		/// </example>
		public HTMLTr Row(int index)
		{
			IHTMLElementCollection tblRows = ((IHTMLTable)htmlElement).rows;
			return new HTMLTr((IHTMLElement)tblRows.item(null, index));
		}

	   
	}
}
