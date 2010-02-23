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
using System.Text.RegularExpressions;
using System.Threading;
using QAliber.Engine.Controls;
using System.Windows.Automation;
using System.Windows;
using SHDocVw;
using System.ComponentModel;
using QAliber.Engine.Controls.UIA;
using System.Diagnostics;

namespace QAliber.Engine.Controls.Web
{
	public class WebPage : WebControl, IDisposable
	{
		#region Constructors
		public WebPage(InternetExplorer ieInstance, AutomationElement uiaElement)
			: base(null)
		{
			this.ieInstance = ieInstance;
			this.uiaElement = uiaElement;

			Thread initDocThread = new Thread(new ThreadStart(InitDocWorker));
			initDocThread.Start();
			initDocThread.Join();

			ieInstance.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(ieInstance_DocumentComplete);
			ieInstance.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(ieInstance_BeforeNavigate2);
		}
		
		#endregion

		#region Events
		public event EventHandler<NavigationEventArgs> BeforeNavigation;
		public event EventHandler<NavigationEventArgs> AfterNavigation;
		#endregion

		#region Find Methods
		/// <summary>
		/// Get web control by its ID. 
		/// When a control uses ID you should retrieve it with FindByID, also
		/// if you looking for a child of control with ID the best practice is to find
		/// the parent first with FindById and then drill down to its child.
		/// </summary>
		/// <param name="tagName">The tag name is the control type we looking for</param>
		/// <param name="id">the id og the control</param>
		/// <returns>WebControl with requested id and type, null if id or tagName doesnt match </returns>
		/// <example>
		/// <code>
		///    //On yahoo click the main story link
		///    //Note the main story Div is retrieve by ID and then drill down to the link
		///    ((HTMLLink)Browser.This.CurrentPage.FindByID("DIV", "p_13872472-main_story")["H2", 2]["A", 1]).Click();
		/// </code>
		/// </example>
		public WebControl FindByID(string tagName, string id)
		{
			IHTMLElement element = null;
			
			element = docInstance.getElementById(id);
			
			//if (element == null)
			//{
			//	  foreach (UIControlBase child in Children)
			//	  {
			//		  HTMLFrame frame = child as HTMLFrame;
			//		  if (frame != null)
			//		  {
			//			  element = frame.Document.getElementById(id);
			//			  if (element != null)
			//				  break;
			//		  }
			//	  }
			//}
			if (element != null)
			{
				if (element.tagName.ToLower() != tagName.ToLower())
				{
					QAliber.Logger.Log.Default.Error("The requested ID (" + id + " does not match the requested tag (" + tagName + ") it matched " +element.tagName
						,"",QAliber.Logger.EntryVerbosity.Internal);
					return null;
				}

				return WebControl.GetControlByType(element);
			}
			else
				return null;
		}
		///// <summary>
		///// Look for a WebControl by tag, name and index.
		///// </summary>
		///// <param name="tag">The html tag</param>
		///// <param name="index">the order of the control (of the searched tag name) under its parent.
		///// 1st control is 1</param>
		///// <returns>WebControl if tag and index found, null if not</returns>
		////public WebControl FindByTag(string tag, int index)
		////{
		  
		////	IHTMLElement element;
		////	IHTMLElementCollection elements = docInstance.getElementsByTagName(tag);
		////	if (elements != null && elements.length > 0)
		////	{
		////		if (elements.length > 1)
		////			element = (IHTMLElement)elements.item(null, index);
		////		else
		////			element = (IHTMLElement)elements.item(null, 0);

		////		return WebControl.GetControlByType(element);
		////	}
		////	else
		////		return null;
		////}

