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
using QAliber.Engine.Controls.UIA;
using System.Windows.Automation;
using System.Windows;
using System.Runtime.InteropServices;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Represents the root element of the controls tree as seen by UI automation
	/// </summary>
	public class UIARoot : UIAControl, IControlLocator
	{
		internal UIARoot() : base( AutomationElement.RootElement.GetUpdatedCache( UIAControl.SearchCache ) ) {
		}

		public override string CodePath
		{
			get
			{
				return "code:Desktop.UIA";
			}
		}

		#region IControlLocator Members

		/// <summary>
		/// Get the UIControl from the point the cursor is on
		/// </summary>
		///<example>
		/// <code>
		///  Desktop.UIA.MoveMouseTo(new Point(300, 300));
		///  UIControl control = Desktop.UIA.GetControlFromCursor();
		///  string controlName = control.Name;
		/// </code>
		/// </example>
		/// <returns>UIControl from the mouse location</returns>
		public UIControlBase GetControlFromCursor()
		{
			try
			{
				AutomationElement element = AutomationElement.FromPoint(
					new Point(System.Windows.Forms.Cursor.Position.X,
							  System.Windows.Forms.Cursor.Position.Y));
				if (element != null)
				{
					return GetControlByType(element);
				}
			}
			catch { };
			return null;
		}
		/// <summary>
		/// Get the UIControl from any point on desktop
		/// </summary>
		/// <param name="pt">Point(x,y)-  x,y are pixels</param>
		/// <example>
		/// <code>
		///   UIControl control = Desktop.UIA.GetControlFromPoint(new Point(300, 300));
		///   string controlName = control.Name;
		/// </code>
		/// </example>
		/// <returns>UIControl from the specified point</returns>
		public UIControlBase GetControlFromPoint(Point pt)
		{
			try
			{
				AutomationElement element = AutomationElement.FromPoint(pt);
				if (element != null)
				{
					return GetControlByType(element);
				}
			}
			catch (COMException)
			{
				QAliber.Logger.Log.Default.Error("Could not recognize object at point", pt.ToString(), QAliber.Logger.EntryVerbosity.Internal);
			}
			catch (ElementNotAvailableException)
			{

			}
			return null;
		}

	   
		public UIControlBase GetFocusedElement()
		{
			return UIAControl.GetControlByType(AutomationElement.FocusedElement);
		}

		#endregion
	}
}
