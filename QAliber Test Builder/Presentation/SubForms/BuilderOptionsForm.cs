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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;

namespace QAliber.Builder.Presentation.SubForms
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