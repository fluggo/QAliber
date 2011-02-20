using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using System.Windows.Automation;
using System.Windows;
using WatiN.Core.Native;
using System.Threading;
using System.Diagnostics;

namespace QAliber.Engine.Controls.Watin
{
	/// <summary>
	/// Watin Browser wraps the Watin.Core.IE and Watin.Core.FireFox. Watin users would fill at home by creating a call to this QAliber element
	/// Like this:
	/// <code>
	/// WatiN.Core.IE browser = Desktop.Watin.IE("http://www.google.com/");
	/// browser.Button(Find.ByName("btnG"));
	/// </code>
	/// </summary>
	public class WatBrowser :UIControlBase
	{
		public WatBrowser(Browser b)
		{
		   browser = b;
		   pageWinElement = RetrievePageByHandle(b.hWnd);
		   // pageWinElement.Current.ProcessId
		}

		/// <summary>
		/// Page is identifued by the unique url. this allow the users to retrieve browser by URL
		/// </summary>
		public override string Name
		{
			get
			{
				return browser.Url;
			}
		}
		/// <summary>
		/// The layout of the page is actually the body of the document (all html controls are relatieve to it)
		/// </summary>
		public override Rect Layout
		{
			get
			{
				if (layout == Rect.Empty)
				{
					//Thread initDocThread = new Thread(new ThreadStart(InitUIAWorker));
					//initDocThread.Start();
					//initDocThread.Join();
					pageWinElement = RetrievePageByHandle(browser.hWnd);
					if (pageWinElement != null)
						layout = pageWinElement.Current.BoundingRectangle;
				}
				return layout;
			}
		}
		
		private void InitUIAWorker()
		{
			pageWinElement = RetrievePageByHandle(browser.hWnd);
		}

		private void ControlFromPointWorker(object pt)
		{
			lock (this)
			{
				htmlElem = browser.NativeDocument.ElementFromPoint((int)((Point)pt).X, (int)((Point)pt).Y);
			}
		   // return htmlElem;
		}

		public override Process Process
		{
			get 
			{
				if (pageWinElement == null)
					return null;
				return System.Diagnostics.Process.GetProcessById(pageWinElement.Current.ProcessId); 
			}
		}

		public override void SetFocus()
		{
			try
			{
				base.SetFocus();
				browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Show);
				browser.DomContainer.Body.Focus();
			}
			catch
			{
			}
		}

