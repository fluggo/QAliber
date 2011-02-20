using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;

namespace QAliber.Runner
{
	public partial class BuilderOptionsForm : Form
	{
		public BuilderOptionsForm()
		{
			InitializeComponent();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.TestCasesAssemblyDir = folderBrowserDialog.SelectedPath;
			}

		}

		private void btnBrowseLog_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.LogLocation = folderBrowserDialog.SelectedPath;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.AnimateCursor = QAliber.Engine.PlayerConfig.Default.AnimateMouseCursor;
			Properties.Settings.Default.DelayAfterAction = (uint)QAliber.Engine.PlayerConfig.Default.DelayAfterAction;
			Properties.Settings.Default.ControlAutoWaitTimeout = (uint)QAliber.Engine.PlayerConfig.Default.AutoWaitForControl;
			Properties.Settings.Default.BlockUserInput = QAliber.Engine.PlayerConfig.Default.BlockUserInput;
			Properties.Settings.Default.Save();

			TestController.Default.LogDirectoryStructure = Properties.Settings.Default.LogStructure;
			TestController.Default.LogPath = Properties.Settings.Default.LogLocation;
			TestController.Default.RemoteAssemblyDirectory = Properties.Settings.Default.TestCasesAssemblyDir;

			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		

	   
	}
}