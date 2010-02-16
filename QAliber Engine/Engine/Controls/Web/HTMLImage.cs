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
