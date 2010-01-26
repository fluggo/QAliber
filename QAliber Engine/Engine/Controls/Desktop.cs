using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Automation;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Controls.WPF;

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

		private static WPFRoot wpfRoot = null;
		private static UIARoot uiaRoot = null;
		private static WebRoot webRoot = null;
	}
}