		public override string CodePath
		{
			get
			{
				switch (browserType)
				{
					case browserType.IE:
						return "Desktop.Watin.IE(\"" + browser.Url + "\")";
					case browserType.FireFox:
						return "Desktop.Watin.FF(\"" + browser.Url + "\")";
					default:
						return "Desktop.Watin.Chrome(\"" + browser.Url + "\")";
				}
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

		public override UIControlBase Parent
		{
			get
			{
				return Desktop.Watin;
			}
			set
			{
				base.Parent = value;
			}
		}
		/// <summary>
		/// Get the page AutomationElement control, set the type (FireFox or IE)
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		internal AutomationElement RetrievePageByHandle(IntPtr handle)
		{
			pageWinElement = AutomationElement.FromHandle(handle);
			AutomationElement ieElement = null;
			if (pageWinElement != null)
			{
				if (pageWinElement.Current.ClassName == "MozillaUIWindowClass")
				{
					browserType = browserType.FireFox;
					ieElement = pageWinElement.FindFirst(TreeScope.Descendants,
				   new PropertyCondition(AutomationElement.ClassNameProperty, "MozillaContentWindowClass"));
				}
				else
				{
					try
					{
						ieElement = pageWinElement.FindFirst(TreeScope.Descendants,
							 new PropertyCondition(AutomationElement.ClassNameProperty, "Internet Explorer_Server"));
					}
					catch { }
				}

				if (ieElement != null)
					return ieElement;
			}
			return null;
		}

		internal browserType BrowserType
		{
			get { return browserType; }
			set { browserType = value; }
		}

		public Browser BrowserPage
		{
			get { return browser; }
			set { browser = value; }
		}
		/// <summary>
		/// Retrieve the html eleme from document by x,y position in the doc.
		/// Create a Watin Element and by finding the control type and index create the WatingControl.
		/// </summary>
		/// <param name="x">X positing of the control relative to the document (body, upper left it 0,0)</param>
		/// <param name="y">Y positing of the control relative to the document (body, upper left it 0,0)</param>
		/// <returns>Watin control with known groupe type and index inside the group</returns>
		public WatinControl GetElementFromPoint(int x, int y)
		{
			 WatinControl wat;
		   //  Point er = new Point(x,y);
		   //Thread th = new System.Threading.Thread(new ParameterizedThreadStart(ControlFromPointWorker));
		   // th.Start(er);
		   // th.Join();
	   
		  INativeElement htmlElem = browser.NativeDocument.ElementFromPoint(x,y);
			if (htmlElem.TagName.Contains("FRAME"))
			{
				int sourceIdx = htmlElem.GetElementSourceIndex();
				WatinBaseControl watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Frames);

				IEnumerator<Frame> frmEnum = browser.Frames.GetEnumerator();

				while (frmEnum.MoveNext())
				{
				   INativeElement htmlElem2 =  frmEnum.Current.NativeDocument.ContainingFrameElement;
				   if (sourceIdx == htmlElem2.GetElementSourceIndex())
					{
						WatFrame frameParent = new WatFrame(new WatinBaseControl(this, WatinBaseTypes.Frames), frmEnum.Current, sourceIdx);

						//htmlElem = frmEnum.Current.NativeDocument.ElementFromPoint(x - htmlElem2.GetAbsElementBounds().Left, y - htmlElem2.GetAbsElementBounds().Top);
						//wat = CreateWatinControl(htmlElem);
						return frameParent.GetControlFromPoint(x,y);
					}
				}
			}

		   wat = CreateWatinControl(htmlElem);
			return wat;
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
					watElem = new Area(browser.DomContainer, htmlElem);
					watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Areas);
					IEnumerator<Area> areaEnum = browser.Areas.GetEnumerator();

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
								watElem = new Button(browser.DomContainer, htmlElem);
								watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Buttons);
								IEnumerator<Button> buttonEnum = browser.Buttons.GetEnumerator();
								
								while (buttonEnum.MoveNext())
								{
									if (buttonEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
										break;
									watElemIndex++;
								}
								return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

							case "checkbox":
								watElem = new CheckBox(browser.DomContainer, htmlElem);
								watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.CheckBoxes);
								IEnumerator<CheckBox> cbEnum = browser.CheckBoxes.GetEnumerator();

