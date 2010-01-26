namespace QAliber.Builder.Presentation.SubForms
{
	partial class MacroRecorderForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroRecorderForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRecord = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.macroRecordingsDataSet = new QAliber.Recorder.MacroRecorder.MacroRecordingsDataSet();
			this.macroBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Action = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Key = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.pressedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.origIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panelLow = new System.Windows.Forms.Panel();
			this.btnOk = new System.Windows.Forms.Button();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.macroRecordingsDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.macroBindingSource)).BeginInit();
			this.panelLow.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButtonPlay,
			this.toolStripButtonRecord,
			this.toolStripButtonPause,
			this.toolStripButtonStop,
			this.toolStripSeparator1,
			this.toolStripButtonSave,
			this.toolStripButtonLoad});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(620, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// toolStripButtonPlay
			// 
			this.toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPlay.Enabled = false;
			this.toolStripButtonPlay.Image = global::QAliber.Builder.Presentation.Properties.Resources.DebugPlay;
			this.toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPlay.Name = "toolStripButtonPlay";
			this.toolStripButtonPlay.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPlay.Text = "Play Macro";
			this.toolStripButtonPlay.Click += new System.EventHandler(this.toolStripButtonPlay_Click);
			// 
			// toolStripButtonRecord
			// 
			this.toolStripButtonRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecord.Image")));
			this.toolStripButtonRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRecord.Name = "toolStripButtonRecord";
			this.toolStripButtonRecord.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRecord.Text = "Record";
			this.toolStripButtonRecord.Click += new System.EventHandler(this.toolStripButtonRecord_Click);
			// 
			// toolStripButtonPause
			// 
			this.toolStripButtonPause.CheckOnClick = true;
			this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPause.Enabled = false;
			this.toolStripButtonPause.Image = global::QAliber.Builder.Presentation.Properties.Resources.Pause;
			this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPause.Name = "toolStripButtonPause";
			this.toolStripButtonPause.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPause.Text = "Pause";
			this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
			// 
			// toolStripButtonStop
			// 
			this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonStop.Enabled = false;
			this.toolStripButtonStop.Image = global::QAliber.Builder.Presentation.Properties.Resources.Stop;
			this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonStop.Name = "toolStripButtonStop";
			this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonStop.Text = "Stop";
			this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonSave
			// 
			this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSave.Image = global::QAliber.Builder.Presentation.Properties.Resources.Save;
			this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSave.Name = "toolStripButtonSave";
			this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSave.Text = "Save";
			this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
			// 
			// toolStripButtonLoad
			// 
			this.toolStripButtonLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonLoad.Image = global::QAliber.Builder.Presentation.Properties.Resources.Open;
			this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonLoad.Name = "toolStripButtonLoad";
			this.toolStripButtonLoad.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonLoad.Text = "Load";
			this.toolStripButtonLoad.Click += new System.EventHandler(this.toolStripButtonLoad_Click);
			// 
			// dataGridView
			// 
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridView.AutoGenerateColumns = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.timeDataGridViewTextBoxColumn,
			this.xDataGridViewTextBoxColumn,
			this.yDataGridViewTextBoxColumn,
			this.Action,
			this.Key,
			this.pressedDataGridViewCheckBoxColumn,
			this.origIndexDataGridViewTextBoxColumn});
			this.dataGridView.DataSource = this.macroBindingSource;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(255)))), ((int)(((byte)(210)))));
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView.DefaultCellStyle = dataGridViewCellStyle7;
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 25);
			this.dataGridView.Name = "dataGridView";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.dataGridView.Size = new System.Drawing.Size(620, 497);
			this.dataGridView.TabIndex = 1;
			this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Macro Recordings|*.macro|All files|*.*";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Macro Recordings|*.macro|All files|*.*";
			// 
			// macroRecordingsDataSet
			// 
			this.macroRecordingsDataSet.DataSetName = "MacroRecordingsDataSet";
			this.macroRecordingsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// macroBindingSource
			// 
			this.macroBindingSource.DataMember = "MacroEntries";
			this.macroBindingSource.DataSource = this.macroRecordingsDataSet;
			// 
			// timeDataGridViewTextBoxColumn
			// 
			this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
			this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
			this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
			// 
			// xDataGridViewTextBoxColumn
			// 
			this.xDataGridViewTextBoxColumn.DataPropertyName = "X";
			this.xDataGridViewTextBoxColumn.HeaderText = "X";
			this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
			// 
			// yDataGridViewTextBoxColumn
			// 
			this.yDataGridViewTextBoxColumn.DataPropertyName = "Y";
			this.yDataGridViewTextBoxColumn.HeaderText = "Y";
			this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
			// 
			// Action
			// 
			this.Action.DataPropertyName = "Action";
			this.Action.HeaderText = "Mouse Action";
			this.Action.Name = "Action";
			this.Action.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// Key
			// 
			this.Key.DataPropertyName = "Key";
			this.Key.HeaderText = "Key";
			this.Key.Name = "Key";
			this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// pressedDataGridViewCheckBoxColumn
			// 
			this.pressedDataGridViewCheckBoxColumn.DataPropertyName = "Pressed";
			this.pressedDataGridViewCheckBoxColumn.HeaderText = "Key Pressed ?";
			this.pressedDataGridViewCheckBoxColumn.Name = "pressedDataGridViewCheckBoxColumn";
			// 
			// origIndexDataGridViewTextBoxColumn
			// 
			this.origIndexDataGridViewTextBoxColumn.DataPropertyName = "OrigIndex";
			this.origIndexDataGridViewTextBoxColumn.HeaderText = "OrigIndex";
			this.origIndexDataGridViewTextBoxColumn.Name = "origIndexDataGridViewTextBoxColumn";
			this.origIndexDataGridViewTextBoxColumn.Visible = false;
			// 
			// panelLow
			// 
			this.panelLow.Controls.Add(this.btnOk);
			this.panelLow.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelLow.Location = new System.Drawing.Point(0, 492);
			this.panelLow.Name = "panelLow";
			this.panelLow.Size = new System.Drawing.Size(620, 30);
			this.panelLow.TabIndex = 2;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(533, 3);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// MacroRecorderForm
			// 
			this.ClientSize = new System.Drawing.Size(620, 522);
			this.Controls.Add(this.panelLow);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.toolStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "MacroRecorderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Record Macro";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MacroRecorderForm_FormClosing);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.macroRecordingsDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.macroBindingSource)).EndInit();
			this.panelLow.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonRecord;
		private System.Windows.Forms.ToolStripButton toolStripButtonPause;
		private System.Windows.Forms.ToolStripButton toolStripButtonStop;
		private System.Windows.Forms.ToolStripButton toolStripButtonPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonSave;
		private System.Windows.Forms.ToolStripButton toolStripButtonLoad;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private QAliber.Recorder.MacroRecorder.MacroRecordingsDataSet macroRecordingsDataSet;
		private System.Windows.Forms.BindingSource macroBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn Action;
		private System.Windows.Forms.DataGridViewComboBoxColumn Key;
		private System.Windows.Forms.DataGridViewCheckBoxColumn pressedDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn origIndexDataGridViewTextBoxColumn;
		private System.Windows.Forms.Panel panelLow;
		private System.Windows.Forms.Button btnOk;
	}
}