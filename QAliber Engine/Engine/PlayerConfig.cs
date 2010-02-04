using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace QAliber.Engine
{
	public class PlayerConfig //: Microsoft.VisualStudio.Shell.DialogPage
	{
		public PlayerConfig()
		{

		}

		private bool webEnabled;

		[Browsable(false)]
		public bool WebEnabled
		{
			get { return webEnabled; }
			set { webEnabled = value; }
		}
	

		private int delayAfterAction = 250;

		[Category("Player")]
		[DisplayName("Delay After Action")]
		[Description("The amount of time (in miliseconds) to wait after each action")]
		public int DelayAfterAction
		{
			get { return delayAfterAction; }
			set { delayAfterAction = value; }
		}

		private bool animateMouseCursor = true;

		[Category("Player")]
		[DisplayName("Animate Mouse Cursor")]
		[Description("Should the mouse cursor move between coordinates")]
		public bool AnimateMouseCursor
		{
			get { return animateMouseCursor; }
			set { animateMouseCursor = value; }
		}

		private int autoWaitForControl = 5000;

		[Category("Player")]
		[DisplayName("Control Auto Wait Timeout")]
		[Description("The amount of time (in miliseconds) to wait for a control when accessing it")]
		public int AutoWaitForControl
		{
			get { return autoWaitForControl; }
			set { autoWaitForControl = value; }
		}

		private bool blockUserInput = false;

		[Category("Player")]
		[DisplayName("Block Input")]
		[Description("Should the mouse be blocked from the user when running when simulating mouse & kb activities")]
		public bool BlockUserInput
		{
			get { return blockUserInput; }
			set { blockUserInput = value; }
		}

		public static PlayerConfig Default
		{
			get
			{
				if (instance == null)
					instance = new PlayerConfig();
				return instance;
			}
			set { instance = value; }
		}
	
		private static PlayerConfig instance;
	}
}
