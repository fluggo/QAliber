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
	public class DragFromImage : ClickOnImage
	{
		public DragFromImage()
			: base()
		{
			name = "Drag From Image";
			actionType = QAliber.Engine.ControlActionType.Drag;

		}

		[Category("Mouse")]
		[DisplayName("Drag Offset")]
		[Description("The offset in pixels (x offset, y offset) to do the drag")]
		public System.Drawing.Size Offset 
		{
			get { return dragOffset; }
			set { dragOffset = value; }
		}
	}


}
