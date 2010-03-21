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

//http://www.koders.com/csharp/fid36DB75029F9A8E6EDAD39C9C3B44F936A9F18A55.aspx?s=mdef%3Ainsert
namespace QAliber.Engine.Controls.Watin
{
  
	public class WatinControl : UIControlBase 
	{
		#region Constructors
		public WatinControl(WatinBaseControl parent, WatiN.Core.Element element,int idx)
		{
			this.parent = parent;
			if (element != null)
			{
				htmlElement = element;
				index = idx;
			}
		}
	   
		#endregion

		#region UIControlBase Method Overrides
		/// <summary>
		/// Set the focus for the html element
		/// </summary>
		public override void SetFocus()
		{
			try
			{
			  parent.SetFocus();
			  htmlElement.NativeElement.SetFocus();
			}
			catch
			{
			}
		}
		
		public override void Refresh()
		{
			base.Refresh();
			index = 0;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Get the index of the control in the watingBaseControl category (Divs,Butoons etc')
		/// The index is given by the BaseControl on this Ctor
		/// </summary>
		public override int Index
		{
			get
			{
			   return index;
			}
			set
			{
				index = value;
			}
		}

		public override string CodePath
		{
			get
			{
				if (codePath == string.Empty)
				{
					string prefix = String.Empty;

					if (this.ID != null)
					{
						prefix = parent.Parent.CodePath + "." + UIType + "(Find.ById(\"" + ID + "\"))";
						codePath = prefix;
					}
					else if (this.Name != null)
					{
						prefix = parent.Parent.CodePath + "." + UIType +"(Find.ByName(\"" + Name + "\"))";
						codePath = prefix;
					}
					else
					{
						prefix = parent.CodePath + "[" +Index + "]";
						codePath = prefix;
					}
				}
				return codePath;
			}
		}
		/// <summary>
		/// Retrive the control ID.
		/// </summary>
		/// <returns>string ID or empty string if the control has no ID </returns>
		public override string ID
		{
			get 
			{
				try
				{
					return htmlElement.Id;
				}
				catch
				{
					return "";
				}
			}
		}
		/// <summary>
		/// Retrieve the HTML type of the control (HTMLInput,HTMLForm,HTNLTd etc')
		/// </summary>
		/// <example>
		/// <code>
		///  //Lets search web control with the name q
		///   WebControl control = Browser.This.CurrentPage.FindByName("q");
		///   string type = control.UIType;
		///    //Since we not sure about q type
		///   if (type == "HTMLInput")
		///		  ((HTMLInput)control).Write("Search me");
		/// </code>
		/// </example>
		/// <returns>string Control Type as it reflect by object.GetType() </returns>
		public override string ClassName
		{
			get { return htmlElement.ClassName; }
		}
		/// <summary>
		/// Retriev the Name of the control, Since we create the parent watinBaseControl we add a "Dummy" (no htmlElement) child to show the + in the tree of the parent node
		/// This is replace with real control when the user access the child nodes, return the Name of control
		/// </summary>
		/// <returns>The name of the control or empty string if has no name</returns>
		public override string Name
		{
			get 
			{
				if (htmlElement == null)
					return "dummy";

			  return htmlElement.Name;
			}
		}
		/// <summary>
		/// Return the Type of the WatinControl (Div,Button,Form etc')
		/// </summary>
		public override string UIType
		{
			get
			{
				return htmlElement.GetType().Name;
			}
		}
		/// <summary>
		/// Show the control in the spy tree by using ID or Name or the InnerText if its short, else show the child index in the type group
		/// </summary>
		public override string VisibleName
		{
			get
			{
				if (!string.IsNullOrEmpty(ID))
					return ID;
				else if (!string.IsNullOrEmpty(Name))
					return Name;
				else if (!string.IsNullOrEmpty(InnerText) && InnerText.Length < 32 && InnerText.Length > 1)
					return InnerText ;
				else
					return htmlElement.TagName + "[" + index + "]";
			}
		}

	  
		/// <summary>
		/// Retrieve the HTML Tag name.
		/// </summary>
		[Category("Watin")]
		[DisplayName("Tag Name")]
		public string TagName
		{
			get
			{
				return htmlElement.TagName;
			}
		}
		/// <summary>
		/// Retrieve Rect layout of the control in the page and relative to the current Page.
		/// Layout is relevant only for IE, as we record and use desktop relatice highlight only for IE.
		/// the FireFox element will return unique Rect with negative size to tell the caller this is FireFox element
		/// and use Html / FireFox browser relation and not Desktop.
		/// </summary>
		/// <returns>System.Windows.Rect control layout</returns>
		public override Rect Layout
		{
			get
			{
				System.Drawing.Rectangle rect;
			  
				try
				{
					rect = htmlElement.NativeElement.GetAbsElementBounds();
				}
				catch (NotImplementedException e)//WatiN throw this for FireFox
				{
					return new Rect(-1111, 1, 0, 0);
				}
			  
				Rect res = new Rect(rect.Left,
					rect.Top, 
					rect.Right - rect.Left, 
					rect.Bottom - rect.Top);

				UIControlBase ances = parent.Parent;

				if (this.UIType.Contains("Table"))
				{
					while (!(ances is WatBrowser))
					{
						ances = ances.Parent;
					}
				}

				res.Offset(ances.Layout.Left, ances.Layout.Top);
				return res;
			}
		}
		/// <summary>
		/// Call Watin flash (used to highlight fireFox elements)
		/// </summary>
		/// <param name="times">number of flashes</param>
		public void Flash(int times)
		{
			parent.SetFocus();
			htmlElement.Focus();
			htmlElement.Flash(times);
		}

		/// <summary>
		/// Call Watin InnerText. (Retrieve the string between the open tag <X> to the close tag </X>).
		/// </summary>
		/// <returns>The text between the control tags (e.g.) <TD> </TD></returns>
		[Category("Watin")]
		public virtual string InnerText
		{
			get { return htmlElement.Text; }
		}
		
		/// <summary>
		/// Retrieve the parent WatinBaseControl 
		/// </summary>
		[Browsable(false)]
		public override UIControlBase Parent
		{
			get
			{
				return parent;
			}
		}


		public override List<UIControlBase> Children
		{
			get
			{
				if (children == null && ((WatinBaseControl )Parent).BaseType == WatinBaseTypes.Tables)
				{
					children = new List<UIControlBase>();
					children.Add(new WatinBaseControl(this, WatinBaseTypes.TableRows));
				}

				if (children == null && ((WatinBaseControl)Parent).BaseType == WatinBaseTypes.TableRows)
				{
					children = new List<UIControlBase>();
					children.Add(new WatinBaseControl(this, WatinBaseTypes.TableCells));
				}

				return children;
			}
		}

		/// <summary>
		/// Retrieve the style of the web control.
		/// </summary>
		[Category("Watin")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public WatiN.Core.Style Style
		{
			get {return htmlElement.Style; }
		}
		/// <summary>
		/// Call Watin Enable (Verify if the control is enabled, allow intercation).
		/// </summary>
		/// <returns>true if the control enabled, false if not</returns>
		public override bool Enabled
		{
			get
			{
				try
				{
					return htmlElement.Enabled;
				}
				catch
				{
					return false;
				}
			}
		}
		/// <summary>
		/// Verify if the control is visible.
		/// </summary>
		/// <returns>true if the control visible, false if not</returns>
		public override bool Visible
		{
			get
			{
				try
				{
					return htmlElement.Style.Display != "hidden";
				}
				catch
				{
					return false;
				}
			}
		}
			   

		internal Element WatinElement
		{
			get { return htmlElement; }
			set { htmlElement = value; }
		}
		

		#endregion
	   
		#region Private Fields
		private Element htmlElement;
		private WatinBaseControl child;
		#endregion

	   
	}
}
