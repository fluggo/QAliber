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
		UIControlBase _parent;

		#region Constructors
		public WatinBaseControl(WatBrowser browser, WatinBaseTypes type)
		{
			_parent = browser;
			this.type = type;
		}

		public WatinBaseControl(WatinControl elementWithChilds, WatinBaseTypes type)
		{
			_parent = elementWithChilds;
			this.type = type;
		}

		public WatinBaseControl(WatFrame frameWithChilds, WatinBaseTypes type)
		{
			_parent = frameWithChilds;
			this.type = type;
		}
		
		#endregion
		
		public override string CodePath
		{
			get
			{
				return Parent.CodePath + "." + type;
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
			Parent.SetFocus();
		}

		public override UIControlBase[] GetChildren() {
			if (Parent is WatBrowser)
				return GetBrowserChilds();
			if (Parent is WatinControl)
				return GetElementChilds();
			else if (Parent is WatFrame)
				return GetFrameChilds();

			return new UIControlBase[0];
		}

		/// <summary>
		/// Not a real control, but need to return layout so qaliber will display it in the control browser tree
		/// to allow acess to the children (if has children return 0 value rect so it wont be ignored in controls tree)
		/// </summary>
		public override Rect Layout
		{
			get
			{
				if (GetChildren().Length == 0)
					return Rect.Empty;

				return new Rect(0, 0, 0, 0);
			}
		}

		public override string UIType
		{
			get
			{
				return type.ToString();
			}
		}

		private UIControlBase[] GetBrowserChilds()
		{
			List<UIControlBase> children = new List<UIControlBase>();
					int idx = 0;
					switch (type)
					{
						case WatinBaseTypes.Areas:
							foreach (Area element in ((WatBrowser) Parent).BrowserPage.Areas)
							{
							   
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();

						case WatinBaseTypes.Buttons:
							foreach (WatiN.Core.Button element in ((WatBrowser)Parent).BrowserPage.Buttons)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.CheckBoxes:
							foreach (WatiN.Core.CheckBox element in ((WatBrowser)Parent).BrowserPage.CheckBoxes)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Divs:
							foreach (WatiN.Core.Div element in ((WatBrowser)Parent).BrowserPage.Divs)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Forms:
							foreach (WatiN.Core.Form element in ((WatBrowser)Parent).BrowserPage.Forms)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Frames:
							foreach (WatiN.Core.Frame element in ((WatBrowser)Parent).BrowserPage.Frames)
							{
								children.Add(new WatFrame(this, element, idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Images:
							foreach (WatiN.Core.Image element in ((WatBrowser)Parent).BrowserPage.Images)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Labels:
							foreach (WatiN.Core.Label element in ((WatBrowser)Parent).BrowserPage.Labels)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Links:
							foreach (WatiN.Core.Link element in ((WatBrowser)Parent).BrowserPage.Links)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Paras:
							foreach (WatiN.Core.Para element in ((WatBrowser)Parent).BrowserPage.Paras)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.RadioButtons:
							foreach (WatiN.Core.RadioButton element in ((WatBrowser)Parent).BrowserPage.RadioButtons)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.SelectLists:
							foreach (WatiN.Core.SelectList element in ((WatBrowser)Parent).BrowserPage.SelectLists)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Spans:
							foreach (WatiN.Core.Span element in ((WatBrowser)Parent).BrowserPage.Spans)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.TableBodies:
							foreach (WatiN.Core.TableBody element in ((WatBrowser)Parent).BrowserPage.TableBodies)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.TableCells:
							foreach (WatiN.Core.TableCell element in ((WatBrowser)Parent).BrowserPage.TableCells)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.TableRows:
							foreach (WatiN.Core.TableRow element in ((WatBrowser)Parent).BrowserPage.TableRows)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.Tables:
							foreach (WatiN.Core.Table element in ((WatBrowser)Parent).BrowserPage.Tables)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						case WatinBaseTypes.TextFields:
							foreach (WatiN.Core.TextField element in ((WatBrowser)Parent).BrowserPage.TextFields)
							{
								children.Add(new WatinControl(this,element,idx));
								idx++;
							}
							return children.ToArray();
						default:
							return null;
					}
		}

		private UIControlBase[] GetElementChilds()
		{
			List<UIControlBase> children = new List<UIControlBase>();
			int idx = 0;
			switch (type)
			{
				case WatinBaseTypes.Areas:
					foreach (Area element in ((WatinControl)Parent).WatinElement.DomContainer.Areas)
					{

						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();

				case WatinBaseTypes.Buttons:
					foreach (WatiN.Core.Button element in ((WatinControl)Parent).WatinElement.DomContainer.Buttons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.CheckBoxes:
					foreach (WatiN.Core.CheckBox element in ((WatinControl)Parent).WatinElement.DomContainer.CheckBoxes)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Divs:
					foreach (WatiN.Core.Div element in ((WatinControl)Parent).WatinElement.DomContainer.Divs)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Forms:
					foreach (WatiN.Core.Form element in ((WatinControl)Parent).WatinElement.DomContainer.Forms)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();

				case WatinBaseTypes.Images:
					foreach (WatiN.Core.Image element in ((WatinControl)Parent).WatinElement.DomContainer.Images)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Labels:
					foreach (WatiN.Core.Label element in ((WatinControl)Parent).WatinElement.DomContainer.Labels)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Links:
					foreach (WatiN.Core.Link element in ((WatinControl)Parent).WatinElement.DomContainer.Links)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Paras:
					foreach (WatiN.Core.Para element in ((WatinControl)Parent).WatinElement.DomContainer.Paras)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.RadioButtons:
					foreach (WatiN.Core.RadioButton element in ((WatinControl)Parent).WatinElement.DomContainer.RadioButtons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.SelectLists:
					foreach (WatiN.Core.SelectList element in ((WatinControl)Parent).WatinElement.DomContainer.SelectLists)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Spans:
					foreach (WatiN.Core.Span element in ((WatinControl)Parent).WatinElement.DomContainer.Spans)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TableBodies:
					foreach (WatiN.Core.TableBody element in ((Table)((WatinControl)Parent).WatinElement).TableBodies)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TableCells:
					foreach (WatiN.Core.TableCell element in ((TableRow)((WatinControl)Parent).WatinElement).TableCells)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TableRows:
					foreach (WatiN.Core.TableRow element in ((Table)((WatinControl)Parent).WatinElement).TableRows)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Tables:
					foreach (WatiN.Core.Table element in ((Table)((WatinControl)Parent).WatinElement).Tables)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TextFields:
					foreach (WatiN.Core.TextField element in ((WatinControl)Parent).WatinElement.DomContainer.TextFields)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				default:
					return null;
			}
		}

		private UIControlBase[] GetFrameChilds()
		{
			List<UIControlBase> children = new List<UIControlBase>();
			int idx = 0;
			switch (type)
			{
				case WatinBaseTypes.Areas:
					foreach (Area element in ((WatFrame)Parent).DocElement.Areas)
					{

						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();

				case WatinBaseTypes.Buttons:
					foreach (WatiN.Core.Button element in ((WatFrame)Parent).DocElement.Buttons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.CheckBoxes:
					foreach (WatiN.Core.CheckBox element in ((WatFrame)Parent).DocElement.CheckBoxes)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Divs:
					foreach (WatiN.Core.Div element in ((WatFrame)Parent).DocElement.Divs)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Forms:
					foreach (WatiN.Core.Form element in ((WatFrame)Parent).DocElement.Forms)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Frames:
					foreach (WatiN.Core.Frame element in ((WatFrame)Parent).DocElement.Frames)
					{
						children.Add(new WatFrame(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Images:
					foreach (WatiN.Core.Image element in ((WatFrame)Parent).DocElement.Images)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Labels:
					foreach (WatiN.Core.Label element in ((WatFrame)Parent).DocElement.Labels)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Links:
					foreach (WatiN.Core.Link element in ((WatFrame)Parent).DocElement.Links)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Paras:
					foreach (WatiN.Core.Para element in ((WatFrame)Parent).DocElement.Paras)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.RadioButtons:
					foreach (WatiN.Core.RadioButton element in ((WatFrame)Parent).DocElement.RadioButtons)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.SelectLists:
					foreach (WatiN.Core.SelectList element in ((WatFrame)Parent).DocElement.SelectLists)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Spans:
					foreach (WatiN.Core.Span element in ((WatFrame)Parent).DocElement.Spans)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TableBodies:
					foreach (WatiN.Core.TableBody element in ((WatFrame)Parent).DocElement.TableBodies)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TableCells:
					foreach (WatiN.Core.TableCell element in ((WatFrame)Parent).DocElement.TableCells)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TableRows:
					foreach (WatiN.Core.TableRow element in ((WatFrame)Parent).DocElement.TableRows)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.Tables:
					foreach (WatiN.Core.Table element in ((WatFrame)Parent).DocElement.Tables)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
				case WatinBaseTypes.TextFields:
					foreach (WatiN.Core.TextField element in ((WatFrame)Parent).DocElement.TextFields)
					{
						children.Add(new WatinControl(this, element, idx));
						idx++;
					}
					return children.ToArray();
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
