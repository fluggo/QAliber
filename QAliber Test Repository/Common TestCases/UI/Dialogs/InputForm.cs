using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Repository.CommonTestCases.UI.Dialogs
{
	public partial class InputForm : Form
	{
		public InputForm(string title, string text)
		{
			InitializeComponent();
			this.Text = title;
			this.labelText.Text = text;
		}


		public string Input
		{
			get { return textBox.Text; }
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	
	}
}