		/// <summary>
		/// Find WebControl by its tag, name and index. Adding the make the code more readable and the index promises uinque selection
		/// </summary>
		/// <param name="name">Name of the control, as it reflect in the Name property</param>
		/// <param name="tagName">The html tag</param>
		/// <param name="index">If more then 1 item found with this name, retrieve by the index providec</param>
		/// <returns>WebControl with matched tag and name and within expected index, return null if no control found </returns>
		/// <example>
		/// <code>
		///   HTMLInput searchText = Browser.This.CurrentPage.FindByName("INPUT","q",0) as HTMLInput;
		///   searchText.Write("Search");
		/// </code>
		/// </example>
		public WebControl FindByName(string tagName , string name , int index)
		{
			IHTMLElement element;
			IHTMLElementCollection elements = docInstance.getElementsByName(name);
			//if (elements == null || elements.length == 0)
			//{
			//	  foreach (UIControlBase child in Children)
			//	  {
			//		  HTMLFrame frame = child as HTMLFrame;
			//		  if (frame != null)
			//		  {
			//			  elements = frame.Document.getElementsByName(name);
			//			  if (elements != null && elements.length > 0)
			//				  break;
			//		  }
			//	  }
			//}
			if (elements != null && elements.length > 0)
			{
				if (elements.length > index && index > 0 )
					element = (IHTMLElement)elements.item(null, index - 1);
				else
					element = (IHTMLElement)elements.item(null, 0);

				if (element.tagName.ToLower() != tagName.ToLower() && tagName != "unknown")
				{
					QAliber.Logger.Log.Default.Error("Expected tag name (" + tagName +") did not match any of the elements found","",QAliber.Logger.EntryVerbosity.Internal);
					return null;
				}

				return WebControl.GetControlByType(element);
			}
			else
				return null;
		}
		/// <summary>
		/// Find WebControl by its name and Tag. Adding the make the code more readable and make the selection more unique unique
		/// </summary>
		/// <param name="name">Name of the control, as it reflect in the Name property</param>
		/// <param name="tagName">The html tag</param>
		/// <returns>WebControl with matched tag and name, return null if no control found </returns>
		/// <example>
		/// <code>
		///   HTMLInput searchText = Browser.This.CurrentPage.FindByName("INPUT","q") as HTMLInput;
		///   searchText.Write("Search");
		/// </code>
		/// </example>
		public WebControl FindByName(string tagName, string name)
		{
			return FindByName(tagName, name, -1);
		}
		/// <summary>
		/// Find WebControl by its name. Note names are not unique if you
		/// know the type or index use the override calss (FindByName(string tagName, string name))
		/// to get the control.
		/// </summary>
		/// <param name="name">Name of the control, as it reflect in the Name property</param>
		/// <returns>WebControl with matched name, return null if no control found </returns>
		/// <example>
		/// <code>
		///   HTMLInput searchText = Browser.This.CurrentPage.FindByName("q") as HTMLInput;
		///   searchText.Write("Search");
		/// </code>
		/// </example>
		public WebControl FindByName(string name)
		{
			return FindByName("unknown", name, -1);
		}
		/// <summary>
		/// Retrieve the control by abslute index.
		/// AbsoluteIndex assures you'll get the correct call as long no changes (remove or and tags) were done in the page.
		/// </summary>
		/// <param name="tagName">The tag of the control. This makes the code more readable, and make sure you get the correct control if absolute indexs changed on the page</param>
		/// <param name="index">the absoluteIndex property of the control</param>
		/// <returns>Web control if tag and index match, else return null</returns>
		/// <example>
		/// <code>
		///   HTMLInput searchText = Browser.This.CurrentPage.FindByAbsIndex("INPUT",64) as HTMLInput;
		///   searchText.Write("Search");
		/// </code>
		/// </example>
		/// <remarks>
		/// Any change in the page may cause this call to fail, use it only if you have to
		/// and on pages which does not require high maintanance.
		/// </remarks>
		public WebControl FindByAbsIndex(string tagName, int index)
		{
			WebControl res = FindByAbsIndex(index);

			if (tagName == res.TagName)
				return res;

			return null;
		}
		/// <summary>
		/// Retrieve the control by abslute index.
		/// AbsoluteIndex assures you'll get the correct call as long no changes (remove or and tags) were done in the page.
		/// </summary>
		/// <param name="index">the absoluteIndex property of the control</param>
		/// <returns>Web control if tag and index match, else return null</returns>
		/// <example>
		/// <code>
		///   HTMLInput searchText = Browser.This.CurrentPage.FindByAbsIndex(64) as HTMLInput;
		///   searchText.Write("Search");
		/// </code>
		/// </example>
		/// <remarks>
		/// Any change in the page may cause this call to fail, use it only if you have to
		/// and on pages which does not require high maintanance.
		/// </remarks>
		public WebControl FindByAbsIndex(int index)
		{
			IHTMLElementCollection allElements = (IHTMLElementCollection)docInstance.all;
			return WebControl.GetControlByType((IHTMLElement)allElements.item(index, null));
		}
		/// <summary>
		/// Get the control from x,y location relative to IE page body. 
		/// </summary>
		/// <param name="x">x location in pixel (left is 0)</param>
		/// <param name="y">y location in pixel (Top is 0)</param>
		/// <returns>WebControl from the requested location, or null if the location is out of Body layout </returns>
		/// <example>
		/// <code>
		///  //Location (x,y) is changes when you restore and change the IE page size
		///  HTMLInput searchText = Browser.This.CurrentPage.FindControlFromPoint(24,205) as HTMLInput;
		///   searchText.Write("Search");
		/// </code>
		/// </example>
		public WebControl FindControlFromPoint(int x, int y)
		{
			IHTMLElement element = docInstance.elementFromPoint(x, y);
			//if (string.Compare(element.tagName, "iframe", true) == 0)
			//{
			//	  foreach (UIControlBase child in Children)
			//	  {
			//		  HTMLFrame frame = child as HTMLFrame;
			//		  if (frame != null)
			//		  {
			//			  if (frame.AbsoluteIndex == element.sourceIndex)
			//				  return WebControl.GetControlByType(frame.Document.elementFromPoint(x, y));
			//		  }
			//	  }
			//}
			return WebControl.GetControlByType(docInstance.elementFromPoint(x, y));
		}

