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
using WatiN.Core.Native;

//http://www.koders.com/csharp/fid36DB75029F9A8E6EDAD39C9C3B44F936A9F18A55.aspx?s=mdef%3Ainsert
namespace QAliber.Engine.Controls.Watin
{

	public class WatFrame : UIControlBase
	{
		#region Constructors
		public WatFrame(WatinBaseControl parent, WatiN.Core.Frame element, int idx)
		{
			this.parent = parent;
			if (element != null)
			{
				docElement = element;
			   // docElement
				_index = idx;
			}
		}
	   
		#endregion

		#region UIControlBase Method Overrides
	   
		
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
		/// Get the index of the control in the watingBaseControl category (Divs,Butoons etc')
		/// The index is given by the BaseControl on this Ctor
		/// </summary>
		public override int Index
		{
			get
			{
				return _index;
			}
		}

		string _codePath;

		public override string CodePath
		{
			get
			{

				if (_codePath == null)
				{
					string prefix = String.Empty;

					if (this.ID != null)
					{
						prefix = parent.Parent.CodePath + "." + UIType + "(Find.ById(\"" + ID + "\"))";
						_codePath = prefix;
					}
					else if (this.Name != null)
					{
						prefix = parent.Parent.CodePath + "." + UIType + "(Find.ByName(\"" + Name + "\"))";
						_codePath = prefix;
					}

					else
					{
						prefix = parent.CodePath + "[" + Index + "]";
						_codePath = prefix;
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
					return docElement.Id;
				}
				catch
				{
					return "";
				}
			}
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
				//if (docElement == null)
				//	  return "dummy";

				return docElement.Name;
			}
		}
		/// <summary>
		/// Return the Type of the WatinControl (Div,Button,Form etc')
		/// </summary>
		public override string UIType
		{
			get
			{
				return docElement.GetType().Name;
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
				else if (!string.IsNullOrEmpty(InnerText) && InnerText.Length < 32)
					return InnerText;
				else
					return "Frame[" + _index + "]";
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
					rect = docElement.NativeDocument.ContainingFrameElement.GetAbsElementBounds(); 
				}
				catch (NotImplementedException)//WatiN throw this for FireFox
				{
					return new Rect(-1112, 1, 0, 0);
				}
			   

				//System.Drawing.Rectangle rect = htmlElement.Ancestor("body").NativeElement.GetElementBounds();

				Rect res = new Rect(rect.Left,
					rect.Top,
					rect.Right - rect.Left,
					rect.Bottom - rect.Top);

				res.Offset(parent.Parent.Layout.Left, parent.Parent.Layout.Top);
				return res;
			}
		}

		public override void SetFocus()
		{
			parent.SetFocus();
		}
	   

