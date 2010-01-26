using System;
using System.Collections.Generic;
using System.Text;
using QAliber.Engine.Patterns;
using System.ComponentModel;
using System.Windows.Automation;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Represents a panel control
	/// </summary>
	public class UIAPane : UIAControl, ITransform
	{
		public UIAPane(AutomationElement element)
			: base(element)
		{

		}

		#region ITransform Members

		public void Move(double x, double y)
		{
			PatternsExecutor.Move(automationElement, x, y);
		}

		public void Resize(double width, double height)
		{
			PatternsExecutor.Resize(automationElement, width, height);
		}

		public void Rotate(double degrees)
		{
			PatternsExecutor.Rotate(automationElement, degrees);
		}

		[Category("Transform")]
		[DisplayName("Can Move ?")]
		public bool CanMove
		{
			get { return PatternsExecutor.CanMove(automationElement); }
		}

		[Category("Transform")]
		[DisplayName("Can Resize ?")]
		public bool CanResize
		{
			get { return PatternsExecutor.CanResize(automationElement); }
		}

		[Category("Transform")]
		[DisplayName("Can Rotate ?")]
		public bool CanRotate
		{
			get { return PatternsExecutor.CanRotate(automationElement); }
		}

		#endregion
	}
}
