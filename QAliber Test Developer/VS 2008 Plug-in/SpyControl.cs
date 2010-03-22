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
using System.Windows.Automation;

using QAliber.Engine.Controls;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Controls.Watin;
using System.Threading;

using ManagedWinapi;
using QAliber.Recorder;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE80;
using Microsoft.VisualStudio.OLE.Interop;
using System.Runtime.InteropServices;
using EnvDTE;
using QAliber.Engine;
using QAliber.VS2005.Plugin.Commands;
using QAliber.Engine.Win32;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Controls.WPF;
using QAliber.Engine.Controls.Watin;

namespace QAliber.VS2005.Plugin
{
	public partial class SpyControl : UserControl
	{
		public SpyControl()
		{
			InitializeComponent();

			toolStripComboBoxSpyAs.SelectedIndex = 0;
			rootControl = Desktop.UIA;

			FillTree();
			InitHotkey();
			QAliber.Logger.Log.Default.Enabled = false;

			Statics.Commands[(int)Commands.CommandType.Record].Invoked += new EventHandler(RecordCommandInvoked);
			Statics.Commands[(int)Commands.CommandType.StopRecord].Invoked += new EventHandler(StopRecordCommandInvoked);

			
			
		}

		public SpyControl(SpyToolWindow parent)
			: this()
		{
			this.parent = parent;
			((RecordCommand)Statics.Commands[(int)Commands.CommandType.Record]).SpyToolWin = parent;
			((StopRecordCommand)Statics.Commands[(int)Commands.CommandType.StopRecord]).SpyToolWin = parent;
		}

		protected override bool ProcessDialogChar(char charCode)
		{
			// If we're the top-level form or control, we need to do the mnemonic handling
			if (charCode != ' ' && ProcessMnemonic(charCode))
			{
				return true;
			}
			return base.ProcessDialogChar(charCode);
		}

		protected override void WndProc(ref Message m)
		{
			
				if (capturing)
				{
					switch (m.Msg)
					{
						case (int)QAliber.Recorder.Structures.MouseMessages.WM_LBUTTONUP:
							User32.ReleaseCapture();
							GDI32.RedrawWindow(capturedElement);
							Cursor = Cursors.Default;
							toolStripCapture.Image = Resources.Crosshair;
							hotkey_HotkeyPressed(this, EventArgs.Empty);
							capturing = false;
							break;
						case (int)QAliber.Recorder.Structures.MouseMessages.WM_MOUSEMOVE:
							try
							{
								AutomationElement element = AutomationElement.FromPoint(new System.Windows.Point(Cursor.Position.X, Cursor.Position.Y));
								if (!element.Equals(capturedElement))
								{

									GDI32.RedrawWindow(capturedElement);
									GDI32.HighlightWindow(element);
									capturedElement = element;

								}
							}
							catch
							{
							}
							break;
						default:
							break;


					}
				}
				base.WndProc(ref m);
			
			
		}

		private void InitHotkey()
		{
			hotkey = new Hotkey();
			hotkey.Ctrl = true;
			hotkey.Shift = true;
			hotkey.KeyCode = Keys.D1;
			hotkey.HotkeyPressed += new EventHandler(hotkey_HotkeyPressed);
			hotkey.Enabled = true;

			Hotkey stopHotkey = new Hotkey();
			stopHotkey.Ctrl = true;
			stopHotkey.Shift = true;
			stopHotkey.KeyCode = Keys.D6;
			stopHotkey.HotkeyPressed += new EventHandler(stopHotkey_HotkeyPressed);
			stopHotkey.Enabled = true;
		}
	 
		private void FillTree()
		{
			treeView.Nodes.Clear();
			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			TreeNode node = new TreeNode("Desktop");
			node.Tag = rootControl;
			node.ContextMenuStrip = nodeContextMenu;
			node.SelectedImageKey = node.ImageKey = "control.bmp";
			UIControlBase control = node.Tag as UIControlBase;
			treeView.Nodes.Add(node);
			foreach (UIControlBase child in control.Children)
			{
				try
				{
					TreeNode newNode = new TreeNode(child.VisibleName);
					newNode.Tag = child;
					newNode.SelectedImageKey = newNode.ImageKey = GetImageIndexByControlType(child);

					newNode.ContextMenuStrip = nodeContextMenu;
					node.Nodes.Add(newNode);
				}
				catch (Exception ex)
				{
					//TODO : report exception
				}
			}
			this.Cursor = oldCursor;

		}

