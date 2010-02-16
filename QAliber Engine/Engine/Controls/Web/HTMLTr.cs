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
	/// Represents TR HTML tag control.
	/// This is a row element in the table.
	/// A child controll will usually be HTMLTd (cell)
	/// </summary>
	[DisplayName("Row")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class HTMLTr : WebControl
	{
		public HTMLTr(IHTMLElement element)
			: base(element)
		{ }
		/// <summary>
		/// Retrieve the current row index in the table it resides in.
		/// 1stt row Index is 1.
		/// </summary>
		/// <example>
		/// <code>
		///   int index = ((HTMLTr)Browser.This.CurrentPage.FindByID("TABLE", "gaia_table")["TBODY", 1]["TR", 3]).Index;
		/// </code>
		/// </example>
		/// <returns> int the row Index, first row is Index 1</returns>
		[Category("HTMLRow")]
		[DisplayName("Row Index")]
		public int RowIndex
		{
			get
			{
				return ((IHTMLTableRow)htmlElement).rowIndex;
			}
		}
		/// <summary>
		/// Retrieve the number of cells (HTMLTd) in the row.
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLTd cell;
		///    HTMLTr mailRow = Browser.This.CurrentPage.FindByID("TABLE", "gaia_table")["TBODY", 1]["TR", 3] as HTMLTr;
		///    for (int idx = 0; idx &lt; mailRow.NumberOfCells; idx++)
		///    {
		///		   if (mailRow.Cell(idx).InnerText.Contains("Email"))
		///		   {
		///
		///		   }
		///    }
		/// </code>
		/// </example>
		[Category("HTMLRow")]
		[DisplayName("Cells Count")]
		public int NumberOfCells
		{
			get
			{
				return ((IHTMLTableRow)htmlElement).cells.length;
			}
		}
		/// <summary>
		/// Retrieve a Cell by Index in the row
		/// </summary>
		/// <param name="index"> The cell index as it appear in the Index property of the HTMLTd element</param>
		/// <example>
		/// <code>
		///    HTMLTd cell;
		///    HTMLTr mailRow = Browser.This.CurrentPage.FindByID("TABLE", "gaia_table")["TBODY", 1]["TR", 3] as HTMLTr;
		///    for (int idx = 0; idx &lt; mailRow.NumberOfCells; idx++)
		///    {
		///		   if (mailRow.Cell(idx).InnerText.Contains("Email"))
		///		   {
		///
		///		   }
		///    }
		/// </code>
		/// </example> 
		/// <returns>HTMlTd with requested index, or null if index not found</returns>
		public HTMLTd Cell(int index)
		{
			if (index <= NumberOfCells)
				return new HTMLTd((IHTMLElement)((IHTMLTableRow)htmlElement).cells.item(null, index));

			else
				return null;
		}

		/// <summary>
		/// Retrieve the hosted HTMLTable for this row
		/// </summary>
		/// <example>
		/// <code>
		///    //On google sign in
		///   HTMLTd cell;
		///   HTMLTr mailRow = Browser.This.CurrentPage.FindByID("TABLE", "gaia_table")["TBODY", 1]["TR", 3] as HTMLTr;
		///   HTMLTable signInTable = mailRow.FindHostTable();
		///   int numOfRow = signInTable.Rows.Length;
		/// </code>
		/// </example>
		/// <returns>HTMLTable</returns>
		   
		public HTMLTable FindHostTable()
		{
			IHTMLElement curParrent = htmlElement.parentElement;

			while (! (curParrent is IHTMLTable) )
				curParrent = curParrent.parentElement;

			return (HTMLTable)WebControl.GetControlByType(curParrent);
		}

		#region props
		/// <summary>
		/// Retrieve the HTMLTd which reside inside this row
		/// </summary>
		/// <example>
		/// <code>
		///   //On google sign in
		///   HTMLTr mailRow = Browser.This.CurrentPage.FindByID("TABLE", "gaia_table")["TBODY", 1]["TR", 3] as HTMLTr;
		///   HTMLTd [] cells = mailRow.Cells;
		/// </code>
		/// </example>
		[Category("HTMLRow")]
		public HTMLTd[] Cells
		{
			get
			{
				HTMLTd[] cellArr = new HTMLTd[NumberOfCells];
				for (int idx = 0; idx < cellArr.Length; idx++)
					cellArr[idx] = Cell(idx);

				return cellArr;
			}
		}
		#endregion
		}
}
