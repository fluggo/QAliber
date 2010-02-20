using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Builder.Presentation.SubForms
{
	public partial class FindForm : Form
	{
		public FindForm()
		{
			InitializeComponent();
		}

		public FindForm(ScenarioControl sc) : this()
		{
			this.sc = sc;
			
		}

		private void FillCombo()
		{
			if (sc.SelectedTestCase != null)
			{
				cmbFindProperty.Items.Clear();
				foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(sc.SelectedTestCase))
				{
					if (prop.IsBrowsable)
						cmbFindProperty.Items.Add(prop.DisplayName);
				}
			}
		}

		private void cmbFindProperty_DropDown(object sender, EventArgs e)
		{
			FillCombo();
		}


		private void btnFind_Click(object sender, EventArgs e)
		{
			sc.FindNextPropertyValuePair(cmbFindProperty.Text, txtValueFind.Text, chkExactMatch.Checked);
		}

		private void btnFindPrev_Click(object sender, EventArgs e)
		{
			sc.FindPrevPropertyValuePair(cmbFindProperty.Text, txtValueFind.Text, chkExactMatch.Checked);
		}

		private ScenarioControl sc;

			  
		
	}
}
