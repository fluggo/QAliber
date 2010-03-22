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
using WatiN.Core;
using QAliber.Engine.Controls.Web;
using System.Text.RegularExpressions;

namespace QAliber.Engine.Controls.Watin
{
	/// <summary>
	/// Represents the root element of Watin controls. This will hold all the open browsers and provide access to both
	/// FireFox and IE instances.
	/// </summary>
	public class WatinRoot :UIControlBase, IControlLocator
	{
	  
		internal WatinRoot()
			: base()
		{
		   
	  
		}
		/// <summary>
		/// Get Watin.Core.FireFox browser, retrieved by URL
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public FireFox FF(string url)
		{
			foreach ( UIControlBase b in Children)
			{
				if (((WatBrowser)b).BrowserType == browserType.FireFox && ((WatBrowser)b).Name == url)
					return (FireFox) ((WatBrowser)b).BrowserPage;
			}
			Logger.Log.Default.Error("No FireFox browser found,with provided title found","",QAliber.Logger.EntryVerbosity.Internal);
			return null;
		}

		/// <summary>
		/// Get Watin.Core.IE browser, retrieved by URL
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public IE IE(string url)
		{
			foreach (UIControlBase b in Children)
			{
				if (((WatBrowser)b).BrowserType == browserType.IE && ((WatBrowser)b).Name == url)
					return (IE)((WatBrowser)b).BrowserPage;
			}
			Logger.Log.Default.Error("No IE browser found,with provided URL found", "", QAliber.Logger.EntryVerbosity.Internal);
			return null;
		}

		/// <summary>
		/// Get Watin.Core.IE browser, retrieved by URL with regular expression.
		/// Works better when URLs are long, needs only partial match.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public IE IE(string url,bool useRegex)
		{
			Regex regex = new Regex(url.ToLower());
			foreach (UIControlBase b in Children)
			{
				if (((WatBrowser)b).BrowserType == browserType.IE && regex.IsMatch(((WatBrowser)b).Name.ToLower()))
					return (IE)((WatBrowser)b).BrowserPage;
			}
			Logger.Log.Default.Error("No IE browser found,with provided URL regex found", "", QAliber.Logger.EntryVerbosity.Internal);
			return null;
		}

		/// <summary>
		/// Get Watin.Core.IE browser, retrieved by index, the (Watin priority, we use WatiN.Core.IE.InternetExplorers() which retrives browsers by active priority
		/// , e.g. focused tab is 0 ,last access has the lowest index)
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public IE IE(int index)
		{
			int ieIdx = 0;
			int generalIdx = 0;

			while (ieIdx < children.Count && generalIdx < children.Count)
			{
				if (((WatBrowser)children[ieIdx]).BrowserType == browserType.IE)
				{
					if (ieIdx == index)
						return (IE)((WatBrowser)children[ieIdx]).BrowserPage;
					else
						ieIdx++;
				}
				generalIdx++;
			}
			Logger.Log.Default.Error("No IE browser found in the index", "", QAliber.Logger.EntryVerbosity.Internal);
			return null;
		}

		
		public override string CodePath
		{
			get
			{
				return "Desktop.Watin";
			}
		}

		public override List<UIControlBase> Children
		{
			get
			{
				if (children == null)
				{
					children = new List<UIControlBase>();
				   
					foreach (IE browser in WatiN.Core.IE.InternetExplorers())
					{
						if (browser.NativeDocument != null)
						{
							children.Add(new WatBrowser(browser));
						}
					}
					FireFox ffBrowser = null;
					try
					{
						 ffBrowser = FireFox.AttachTo<FireFox>(WatiN.Core.Constraints.AnyConstraint.Instance, 2);
					}
					catch { }
					if (ffBrowser != null)
						children.Add(new WatBrowser(ffBrowser));
				  
					
					
					return children;
				}

				return children;
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			if (children != null)
				children = null;
		}


		#region IControlLocator Members

		public UIControlBase GetControlFromCursor()
		{
			System.Windows.Point pt = new System.Windows.Point(System.Windows.Forms.Cursor.Position.X,
								System.Windows.Forms.Cursor.Position.Y);
			return GetControlFromPoint(pt);
		}

		public UIControlBase GetControlFromPoint(Point pt)
		{
			
			
				//watin handles return active browser with index = 0
				WatBrowser focusedBrowser = (WatBrowser)Children[0];
				//get the abs bounds relative to the page top left

				WatinControl element = focusedBrowser.GetElementFromPoint((int)pt.X - (int)focusedBrowser.Layout.Left, (int)pt.Y - (int)focusedBrowser.Layout.Top);
				if (element != null)
					return element;

				return null;
			
		}

		public UIControlBase GetFocusedElement()
		{
			return null;
		  
		}

		
		#endregion

	  


		List<UIControlBase> children;
		FireFox ffBrowser;
	}

	public delegate UIControlBase LocatorDelegate(Point pt);
}
