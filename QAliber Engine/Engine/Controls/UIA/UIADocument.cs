using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIADocument class represents a document (long text controls like can be found on windows notepad)
	/// in a user-interface under windows OS.
	/// </summary>
	public class UIADocument : UIAControl, IText, IScrollable
	{
		/// <summary>
		/// Ctor to initiate a UIADocument wrapper to the UI automation Document control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation Document element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIADocument(AutomationElement element)
			: base(element)
		{

		}

		#region IText Members
		/// <summary>
		/// Retrieve the text from the document control.
		/// </summary>
		/// <example>
		/// Launch notepad and reat text.
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIADocument doc = notepadWin[@"", @"Edit", @"15"] as UIADocument;
		///    doc.Write("Hello World");
		///    MessageBox.Show(doc.Text);
		/// </code>
		/// </example>
		/// <returns>string of the Text or an empty string if no text found </returns>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion

		#region IScrollable Members
		/// <summary>
		/// Scroll the document, Horizontal and Vertical, scroller by % from the top / left
		/// </summary>
		/// <param name="horPercents"></param>
		/// <param name="verPercents"></param>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIADocument doc = notepadWin[@"", @"Edit", @"15"] as UIADocument;
		///    doc.Write("Start");
		///    for (int idx = 100; idx > 100; idx--)
		///		   doc.Write("{ENTER}");
		///    doc.Write("End");
		///    //When done scroll 10% from the top, note: since no Horizontal scroller
		///    //it is set to a negative (otherwise this will fail)
		///    doc.Scroll(-1,10);
		/// </code>
		/// </example>
		/// <remarks>If scroller is not visible, be sure to set the value to negative</remarks>
		public void Scroll(double horPercents, double verPercents)
		{
			PatternsExecutor.Scroll(this, horPercents, verPercents);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UAIDocument doc = notepadWin[@"", @"Edit", @"15"] as UAIDocument;
		///    MessageBox.Show("On clean document no scrollers so, can scroll hor or ver is " 
		///		   + doc.CanScrollHorizontal + " and " + doc.CanScrollVertical);
		///
		///    doc.Write("Start");
		///		
		///		for (int idx = 100; idx > 0; idx--)
		///		   doc.Write("{ENTER}");
		///
		///    doc.Write("End");
		///    MessageBox.Show("after writing some text horrizontal is still " + doc.CanScrollHorizontal + " but vertical now is: " + doc.CanScrollVertical + ". Press ok to scroll up");
		///    //When done scroll 10% from the top, note: since no Horizontal scroller
		///    //it is set to a negative (otherwise this will fail)
		///    doc.Scroll(-1,10);
		/// </code>
		/// </example>
		[Category("Scroll")]
		[DisplayName("Can Scroll Horizontally ?")]
		public bool CanScrollHorizontal
		{
			get { return PatternsExecutor.CanHorScroll(this); }
		}
		/// <summary>
		/// Check if the document can be scrolled Vertically
		/// </summary>
		/// <example>
		/// <code>
		///		System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIADocument doc = notepadWin[@"", @"Edit", @"15"] as UIADocument;
		///    MessageBox.Show("On clean document no scrollers so, can scroll hor or ver is " 
		///		   + doc.CanScrollHorizontal + " and " + doc.CanScrollVertical);
		///
		///    doc.Write("Start");
		///		
		///		for (int idx = 100; idx > 0; idx--)
		///		   doc.Write("{ENTER}");
		///
		///    doc.Write("End");
		///    MessageBox.Show("after writing some text horrizontal is still " + doc.CanScrollHorizontal + " but vertical now is: " + doc.CanScrollVertical + ". Press ok to scroll up");
		///    //When done scroll 10% from the top, note: since no Horizontal scroller
		///    //it is set to a negative (otherwise this will fail)
		///    doc.Scroll(-1,10);
		/// </code>
		/// </example>
		[Category("Scroll")]
		[DisplayName("Can Scroll Vertically ?")]
		public bool CanScrollVertical
		{
			get { return PatternsExecutor.CanVerScroll(this); }
		}
		/// <summary>
		/// Get the current position of the horizental scroller, in % from left.
		/// </summary>
		/// <example>
		/// This example will launce notepad to demostrae the scroller functionality.
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIADocument doc = notepadWin[@"", @"Edit", @"15"] as UIADocument;
		///   doc.Write("Start");
		///    for (int idx = 100; idx > 0; idx--)
		///    {
		///		   doc.Write("{ENTER}");
		///		   if (idx % 50 == 0)
		///		   {
		///			   MessageBox.Show("vertical % is: " + doc.VericalPercents + "\n Horizental % is: " + doc.HorizontalPercents);
		///		   }
		///    }
		///    for (int idx = 50; idx > 0; idx--)
		///    {
		///		   doc.Write("{TAB}a");
		///		   if (idx % 25 == 0)
		///		   {
		///			   MessageBox.Show("vertical % is: " + doc.VericalPercents + "\n Horizental % is: " + doc.HorizontalPercents);
		///		   }
		///    }
		///    MessageBox.Show("Scroll back");
		///		   doc.Scroll(20, 10);
		///		   MessageBox.Show("vertical % is: " + doc.VericalPercents + "\n Horizental % is: " + doc.HorizontalPercents);
		/// </code>
		/// </example>
		/// <returns>-1 if no scroller,else the position in double</returns>
		[Category("Scroll")]
		[DisplayName("Horizontal Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double HorizontalPercents
		{
			get { return PatternsExecutor.GetHorScrollPercents(this); }
		}

		/// <summary>
		/// Get the current position of the vertical scroller, in % from top.
		/// </summary>
		/// <example>
		/// This example will launce notepad to demostrae the scroller functionality.
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    UIADocument doc = notepadWin[@"", @"Edit", @"15"] as UIADocument;
		///   doc.Write("Start");
		///    for (int idx = 100; idx > 0; idx--)
		///    {
		///		   doc.Write("{ENTER}");
		///		   if (idx % 50 == 0)
		///		   {
		///			   MessageBox.Show("vertical % is: " + doc.VericalPercents + "\n Horizental % is: " + doc.HorizontalPercents);
		///		   }
		///    }
		///    for (int idx = 50; idx > 0; idx--)
		///    {
		///		   doc.Write("{TAB}a");
		///		   if (idx % 25 == 0)
		///		   {
		///			   MessageBox.Show("vertical % is: " + doc.VericalPercents + "\n Horizental % is: " + doc.HorizontalPercents);
		///		   }
		///    }
		///    MessageBox.Show("Scroll back");
		///		   doc.Scroll(20, 10);
		///		   MessageBox.Show("vertical % is: " + doc.VericalPercents + "\n Horizental % is: " + doc.HorizontalPercents);
		/// </code>
		/// </example>
		/// <returns>-1 if no scroller,else the position in double</returns>
		[Category("Scroll")]
		[DisplayName("Vertical Scroll Position")]
		[Description("The position is in percents (0-100)")]
		public double VericalPercents
		{
			get { return PatternsExecutor.GetVerScrollPercents(this); }
		}

		#endregion
	}
}
