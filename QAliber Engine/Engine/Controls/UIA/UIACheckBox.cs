using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation;
using QAliber.Engine.Patterns;
using System.ComponentModel;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Represents	a checkbox wrapper over UUIAutomation.
	/// Use this to handle checkbox controls.
	/// Checkbox controls can be handled with click, or check/uncheck actions.
	/// Note unlike clicks, check/uncheck doesn't simulate mouse movement and doesn't call OnClick events.
	/// </summary>
	public class UIACheckBox : UIAControl, IToggle
	{
		/// <summary>
		/// Ctor to initiate a UIACheckBox wrapper to the UI automation checkbox control 
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation button element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIACheckBox(AutomationElement element)
			: base(element)
		{

		}

		#region IToggle Members
		/// <summary>
		/// Check (without click) the checkbox control
		/// </summary>
		/// <example>
		/// Launch calc and check the Inv checkbox
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///    UIACheckBox invCB = calcWin[@"Inv", @"Button", @"140"] as UIACheckBox;
		///    invCB.Check();
		/// </code>
		/// </example>
		/// <remarks>Note the check action is done without click (you can verify check state and click)
		/// so click events wont be fired only onCheck events.
		/// </remarks>
		public void Check()
		{
			if (CheckState != ToggleState.On)
				Toggle();
		}
		/// <summary>
		/// UnCheck (without click) the checkbox control
		/// </summary>
		/// <example>
		/// Launch calc and check the Inv checkbox
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///    UICheckBox invCB = calcWin[@"Inv", @"Button", @"140"] as UICheckBox;
		///    invCB.Check();
		///    MessageBox.Show("See the Inv Button is checked, now unchek it");
		///    invCB.UnCheck();
		/// </code>
		/// </example>
		/// <remarks>Note the check action is done without click (you can verify check state and click)
		/// so click events wont be fired only onCheck events.
		/// </remarks>
		public void UnCheck()
		{
			if (CheckState != ToggleState.Off)
				Toggle();
		}

		public void Toggle()
		{
			PatternsExecutor.Toggle(automationElement);
		}
		/// <summary>
		/// Verify the check state of the checkbox.
		/// </summary>
		/// <example>
		/// This code will check / uncheck Inv button in calc, based on the check state
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///    UIACheckBox invCB = calcWin[@"Inv", @"Button", @"140"] as UIACheckBox;
		///    if (invCB.CheckState == System.Windows.Automation.ToggleState.Off)
		///    {
		///		   invCB.Check();
		///		   MessageBox.Show("Button is checked, now unchek it");
		///    }
		///    if (invCB.CheckState == System.Windows.Automation.ToggleState.On)
		///    {
		///		   invCB.UnCheck();
		///   }
		/// </code>
		/// </example>
		/// <remarks>Note the check state is retrieved by using the windows UI Automation</remarks>
		[Category("Toggle")]
		[DisplayName("Check State")]
		public ToggleState CheckState
		{
			get { return PatternsExecutor.GetToggleState(automationElement); }
		}

		#endregion
	   

	}
}
