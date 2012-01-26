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
//http://www.koders.com/csharp/fid36DB75029F9A8E6EDAD39C9C3B44F936A9F18A55.aspx?s=mdef%3Ainsert
namespace QAliber.Engine.Controls.Web
{
	public abstract class WebControl : UIControlBase
	{
		#region Constructors
		public WebControl(IHTMLElement element)
		{
			htmlElement = element;
			if (element != null)
				this.controlStyle = htmlElement.style;
		}
		#endregion

		#region UIControlBase Method Overrides
		
		public override void SetFocus()
		{
			try
			{
				Page.SetFocus();
				((IHTMLElement2)htmlElement).focus();
			}
			catch
			{
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			_index = 0;
			_codePath = null;
		}

		#endregion
		#region Properties
		int _index;

		/// <summary>
		/// Retrieve the Wec control Index.
		/// </summary>
		/// <example>
		/// <code>
		///  HTMLInput searchButton = Browser.This.CurrentPage.FindByName("Input","q") as HTMLInput;
		///  int controlIndex = searchButton.Index;
		/// </code>
		/// </example>
		/// <returns>The index of the control is the ordinal position relative to the control parent control</returns>
		public override int Index
		{
			get
			{
				if (_index == 0)
				{
					_index = 1;
					IHTMLElementCollection elements = (IHTMLElementCollection)this.htmlElement.parentElement.children;
					int idxCounter = 0;
					foreach (IHTMLElement element in elements)
					{
						if (this.TagName.ToLower() == element.tagName.ToLower())
						{
							idxCounter++;
							if (this.AbsoluteIndex == element.sourceIndex)
							{
								_index = idxCounter;
								break;
							}
						}
					}
				}
				return _index;
			}
		}

		string _codePath = null;

		public override string CodePath
		{
			get
			{
				if (_codePath == null)
				{
					string prefix = String.Empty;
					UIControlBase parent = Parent;
					if (parent == null)
						_codePath = "Desktop.Web";
					else if (this.ID != null)
					{
						prefix = "Desktop.Web.CurrentPage.FindByID(\"" + TagName + "\", \"" + ID + "\")";
						_codePath = prefix;
					}
					else if (this.Name != "")
					{
						prefix = "Desktop.Web.CurrentPage.FindByName(\"" + TagName + "\", \"" + Name + "\")";
						_codePath = prefix;
					}
					else if (useAbsIdx)
					{
						prefix = "Desktop.Web.CurrentPage.FindByAbsIndex(\"" + Name + "\", " + AbsoluteIndex + ")";
						_codePath = prefix;
					}
					else
					{
						prefix = Parent.CodePath;
						_codePath = prefix + "[\"" + TagName + "\", " + Index + "]";
					}
				}
				return _codePath;
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
					return htmlElement.id;
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
			get { return htmlElement.className; }
		}
		/// <summary>
		/// Retriev the Name of the control, this is the name you see in control Name Property.
		/// When a control has name you can find it on page using Browser.This.CurrentPage.FindByName()
		/// </summary>
		/// <example>
		/// <code>
		///   //Lets search web control with the name q
		///    WebControl control = Browser.This.CurrentPage.FindByName("q");
		///   //indeed name is q
		///   string name = control.Name;
		/// </code>
		/// </example>
		/// <returns>The name of the control or empty string if has no name</returns>
		public override string Name
		{
			get 
			{
				object nameAttr = htmlElement.getAttribute("name", 0);
				if (nameAttr != null)
					return nameAttr.ToString();
				return "";
			}
		}
		/// <summary>
		/// Retrieve the HTML Tag name.
		/// </summary>
		/// <example>
		/// <code>
		///  //Lets search web control with the name q
		///   WebControl control = Browser.This.CurrentPage.FindByName("q");
		///   //Tag name - the HTML tag name, here we expect INPUT tag
		///   string tagName = control.TagName;
		/// </code>
		/// </example>
		[Category("Web")]
		[DisplayName("Tag Name")]
		public string TagName
		{
			get
			{
				return htmlElement.tagName;
			}
		}
		/// <summary>
		/// Retrieve Rect layout of the control in the page and relative to the current Page
		/// </summary>
		/// <example>
		/// <code>
		///    UIAWindow IEwin = Desktop.UIA[@"Google - Windows Internet Explorer", @"IEFrame", @"UIAWindow"] as UIAWindow;
		///    //Lets search web control with the name q
		///   WebControl control = Browser.This.CurrentPage.FindByName("q");
		///   Rect qLayout = control.Layout;
		///    //get the top left position relative to the Page!
		///   Point qPosition = qLayout.TopLeft;
		///
		///   IEwin.Restore();
		///   IEwin.Move(300, 300);
		///   //get the top left position relative to the Page! did not change
		///   qPosition = qLayout.TopLeft;
		/// </code>
		/// </example>
		/// <returns>System.Windows.Rect control layout</returns>
		public override Rect Layout
		{
			get
			{
				IHTMLRect rect = ((IHTMLElement2)this.htmlElement).getBoundingClientRect();
				Rect res = new Rect(rect.left,
					rect.top, 
					rect.right - rect.left, 
					rect.bottom - rect.top);
				res.Offset(Desktop.Web.CurrentPage.Layout.Left, Desktop.Web.CurrentPage.Layout.Top);
				return res;
			}
		}

		/// <summary>
		/// Retrieve the string between the open tag <X> to the close tag </X>.
		/// </summary>
		/// <example>
		/// <code>
		///  HTMLFont fontTag = Browser.This.CurrentPage.Body["CENTER", 1]["P", 1]["FONT", 1] as HTMLFont;
		///  string year = fontTag.InnerText;
		/// </code>
		/// </example>
		/// <returns>The text between the control tags (e.g.) <TD> </TD></returns>
		[Category("Web")]
		public virtual string InnerText
		{
			get { return htmlElement.innerText; }
		}
		/// <summary>
		/// Return the Absolute index, which is serial and constant counted from the HTMLBody
		/// element, down to its descendants.
		/// </summary>
		/// <example>
		/// <code>
		///   //Body will always get the lowest AbsoluteIndex
		///   int body = Browser.This.CurrentPage.Body.AbsoluteIndex;
		///   int mainForm = Browser.This.CurrentPage.FindByName("f").AbsoluteIndex;
		///   WebControl control = Browser.This.CurrentPage.FindByAbsIndex(mainForm);
		///   string controlName = control.Name;
		/// </code>
		/// </example>
		/// <returns>int Absolute index of the control in the DOM tree </returns>
		[Category("Identifiers")]
		public virtual int AbsoluteIndex
		{
			get
			{
				return htmlElement.sourceIndex;
			}
		}
		[Category("Web")]
		public int TabIndex
		{
			get
			{
				return ((IHTMLElement2)htmlElement).tabIndex;
			}
		}
		
		/// <summary>
		/// Retrieve the parent control for this HTML control.
		/// </summary>
		/// <example>
		/// <code>
		///   HTMLForm mainForm = Browser.This.CurrentPage.FindByName("f") as HTMLForm;
		///   WebControl control = mainForm.Parent as WebControl;
		///   string type = control.UIType;
		/// </code>
		/// </example>
		/// <returns>UIControl the parent of this control in the controls hierarchy.
		/// Note it returns UIControl, while usually it will be WebControl (inherites UIControl)
		/// Body parent is a UIControl</returns>
		[Browsable(false)]
		public override UIControlBase Parent
		{
			get
			{
				if (parent == null)
				{
					if (htmlElement.parentElement != null)
						parent = WebControl.GetControlByType(this.htmlElement.parentElement);
				}
				return parent;
			}
			set { parent = value;}
		}
		/// <summary>
		/// Retrieve the style of the web control.
		/// </summary>
		/// <example>
		/// <code>
		///   using mshtml;
		/// ...
		///   HTMLInput searchButton = Browser.This.CurrentPage.FindByName("btnG") as HTMLInput;
		///   string color = searchButton.Style.backgroundColor.ToString();
		/// </code>
		/// </example>
		/// <returns>IHTMLCurrentStyle dictionary allow you to access all style properties </returns>
		/// <remarks>
		/// Note: Before using style from the test project you need to add reference to 
		/// Microsoft.mshtml to your project.
		/// </remarks>
		[Category("Web")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public IHTMLCurrentStyle Style
		{
			get { return ((IHTMLElement2)htmlElement).currentStyle; }
		}
		/// <summary>
		/// Verify if the control is enabled, allow intercation.
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLInput searchButton = Browser.This.CurrentPage.FindByName("btnG") as HTMLInput;
		///    if (searchButton.Enabled)
		///		   searchButton.Click();
		/// </code>
		/// </example>
		/// <returns>true if the control enabled, false if not</returns>
		public override bool Enabled
		{
			get
			{
				try
				{
					return !((IHTMLElement3)htmlElement).disabled;
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
		/// <example>
		/// <code>
		///    HTMLInput searchButton = Browser.This.CurrentPage.FindByName("btnG") as HTMLInput;
		///    if (searchButton.Visible)
		///		   searchButton.Click();
		/// </code>
		/// </example>
		/// <returns>true if the control visible, false if not</returns>
		public override bool Visible
		{
			get
			{
				return htmlElement.style.visibility != "hidden";
			}
		}
		/// <summary>
		/// Retrieve the string of the tooltip info you get when hovering the control
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLInput searchTB = Browser.This.CurrentPage.FindByName("q") as HTMLInput;
		///    string helpTxt = searchTB.HelpText;
		///    //Just for the demo lets hover and see this HelpText
		///    searchTB.MoveMouseTo(new Point(5, 10));
		///    System.Threading.Thread.Sleep(4000);
		/// </code>
		/// </example>
		/// <returns>string text of the help tool tip</returns>
		public virtual string HelpText
		{
			get { return htmlElement.title; }
		}

		
		
		/// <summary>
		/// Retrieve all children of this control
		/// </summary>
		/// <example>
		/// <code>
		///   HTMLForm searchForm =  Browser.This.CurrentPage.FindByName("FORM", "f") as HTMLForm;
		///   HTMLTd searchRow = searchForm["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 2] as HTMLTd;
		///   UIControl[] all = searchRow.Children;
		///   //We know its all WebControls
		///   string firstChildType = ((WebControl)all[0]).UIType; 
		/// </code>
		/// </example>
		/// <returns>UIControl[] if has children or empty array if have none </returns>
		[Browsable(false)]
		public override List<UIControlBase> Children
		{
			get
			{
				if (children == null)
				{
					children = new List<UIControlBase>();
					IHTMLElementCollection elements = (IHTMLElementCollection)htmlElement.children;
					foreach (IHTMLElement element in elements)
					{
						WebControl child = WebControl.GetControlByType(element);
						if (child != null)
						{
							children.Add(child);
							child.parent = this;
						}
					}
				}
				return children;
			}
		}

		public override Process Process
		{
			get
			{
				try
				{
					return Process.GetProcessesByName("iexplore")[0];
				}
				catch
				{
					return null;
				}
			}
		}

		public virtual WebPage Page
		{
			get
			{
				UIControlBase parent = this;
				while (parent != null)
				{
					if (parent is WebPage)
						return (WebPage)parent;
					parent = parent.Parent;
				}
				return null;
			}
		}
		#endregion
		#region Indexers
		/// <summary>
		/// Gets the a Web control with matching tag name in certain index, 
		/// In this indexer you can select from list, if you're not fammilier with the tag names
		/// </summary>
		/// <example>
		/// Both saples below will click the about link the first example
		/// looks for the 3rd UNKNOWN (any) tag and the second goes through all links
		/// and clicks on the third.
		/// <code>
		/// HTMLLink about = Browser.This.CurrentPage.Body["CENTER", 1]["FONT", 1][WebControlType.UNKNOWN, 3] as HTMLLink;
		/// about.Click();
		/// 
		/// //or
		/// 
		/// HTMLLink about = Browser.This.CurrentPage.Body["CENTER", 1]["FONT", 1][WebControlType.A, 3] as HTMLLink;
		/// about.Click();
		/// </code>
		/// </example>
		/// <param name="type"></param>
		/// <param name="localIndex"></param>
		/// <returns>If tag and index found return a WebControl, else return null</returns>
		public virtual WebControl this[WebControlType type, int localIndex]
		{
			get
			{
				if (Exists)
				{
					IHTMLElementCollection elements = (IHTMLElementCollection) htmlElement.children;
						// new PropertyCondition(AutomationElement.IsControlElementProperty, true));
					int idxCounter = 1;


					foreach (IHTMLElement element in elements)
					{
						if (element.tagName.ToLower().Contains(type.ToString().ToLower()) ||
							type == WebControlType.UNKNOWN)
						{
							if (idxCounter == localIndex)
								return GetControlByType(element);
							else
								idxCounter++;
						}
					}
				}
				return new WebNullControl();
			}
		}
		/// <summary>
		/// Retrive an HTML element by using the tag name, and index for unique identification.
		/// </summary>
		/// <param name="type">the Tag Name as it reflects in the properties</param>
		/// <param name="localIndex">The control Index property. The child order from
		/// the father is constant, where the 1st is 1</param>
		/// <example>
		/// For this example assume IE is open on google.com
		/// <code>
		///  //Since our Main form has name, lets retrieve it first
		///  //googleMain was added as alias to Browser.This.CurrentPage (either create alias or use Browser.This.CurrentPage in the code)
		///   HTMLForm mainForm = Aliases.googleMain.FindByName("FORM", "f") as HTMLForm;
		///    //using tags,index drill down to our requested control
		///   HTMLTr tableRow = mainForm["TABLE", 1]["TBODY", 1]["TR", 1] as HTMLTr;
		///   HTMLTd searchArea = tableRow["TD", 2] as HTMLTd;
		///    //Assuming we don't have name for the inputs control:
		///   ((HTMLInput)searchArea["input",3]).Write("QAliber test framework");//search string
		///   ((HTMLInput)searchArea["input", 4]).Click();//click search button
		/// </code>
		/// </example>
		/// <returns>WebControl if the control is found, null if not</returns>
		public new WebControl this[string type, int localIndex]
		{
			get
			{
				if (Exists)
				{
					IHTMLElementCollection elements = (IHTMLElementCollection) htmlElement.children;
						// new PropertyCondition(AutomationElement.IsControlElementProperty, true));
					int idxCounter = 1;
					foreach (IHTMLElement element in elements)
					{
						if (element.tagName.ToLower().Contains(type.ToLower()))
						{
							if (idxCounter == localIndex)
								return GetControlByType(element);
							else
								idxCounter++;
						}
					}
				}
				return new WebNullControl();
			}
		}
		#endregion
		public static WebControl GetControlByType(IHTMLElement element)
		{
			string type = element.tagName.ToLower();
			switch (type)
			{
				case "table":
					return new HTMLTable(element);
				case "input":
					return new HTMLInput(element);
				case "form":
					return new HTMLForm(element);
				case "tr":
					return new HTMLTr(element);
				case "td":
					return new HTMLTd(element);
				case "div":
					return new HTMLDiv(element);
				case "block":
				case "center":
					return new HTMLBlock(element);
				case "br":
					return new HTMLBr(element);
				case "tbody":
					return new HTMLTbody(element);
				case "img":
					return new HTMLImage(element);
				case "a":
					return new HTMLLink(element);
				case "body":
					return Desktop.Web.CurrentPage;
				case "select":
					return new HTMLSelect(element);
				case "script":
					return new HTMLScript(element);
				case "option":
					return new HTMLOption(element);
				case "font":
				case "basefont":
					return new HTMLFont(element);
				case "object":
					return new HTMLObject(element);
				case "h1":
				case "h2":
				case "h3":
				case "h4":
				case "h5":
				case "h6":
					return new HTMLHeader(element);
				case "li":
					return new HTMLLi(element);
				case "ul":
					return new HTMLUl(element);
				case "ol":
					return new HTMLOl(element);
				case "html":
					return null;
				//case "p":
				//	  return new HTMLP(element);
				case "textarea":
					return new HTMLTextArea(element);
				case "button":
					return new HTMLButton(element);
				default:
					return new HTMLTag(element);
			}
		}

		#region Private Fields
		internal IHTMLElement htmlElement;
		protected IHTMLStyle controlStyle;
		public static bool useAbsIdx = false;
		#endregion
	}
	public enum WebControlType
	{
		BODY,
		DIV,
		Bloc,
		BR,
		FORM,
		TABLE,
		TBODY,
		TR,
		TD,
		INPUT,
		A,
		SELECT,
		IMG,
		H,
		OPTION,
		LI,
		UL,
		OL,
		UNKNOWN
	}
}
