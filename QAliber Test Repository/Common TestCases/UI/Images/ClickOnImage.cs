using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using System.Drawing;
using QAliber.ImageHandling;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	public class ClickOnImage : OperateOnImage
	{
		public ClickOnImage()
		{
			name = "Click On Image";
			icon = Properties.Resources.Mouse;
			actionType = QAliber.Engine.ControlActionType.Click;
			button = MouseButtons.Left;
		}

		[Category("Mouse")]
		[DisplayName("Button")]
		[Description("The mouse button to click")]
		public MouseButtons Button
		{
			get { return button; }
			set { button = value; }
		}
	
	}


}