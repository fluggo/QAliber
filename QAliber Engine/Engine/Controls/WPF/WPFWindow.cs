using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace QAliber.Engine.Controls.WPF
{
	[Serializable]
	public class WPFWindow : WPFControl
	{
		public WPFWindow(FrameworkElement fwe, UpdateMethod updateMethod) : base(fwe, updateMethod)
		{

		}

		public new int Handle
		{
			get
			{
				return handle;
			}
			set
			{
				handle = value;
			}
		}

	}
}
