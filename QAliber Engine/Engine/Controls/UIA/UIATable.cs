using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using ManagedWinapi.Accessibility;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIATable class represents a table in a user-interface under windows OS.
	/// </summary>
	public class UIATable : UIAControl, ITable
	{
		/// <summary>
		/// Ctor to initiate a UIATable wrapper to the UI automation Table control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Table element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIATable(AutomationElement element)
			: base(element)
		{

		}

		#region ITable Members
		/// <summary>
		/// Retrive the number of columns in current UIATable control.
		/// </summary>
		/// <example>
		/// <code>
		///   //Run WMP 11 on windows xp - should open in library
		///   System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("wmplayer");
		///   UIAWindow wmpWin = Desktop.UIA[@"Windows Media Player", @"WMPlayerApp", @"UIAWindow"] as UIAWindow;
		///   UIATable primaryListViewTbl = wmpWin[@"Windows Media Player", @"WMPAppHost", @"UIAPane"][@"", @"WMP Skin Host", @"UIAPane"][@"LibraryContainer", @"ATL:131D5748", @"UIAPane"][@"PrimaryListView", @"SysListView32", @"UIATable"] as UIATable;
		///   int numOfRows = primaryListViewTbl.Rows;
		///   int numOfCol = primaryListViewTbl.Columns;
		///   string titleName = primaryListViewTbl.GetCell(0, 2).Name;
		/// </code>
		/// </example>
		/// <returns>int number of columns</returns>
		[Category("Table")]
		public int Columns
		{
			get { return PatternsExecutor.TableGetColumnCount(automationElement); }
		}
		/// <summary>
		/// Retrive the number of rows in current UIATable control.
		/// </summary>
		/// <example>
		/// <code>
		///   //Run WMP 11 on windows xp - should open in library
		///   System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("wmplayer");
		///   UIAWindow wmpWin = Desktop.UIA[@"Windows Media Player", @"WMPlayerApp", @"UIAWindow"] as UIAWindow;
		///   UIATable primaryListViewTbl = wmpWin[@"Windows Media Player", @"WMPAppHost", @"UIAPane"][@"", @"WMP Skin Host", @"UIAPane"][@"LibraryContainer", @"ATL:131D5748", @"UIAPane"][@"PrimaryListView", @"SysListView32", @"UIATable"] as UIATable;
		///   int numOfRows = primaryListViewTbl.Rows;
		///   int numOfCol = primaryListViewTbl.Columns;
		///   string titleName = primaryListViewTbl.GetCell(0, 2).Name;
		/// </code>
		/// </example>
		/// <returns>int number of rows</returns>
		[Category("Table")]
		public int Rows
		{
			get { return PatternsExecutor.TableGetRowCount(automationElement); }
		}
		/// <summary>
		/// Retrive the a cell (UIControl) from the current UIATable control.
		/// </summary>
		/// <example>
		/// <code>
		///   //Run WMP 11 on windows xp - should open in library
		///   System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("wmplayer");
		///   UIAWindow wmpWin = Desktop.UIA[@"Windows Media Player", @"WMPlayerApp", @"UIAWindow"] as UIAWindow;
		///   UIATable primaryListViewTbl = wmpWin[@"Windows Media Player", @"WMPAppHost", @"UIAPane"][@"", @"WMP Skin Host", @"UIAPane"][@"LibraryContainer", @"ATL:131D5748", @"UIAPane"][@"PrimaryListView", @"SysListView32", @"UIATable"] as UIATable;
		///   int numOfRows = primaryListViewTbl.Rows;
		///   int numOfCol = primaryListViewTbl.Columns;
		///   string titleName = primaryListViewTbl.GetCell(0, 2).Name;
		/// </code>
		/// </example>
		/// <param name="row">the row index in the table first row is 0</param>
		/// <param name="column">The column index in the table, first column is 0</param>
		/// <returns>UIControl if cell found, null if no cell in provided row/cell</returns>
		public UIAControl GetCell(int row, int column)
		{
			return PatternsExecutor.TableGetCell(automationElement, row, column);
		}

		#endregion
	}
}
