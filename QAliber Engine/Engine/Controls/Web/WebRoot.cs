using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using SHDocVw;
using System.Windows.Forms;
using mshtml;

namespace QAliber.Engine.Controls.Web
{
	public class WebRoot : UIControlBase, IControlLocator
	{
		internal WebRoot()
		{
			ieWindows.WindowRegistered += new SHDocVw.DShellWindowsEvents_WindowRegisteredEventHandler(ieWindows_WindowRegistered);
			ieWindows.WindowRevoked += new SHDocVw.DShellWindowsEvents_WindowRevokedEventHandler(ieWindows_WindowRevoked);
		}

		public event EventHandler<NavigationEventArgs> BeforeNavigationInAnyPage;
		public event EventHandler<NavigationEventArgs> AfterNavigationInAnyPage;


		public override List<UIControlBase> Children
		{
			get
			{
				
				if (children == null)
				{
					children = new List<UIControlBase>();
					if (CurrentPage != null)
						children.Add(CurrentPage);
				}
				return children;
				
			}
		}

		public override string CodePath
		{
			get
			{
				return "Desktop.Web";
			}
		}

		public WebPage CurrentPage
		{
			get
			{
				if (page == null)
					BuildPages();
				lock (this)
				{
					return page;
				}
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			ClearEvents();
			page = null;
		}

		public void ClearEvents()
		{
			BeforeNavigationInAnyPage = null;
			AfterNavigationInAnyPage = null;
			if (page != null)
				page.Dispose();
		}

		internal AutomationElement RetrievePageByHandle(int handle)
		{
			AutomationElement pageWinElement = AutomationElement.FromHandle(new IntPtr(handle));
			if (pageWinElement != null)
			{
				AutomationElement ieElement = pageWinElement.FindFirst(TreeScope.Descendants,
					new PropertyCondition(AutomationElement.ClassNameProperty, "Internet Explorer_Server"));
				if (ieElement != null)
					return ieElement;
			}
			return null;
		}

		private void BuildPages()
		{
			
				
				foreach (InternetExplorer ie in ieWindows)
				{
					if (ie.Document is HTMLDocument)
					{
						HTMLDocument doc = (HTMLDocument)ie.Document;
						if (!string.IsNullOrEmpty(doc.title) && ie.Visible)
						{
							//AutomationElement matchingPage = RetrievePageByName(doc.title);
							//if (matchingPage != null)
							//{
								lock (this)
								{
									ClearEvents();
									page = new WebPage(ie, null);
									page.BeforeNavigation += new EventHandler<NavigationEventArgs>(BeforeNavigationOfAnyPage);
									page.AfterNavigation += new EventHandler<NavigationEventArgs>(AfterNavigationOfAnyPage);
								}
							//}
						}
					}
				}
			
		}

		private void BeforeNavigationOfAnyPage(object sender, NavigationEventArgs e)
		{
			if (BeforeNavigationInAnyPage != null)
			{
				BeforeNavigationInAnyPage(sender, e);
			}
			
		}

		private void AfterNavigationOfAnyPage(object sender, NavigationEventArgs e)
		{
			if (AfterNavigationInAnyPage != null)
			{
				AfterNavigationInAnyPage(sender, e);
			}
		}

		private void ieWindows_WindowRevoked(int lCookie)
		{
			BuildPages();
		}

		private void ieWindows_WindowRegistered(int lCookie)
		{
			BuildPages();
		}

		#region IControlLocator Members

		public UIControlBase GetControlFromCursor()
		{
			System.Windows.Point pt = new System.Windows.Point(System.Windows.Forms.Cursor.Position.X,
							  System.Windows.Forms.Cursor.Position.Y);
			return GetControlFromPoint(pt);
		}

		public UIControlBase GetControlFromPoint(System.Windows.Point pt)
		{
			lock (this)
			{
				if (page != null)
				{
					IHTMLElement element = page.Document.elementFromPoint((int)pt.X - (int)page.Layout.Left, (int)pt.Y - (int)page.Layout.Top);
					if (element != null)
						return WebControl.GetControlByType(element);

				}
			}
			return null;
		}

		public UIControlBase GetFocusedElement()
		{
			if (page != null)
				return page.ActiveElement;
			return null;

		}
		#endregion

		private ShellWindows ieWindows = new ShellWindows();
		private WebPage page;

	}
}
