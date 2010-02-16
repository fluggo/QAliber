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
using System.Text;
using System.Windows.Automation;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel;

using ManagedWinapi.Accessibility;
using ManagedWinapi.Windows;
using QAliber.Engine.Patterns;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ManagedInjector;
using System.Threading;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Abstract class. represents a control wrapper over UIAutomation.
	/// All UIA Desktop elements derive from this class.
	/// </summary>
	[Serializable]
	public abstract class UIAControl : UIControlBase
	{
		#region Constructores
		/// <summary>
		/// Abstract class Ctor, used to initiate the base UI control with the UI Automation element.
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		public UIAControl(AutomationElement element)
		{
			automationElement = element;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the underlying UIAutomation object of the control 
		/// </summary>
		/// <seealso href="http://msdn.microsoft.com/en-us/library/ms747327.aspx">
		/// UI Automation Overview (MSDN)
		/// </seealso>
		[Browsable(false)]
		public AutomationElement UIAutomationElement
		{
			get { return automationElement; }
		}

		/// <summary>
		/// The MSAA API for the control
		/// </summary>
		[Editor(typeof(UITypeEditor.MSAAUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Accessibility")]
		public SystemAccessibleObject MSAA
		{
			get
			{
				if (sao == null)
				{
					try
					{
						if (automationElement != null && Handle != 0)
						{
							SystemWindow win = new SystemWindow(
								new IntPtr(Handle));
							sao = SystemAccessibleObject.FromWindow(win, AccessibleObjectID.OBJID_WINDOW);
						}
					}
					catch (System.Runtime.InteropServices.COMException)
					{
						sao = null;
					}
				}
				return sao;
			}
		}

		public override List<UIControlBase> Children
		{
			get
			{
				if (children == null)
				{
					children = new List<UIControlBase>();
					AutomationElementCollection elements = automationElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsControlElementProperty, true));
					foreach (AutomationElement element in elements)
					{
						UIAControl control = UIAControl.GetControlByType(element);
						if (control != null)
						{
							control.Index = children.Count;
							children.Add(control);
							control.parent = this;
						}
					}
				}
				return children;
			}
		}

		public override bool Visible
		{
			get { return !automationElement.Current.IsOffscreen && Layout != Rect.Empty
				&& Layout.X >= 0 && Layout.Y >= 0 
				&& Layout.Width >=0 && Layout.Height >= 0; }
				
		}

		
		public override bool Enabled
		{
			get { return automationElement.Current.IsEnabled; }
		}

		/// <summary>
		/// An additional help string associated with the control
		/// </summary>
		[Category("Common")]
		[Description("Additional text associated with the control")]
		public virtual string HelpText
		{
			get { return automationElement.Current.HelpText; }
		}

		public override string ID
		{
			get 
			{
				if (id == string.Empty)
				{
					if (automationElement.Current.AutomationId == "" ||
						IsIdHandle(automationElement.Current.AutomationId))
					{
						id = UIType;
					}
					else
						id = automationElement.Current.AutomationId;

				}
				return id; 
			}
		}

		public override string CodePath
		{
			get
			{
				if (codePath == string.Empty)
				{
					string prefix = String.Empty;
					UIControlBase parent = Parent;
					if (parent == null || (parent is UIAControl) && ((UIAControl)parent).UIAutomationElement.Equals(AutomationElement.RootElement))
						prefix = "Desktop.UIA";
					else
						prefix = parent.CodePath;
					codePath = prefix + "[@\"" + Name + "\", @\"" + ClassName + "\", @\"" + ID + "\"]";
				}
				return codePath;
			}
		}

		public override Rect Layout
		{
			get { return automationElement.Current.BoundingRectangle; }
		}

		/// <summary>
		/// The type of the control as it exposed by the UI Automation framework.
		/// <example>
		/// <code>
		///  UIAButton button1 = null;
		///    UIAPane win = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"] as UIAPane;
		///    foreach (UIControl control in win.Children)
		///    {
		///		   if (control.Type == System.Windows.Automation.ControlType.Button)
		///		   {
		///			   button1 = control as UIAButton;
		///			   break;
		///		   }
		///    }
		///    button1.Click();
		/// </code>
		/// </example>
		/// </summary>
		[Browsable(false)]
		public virtual ControlType UIAControlType
		{
			get { return automationElement.Current.ControlType; }
		}

		public override string Name
		{
			get { return automationElement.Current.Name; }
		}

		
		public override string ClassName
		{
			get { return automationElement.Current.ClassName; }
		}

		public override int Handle
		{
			get
			{
				return automationElement.Current.NativeWindowHandle;
			}
		}

		public override UIControlBase Parent
		{
			get
			{
				if (parent == null)
				{
					AutomationElement element = walker.GetParent(automationElement);
					if (element != null)
					{
						parent = UIAControl.GetControlByType(element);
					}
				}
				return parent;
			}
		}

		public override Process Process
		{
			get { return System.Diagnostics.Process.GetProcessById(automationElement.Current.ProcessId); }
		}

		/// <summary>
		/// Indicates whether the control is a winforms control
		/// </summary>
		[Browsable(false)]
		public bool IsWinForms
		{
			get { return ClassName.StartsWith("WindowsForms"); }
		}
		#endregion

		#region Methods
		public override void SetFocus()
		{
			try
			{
				if (!this.UIType.EndsWith("Item"))
					automationElement.SetFocus();
			}
			catch (InvalidOperationException)
			{
			}
			finally
			{
				UIControlBase prnt = Parent;
				if (prnt != null)
					prnt.SetFocus();
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			sao = null;
		}

		/// <summary>
		/// Applicable only on WinFormsControl (you can query it by the IsWinForms property), gets the serializable properties of the control with types that are commonly references
		/// </summary>
		public void GetWinFormsProperties()
		{
			if (IsWinForms)
			{
				File.Delete(UIControlBase.tmpFile);
				Injector.Launch(new IntPtr(UIAutomationElement.Current.NativeWindowHandle),
							this.GetType().Assembly.Location, 
							typeof(UIAControl).FullName, "QueryWinForms",
							UIAutomationElement.Current.NativeWindowHandle.ToString());
				int i = 0;
				while (!File.Exists(UIControlBase.tmpFile))
				{
					i++;
					if (i > 20)
						return;
					Thread.Sleep(100);
				}
				using (FileStream fs = File.Open(UIControlBase.tmpFile, FileMode.Open))
				{
					BinaryFormatter bf = new BinaryFormatter();
					bf.Binder = new WPF.AllowAllAssemblyVersionsDeserializationBinder();
					extendedProperties = bf.Deserialize(fs) as Dictionary<string, object>;

				}
			}
		}

		/// <summary>
		/// Converts a UIAutomation element to QAliber UIAControl
		/// </summary>
		/// <param name="element">The UIAutomation element to convert</param>
		/// <returns></returns>
		public static UIAControl GetControlByType(AutomationElement element)
		{
			if (element.Current.ControlType.Id == ControlType.Button.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.Calendar.Id)
				return new UIATable(element);
			else if (element.Current.ControlType.Id == ControlType.CheckBox.Id)
				return new UIACheckBox(element);
			else if (element.Current.ControlType.Id == ControlType.ComboBox.Id)
				return new UIAComboBox(element);
			else if (element.Current.ControlType.Id == ControlType.Custom.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.DataItem.Id)
				return new UIAListItem(element);
			else if (element.Current.ControlType.Id == ControlType.DataGrid.Id)
				return new UIATable(element);
			else if (element.Current.ControlType.Id == ControlType.Document.Id)
				return new UIADocument(element);
			else if (element.Current.ControlType.Id == ControlType.Edit.Id)
				return new UIAEditBox(element);
			else if (element.Current.ControlType.Id == ControlType.Group.Id)
				return new UIAGroup(element);
			else if (element.Current.ControlType.Id == ControlType.Header.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.HeaderItem.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.Hyperlink.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.Image.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.List.Id)
				return new UIAListBox(element);
			else if (element.Current.ControlType.Id == ControlType.ListItem.Id)
				return new UIAListItem(element);
			else if (element.Current.ControlType.Id == ControlType.Menu.Id)
				return new UIAMenu(element);
			else if (element.Current.ControlType.Id == ControlType.MenuBar.Id)
				return new UIAMenu(element);
			else if (element.Current.ControlType.Id == ControlType.MenuItem.Id)
				return new UIAMenuItem(element);
			else if (element.Current.ControlType.Id == ControlType.Pane.Id)
				return new UIAPane(element);
			else if (element.Current.ControlType.Id == ControlType.ProgressBar.Id)
				return new UIARange(element);
			else if (element.Current.ControlType.Id == ControlType.RadioButton.Id)
				return new UIARadioButton(element);
			else if (element.Current.ControlType.Id == ControlType.Separator.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.ScrollBar.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.Slider.Id)
				return new UIARange(element);
			else if (element.Current.ControlType.Id == ControlType.Spinner.Id)
				return new UIARange(element);
			else if (element.Current.ControlType.Id == ControlType.SplitButton.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.StatusBar.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.Tab.Id)
				return new UIATab(element);
			else if (element.Current.ControlType.Id == ControlType.TabItem.Id)
				return new UIATabItem(element);
			else if (element.Current.ControlType.Id == ControlType.Table.Id)
				return new UIATable(element);
			else if (element.Current.ControlType.Id == ControlType.Text.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.Thumb.Id)
				return new UIALabel(element);
			else if (element.Current.ControlType.Id == ControlType.TitleBar.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.ToolBar.Id)
				return new UIAToolbar(element);
			else if (element.Current.ControlType.Id == ControlType.ToolTip.Id)
				return new UIAButton(element);
			else if (element.Current.ControlType.Id == ControlType.Tree.Id)
				return new UIATree(element);
			else if (element.Current.ControlType.Id == ControlType.TreeItem.Id)
				return new UIATreeItem(element);
			else if (element.Current.ControlType.Id == ControlType.Window.Id)
				return new UIAWindow(element);
			else
				return null;
		}

		/// <summary>
		/// This method is for QAliber internal usage
		/// </summary>
		/// <param name="strHandle"></param>
		public static void QueryWinForms(string strHandle)
		{
			try
			{
				IntPtr handle = new IntPtr(int.Parse(strHandle));
				Control c = Control.FromHandle(handle);
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(c);
				Dictionary<string, object> dict = new Dictionary<string, object>();
				foreach (PropertyDescriptor prop in props)
				{
					object obj = prop.GetValue(c);
					if (!string.IsNullOrEmpty(prop.Name) && obj != null
						&& obj.GetType().IsSerializable
						&& obj.GetType().FullName.StartsWith("System"))
					{
						try
						{
							//Trying to serialize it to see it fits
							using (MemoryStream ms = new MemoryStream())
							{
								BinaryFormatter bf = new BinaryFormatter();
								bf.Serialize(ms, obj);
							}
							dict.Add(prop.Name, obj);
						}
						catch (System.Runtime.Serialization.SerializationException)
						{
						}

					}
				}

				using (FileStream fs = File.Open(UIControlBase.tmpFile, FileMode.Create))
				{
					BinaryFormatter bf = new BinaryFormatter();
					bf.Serialize(fs, dict);
				}
			}
			catch //(Exception ex)
			{
				//Todo : log it and remove it
				//MessageBox.Show(ex.Message, "Error");
			}
		}
		#endregion

		#region Private/Protected Methods

		private bool IsIdHandle(string id)
		{
			int res = 0;
			if (int.TryParse(id, out res))
			{
				if (res > 10000)
					return true;
			}
			return false;

		}

		#endregion

		#region Private Fields
		[NonSerialized]
		protected AutomationElement automationElement;
		[NonSerialized]
		protected SystemAccessibleObject sao = null;

		protected static TreeWalker walker = TreeWalker.ControlViewWalker;
		#endregion

	}

	

	

	
}
