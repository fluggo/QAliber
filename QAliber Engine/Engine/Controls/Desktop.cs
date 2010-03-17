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

using System.Windows.Automation;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Controls.WPF;
using QAliber.Engine.Controls.Watin;

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

		private static WPFRoot wpfRoot = null;
		private static UIARoot uiaRoot = null;
		private static WebRoot webRoot = null;
		private static WatinRoot watinRoot = null;
	}
}

