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
