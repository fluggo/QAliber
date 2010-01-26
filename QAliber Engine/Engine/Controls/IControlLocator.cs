using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace QAliber.Engine.Controls
{
	public interface IControlLocator
	{
		UIControlBase GetControlFromCursor();
		UIControlBase GetControlFromPoint(Point pt);
		UIControlBase GetFocusedElement();

	}
}
