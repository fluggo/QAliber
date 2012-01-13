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
			get
			{
				return textBox.Text;
			}
		}

		public System.Windows.Point Coordinate
		{
			get
			{
				return coordinate;
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
						User32.ReleaseCapture();
						GDI32.RedrawWindow(capturedElement);
						Cursor = Cursors.Default;
						btnCursor.BackgroundImage = Properties.Resources.Crosshair;
						capturing = false;
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
							labelTypeDyn.Text = control.UIType;
							coordinate = new System.Windows.Point((int)(Cursor.Position.X - control.Layout.X), (int)(Cursor.Position.Y - control.Layout.Y));
							textBoxXY.Text = coordinate.ToString();
						}
						break;
					}
					case 0x0200: { //MouseMove
						// Simulate a mouse move over this point to activate any important mouseover behaviors
						AutomationElement element = AutomationElement.FromPoint(new System.Windows.Point(Cursor.Position.X, Cursor.Position.Y));

						if( element != null ) {
							if( !element.Equals( capturedElement ) )
								GDI32.RedrawWindow(capturedElement);

							UIControlBase control = UIAControl.GetControlByType(element);

							if( control != null ) {
								System.Windows.Point clientPoint = new System.Windows.Point((int)(Cursor.Position.X - control.Layout.X), (int)(Cursor.Position.Y - control.Layout.Y));

								SendMessage( new HandleRef( this, (IntPtr) control.Handle ), 0x0200, (IntPtr) 0, (IntPtr) (((int) clientPoint.Y << 16) | (int) clientPoint.X) );
							}
						}

						// Recapture element in case things have changed
						element = AutomationElement.FromPoint(new System.Windows.Point(Cursor.Position.X, Cursor.Position.Y));

						if (element != null && !element.Equals(capturedElement))
						{
							GDI32.RedrawWindow(capturedElement);
							GDI32.HighlightWindow(element);
							capturedElement = element;
						}

					}

						break;
					default:
						break;


				}
			}
			base.WndProc(ref m);
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
				Cursor = new Cursor(
					Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
						@"Resources\CrossIcon.cur"));
				capturing = true;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private Form form;
		private bool capturing;
		private AutomationElement capturedElement;
		private System.Windows.Point coordinate;
		

		
	}
}