		private void FillTreeRec(TreeNode node, int depth)
		{
			node.Nodes.Clear();
			TreeNode newNode = null;
			UIControlBase control = node.Tag as UIControlBase;
			if (depth > 0 && control.Children != null)
			{

				foreach (UIControlBase child in control.Children)
				{
					try
					{
						if (child != null)
						{
							newNode = new TreeNode(child.VisibleName);
							newNode.Tag = child;
							newNode.SelectedImageKey = newNode.ImageKey = GetImageIndexByControlType(child);

							newNode.ContextMenuStrip = nodeContextMenu;
							node.Nodes.Add(newNode);
							FillTreeRec(newNode, depth - 1);
						}
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("FillTree exception - " + ex.Message);

					}
				}
			}
		}

		private void FillTreeFromLeaf(TreeNode node, UIControlBase control)
		{
			if (control.Parent != null)
			{
				TreeNode parentNode = new TreeNode(control.Parent.VisibleName);
				parentNode.Tag = control.Parent;
				parentNode.SelectedImageKey = parentNode.ImageKey = GetImageIndexByControlType(control.Parent);
				parentNode.ContextMenuStrip = nodeContextMenu;
				parentNode.Nodes.Add(node);
				FillTreeFromLeaf(parentNode, control.Parent);
			}
			else
			{
				node.Text = "Desktop";
				node.Tag = rootControl;
				node.ContextMenuStrip = nodeContextMenu;
				treeView.Nodes.Add(node);
			}
		}

		private void HighlightWorker(object obj)
		{
			UIControlBase control = obj as UIControlBase;
			if (control.Layout != System.Windows.Rect.Empty)
			{
				if (control.Layout.X == -1111) 
				{
					((WatinControl)control).Flash(4);
					return;
				}
				control.SetFocus();

				QAliber.Engine.Win32.GDI32.HighlightRectangleDesktop(
					   control, 2500);
			}
		}

		private string GetImageIndexByControlType(UIControlBase control)
		{
			switch (control.UIType)
			{
				case "UIAButton":
				case "Button":
				case "Buttons":
					return "button.bmp";
				case "UIACheckBox":
				case "CheckBox":
				case "CheckBoxes":
					return "checkbox.bmp";
				case "UIAComboBox":
					return "combobox.bmp";
				case "HTMLDiv":
				case "UIADocument":
				case "UIAPane":
				case "Div":
				case "Para":
				case "Form":
					return "panel.bmp";
				case "UIAEditBox":
				case "TextField":
				case "TextFields":
					return "editbox.bmp";
				case "UIALabel":
				case "Label":
				case "Labels":
					return "label.bmp";
				case "HTMLSelect":
				case "UIAListBox":
				case "UIAListItem":
				case "SelectList":
				case "SelectLists":
					return "list.bmp";
				case "UIAMenu":
				case "UIAMenuItem":
					return "menu.bmp";
				case "UIARadioButton":
				case "RadioButton":
				case "RadioButtons":
					return "radiobutton.bmp";
				case "UIATab":
				case "UIATabItem":
					return "tab.bmp";
				case "UIATable":
				case "Table":
				case "TableBody":
				case "TableCell":
				case "TableRow":
				case "Tables":
				case "TableBodies":
				case "TableCells":
				case "TableRows":
					return "table.bmp";
				case "UIATree":
				case "UIATreeItem":
					return "treeview.bmp";
				case "UIAWindow":
					try
					{
						string key = control.Process.MainModule.FileName;
						if (!imageControls.Images.ContainsKey(key))
							imageControls.Images.Add(key, Icon.ExtractAssociatedIcon(control.Process.MainModule.FileName));
						return key;
					}
					catch
					{
						return "window.bmp";
					}
				case "WebPage":
				case "WatBrowser":
					try
					{
						string key = control.Process.MainModule.FileName;
						if (!imageControls.Images.ContainsKey(key))
							imageControls.Images.Add(key, Icon.ExtractAssociatedIcon(control.Process.MainModule.FileName));
						return key;
					}
					catch
					{
						return "window.bmp";
					}
				case "HTMLImage":
				case "Image":
				case "Images":
					return "image.bmp";
				case "HTMLLink":
				case "Link":
				case "Links":
					return "link.bmp";
				default:
					return "control.bmp";
			}
		}

		
		#region Events
		private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			//UIControlBase control = e.Node.Tag as UIControlBase;
			//if (control is WatBrowser)
			//	  FillTreeRec(e.Node, 1);
			//else
				FillTreeRec(e.Node, 2);
			
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			UIControlBase control = e.Node.Tag as UIControlBase;
			if (control != null)
			{
				TrackSelection(control);
				if (!(control is WebRoot || control is WPFRoot || control is WatinRoot))
				{
					try
					{
						//Trying to get the layout, (an indication that the control exists
						System.Windows.Rect dummy = control.Layout;
						if (dummy == System.Windows.Rect.Empty)
							throw new Exception("Layout is empty");
					}
					catch (Exception)
					{
						//Control does not exist, changing icon and disabling context menu
						e.Node.SelectedImageKey = e.Node.ImageKey = "notexists.bmp";
						e.Node.ContextMenuStrip = null;
						treeView.Refresh();
					}
				}
				
			}
		}

