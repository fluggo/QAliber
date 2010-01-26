using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using QAliber.Engine.Patterns;
using System.Windows.Automation;

namespace QAliber.Engine.Controls.UIA
{
	public class UIAGroup : UIAControl, IExpandable
	{
		public UIAGroup(AutomationElement element)
			: base(element)
		{

		}

		#region IExpandable Members

		public void Expand()
		{
			PatternsExecutor.Expand(automationElement);
		}

		public void Collapse()
		{
			PatternsExecutor.Collapse(automationElement);
		}

		[Category("Expand / Collapse")]
		[DisplayName("Expand / Collapse State")]
		public ExpandCollapseState ExpandCollapseState
		{
			get { return PatternsExecutor.GetExpandCollapseState(automationElement); }
		}

		#endregion
	}
}
