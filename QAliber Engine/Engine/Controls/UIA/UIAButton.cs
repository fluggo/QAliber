using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;


namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// UIAButton represents a button wrapper for UIAutomation.
	/// Button may have extended functionality such Toggle, Expand.
	/// While QAliber supports these functionalities, if the button you automate doesn't support the action, 
	/// InvalidOperationException may be thrown.
	/// </summary>
	public class UIAButton : UIAControl, IInvokable, IToggle
	{
		/// <summary>
		/// Ctor to initiate a UIAButton wrapper to the UI automation button control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation button element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAButton(AutomationElement element)
			: base(element)
		{
		}

		#region IInvokable Members
		/// <summary>
		/// Use windows invocation to emulate the click on the button
		/// </summary>
		/// <example>
		/// This example uses both Click() and Invoke() calls to interact with calculator:
		/// <code>
		///		System.Diagnostics.Process.Start("calc");
		///		UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///		UIAButton but_1 = calcWin[@"1", @"Button", @"125"] as UIAButton;
		///		UIAButton but_2 = calcWin[@"2", @"Button", @"126"] as UIAButton;
		///		UIAButton but_add = calcWin[@"+", @"Button", @"92"] as UIAButton;
		///		UIAButton but_eq = calcWin[@"=", @"Button", @"112"] as UIAButton;
		///		//1+2= is done, but only +, = are clicked by the mouse, 1 and 2 are triggered by the InvokePattern
		///		but_1.Invoke();
		///		but_add.Click();
		///		but_2.Invoke();
		///		but_eq.Click();
		/// </code>
		/// </example>
		public void Invoke()
		{
			PatternsExecutor.Invoke(this);
		}

		#endregion

		#region IToggle Members
		/// <summary>
		/// Toggle the button to be checked (pressed down).
		/// Action on the control is done only when the button is unchecked.
		/// </summary>
		/// <remarks>
		/// If the button doesn't support toggling an internal error will be logged
		/// catching InvalidOperationException
		/// </remarks>
		public void Check()
		{
			if (CheckState != ToggleState.On)
				Toggle();
		}
		/// <summary>
		/// Toggle the button to be unchecked (released).
		/// Action on the control is done only when the button is checked.
		/// </summary>
		/// <remarks>
		/// If the button doesn't support toggling an internal error will be logged
		/// catching InvalidOperationException
		/// </remarks>
		public void UnCheck()
		{
			if (CheckState != ToggleState.Off)
				Toggle();
		}
		/// <summary>
		/// Some button allow toggling check / uncheck state (pressed / released)
		/// Toggle change the button state.
		/// </summary>
		/// <remarks>
		/// If the button doesn't support toggling an internal error will be logged
		/// catching InvalidOperationException
		/// </remarks>
		public void Toggle()
		{
			PatternsExecutor.Toggle(automationElement);
		}
		/// <summary>
		/// Retrieve Button Toggle state.
		/// Button has 3 Togglestate (on (Pushed) and off (Released) or Indeterminate) Checkstate  
		/// </summary>
		/// <remarks>
		/// If the button doesn't support toggling, the check stat is allways Off.
		/// </remarks>
		[Category("Toggle")]
		[DisplayName("Check State")]
		public ToggleState CheckState
		{
			get { return PatternsExecutor.GetToggleState(automationElement); }
		}

		#endregion
	}

}
