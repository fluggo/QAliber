using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.Engine
{
	/// <summary>
	/// The direction of the scroll
	/// </summary>
	public enum ScrollType
	{
		Horizontal,
		Vertical
	}

	/// <summary>
	/// The action performed on a control, this passed as an argument in the event UIControlBase.OnBeforeAction
	/// </summary>
	public enum ControlActionType
	{
		MoveMouse,
		Click,
		DoubleClick,
		Drag,
		Write
	}
}
