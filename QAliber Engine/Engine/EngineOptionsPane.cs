using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Engine
{
	public partial class EngineOptionsPane : UserControl
	{
		public EngineOptionsPane()
		{
			InitializeComponent();
			playerConfigBindingSource.DataSource = PlayerConfig.Default;
		}

	}
}
