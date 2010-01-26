using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIALabel class represents a Label in a user-interface under windows OS.
	/// </summary>
	public class UIALabel : UIAControl, IText
	{
		/// <summary>
		/// Ctor to initiate a UIALabel wrapper to the UI automation HeaderItem control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation HeaderItem element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIALabel(AutomationElement element)
			: base(element)
		{

		}

		#region IText Members
		/// <summary>
		/// Return string of the text on the label.
		/// </summary>
		/// <remarks>Note: most labels dont have Text, but the string can be taken from the Name property</remarks>
		[Category("Common")]
		public string Text
		{
			get { return PatternsExecutor.GetText(automationElement); }
		}

		#endregion

		
	}
}
