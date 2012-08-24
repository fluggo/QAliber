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
using QAliber.Engine.Win32;
using System.Windows.Automation;
using QAliber.Engine.Controls;
using System.IO;
using System.Runtime.InteropServices;
using QAliber.Engine.Controls.UIA;
using System.Diagnostics;

namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	public partial class UIControlLocatorForm : Form
	{
		public UIControlLocatorForm()
		{
			InitializeComponent();
		}

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		internal static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		internal static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


		public string ControlPath
		{
			get {
				return textBox.Text;
			}
			set {
				textBox.Text = value;
			}
		}

		public System.Windows.Point Coordinate
		{
			get {
				return coordinate;
			}
			set {
				coordinate = value;
				textBoxXY.Text = coordinate.ToString();
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (capturing)
			{
				switch (m.Msg)
				{
					case 0x0202: { //LButtonUp
						form.Visible = true;
						_timer.Stop();
						User32.ReleaseCapture();
						GDI32.RedrawWindow(capturedElement);
						Cursor = Cursors.Default;
						btnCursor.BackgroundImage = Properties.Resources.Crosshair;
						capturing = false;
						ShowCapturedElement();
						break;
					}
					case 0x0200: { //MouseMove
						System.Windows.Point point = new System.Windows.Point( Cursor.Position.X, Cursor.Position.Y );
						RecaptureElement( point, true );
					}

						break;
					default:
						break;


				}
			}
			base.WndProc(ref m);
		}

		private void HandleTimerTick( object sender, EventArgs e ) {
			// Simulate a mouse move over this point to activate any important mouseover behaviors
			System.Windows.Point point = new System.Windows.Point( Cursor.Position.X, Cursor.Position.Y );

			AutomationElement element = null;

			try {
				element = AutomationElement.FromPoint(new System.Windows.Point(Cursor.Position.X, Cursor.Position.Y));
			}
			catch {
			}

			if( element != null ) {
				/*if( !element.Equals( capturedElement ) )
					GDI32.RedrawWindow(capturedElement);*/

				UIControlBase control = UIAControl.GetControlByType(element);

				if( control != null ) {
					Debug.WriteLine( "Sending MOUSEMOVE" );
					System.Windows.Point clientPoint = new System.Windows.Point((int)(Cursor.Position.X - control.Layout.X), (int)(Cursor.Position.Y - control.Layout.Y));

					SendMessage( new HandleRef( this, control.Handle ), 0x0200, (IntPtr) 0, (IntPtr) (((int) clientPoint.Y << 16) | (int) clientPoint.X) );
				}

				Debug.WriteLine( "Repainting highlight after MOUSEMOVE" );
				GDI32.HighlightWindow(capturedElement);
			}

			RecaptureElement( point, true );
		}

		private void RecaptureElement( System.Windows.Point point, bool highlight ) {
			AutomationElement element = null;

			try {
				// Recapture element in case things have changed
				element = AutomationElement.FromPoint( point );
			}
			catch {
			}

			if ( highlight && element != null && !element.Equals(capturedElement))
			{
				Debug.WriteLine( "Changing highlighted element" );

				GDI32.RedrawWindow(capturedElement);
				GDI32.HighlightWindow(element);

				// Restart the timer to give a second's delay before delivering the mouse move
				_timer.Stop();
				_timer.Start();
			}

			capturedElement = element;
		}

		private void ShowCapturedElement() {
			if( capturedElement == null )
				return;

			UIControlBase control = null;
			if (radioButtonWeb.Checked)
			{
				control = Desktop.Web.GetControlFromCursor();
			}
			else
			{
				control = UIAControl.GetControlByType(capturedElement);
			}
			if (control != null)
			{
				textBox.Text = control.CodePath;
				coordinate = new System.Windows.Point((int)(Cursor.Position.X - control.Layout.X), (int)(Cursor.Position.Y - control.Layout.Y));
				textBoxXY.Text = coordinate.ToString();

				_controlPropertyGrid.SelectedObject = null;

				_hierarchyList.BeginUpdate();
				_hierarchyList.Items.Clear();

				UIControlBase pass = control;

				while( pass != null ) {
					_hierarchyList.Items.Add( new ListViewItem() {
						Text = pass.VisibleName,
						Tag = pass
					} );

					pass = pass.Parent;
				}

				_hierarchyList.EndUpdate();
				_hierarchyList.Items[0].Selected = true;
			}
		}

		private void HandleHierarchyControlSelected( object sender, EventArgs e ) {
			if( _hierarchyList.SelectedItems.Count == 0 ) {
				_controlPropertyGrid.SelectedObject = null;
				return;
			}

			_controlPropertyGrid.SelectedObject = _hierarchyList.SelectedItems[0].Tag;
		}

		private void btnCursor_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				IntPtr mainHandle = FindWindowByCaption(IntPtr.Zero, "QAliber Test Builder");
				form = (Form)Form.FromHandle(mainHandle);
				form.Visible = false;
				User32.SetCapture(this.Handle);
				btnCursor.BackgroundImage = Properties.Resources.EmptyCrosshair;
				Cursor = new Cursor( new MemoryStream( Properties.Resources.CrossIcon ) );
				capturing = true;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		protected override void OnLoad( EventArgs e ) {
			base.OnLoad(e);

			_captureHotkey = new ManagedWinapi.Hotkey() { KeyCode = Keys.Scroll };

			_captureHotkey.HotkeyPressed += delegate( object sender, EventArgs ev ) {
				System.Windows.Point point = new System.Windows.Point( Cursor.Position.X, Cursor.Position.Y );
				RecaptureElement( point, false );
				ShowCapturedElement();
			};

			_captureHotkey.Enabled = true;
		}

		protected override void OnClosed(EventArgs e) {
			_captureHotkey.Dispose();
		}

		private Form form;
		private bool capturing;
		private AutomationElement capturedElement;
		private System.Windows.Point coordinate;

		ManagedWinapi.Hotkey _captureHotkey;
	}
}
