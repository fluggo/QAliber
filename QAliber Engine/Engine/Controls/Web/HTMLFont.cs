using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.ComponentModel;

namespace QAliber.Engine.Controls.Web
{
	/// <summary>
	/// Represents HTML element FONT tag.
	/// The font tag specifies the font face, font size, and font color of text.
	/// Though on most web applications today these are set by Style
	/// </summary>
	public class HTMLFont : WebControl
	{
		public HTMLFont(IHTMLElement element) : base(element) 
		{

		}

		#region props
		/// <summary>
		/// Retrieve the font face (deprecated)
		/// </summary>
		/// <example>
		/// <code>
		/// HTMLFont fon = Browser.This.CurrentPage.FindByName("FORM", "f")["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 3]["FONT", 1] as HTMLFont;
		/// string face = fon.Font;
		/// int fonSize = fon.Size;
		/// string color = fon.Color;
		/// </code>
		/// </example>
		/// <returns>string font type (veranda,arial ,etc')</returns>
		/// <remarks>Deprecated. Use style instead</remarks>
		[Category("HTMLFont")]
		public string Font
		{
			get
			{
				try
				{
					return ((IHTMLFontElement)htmlElement).face;
				}
				catch
				{
					return ((IHTMLBaseFontElement)htmlElement).face;
				}
			}
			set
			{
				try
				{
					((IHTMLFontElement)htmlElement).face = value;
				}
				catch
				{
					((IHTMLBaseFontElement)htmlElement).face = value;
				}
			}
		}
		/// <summary>
		/// Retrieve the font size (deprecated)
		/// </summary>
		/// <example>
		/// <code>
		/// HTMLFont fon = Browser.This.CurrentPage.FindByName("FORM", "f")["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 3]["FONT", 1] as HTMLFont;
		/// string face = fon.Font;
		/// int fonSize = fon.Size;
		/// string color = fon.Color;
		/// </code>
		/// </example>
		/// <returns>int font size</returns>
		/// <remarks>Deprecated. Use style instead</remarks>
		[Category("HTMLFont")]
		public int Size
		{
			get
			{
				try
				{
					return int.Parse((string)((IHTMLFontElement)htmlElement).size);
				}
				catch
				{
					return ((IHTMLBaseFontElement)htmlElement).size;
				}
			}
			set
			{
				try
				{
					((IHTMLFontElement)htmlElement).size = value;
				}
				catch
				{
					((IHTMLBaseFontElement)htmlElement).size = value;
				}
			}
		}
		/// <summary>
		/// Retrieve the font color (deprecated)
		/// </summary>
		/// <example>
		/// <code>
		/// HTMLFont fon = Browser.This.CurrentPage.FindByName("FORM", "f")["TABLE", 1]["TBODY", 1]["TR", 1]["TD", 3]["FONT", 1] as HTMLFont;
		/// string face = fon.Font;
		/// int fonSize = fon.Size;
		/// string color = fon.Color;
		/// </code>
		/// </example>
		/// <returns>string color</returns>
		/// <remarks>Deprecated. Use style instead</remarks>
		[Category("HTMLFont")]
		public string Color
		{
			get
			{
				try
				{
					return (string)((IHTMLFontElement)htmlElement).color;
				}
				catch
				{
					return (string)((IHTMLBaseFontElement)htmlElement).color;
				}
			}
			set
			{
				try
				{
					((IHTMLFontElement)htmlElement).color = value;
				}
				catch
				{
					((IHTMLBaseFontElement)htmlElement).color = value;
				}
			}
		}
		#endregion
	}
}
