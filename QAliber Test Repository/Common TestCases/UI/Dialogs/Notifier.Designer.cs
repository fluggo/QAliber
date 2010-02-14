namespace QAliber.Repository.CommonTestCases.UI.Dialogs
{
	partial class Notifier
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notifier));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Visible = true;

		}

		#endregion

		internal System.Windows.Forms.NotifyIcon notifyIcon;

	}
}
