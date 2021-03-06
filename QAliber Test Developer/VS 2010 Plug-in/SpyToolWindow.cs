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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace QAliber.VS2005.Plugin
{
	/// <summary>
	/// This class implements the tool window exposed by this package and hosts a user control.
	///
	/// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
	/// usually implemented by the package implementer.
	///
	/// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
	/// implementation of the IVsWindowPane interface.
	/// </summary>
	[Guid("c1447f67-c464-4053-b6b3-19684aeadf62")]
	public class SpyToolWindow : ToolWindowPane
	{
		// This is the user control hosted by the tool window; it is exposed to the base class 
		// using the Window property. Note that, even if this class implements IDispose, we are
		// not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
		// the object returned by the Window property.
		internal SpyControl control;

		/// <summary>
		/// Standard constructor for the tool window.
		/// </summary>
		public SpyToolWindow()
			:
			base(null)
		{
			// Set the window title reading it from the resources.
			this.Caption = Resources.ToolWindowTitle;
			// Set the image that will appear on the tab of the window frame
			// when docked with an other window
			// The resource ID correspond to the one defined in the resx file
			// while the Index is the offset in the bitmap strip. Each image in
			// the strip being 16x16.
			this.BitmapResourceID = 300;
			this.BitmapIndex = 3;

			 
			control = new SpyControl(this);
		}

		/// <summary>
		/// This property returns the handle to the user control that should
		/// be hosted in the Tool Window.
		/// </summary>
		override public IWin32Window Window
		{
			get
			{
				return (IWin32Window)control;
			}
		}

		public object GetVsService(Type service)
		{
			return GetService(service);
		}

	}
}
