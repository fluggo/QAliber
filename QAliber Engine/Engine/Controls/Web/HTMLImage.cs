using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.ComponentModel;
namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents HTML IMG element.
	/// Image embeds images into html page.
	/// </summary>
	public class HTMLImage : WebControl
	{
		public HTMLImage(IHTMLElement element)
			: base(element)
		{
		}
		/// <summary>
		/// Get the Href(link) to the picture.
		/// </summary>
		/// <example>
		/// <code>
		/// //On google page
		///    HTMLImage logo = Browser.This.CurrentPage.FindByID("IMG", "logo") as HTMLImage;
		///    string refToImg = logo.Href;
		///    //navigate to img URL
		///    Browser.This.CurrentPage.Navigate(refToImg);
		/// </code>
		/// </example>
		/// <returns>string reference to the image</returns>
		[Category("Web Image")]
		public string Href
		{
			get { return ((IHTMLImgElement)htmlElement).href; }
		}
		/// <summary>
		/// Get the relative path to the image
		/// </summary>
		/// <example>
		/// <code>
		///   //On google page
		///   HTMLImage logo = Browser.This.CurrentPage.FindByID("IMG", "logo") as HTMLImage;
		///   string relativeImagePath = logo.ImgSource;
		/// </code>
		/// </example>
		/// <returns>string relative path to the image</returns>
		[Category("Web Image")]
		public string ImgSource
		{
			get { return ((IHTMLImgElement)htmlElement).src; }
		}
	}
}