		#endregion

		#region Methods
		/// <summary>
		/// Retrieve form on this page by its name
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLForm mainForm = Browser.This.CurrentPage.Form("f") as HTMLForm;
		///    HTMLInput search = mainForm["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 2]["INPUT", 3] as HTMLInput;
		///    search.Write("Search");
		/// </code>
		/// </example>
		/// <param name="formName">Name property of the requested form</param>
		/// <returns>HTMLForm with the requested Name. or null if form with the requested name not found</returns>
		public HTMLForm Form(string formName)
		{
			IHTMLElementCollection forms = docInstance.forms;
			foreach (IHTMLElement form in forms)
			{
				if (((IHTMLFormElement)form).name == formName)
					return new HTMLForm(form);
			}

			return null;
		}
		/// <summary>
		/// Retrieve form on this page by its name
		/// </summary>
		/// <example>
		/// <code>
		///    HTMLForm mainForm = Browser.This.CurrentPage.Form(0) as HTMLForm;
		///    HTMLInput search = mainForm["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 2]["INPUT", 3] as HTMLInput;
		///    search.Write("Search");
		/// </code>
		/// </example>
		/// <param name="index">The index of the form out of the forms found. 1st is 0</param>
		/// <returns>HTMLForm at requested index. or number of forms on page is smaller then index</returns>
		public HTMLForm Form(int index)
		{
			IHTMLElementCollection forms = docInstance.forms;
			if (forms != null && forms.length > 0)
				return new HTMLForm((IHTMLElement)forms.item(null, index));

			return null;
		}
		/// <summary>
		/// Nevigate to requested URL
		/// </summary>
		/// <param name="url">string URL of the page you want to navigate to</param>
		/// <returns>Page you navgate to and currently focused in IE</returns>
		/// <example>
		/// <code>
		///   Browser.This.CurrentPage.Navigate("www.amazon.com");
		///   bool pageFound = Browser.This.WaitForPage("amazon", 30,true);
		///    if (pageFound)
		///		   Browser.This.CurrentPage.SetFocus();
		/// </code>
		/// </example>
		public WebPage Navigate(string url)
		{
			ieInstance.Navigate(url, ref nil, ref nil, ref nil, ref nil);
			if (WaitForPage(url))
			{
				docInstance = (HTMLDocument)ieInstance.Document;
				htmlElement = docInstance.body;
			}
			return this;
		}

		public bool WaitForPage(string url)
		{
			return WaitForPage(url, 15, false);
		}

		public bool WaitForPage(string url, int timeoutInSeconds, bool useRegex)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			Regex regex = new Regex(url.ToLower());
			while (stopWatch.ElapsedMilliseconds < timeoutInSeconds * 1000)
			{
				if (loaded)
				{
					if (!useRegex)
					{
						if (docInstance.url == url)
						{
							return true;
						}
					}
					else if (regex.IsMatch(docInstance.url.ToLower()))
					{
						return true;
					}	
				}
				System.Threading.Thread.Sleep(100);
			}
			return false;
		}

		/// <summary>
		/// Set focus on current page.
		/// </summary>
		/// <example>
		/// <code>
		///   Desktop.Web.CurrentPage.Navigate("www.amazon.com");
		///   bool pageFound = Desktop.Web.WaitForPage("amazon", 30,true);
		///    if (pageFound)
		///		   Desktop.Web.CurrentPage.SetFocus();
		/// </code>
		/// </example>
		public override void SetFocus()
		{
			UIAControl.GetControlByType(uiaElement).SetFocus();
			docInstance.focus();
		}

		public override void Refresh()
		{
			base.Refresh();
			htmlElement = docInstance.body;
		}

