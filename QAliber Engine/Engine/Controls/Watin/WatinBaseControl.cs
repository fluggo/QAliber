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
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows;
using QAliber.Engine.Controls;
using System.Threading;
using System.Windows.Automation;
using WatiN.Core;

namespace QAliber.Engine.Controls.Watin
{
	/// <summary>
	/// WatinBaseControl is not a real control. Watin is mapping all controls inside categories for quick access 
	/// (Buttons,Divs,Formes etc') this class describes this hirarchy.
	/// Note: the watin plugin in QAliber, unlike Web or UIA, does not provide access to real controls, but instead provide
	/// a bridge to identify WatiN.Core elements using QAliber and provide easy mapping of the controls through our interface.
	/// The actual interaction is done using WatiN.Core engine.
	/// </summary>
	public class WatinBaseControl : UIControlBase
	{
		#region Constructors
		public WatinBaseControl(WatBrowser browser, WatinBaseTypes type)
		{
			parent = browser;
			this.type = type;
		}

		public WatinBaseControl(WatinControl elementWithChilds, WatinBaseTypes type)
		{
			parent = elementWithChilds;
			this.type = type;
		}

		public WatinBaseControl(WatFrame frameWithChilds, WatinBaseTypes type)
		{
			parent = frameWithChilds;
			this.type = type;
		}
		
		#endregion
		
		public override string CodePath
		{
			get
			{
				return parent.CodePath +"." + type ;
			}
		}
		/// <summary>
		///Describe the group type: Buttons,Divs,etc'
		/// </summary>
		public override string Name
		{
			get
			{
				return type.ToString();
			}
		}
		/// <summary>
		/// Not a real control to show here, simply call the browser focus
		/// </summary>
		public override void SetFocus()
		{
			parent.SetFocus();
		}
		
		public override List<UIControlBase> Children
		{
			get
			{
				if (children == null)
				{
					if (parent is WatBrowser)
						children = GetBrowserChilds();
					if (parent is WatinControl)
						children = GetElementChilds();
					else if (parent is WatFrame)
						children = GetFrameChilds();
				}
				return children;
			}
		}
		/// <summary>
		/// Not a real control, but need to return layout so qaliber will display it in the control browser tree
		/// to allow acess to the children (if has children return 0 value rect so it wont be ignored in controls tree)
		/// </summary>
		public override Rect Layout
		{
			get
			{
				if (Children.Count == 0)
					return Rect.Empty;

				return new Rect(0, 0, 0, 0);
			}
		}

