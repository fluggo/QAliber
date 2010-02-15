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
	public class WriteOnImage : OperateOnImage
	{
		public WriteOnImage()
			: base()
		{
			name = "Send Keys To Image";
			icon = Properties.Resources.Keyboard;
			actionType = QAliber.Engine.ControlActionType.Write;

		}

		[Category("Keyboard")]
		[Description("The keys to send to the image")]
		public string Text 
		{
			get { return keys; }
			set { keys = value; }
		}
	}


}