		private void highlightMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode node = treeView.SelectedNode;
			if (node != null && node.Tag is UIControlBase)
			{

				new System.Threading.Thread(new ParameterizedThreadStart(HighlightWorker)).Start(node.Tag);

			}
		}

		private void viewImageMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode node = treeView.SelectedNode;
			if (node != null && node.Tag is UIControlBase)
			{
				UIControlBase control = node.Tag as UIControlBase;
				ImageViewer viewer = new ImageViewer(control.CodePath);
				viewer.SetImage(control.GetImage());
				viewer.ShowDialog();
			}
		}

		private void addAliasMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode node = treeView.SelectedNode;
			if (node != null && node.Tag is UIControlBase)
			{
				UIControlBase control = node.Tag as UIControlBase;
				Aliases.ManageAliasesForm form = new Aliases.ManageAliasesForm(control.CodePath, control.UIType);
				if (form.Initiated)
					form.ShowDialog();
			}
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			if (toolStripComboBoxSpyAs.Text == "WPF")
			{
				rootControl.Refresh();
				FillTree();
			}
			else
			{
				TreeNode node = treeView.SelectedNode;
				if (node != null && node.Tag is UIControlBase)
				{
					UIControlBase control = node.Tag as UIControlBase;
					control.Refresh();
					FillTreeRec(node, 2);
				}
			}
			this.Cursor = oldCursor;
		}

		private void winFormsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode node = treeView.SelectedNode;
			if (node != null && node.Tag is UIAControl)
			{
				UIAControl control = node.Tag as UIAControl;
				control.GetWinFormsProperties();
				TrackSelection(control);

			}
		}

		private void nodeContextMenu_Opening(object sender, CancelEventArgs e)
		{
			TreeNode node = treeView.SelectedNode;
			if (node != null && node.Tag is UIAControl)
			{
				UIAControl control = node.Tag as UIAControl;
				winFormsToolStripMenuItem.Enabled = control.IsWinForms;

			}
		}


		private void treeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TreeNode node = treeView.GetNodeAt(e.Location);
				if (node != null)
					treeView.SelectedNode = node;
			}
		}

		private void hotkey_HotkeyPressed(object sender, EventArgs e)
		{
			try
			{
				UIControlBase control;
				control = ((IControlLocator)rootControl).GetControlFromCursor();
				

				if (control != null)
				{
					TreeNode node = new TreeNode(control.VisibleName);
					node.ContextMenuStrip = nodeContextMenu;
					node.Tag = control;
					node.SelectedImageKey = node.ImageKey = GetImageIndexByControlType(control);
					treeView.Nodes.Clear();
					treeView.BeforeExpand -= new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
					FillTreeFromLeaf(node, control);
					FillTreeRec(node, 1);
					treeView.SelectedNode = node;
					node.EnsureVisible();
					treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
					Statics.DTE.MainWindow.Activate();
					
					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error while tracking control", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void toolStripComboBoxSpyAs_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (toolStripComboBoxSpyAs.SelectedItem.ToString())
			{
				case "UI Automation":
					rootControl = Desktop.UIA;
					toolStripRecord.Enabled = true;
					break;
				case "Web (DOM)":
					rootControl = Desktop.Web;
					toolStripRecord.Enabled = true;
					break;
				case "WPF":
					rootControl = Desktop.WPF;
					toolStripRecord.Enabled = false;
					break;
				case "Web (Watin)":
					rootControl = Desktop.Watin;
					toolStripRecord.Enabled = false;
					break;
				default:
					rootControl = Desktop.UIA;
					break;
			}
			FillTree();
		}

		private void stopHotkey_HotkeyPressed(object sender, EventArgs e)
		{

			Statics.Commands[(int)Commands.CommandType.StopRecord].Invoke();
		}

		private void toolStripRecord_Click(object sender, EventArgs e)
		{
			Statics.Commands[(int)Commands.CommandType.Record].Invoke();
		}

		private void toolStripStop_Click(object sender, EventArgs e)
		{
			Statics.Commands[(int)Commands.CommandType.StopRecord].Invoke();
		}

		private void toolStripCapture_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left)
				{
					User32.SetCapture(this.Handle);
					toolStripCapture.Image = Resources.Circle;
					string path = System.IO.Path.Combine(
						System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
						, @"Resources\CrossIcon.cur");
					Cursor = new Cursor(User32.LoadCursorFromFile(path));
					capturing = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error while capturing", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void RecordCommandInvoked(object sender, EventArgs e)
		{
			if (toolStrip.InvokeRequired)
			{
				toolStrip.Invoke(new EventHandler(RecordCommandInvoked), sender, e);
			}
			else
			{
				toolStripRecord.Enabled = false;
				toolStripStop.Enabled = true;
			}
		}

		private void StopRecordCommandInvoked(object sender, EventArgs e)
		{
			toolStripRecord.Enabled = true;
			toolStripStop.Enabled = false;
		}

		#endregion

		#region Property Grid Integration
		private void TrackSelection(UIControlBase control)
		{
			if (frame == null)
			{
				IVsUIShell shell = GetService(typeof(SVsUIShell)) as IVsUIShell;
				if (shell != null)
				{
					Guid guidPropertyBrowser = new
						Guid(ToolWindowGuids.PropertyBrowser);
					shell.FindToolWindow((uint)__VSFINDTOOLWIN.FTW_fForceCreate,
						ref guidPropertyBrowser, out frame);
				}
			}
			if (frame != null)
			{
				frame.Show();
			}
			if (mySelContainer == null)
			{
				mySelContainer = new Microsoft.VisualStudio.Shell.SelectionContainer();
			}

			mySelItems = new System.Collections.ArrayList();

			if (control != null)
			{
				mySelItems.Add(control);
			}

			mySelContainer.SelectedObjects = mySelItems;

			ITrackSelection track = GetService(typeof(STrackSelection))
				as ITrackSelection;
			if (track != null)
			{
				track.OnSelectChange(mySelContainer);
			}
		}

		protected override object GetService(Type service)
		{
			object obj = null;
			if (parent != null)
			{
				obj = parent.GetVsService(service);
			}
			if (obj == null)
			{
				obj = base.GetService(service);
			}
			return obj;
		}
		#endregion


		internal UIControlBase rootControl;
		private bool capturing;
		private AutomationElement capturedElement;
		private Hotkey hotkey;
		private LLRecorder recorder = new LLRecorder();
		private Microsoft.VisualStudio.Shell.SelectionContainer mySelContainer;
		private System.Collections.ArrayList mySelItems;
		private IVsWindowFrame frame = null;
		private SpyToolWindow parent;
	}

	class IDEDetector
	{
		[DllImport("ole32.dll")]
		static extern int GetRunningObjectTable(uint reserved, out IRunningObjectTable pprot);

		[DllImport("ole32.dll")]
		static extern int CreateBindCtx(uint reserved, out IBindCtx pctx);

		public static DTE2 SeekDTE2InstanceFromROT(String moniker)
		{

			IRunningObjectTable prot = null;
			IEnumMoniker pmonkenum = null;
			uint pfeteched = 0;
			DTE2 ret = null;
			try
			{
				//get rot
				if ((GetRunningObjectTable(0, out prot) != 0) || (prot == null)) return ret;
				prot.EnumRunning(out pmonkenum);
				pmonkenum.Reset();
				IMoniker[] monikers = new IMoniker[1];
				while (pmonkenum.Next(1, monikers, out pfeteched) == 0)
				{
					String insname;
					IBindCtx pctx;
					CreateBindCtx(0, out pctx);
					monikers[0].GetDisplayName(pctx, null, out insname);
					Marshal.ReleaseComObject(pctx);
					if (string.Compare(insname, moniker) == 0) //lookup by item moniker
					{
						Object obj;
						prot.GetObject(monikers[0], out obj);
						ret = (DTE2)obj;
					}
				}
			}
			finally
			{
				if (prot != null) Marshal.ReleaseComObject(prot);
				if (pmonkenum != null) Marshal.ReleaseComObject(pmonkenum);
			}
			return ret;
		}

	}
}