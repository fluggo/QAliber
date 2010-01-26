using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.ComponentModel;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents HTML FORM element. Forms used to hold Input controls and send data to server.
	/// </summary>
	public class HTMLForm : WebControl
	{
		public HTMLForm(IHTMLElement element)
			: base(element)
		{
		   
		}

		/// <summary>
		/// Submit this form. Submit is use to pass data to the server.
		/// </summary>
		/// <example>
		/// In the code below,lets search google by submiting the from instead of clicking Google Search button
		/// <code>
		///    HTMLInput searchBox = Browser.This.CurrentPage.FindByName("INPUT", "q") as HTMLInput;
		///    searchBox.Write("find");
		///    HTMLForm searchForm = Browser.This.CurrentPage.FindByName("FORM", "f") as HTMLForm;
		///    searchForm.Submit();
		/// </code>
		/// </example>
		public void Submit()
		{
			((IHTMLFormElement)htmlElement).submit();
		}

		#region properties
		/// <summary>
		/// Retrieve the form name.
		/// </summary>
		/// <example>
		/// <code>
		///  HTMLForm searchForm = Browser.This.CurrentPage.FindByName("FORM", "f") as HTMLForm;
		///  string formName = searchForm.FormName;
		/// </code>
		/// </example>
		[Category("HTMLForm")]
		public string FormName
		{
			get
			{
				return ((IHTMLFormElement)htmlElement).name;
			}
		}

		#endregion

	}
}
