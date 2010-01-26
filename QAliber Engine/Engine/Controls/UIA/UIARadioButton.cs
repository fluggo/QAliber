using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIARadioButton	class represents a RadioButton	control (wrapper to the UI Automation RadioButton  control)
	/// in a user-interface under windows OS.
	/// </summary>
	public class UIARadioButton : UIAControl, ISelectable
	{
		/// <summary>
		/// Ctor to initiate a UIARadioButton wrapper to the UI automation RadioButton control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation RadioButton element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIARadioButton(AutomationElement element)
			: base(element)
		{

		}

		#region ISelectable Members
		/// <summary>
		/// Select the radio button.
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///    UIARadioButton selectBin = calcWin[@"Bin", @"Button", @"309"] as UIARadioButton;
		///    UIARadioButton selectDec = calcWin[@"Dec", @"Button", @"307"] as UIARadioButton;
		///    if (!selectBin.IsSelected)
		///		   selectBin.Select();
		///
		///    System.Threading.Thread.Sleep(3000);
		///    MessageBox.Show("Change selection back to Dec");
		///    selectDec.Select();
		/// </code>
		/// </example>
		public void Select()
		{
			PatternsExecutor.Select(this);
		}

		public void AddToSelection()
		{
			PatternsExecutor.AddToSelection(this);
		}

		public void RemoveFromSelection()
		{
			PatternsExecutor.RemoveFromSelection(this);
		}
		/// <summary>
		/// Verify if the radio button currently selected.
		/// </summary>
		/// <example>
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///    UIARadioButton selectBin = calcWin[@"Bin", @"Button", @"309"] as UIARadioButton;
		///    UIARadioButton selectDec = calcWin[@"Dec", @"Button", @"307"] as UIARadioButton;
		///    if (!selectBin.IsSelected)
		///		   selectBin.Select();
		///
		///    System.Threading.Thread.Sleep(3000);
		///    MessageBox.Show("Change selection back to Dec");
		///    selectDec.Select();
		/// </code>
		/// </example>
		/// <returns>True if the radio button is selected, otherwise false </returns>
		[Category("List")]
		[DisplayName("Is Selected ?")]
		public bool IsSelected
		{
			get { return PatternsExecutor.IsSeleced(this); }
		}

		#endregion

	}
}
