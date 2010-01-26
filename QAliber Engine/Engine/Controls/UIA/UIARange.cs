using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIRange class represents a ProgressBar control (wrapper to the UI Automation ProgressBar control)
	/// in a user-interface under windows OS.
	/// </summary>
	public class UIARange : UIAControl, IRangeValue, IText
	{
		/// <summary>
		/// Ctor to initiate a UIRange wrapper to the UI automation ProgressBar control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation ProgressBar element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIARange(AutomationElement element)
			: base(element)
		{

		}

		#region IRangeValue Members
		/// <summary>
		/// Get current value of the range.
		/// </summary>
		/// <example>
		/// <code>
		///  System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("sndrec32");
		///    UIAWindow sndRecWin = Desktop.UIA[@"Sound - Sound Recorder", @"SoundRec", @"UIAWindow"] as UIAWindow;
		///    UIAButton recordButton = sndRecWin[@"", @"Button", @"209"] as UIAButton;
		///    UIRange recordRange = sndRecWin[@"", @"msctls_trackbar32", @"204"] as UIRange;
		///    MessageBox.Show("Before record: \n Min volume: " + recordRange.MinimumValue	+ ", Current volume: " + recordRange.Value + " ,Max volume: " + recordRange.MaximumValue);
		///    recordButton.Click();
		///    //wait for 4 while recording
		///    System.Threading.Thread.Sleep(4000);
		///    MessageBox.Show("After record: \n Min volume: " + recordRange.MinimumValue + ", Current volume: " + recordRange.Value + " ,Max volume: " + recordRange.MaximumValue);
		///    recoderProc.Kill();
		/// </code>
		/// </example>
		/// <returns>double representation of the current poisition relative to the minimum value </returns>
		[Category("Range")]
		public double Value
		{
			get { return PatternsExecutor.GetRange(automationElement); }
		}
		/// <summary>
		/// Get current value of the range.
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("sndrec32");
		///    UIAWindow sndRecWin = Desktop.UIA[@"Sound - Sound Recorder", @"SoundRec", @"UIAWindow"] as UIAWindow;
		///    UIAButton recordButton = sndRecWin[@"", @"Button", @"209"] as UIAButton;
		///    UIRange recordRange = sndRecWin[@"", @"msctls_trackbar32", @"204"] as UIRange;
		///    MessageBox.Show("Before record: \n Min volume: " + recordRange.MinimumValue	+ ", Current volume: " + recordRange.Value + " ,Max volume: " + recordRange.MaximumValue);
		///    recordButton.Click();
		///    //wait for 4 while recording
		///    System.Threading.Thread.Sleep(4000);
		///    MessageBox.Show("After record: \n Min volume: " + recordRange.MinimumValue + ", Current volume: " + recordRange.Value + " ,Max volume: " + recordRange.MaximumValue);
		///    recoderProc.Kill();
		/// </code>
		/// </example>
		/// <returns>double representation of the maximum value allowed</returns>
		[Category("Range")]
		[DisplayName("Maximum Value")]
		public double MaximumValue
		{
			get { return PatternsExecutor.GetRangeMax(automationElement); }
		}
		/// <summary>
		/// Get current value of the range.
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process recoderProc = System.Diagnostics.Process.Start("sndrec32");
		///    UIAWindow sndRecWin = Desktop.UIA[@"Sound - Sound Recorder", @"SoundRec", @"UIAWindow"] as UIAWindow;
		///    UIAButton recordButton = sndRecWin[@"", @"Button", @"209"] as UIAButton;
		///    UIRange recordRange = sndRecWin[@"", @"msctls_trackbar32", @"204"] as UIRange;
		///    MessageBox.Show("Before record: \n Min volume: " + recordRange.MinimumValue	+ ", Current volume: " + recordRange.Value + " ,Max volume: " + recordRange.MaximumValue);
		///    recordButton.Click();
		///    //wait for 4 while recording
		///    System.Threading.Thread.Sleep(4000);
		///    MessageBox.Show("After record: \n Min volume: " + recordRange.MinimumValue + ", Current volume: " + recordRange.Value + " ,Max volume: " + recordRange.MaximumValue);
		///    recoderProc.Kill();
		/// </code>
		/// </example>
		/// <returns>double representation of the minimum value for the range control</returns>
		[Category("Range")]
		[DisplayName("Minimum Value")]
		public double MinimumValue
		{
			get { return PatternsExecutor.GetRangeMin(automationElement); }
		}

		#endregion

		#region IText Members
		/// <summary>
		/// Retrieve the text string from the range control
		/// </summary>
		/// <example>
		/// <code>
		/// string txt = control.Text;
		/// </code>
		/// </example>
		/// <returns>string of the text the control provide for the current range value </returns>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion
	}

	
}
