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
 
namespace QAliber.Engine
{
	partial class EngineOptionsPane
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.playerConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.numericUpDownDelayAfter = new System.Windows.Forms.NumericUpDown();
			this.lblDelayAfterAction = new System.Windows.Forms.Label();
			this.lblAutoWait = new System.Windows.Forms.Label();
			this.numericUpDownAutoWait = new System.Windows.Forms.NumericUpDown();
			this.animateMouseCheckBox = new System.Windows.Forms.CheckBox();
			this.checkBoxBlockUserInput = new System.Windows.Forms.CheckBox();
			this.groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerConfigBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoWait)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.checkBoxBlockUserInput);
			this.groupBox.Controls.Add(this.numericUpDownDelayAfter);
			this.groupBox.Controls.Add(this.lblDelayAfterAction);
			this.groupBox.Controls.Add(this.lblAutoWait);
			this.groupBox.Controls.Add(this.numericUpDownAutoWait);
			this.groupBox.Controls.Add(this.animateMouseCheckBox);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox.Size = new System.Drawing.Size(297, 223);
			this.groupBox.TabIndex = 0;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Engine Options";
			// 
			// numericUpDownDelayAfter
			// 
			this.numericUpDownDelayAfter.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.playerConfigBindingSource, "DelayAfterAction", true));
			this.numericUpDownDelayAfter.Increment = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.numericUpDownDelayAfter.Location = new System.Drawing.Point(218, 162);
			this.numericUpDownDelayAfter.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.numericUpDownDelayAfter.Name = "numericUpDownDelayAfter";
			this.numericUpDownDelayAfter.Size = new System.Drawing.Size(57, 20);
			this.numericUpDownDelayAfter.TabIndex = 4;
			this.numericUpDownDelayAfter.ThousandsSeparator = true;
			// 
			// lblDelayAfterAction
			// 
			this.lblDelayAfterAction.AutoSize = true;
			this.lblDelayAfterAction.Location = new System.Drawing.Point(27, 164);
			this.lblDelayAfterAction.Name = "lblDelayAfterAction";
			this.lblDelayAfterAction.Size = new System.Drawing.Size(168, 13);
			this.lblDelayAfterAction.TabIndex = 3;
			this.lblDelayAfterAction.Text = "Delay After Action (in milliseconds)";
			// 
			// lblAutoWait
			// 
			this.lblAutoWait.AutoSize = true;
			this.lblAutoWait.Location = new System.Drawing.Point(27, 123);
			this.lblAutoWait.Name = "lblAutoWait";
			this.lblAutoWait.Size = new System.Drawing.Size(184, 13);
			this.lblAutoWait.TabIndex = 2;
			this.lblAutoWait.Text = "Auto Wait For Control (in milliseconds)";
			// 
			// numericUpDownAutoWait
			// 
			this.numericUpDownAutoWait.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.playerConfigBindingSource, "AutoWaitForControl", true));
			this.numericUpDownAutoWait.Increment = new decimal(new int[] {
			100,
			0,
			0,
			0});
			this.numericUpDownAutoWait.Location = new System.Drawing.Point(218, 121);
			this.numericUpDownAutoWait.Maximum = new decimal(new int[] {
			100000,
			0,
			0,
			0});
			this.numericUpDownAutoWait.Name = "numericUpDownAutoWait";
			this.numericUpDownAutoWait.Size = new System.Drawing.Size(57, 20);
			this.numericUpDownAutoWait.TabIndex = 1;
			this.numericUpDownAutoWait.ThousandsSeparator = true;
			// 
			// animateMouseCheckBox
			// 
			this.animateMouseCheckBox.AutoSize = true;
			this.animateMouseCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playerConfigBindingSource, "AnimateMouseCursor", true));
			this.animateMouseCheckBox.Location = new System.Drawing.Point(30, 30);
			this.animateMouseCheckBox.Name = "animateMouseCheckBox";
			this.animateMouseCheckBox.Size = new System.Drawing.Size(99, 17);
			this.animateMouseCheckBox.TabIndex = 0;
			this.animateMouseCheckBox.Text = "Animate Mouse";
			this.animateMouseCheckBox.UseVisualStyleBackColor = true;
			// 
			// checkBoxBlockUserInput
			// 
			this.checkBoxBlockUserInput.AutoSize = true;
			this.checkBoxBlockUserInput.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playerConfigBindingSource, "BlockUserInput", true));
			this.checkBoxBlockUserInput.Checked = true;
			this.checkBoxBlockUserInput.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxBlockUserInput.Location = new System.Drawing.Point(30, 67);
			this.checkBoxBlockUserInput.Name = "checkBoxBlockUserInput";
			this.checkBoxBlockUserInput.Size = new System.Drawing.Size(236, 30);
			this.checkBoxBlockUserInput.TabIndex = 5;
			this.checkBoxBlockUserInput.Text = "Block mouse && keyboard activities from user,\r\nwhen simulating them";
			this.checkBoxBlockUserInput.UseVisualStyleBackColor = true;
			// 
			// EngineOptionsPane
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox);
			this.Name = "EngineOptionsPane";
			this.Size = new System.Drawing.Size(297, 223);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerConfigBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoWait)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Label lblAutoWait;
		private System.Windows.Forms.NumericUpDown numericUpDownAutoWait;
		private System.Windows.Forms.CheckBox animateMouseCheckBox;
		private System.Windows.Forms.BindingSource playerConfigBindingSource;
		private System.Windows.Forms.NumericUpDown numericUpDownDelayAfter;
		private System.Windows.Forms.Label lblDelayAfterAction;
		private System.Windows.Forms.CheckBox checkBoxBlockUserInput;
	}
}
