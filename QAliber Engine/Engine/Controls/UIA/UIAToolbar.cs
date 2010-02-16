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
using QAliber.Engine.Patterns;
using System.ComponentModel;
using System.Windows.Automation;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Represents toolbar control in windows.
	/// </summary>
	public class UIAToolbar : UIAControl, IExpandable, ITransform
	{
		public UIAToolbar(AutomationElement element)
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