		/// <summary>
		/// Call Watin InnerText. (Retrieve the string between the open tag <X> to the close tag </X>).
		/// </summary>
		/// <returns>The text between the control tags (e.g.) <TD> </TD></returns>
		[Category("Watin")]
		public virtual string InnerText
		{
			get { return docElement.Html; }
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
				if (children == null)
				{
					children = new List<UIControlBase>();

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Areas));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Buttons));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.CheckBoxes));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Divs));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Forms));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Frames));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Images));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Labels));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Links));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Paras));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.RadioButtons));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.SelectLists));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Spans));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.TableBodies));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.TableCells));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.TableRows));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.Tables));

					children.Add(new WatinBaseControl(this, WatinBaseTypes.TextFields));
				}
				return children;
			}
		}


		#endregion

			 
		#region Private Fields
		private Frame docElement;

		public Frame DocElement
		{
			get { return docElement; }
			set { docElement = value; }
		}
		
		#endregion



		#region IControlLocator Members

		public UIControlBase GetControlFromCursor()
		{
			throw new NotImplementedException();
		}

		public WatinControl GetControlFromPoint(int x , int y)
		{
		   INativeElement htmlElem = docElement.NativeDocument.ElementFromPoint(x - docElement.NativeDocument.ContainingFrameElement.GetAbsElementBounds().Left, y - docElement.NativeDocument.ContainingFrameElement.GetAbsElementBounds().Top);
		   return CreateWatinControl(htmlElem);
		}

		public UIControlBase GetFocusedElement()
		{
			throw new NotImplementedException();
		}

		private WatinControl CreateWatinControl(INativeElement htmlElem)
		{
			Element watElem;
			WatinBaseControl watBaseTypeElem;
			int watElemIndex = 0;
			int sourceIdx = htmlElem.GetElementSourceIndex();
			string getByTag = htmlElem.TagName;

			switch (getByTag)
			{
				case "AREA":
					watElem = new Area( docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Areas);
					IEnumerator<Area> areaEnum = docElement.Areas.GetEnumerator();

					while (areaEnum.MoveNext())
					{
						if (areaEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

				case "INPUT":
					string byType = htmlElem.GetAttributeValue("type");
					switch (byType)
					{
						case "submit":
						case "reset":
						case "button":
							watElem = new WatiN.Core.Button(docElement.DomContainer, htmlElem);
							watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Buttons);
							IEnumerator<WatiN.Core.Button> buttonEnum = docElement.Buttons.GetEnumerator();

							while (buttonEnum.MoveNext())
							{
								if (buttonEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
									break;
								watElemIndex++;
							}
							return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

						case "checkbox":
							watElem = new WatiN.Core.CheckBox(docElement.DomContainer, htmlElem);
							watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.CheckBoxes);
							IEnumerator<WatiN.Core.CheckBox> cbEnum = docElement.CheckBoxes.GetEnumerator();

							while (cbEnum.MoveNext())
							{
								if (cbEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
									break;
								watElemIndex++;
							}
							return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

						case "file":
							watElem = new FileUpload(docElement.DomContainer, htmlElem);
							watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.FileUploads);
							IEnumerator<FileUpload> fuEnum = docElement.FileUploads.GetEnumerator();

							while (fuEnum.MoveNext())
							{
								if (fuEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
									break;
								watElemIndex++;
							}
							return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
						case "hidden":
						case "password":
						case "text":
						case "textarea":
							watElem = new TextField(docElement.DomContainer, htmlElem);
							watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TextFields);
							IEnumerator<TextField> tfEnum = docElement.TextFields.GetEnumerator();

							while (tfEnum.MoveNext())
							{
								if (tfEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
									break;
								watElemIndex++;
							}
							return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
						case "radio":
							watElem = new WatiN.Core.RadioButton(docElement.DomContainer, htmlElem);
							watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.RadioButtons);
							IEnumerator<WatiN.Core.RadioButton> rbEnum = docElement.RadioButtons.GetEnumerator();

							while (rbEnum.MoveNext())
							{
								if (rbEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
									break;
								watElemIndex++;
							}
							return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
					}
					break;
				case "DIV":
					watElem = new Div(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Divs);
					IEnumerator<Div> divEnum = docElement.Divs.GetEnumerator();

					while (divEnum.MoveNext())
					{
						if (divEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "FORM":
					watElem = new WatiN.Core.Form(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Forms);
					IEnumerator<WatiN.Core.Form> formEnum = docElement.Forms.GetEnumerator();

					while (formEnum.MoveNext())
					{
						if (formEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "FRAME":
				case "IFRAME":
					//watFrame = new Frame(browser.DomContainer, htmlElem);
					//watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Forms);
					//IEnumerator<Form> formEnum = browser.Forms.GetEnumerator();

					//while (formEnum.MoveNext())
					//{
					//	  if (formEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
					//		  break;
					//	  watElemIndex++;
					//}
					//return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
					break;
				case "IMG":
					watElem = new Image(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Images);
					IEnumerator<Image> imgEnum = docElement.Images.GetEnumerator();

					while (imgEnum.MoveNext())
					{
						if (imgEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "LABLE":
					watElem = new WatiN.Core.Label(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Labels);
					IEnumerator<WatiN.Core.Label> lblEnum = docElement.Labels.GetEnumerator();

					while (lblEnum.MoveNext())
					{
						if (lblEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "A":
					watElem = new Link(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Links);
					IEnumerator<Link> lnkEnum = docElement.Links.GetEnumerator();

					while (lnkEnum.MoveNext())
					{
						if (lnkEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

				case "P":
					watElem = new Para(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Paras);
					IEnumerator<Para> paraEnum = docElement.Paras.GetEnumerator();

					while (paraEnum.MoveNext())
					{
						if (paraEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "SELECT":
					watElem = new SelectList(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.SelectLists);
					IEnumerator<SelectList> slEnum = docElement.SelectLists.GetEnumerator();

					while (slEnum.MoveNext())
					{
						if (slEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "SPAN":
					watElem = new Span(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Spans);
					IEnumerator<Span> spanEnum = docElement.Spans.GetEnumerator();

					while (spanEnum.MoveNext())
					{
						if (spanEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TBODY":
					watElem = new TableBody(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TableBodies);
					IEnumerator<TableBody> tbEnum = docElement.TableBodies.GetEnumerator();

					while (tbEnum.MoveNext())
					{
						if (tbEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TD":
					watElem = new TableCell(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TableCells);
					IEnumerator<TableCell> tdEnum = docElement.TableCells.GetEnumerator();

					while (tdEnum.MoveNext())
					{
						if (tdEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TR":
					watElem = new TableRow(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TableRows);
					IEnumerator<TableRow> trEnum = docElement.TableRows.GetEnumerator();

					while (trEnum.MoveNext())
					{
						if (trEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TABLE":
					watElem = new Table(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Tables);
					IEnumerator<Table> tableEnum = docElement.Tables.GetEnumerator();

					while (tableEnum.MoveNext())
					{
						if (tableEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TEXTAREA":
					watElem = new TextField(docElement.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TextFields);
					IEnumerator<TextField> taEnum = docElement.TextFields.GetEnumerator();

					while (taEnum.MoveNext())
					{
						if (taEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
							break;
						watElemIndex++;
					}
					return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

				default:
					return null;


			}

			return null;
		}

		#endregion
	}
}
