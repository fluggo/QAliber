using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.ComponentModel;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represent the OPTION html tag.
	/// This is an option in select list.
	/// </summary>
	[DisplayName("Select Option")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class HTMLOption : WebControl
	{
		public HTMLOption(IHTMLElement element) : base(element) 
		{

		}

		#region props
		/// <summary>
		/// Retrieve the text which identify the option
		/// </summary>
		/// <example>
		/// This sample run on google preferences page (http://www.google.com/preferences?hl=en)
		/// <code>
		///    //On google preferences page:
		///   HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///   HTMLOption op = intfaceLang.Options[3];
		///   string opName = op.Text;
		///   bool isOpSelected = op.IsSelected;
		/// </code>
		/// </example>
		public string Text
		{
			get
			{
				return ((IHTMLOptionElement)htmlElement).text;
			}
			set
			{
				((IHTMLOptionElement)htmlElement).text = value;
			}
		}
		/// <summary>
		/// Verify if this OPTION is selected in the hosted list
		/// </summary>
		/// <example>
		/// This sample run on google preferences page (http://www.google.com/preferences?hl=en)
		/// <code>
		///    //On google preferences page:
		///   HTMLSelect intfaceLang = Browser.This.CurrentPage.FindByName("SELECT", "hl") as HTMLSelect;
		///   HTMLOption op = intfaceLang.Options[3];
		///   string opName = op.Text;
		///   bool isOpSelected = op.IsSelected;
		/// </code>
		/// </example>
		public bool IsSelected
		{
			get
			{
				return ((IHTMLOptionElement)htmlElement).selected;
			}
			set
			{
				((IHTMLOptionElement)htmlElement).selected = value;
			}
		}



		#endregion
	}
}
