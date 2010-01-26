using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QAliber.VS2005.Plugin
{
	public partial class ImageInputForm : Form
	{
		public ImageInputForm()
		{
			InitializeComponent();
		}

		public string Input { get { return txtInput.Text; } }

		private void btnOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}