		public override UIControlBase Parent
		{
			get
			{
				return base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}


		public override string UIType
		{
			get
			{
				return type.ToString();
			}
		}

		private List<UIControlBase> GetBrowserChilds()
		{
			children = new List<UIControlBase>();
					int idx = 0;
					switch (type)
					{
						case WatinBaseTypes.Areas:
							foreach (Area element in ((WatBrowser) parent).BrowserPage.Areas)
							{
							   
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;

						case WatinBaseTypes.Buttons:
							foreach (WatiN.Core.Button element in ((WatBrowser)parent).BrowserPage.Buttons)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children; ;
						case WatinBaseTypes.CheckBoxes:
							foreach (WatiN.Core.CheckBox element in ((WatBrowser)parent).BrowserPage.CheckBoxes)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Divs:
							foreach (WatiN.Core.Div element in ((WatBrowser)parent).BrowserPage.Divs)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Forms:
							foreach (WatiN.Core.Form element in ((WatBrowser)parent).BrowserPage.Forms)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Frames:
							foreach (WatiN.Core.Frame element in ((WatBrowser)parent).BrowserPage.Frames)
							{
								children.Add(new WatFrame(this, element, idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Images:
							foreach (WatiN.Core.Image element in ((WatBrowser)parent).BrowserPage.Images)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Labels:
							foreach (WatiN.Core.Label element in ((WatBrowser)parent).BrowserPage.Labels)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Links:
							foreach (WatiN.Core.Link element in ((WatBrowser)parent).BrowserPage.Links)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Paras:
							foreach (WatiN.Core.Para element in ((WatBrowser)parent).BrowserPage.Paras)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.RadioButtons:
							foreach (WatiN.Core.RadioButton element in ((WatBrowser)parent).BrowserPage.RadioButtons)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.SelectLists:
							foreach (WatiN.Core.SelectList element in ((WatBrowser)parent).BrowserPage.SelectLists)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Spans:
							foreach (WatiN.Core.Span element in ((WatBrowser)parent).BrowserPage.Spans)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.TableBodies:
							foreach (WatiN.Core.TableBody element in ((WatBrowser)parent).BrowserPage.TableBodies)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.TableCells:
							foreach (WatiN.Core.TableCell element in ((WatBrowser)parent).BrowserPage.TableCells)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.TableRows:
							foreach (WatiN.Core.TableRow element in ((WatBrowser)parent).BrowserPage.TableRows)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.Tables:
							foreach (WatiN.Core.Table element in ((WatBrowser)parent).BrowserPage.Tables)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						case WatinBaseTypes.TextFields:
							foreach (WatiN.Core.TextField element in ((WatBrowser)parent).BrowserPage.TextFields)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children;
						default:
							return null;
					}
		}

		private List<UIControlBase> GetElementChilds()
		{
			children = new List<UIControlBase>();
			int idx = 0;
			switch (type)
			{
				case WatinBaseTypes.Areas:
					foreach (Area element in ((WatinControl)parent).WatinElement.DomContainer.Areas)
					{

						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;

				case WatinBaseTypes.Buttons:
					foreach (WatiN.Core.Button element in ((WatinControl)parent).WatinElement.DomContainer.Buttons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children; ;
				case WatinBaseTypes.CheckBoxes:
					foreach (WatiN.Core.CheckBox element in ((WatinControl)parent).WatinElement.DomContainer.CheckBoxes)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Divs:
					foreach (WatiN.Core.Div element in ((WatinControl)parent).WatinElement.DomContainer.Divs)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Forms:
					foreach (WatiN.Core.Form element in ((WatinControl)parent).WatinElement.DomContainer.Forms)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;

				case WatinBaseTypes.Images:
					foreach (WatiN.Core.Image element in ((WatinControl)parent).WatinElement.DomContainer.Images)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Labels:
					foreach (WatiN.Core.Label element in ((WatinControl)parent).WatinElement.DomContainer.Labels)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Links:
					foreach (WatiN.Core.Link element in ((WatinControl)parent).WatinElement.DomContainer.Links)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Paras:
					foreach (WatiN.Core.Para element in ((WatinControl)parent).WatinElement.DomContainer.Paras)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.RadioButtons:
					foreach (WatiN.Core.RadioButton element in ((WatinControl)parent).WatinElement.DomContainer.RadioButtons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.SelectLists:
					foreach (WatiN.Core.SelectList element in ((WatinControl)parent).WatinElement.DomContainer.SelectLists)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Spans:
					foreach (WatiN.Core.Span element in ((WatinControl)parent).WatinElement.DomContainer.Spans)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TableBodies:
					foreach (WatiN.Core.TableBody element in ((Table)((WatinControl)parent).WatinElement).TableBodies)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TableCells:
					foreach (WatiN.Core.TableCell element in ((TableRow)((WatinControl)parent).WatinElement).TableCells)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TableRows:
					foreach (WatiN.Core.TableRow element in ((Table)((WatinControl)parent).WatinElement).TableRows)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Tables:
					foreach (WatiN.Core.Table element in ((Table)((WatinControl)parent).WatinElement).Tables)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TextFields:
					foreach (WatiN.Core.TextField element in ((WatinControl)parent).WatinElement.DomContainer.TextFields)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				default:
					return null;
			}
		}

		private List<UIControlBase> GetFrameChilds()
		{
			children = new List<UIControlBase>();
			int idx = 0;
			switch (type)
			{
				case WatinBaseTypes.Areas:
					foreach (Area element in ((WatFrame)parent).DocElement.Areas)
					{

						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;

				case WatinBaseTypes.Buttons:
					foreach (WatiN.Core.Button element in ((WatFrame)parent).DocElement.Buttons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children; ;
				case WatinBaseTypes.CheckBoxes:
					foreach (WatiN.Core.CheckBox element in ((WatFrame)parent).DocElement.CheckBoxes)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Divs:
					foreach (WatiN.Core.Div element in ((WatFrame)parent).DocElement.Divs)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Forms:
					foreach (WatiN.Core.Form element in ((WatFrame)parent).DocElement.Forms)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Frames:
					foreach (WatiN.Core.Frame element in ((WatFrame)parent).DocElement.Frames)
					{
						children.Add(new WatFrame(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Images:
					foreach (WatiN.Core.Image element in ((WatFrame)parent).DocElement.Images)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Labels:
					foreach (WatiN.Core.Label element in ((WatFrame)parent).DocElement.Labels)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Links:
					foreach (WatiN.Core.Link element in ((WatFrame)parent).DocElement.Links)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Paras:
					foreach (WatiN.Core.Para element in ((WatFrame)parent).DocElement.Paras)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.RadioButtons:
					foreach (WatiN.Core.RadioButton element in ((WatFrame)parent).DocElement.RadioButtons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.SelectLists:
					foreach (WatiN.Core.SelectList element in ((WatFrame)parent).DocElement.SelectLists)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Spans:
					foreach (WatiN.Core.Span element in ((WatFrame)parent).DocElement.Spans)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TableBodies:
					foreach (WatiN.Core.TableBody element in ((WatFrame)parent).DocElement.TableBodies)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TableCells:
					foreach (WatiN.Core.TableCell element in ((WatFrame)parent).DocElement.TableCells)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TableRows:
					foreach (WatiN.Core.TableRow element in ((WatFrame)parent).DocElement.TableRows)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.Tables:
					foreach (WatiN.Core.Table element in ((WatFrame)parent).DocElement.Tables)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				case WatinBaseTypes.TextFields:
					foreach (WatiN.Core.TextField element in ((WatFrame)parent).DocElement.TextFields)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children;
				default:
					return null;
			}
		}

		public WatinBaseTypes BaseType
		{
			get { return type; }
			set { type = value; }
		}

		private WatinBaseTypes type;

	   
	}
}
