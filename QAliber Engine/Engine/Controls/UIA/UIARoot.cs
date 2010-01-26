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
		internal UIARoot() : base(AutomationElement.RootElement)
		{
			
		}

		public override string CodePath
		{
			get
			{
				return "Desktop.UIA";
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