		public void ClearEvents()
		{
			try
			{
				ieInstance.DocumentComplete -= new DWebBrowserEvents2_DocumentCompleteEventHandler(ieInstance_DocumentComplete);
				ieInstance.BeforeNavigate2 -= new DWebBrowserEvents2_BeforeNavigate2EventHandler(ieInstance_BeforeNavigate2);
			}
			catch (Exception)
			{
				//TODO : Find if removing events is necessary
			}

			BeforeNavigation = null;
			AfterNavigation = null;
		}

		private void InitDocWorker()
		{
			this.docInstance = (HTMLDocument)ieInstance.Document;
			this.htmlElement = docInstance.body;
			this.controlStyle = htmlElement.style;
			
		}

		private void InitUIAWorker()
		{
			uiaElement = Desktop.Web.RetrievePageByHandle(ieInstance.HWND);

		}

		#endregion

		#region Properties
		public AutomationElement UIAElement
		{
			get { return uiaElement; }
		}

		[Browsable(false)]
		public HTMLDocument Document
		{
			get { return docInstance; }
		}

		[Browsable(false)]
		public InternetExplorer IE
		{
			get { return ieInstance; }
		}
		
		
		/// <summary>
		/// Is the current Page in focus
		/// </summary>
		/// <example>
		/// <code>
		///   if(!Desktop.Web.CurrentPage.IsFocused)
		///    {
		///		   Desktop.Web.CurrentPage.SetFocus();
		///    }
		/// </code>
		/// </example>
		[Category("Web Page")]
		[DisplayName("Is Focused ?")]
		public bool IsFocused
		{
			get
			{
				if (docInstance == null)
					return false;
				return docInstance.hasFocus();
			}
		}
		/// <summary>
		/// Retrieve the number of forms on current page
		/// </summary>
		/// <example>
		/// <code>
		/// int numOfForms = Desktop.Web.CurrentPage.NumberOfForms;
		/// </code>
		/// </example>
		[Category("Web Page")]
		[DisplayName("Forms Count")]
		public int NumberOfForms
		{
			get { return docInstance.forms.length;}
		}
		/// <summary>
		/// Get the current WebControl which is active on the Page.
		/// </summary>
		/// <example>
		/// <code>
		///  while (Desktop.Web.CurrentPage.ActiveElement.InnerText != "About Google")
		///    {
		///		   Desktop.Web.CurrentPage.Write("{TAB}");
		///		   System.Threading.Thread.Sleep(500);
		///    }
		///    Desktop.Web.CurrentPage.FocusedElement.Click();
		/// </code>
		/// </example>
		/// <returrns>Current active WebControl</returrns>
		[Category("Web Page")]
		[DisplayName("Active Element")]
		public WebControl ActiveElement
		{
			get
			{
				return WebControl.GetControlByType(docInstance.activeElement);
			}
		}
		/// <summary>
		/// Get the current page title.
		/// </summary>
		[Category("Web Page")]
		public string Title
		{
			get { return docInstance.title; }
		}

		[Category("Web Page")]
		public string URL
		{
			get { return docInstance.url; }
		}

		[Category("Web Page")]
		[Description("Page ID")]
		public string PageID
		{
			get { return docInstance.uniqueID; }
		}

		public override string CodePath
		{
			get
			{
				return "Desktop.Web.CurrentPage";
			}
		}

		public override Rect Layout
		{
			get
			{
				if (layout == Rect.Empty)
				{
					InitUIAWorker();
					if (uiaElement != null)
						layout = uiaElement.Current.BoundingRectangle;
				}
				return layout;
			}
		}

		public override UIControlBase Parent
		{
			get
			{
				return Desktop.Web;
			}
			set
			{
				base.Parent = value;
			}
		}

		#endregion

		#region Event Handlers
		private void ieInstance_BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
		{
			loaded = false;
			if (BeforeNavigation != null)
				BeforeNavigation(this, new NavigationEventArgs(ieInstance.LocationURL));
		}
			
		private void ieInstance_DocumentComplete(object pDisp, ref object URL)
		{
			if (((IWebBrowser2)pDisp).Parent.Equals(ieInstance.Parent))
			{
				//new Thread(new ThreadStart(InitUIAWorker)).Start();
				Refresh();
				loaded = true;
				if (AfterNavigation != null)
					AfterNavigation(this, new NavigationEventArgs(URL.ToString()));
			}
		   
		}
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			ClearEvents();
		}

		#endregion
		
		private HTMLDocument docInstance;

		private InternetExplorer ieInstance;
		private AutomationElement uiaElement;
		private bool loaded = true;
		private object nil = new object();

	}

	public class NavigationEventArgs : EventArgs
	{
		public NavigationEventArgs(string url)
		{
			this.url = url;
		}
		private string url;

		public string URL
		{
			get { return url; }
			set { url = value; }
		}
	
	}

}