								while (cbEnum.MoveNext())
								{
									if (cbEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
										break;
									watElemIndex++;
								}
								return new WatinControl(watBaseTypeElem, watElem, watElemIndex);

							case "file":
								watElem = new FileUpload(browser.DomContainer, htmlElem);
								watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.FileUploads);
								IEnumerator<FileUpload> fuEnum = browser.FileUploads.GetEnumerator();

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
								watElem = new TextField(browser.DomContainer, htmlElem);
								watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TextFields);
								IEnumerator<TextField> tfEnum = browser.TextFields.GetEnumerator();

								while (tfEnum.MoveNext())
								{
									if (tfEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
										break;
									watElemIndex++;
								}
								return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
							case "radio":
								watElem = new RadioButton(browser.DomContainer, htmlElem);
								watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.RadioButtons);
								IEnumerator<RadioButton> rbEnum = browser.RadioButtons.GetEnumerator();

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
						watElem = new Div(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Divs);
						IEnumerator<Div> divEnum = browser.Divs.GetEnumerator();
					   
						while (divEnum.MoveNext())
						{
							if (divEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "FORM":
						watElem = new Form(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Forms);
						IEnumerator<Form> formEnum = browser.Forms.GetEnumerator();

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
						watElem = new Image(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Images);
						IEnumerator<Image> imgEnum = browser.Images.GetEnumerator();

						while (imgEnum.MoveNext())
						{
							if (imgEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "LABLE":
						watElem = new Label(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Labels);
						IEnumerator<Label> lblEnum = browser.Labels.GetEnumerator();

						while (lblEnum.MoveNext())
						{
							if (lblEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "A":
						watElem = new Link(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Links);
						IEnumerator<Link> lnkEnum = browser.Links.GetEnumerator();

						while (lnkEnum.MoveNext())
						{
							if (lnkEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
			   
				case "P":
						watElem = new Para(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Paras);
						IEnumerator<Para> paraEnum = browser.Paras.GetEnumerator();

						while (paraEnum.MoveNext())
						{
							if (paraEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "SELECT":
						watElem = new SelectList(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.SelectLists);
						IEnumerator<SelectList> slEnum = browser.SelectLists.GetEnumerator();

						while (slEnum.MoveNext())
						{
							if (slEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "SPAN":
						watElem = new Span(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Spans);
						IEnumerator<Span> spanEnum = browser.Spans.GetEnumerator();

						while (spanEnum.MoveNext())
						{
							if (spanEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TBODY":
						watElem = new TableBody(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TableBodies);
						IEnumerator<TableBody> tbEnum = browser.TableBodies.GetEnumerator();

						while (tbEnum.MoveNext())
						{
							if (tbEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TD":
						watElem = new TableCell(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TableCells);
						IEnumerator<TableCell> tdEnum = browser.TableCells.GetEnumerator();

						while (tdEnum.MoveNext())
						{
							if (tdEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TR":
						watElem = new TableRow(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TableRows);
						IEnumerator<TableRow> trEnum = browser.TableRows.GetEnumerator();

						while (trEnum.MoveNext())
						{
							if (trEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TABLE":
						watElem = new Table(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.Tables);
						IEnumerator<Table> tableEnum = browser.Tables.GetEnumerator();

						while (tableEnum.MoveNext())
						{
							if (tableEnum.Current.NativeElement.GetElementSourceIndex() == sourceIdx)
								break;
							watElemIndex++;
						}
						return new WatinControl(watBaseTypeElem, watElem, watElemIndex);
				case "TEXTAREA":
						watElem = new TextField(browser.DomContainer, htmlElem);
						watBaseTypeElem = new WatinBaseControl(this, WatinBaseTypes.TextFields);
						IEnumerator<TextField> taEnum = browser.TextFields.GetEnumerator();

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

		private Element CreateElementFromNative(INativeElement htmlElem)
		{
			Element watElem;
			watElem = new Button(browser.DomContainer, htmlElem);

			return watElem;
		}
		/// <summary>
		/// In order to get the index of the control inside watinBaseGroup (if control has no name or Id, the codepath 
		/// relies on this index to uniquely id the control)
		/// </summary>
		/// <param name="sourceIdx"></param>
		/// <param name="elemEnum"></param>
		/// <returns></returns>
		private int GetWatinElementIndex(int sourceIdx, IEnumerator<Button> elemEnum)
		{
			int elemIdx = 0;
			while (elemEnum.MoveNext())
			{
				if (elemEnum.Current .NativeElement.GetElementSourceIndex() == sourceIdx) 
					break;
				elemIdx++;
			}

			return elemIdx;
		}

		private Browser browser;
		private AutomationElement pageWinElement;
		private browserType browserType = browserType.IE;
		private INativeElement htmlElem;

	 }

	enum browserType
	{
		IE,
		FireFox,
		Chrom
	}

	public enum WatinBaseTypes
	{
		Areas,
		Buttons,
		CheckBoxes,
		Divs,
		FileUploads,
		Forms,
		Frames,
		Images,
		Labels,
		Links,
		Paras,
		RadioButtons,
		SelectLists,
		Spans,
		TableBodies,
		TableCells,
		TableRows,
		Tables,
		TextFields
	}
}
