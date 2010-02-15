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
	public class DoubleClickOnImage : ClickOnImage
	{
		public DoubleClickOnImage() : base()
		{
			name = "Double Click On Image";
			actionType = QAliber.Engine.ControlActionType.DoubleClick;
		}


	}


}
