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
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Controls.WPF;
using QAliber.Engine.Controls.Watin;
using QAliber.Engine.Win32;
using QAliber.Logger;

namespace QAliber.Engine.Controls
{
	/// <summary>
	/// Retrive the Desktop single object for the entire control tree (Singleton)
	/// </summary>
	public class Desktop
	{
		/// <summary>
		/// Maps the desktop as UIAutomation
		/// </summary>
		public static UIARoot UIA
		{
			get
			{
				if (uiaRoot == null)
					uiaRoot = new UIARoot();
				return uiaRoot;
			}
		}

		/// <summary>
		/// Maps the desktop as internet explorer, only web pages will be visible under it's sub tree
		/// </summary>
		public static WebRoot Web
		{
			get
			{
				if (webRoot == null)
					webRoot = new WebRoot();
				return webRoot;
			}
		}

		/// <summary>
		/// Maps the desktop as a father for all WPF applications, only WPF processes will be visible under it's sub tree
		/// </summary>
		public static WPFRoot WPF
		{
			get
			{
				if (wpfRoot == null)
					wpfRoot = new WPFRoot();
				return wpfRoot;
			}
		}

		public static WatinRoot Watin
		{
			get
			{
				if (watinRoot == null)
					watinRoot = new WatinRoot();
				return watinRoot;
			}
		}

		/// <summary>
		/// Presses on the specifed button, at the current location and does not release it
		/// </summary>
		/// <param name="button">The button to press</param>
		public static void MouseDown(MouseButtons button)
		{
			Log.Default.Info(string.Format("Pressing the mouse down for button {0}", button), "", EntryVerbosity.Internal);
			switch (button)
			{
				case MouseButtons.Left:
					LowLevelInput.InjectLowMouseInput(MouseEvents.LEFTDOWN, 
						new Point(Cursor.Position.X, Cursor.Position.Y));
					break;
				case MouseButtons.Right:
					LowLevelInput.InjectLowMouseInput(MouseEvents.RIGHTDOWN,
					   new Point(Cursor.Position.X, Cursor.Position.Y));
					break;
				case MouseButtons.Middle:
					LowLevelInput.InjectLowMouseInput(MouseEvents.MIDDLEDOWN,
					   new Point(Cursor.Position.X, Cursor.Position.Y));
					break;
				case MouseButtons.None:
				case MouseButtons.XButton1:
				case MouseButtons.XButton2:
					break;
				
			}
			
		}

		/// <summary>
		/// Release a pressed button
		/// </summary>
		/// <param name="button">The button to release</param>
		public static void MouseUp(MouseButtons button)
		{
			Log.Default.Info(string.Format("Releasing the mouse for button {0}", button), "", EntryVerbosity.Internal);
			switch (button)
			{
				case MouseButtons.Left:
					LowLevelInput.InjectLowMouseInput(MouseEvents.LEFTUP,
						new Point(Cursor.Position.X, Cursor.Position.Y));
					break;
				case MouseButtons.Right:
					LowLevelInput.InjectLowMouseInput(MouseEvents.RIGHTUP,
					   new Point(Cursor.Position.X, Cursor.Position.Y));
					break;
				case MouseButtons.Middle:
					LowLevelInput.InjectLowMouseInput(MouseEvents.MIDDLEUP,
					   new Point(Cursor.Position.X, Cursor.Position.Y));
					break;
				case MouseButtons.None:
				case MouseButtons.XButton1:
				case MouseButtons.XButton2:
					break;

			}
		}


		private static WPFRoot wpfRoot = null;
		private static UIARoot uiaRoot = null;
		private static WebRoot webRoot = null;
		private static WatinRoot watinRoot = null;
	}
}

