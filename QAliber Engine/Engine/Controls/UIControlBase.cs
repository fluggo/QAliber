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
using System.Reflection;
using QAliber.Logger;

namespace QAliber.Engine.Controls
{
	/// <summary>
	/// Abstract class. represents a control in a user-interface under windows OS.
	/// All Desktop elements derive from this class, most useable control's actions and properties are derived from this class.
	/// Search and drill down indexers inherited by all Desktop controls.
	/// </summary>
	[Serializable]
	public abstract class UIControlBase : ICustomTypeDescriptor
	{
		#region Properties
		/// <summary>
		/// The children of the control (as seen in the UI control browser)
		/// </summary>
		[Browsable(false)]
		public virtual List<UIControlBase> Children
		{
			get
			{
				return children;
			}
		}

		/// <summary>
		/// Is the control exists ?
		/// </summary>
		[Browsable(false)]
		public virtual bool Exists
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// The visibility state of the control. Control is visble when exists on open process window
		/// and not minimized. 
		/// </summary>
		/// <example>
		/// Checking the visibility of the start menu
		///  <code>
		///    UIAButton button1 = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"][@"Start", @"Button", @"304"] as UIAButton;
		///    if (button1.Visible)
		///    {
		///		   
		///    }
		/// </code>
		/// </example>
		/// <returns>true when control is visible</returns>
		/// 
		/// <remarks>
		///		Note: This is not top most, visible is true as long as the control is visible even if behind other windows.
		/// </remarks>
		/// 
		[Category("Common")]
		public virtual bool Visible
		{
			get { return false; }
		}

		/// <summary>
		/// Return the access state of the control (Enabled/Disabled)
		/// </summary>
		/// <example>
		///  <code>
		///    UIAButton button1 = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"][@"Start", @"Button", @"304"] as UIAButton;
		///    if (button1.Enabled)
		///    {
		///		   
		///    }
		/// </code>
		/// </example>
		/// <returns>true when control is Enabled</returns>
		[Category("Common")]
		public virtual bool Enabled
		{
			get { return false; }
		}

		/// <summary>
		/// The native win32 handle of the control
		/// </summary>
		[Category("Common")]
		public virtual IntPtr Handle
		{
			get { return IntPtr.Zero; }
		}


		/// <summary>
		/// The ID of the control. ID is in usually a unique idetifier of the control.
		/// </summary>
		/// <remarks>
		/// If the control has no ID or it has a window handle as an ID, the type is return instead
		/// </remarks>
		[Category("Identifiers")]
		[Description("The identifier of the control")]
		public virtual string ID
		{
			get 
			{
				return string.Empty;
			}
		}


		/// <summary>
		/// The index of the child control among its siblings
		/// </summary>
		[Category("Identifiers")]
		[Description("The index of the control")]
		[DisplayName("Child Index")]
		[ReadOnly(true)]
		public virtual int Index
		{
			get
			{
				return 0;
			}
		}

		/// <summary>
		/// The path to the control, as it will look from code
		/// </summary>
		[Category("Identifiers")]
		[DisplayName("Code Path")]
		[Description("The code representation of the control")]
		public virtual string CodePath
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// The layout of the control in absolute coordinates, (relative to the upper left corner of the desktop)
		/// </summary>
		/// <example>
		/// <code>
		///    UIAButton button1 = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"][@"Start", @"Button", @"304"] as UIAButton;
		///    Rect buttonLayout = button1.Layout;
		///    if (buttonLayout.Location.X > 350)
		///		  Log.Default.Error("Layout error button X location is " + buttonLayout.Location.X);
		/// </code>
		/// </example>
		/// <remarks>
		/// Layout may have negative numbers if the control is hidden / minimized, etc.
		/// </remarks>
		/// <returns>System.Windows.Rect</returns>
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Common")]
		[Description("The layout of the control in absolute desktop coordinates")]
		public virtual Rect Layout
		{
			get { return Rect.Empty; }
		}

		/// <summary>
		/// The name of the current type (UIAButton, UIAEditBox, etc) as it is translated by QAliber engine.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAButton button1 = null;
		///    UIAPane win = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"] as UIAPane;
		///    foreach (UIControlBase control in win.Children)
		///    {
		///		   if (control.UIType == "UIAButton")
		///		   {
		///			   button1 = control as UIAButton;
		///			   break;
		///		   }
		///    }
		///    button1.Click();
		/// </code>
		/// </example>
		/// 
		[Category("Identifiers")]
		[DisplayName("Control Type")]
		[Description("The type of the control")]
		public virtual string UIType
		{
			get { return GetType().Name; }
		}

		/// <summary>
		/// The name of the control 
		/// </summary>
		/// <example>
		/// <code>
		///    UIAButton startButton = null;
		///    UIAPane win = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"] as UIAPane;
		///    foreach (UIControlBase control in win.Children)
		///    {
		///		   if (control.Name == "Start")
		///		   {
		///			   startButton = control as UIAButton;
		///			   break;
		///		   }
		///    }
		///    startButton.Click();
		/// </code>
		/// </example>
		

		[Category("Identifiers")]
		public virtual string Name
		{
			get { return string.Empty; }
		}

		/// <summary>
		/// The name of the control as it appears in the UI Controls browser
		/// </summary>
		[Browsable(false)]
		public virtual string VisibleName
		{
			get
			{
				if (string.IsNullOrEmpty(Name))
				{
					if (string.IsNullOrEmpty(ID))
						return UIType;
					return ID;
				}
				return Name;
			}
		}

		/// <summary>
		/// The class name of the control
		/// </summary>

		protected string className;

		[Category("Identifiers")]
		[DisplayName("Class Name")]
		public virtual string ClassName
		{
			get { return className; }
		}

		/// <summary>
		/// The parent of the control
		/// </summary>
		[Browsable(false)]
		public virtual UIControlBase Parent
		{
			get { return null; }
		}

		/// <summary>
		/// The process that created the control
		/// </summary>
		[Browsable(false)]
		public virtual Process Process
		{
			get { return null; }
		}
		#endregion

		#region Indexers
		/// <summary>
		/// Gets the first child with the given name.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAButton button1 = null;
		///    UIAPane win = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"] as UIAPane;
		///    button1 = win["Start"] as UIAButton;
		///    button1.Click();
		/// </code>
		/// </example>
		/// <param name="name">The name of the child control</param>
		/// <returns>If the name was found, the first control found with that name is returned, otherwise null</returns>
		public virtual UIControlBase this[string name]
		{
			get
			{
				return this[name, false];
			}
		}

		/// <summary>
		/// Gets the first child with a name that matches the regular expression given by the 'name' parameter
		/// </summary>
		/// <example>
		/// <code>
		///    UIAListItem myFolder = null;
		///    UIAListBox desktopWindowShortcuts = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"][@"Desktop", @"SysListView32", @"1"] as UIAListBox;
		///    myFolder = desktopWindowShortcuts["My*", true] as UIAListItem;
		///    myFolder.DoubleClick();
		/// </code>
		/// </example>
		/// <param name="name">The pattern to match the children's names against</param>
		/// <param name="useRegex">If set to true, the 'name' parameter will be treated as regular expression.
		/// If set to false, it is identical to calling UIControl[name]
		/// </param>
		/// <returns>If the name was found, the control with that name is returned, otherwise null</returns>
		/// <seealso href="http://msdn.microsoft.com/en-us/library/2k3te2cs(VS.80).aspx">
		/// MSDN Regular Expression
		/// </seealso>
		/// 
		public virtual UIControlBase this[string name, bool useRegex]
		{
			get
			{
				if (Exists)
				{
					Stopwatch watch = new Stopwatch();
					watch.Start();
					while (watch.ElapsedMilliseconds < PlayerConfig.Default.AutoWaitForControl)
					{
						children = null;
						foreach (UIControlBase child in Children)
						{
							if (useRegex && Regex.Match(child.Name, name).Success)
								return child;
							if (!useRegex && name.ToLower() == child.Name.ToLower())
								return child;
						}
						System.Threading.Thread.Sleep(50);
					}
					QAliber.Logger.Log.Default.Warning(string.Format("Cannot find control [{0}, {1}] for control {2}",
																	 name, useRegex, CodePath), "",
													   QAliber.Logger.EntryVerbosity.Internal);
				}
				return new UINullControl();
			}
		}
		
		/// <summary>
		/// Gets the first child with the given name and id
		/// </summary>
		/// <example>
		/// <code>
		///    UIAButton startButton = null;
		///    UIAPane win = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"] as UIAPane;
		///    startButton = win["Start","304"] as UIAButton;
		///    startButton.DoubleClick();
		/// </code>
		/// </example>
		/// <param name="name">The name of the child control</param>
		/// <param name="id">The id of the child control</param>
		/// <returns>If name and id were found, the control is returned, otherwise null</returns>
		/// <remarks>The id is constant and does not change from run to run.</remarks>
		public UIControlBase this[string name, string id]
		{
			get
			{
				return this[name, id, false];

			}
		}

		/// <summary>
		/// Gets the first child with a name and id that matches the regular expression given by the parameters
		/// </summary>
		/// <example>
		/// <code>
		///    UIAListItem myFolder = null;
		///    UIAListBox desktopWindowShortcuts = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"][@"Desktop", @"SysListView32", @"1"] as UIAListBox;
		///    myFolder = desktopWindowShortcuts["My*","*", true] as UIAListItem;
		///    myFolder.DoubleClick();
		/// </code>
		/// </example>
		/// <param name="name">The pattern to match the children's names against</param>
		/// <param name="id">The pattern to match the children's ids against</param>
		/// <param name="useRegex">If set to true, the 'name' and 'id' parameters will be treated as regular expression.
		/// If set to false, it is identical to calling UIControl[name, id]
		/// </param>
		/// <returns>If the name and id were found, the control with that name is returned, otherwise null</returns>
		/// <see href="http://msdn.microsoft.com/en-us/library/2k3te2cs(VS.80).aspx">
		/// MSDN Regular Expression
		/// </see>
		public UIControlBase this[string name, string id, bool useRegex]
		{
			get
			{
				if (Exists)
				{
					Stopwatch watch = new Stopwatch();
					watch.Start();
					while (watch.ElapsedMilliseconds < PlayerConfig.Default.AutoWaitForControl)
					{
						children = null;
						foreach (UIControlBase child in Children)
						{
							if (useRegex && Regex.Match(child.Name, name).Success &&
								Regex.Match(child.ID, id).Success)
								return child;
							if (!useRegex &&
								name.ToLower() == child.Name.ToLower() &&
								id.ToLower() == child.ID.ToLower())
								return child;
						}
						System.Threading.Thread.Sleep(50);
					}
					QAliber.Logger.Log.Default.Warning(
						string.Format("Cannot find control [{0}, {1}, {2}] for control {3}",
									  name, id, useRegex, CodePath), "", QAliber.Logger.EntryVerbosity.Internal);
				}
				return new UINullControl();
			}
		}

		/// <summary>
		/// Gets the first child with the given name, id and classname
		/// </summary>
		/// <example>
		/// <code>
		///    UIAButton startButton = null;
		///    UIAPane win = Desktop.UIA[@"", @"Shell_TrayWnd", @"UIAPane"] as UIAPane;
		///    startButton = win["Start","Button","304"] as UIAButton;
		///    startButton.Click();
		/// </code>
		/// </example>
		/// <param name="name">The name of the child control</param>
		/// <param name="className">The class name of the child control</param>
		/// <param name="id">The id of the child control, if you want to look only for name and classname you can pass null</param>
		/// <returns>If name, classname and id were found, the control is returned, otherwise null</returns>
		public UIControlBase this[string name, string className, string id]
		{
			get
			{
				if (Exists)
				{
					Stopwatch watch = new Stopwatch();
					watch.Start();
					while (watch.ElapsedMilliseconds < PlayerConfig.Default.AutoWaitForControl)
					{
						children = null;
						foreach (UIControlBase child in Children)
						{
							// BJC: For reasons I can't discern, the automation API returns names for
							// controls that don't have any. For that reason, if name is an empty string,
							// I'll skip searching on name
							if ( (string.IsNullOrEmpty( name ) || child.Name.ToLowerInvariant() == name.ToLowerInvariant()) &&
								(id == null || child.ID.ToLowerInvariant() == id.ToLowerInvariant()) &&
								child.ClassName.ToLowerInvariant() == className.ToLowerInvariant())
								return child;
						}
						System.Threading.Thread.Sleep(50);
					}
					QAliber.Logger.Log.Default.Warning(
						string.Format("Cannot find control [{0}, {1}, {2}] for control {3}",
									  name, className, id, CodePath), "", QAliber.Logger.EntryVerbosity.Internal);
				}
				return new UINullControl();
			}
		}

		public virtual QAliber.Engine.Controls.UIA.UIAControl this[string id, int idIndex]
		{
			get
			{
				if (Exists)
				{
					if (this is QAliber.Engine.Controls.UIA.UIAControl)
					{
						Stopwatch watch = new Stopwatch();
						watch.Start();
						while (watch.ElapsedMilliseconds < PlayerConfig.Default.AutoWaitForControl)
						{
							children = null;
							foreach (QAliber.Engine.Controls.UIA.UIAControl child in Children)
							{
								if (child.ID == id && child.IDIndex == idIndex)
									return child;
							}
							System.Threading.Thread.Sleep(50);
						}
						QAliber.Logger.Log.Default.Warning(
							string.Format("Cannot find control [{0}, {1}] for control {2}",
										  id, idIndex, CodePath), "", QAliber.Logger.EntryVerbosity.Internal);
					}
				}
				return new UIANullControl();
			}
		}
		#endregion

		public static UIControlBase FindControlByPath( string path ) {
			// This is an attempt to standardize how control paths work. Currently,
			// control paths are all C# expressions; in the future we may want to
			// change them to something that could be evaluated a little more smartly.
			// Anyhow, the first thing we do is standardize how we retry a path search:

			string code = @"
UIControlBase c = new UINullControl();

for( int i = 0; i < 3; i++ ) {
	c = " + path + @";

	if( c.Exists )
		return c;

	System.Threading.Thread.Sleep( 0 );
}

return c;";

			return (UIControlBase) CodeEvaluator.Evaluate(code);
		}

		#region Methods
		/// <summary>
		/// Waits for a specific child by name
		/// </summary>
		/// <param name="name">The name of the child control</param>
		/// <param name="timeout">The maximum time (in miliseconds) to wait for the control</param>
		/// <returns>If a child with the given name appears within timeout then the child is returned, if timeout is reached null is returned</returns>
		public virtual UIControlBase WaitForControlByName(string name, int timeout)
		{
			int prevTimeout = PlayerConfig.Default.AutoWaitForControl;
			bool prevLogState = Log.Default.Enabled;
			string exceptionMessage = string.Empty;
			PlayerConfig.Default.AutoWaitForControl = 100;
			Log.Default.Enabled = false;
			Stopwatch stopWatch = new Stopwatch();
			UIControlBase resControl = new UINullControl();
			stopWatch.Start();
			try
			{
				do
				{
					resControl = this[name];
					if (resControl.Exists)
					{
						break;
					}
					System.Threading.Thread.Sleep(50);
				} while (stopWatch.ElapsedMilliseconds < timeout);

			}
			catch (Exception ex)
			{
				exceptionMessage = ex.Message;
			}
			finally
			{
				stopWatch.Stop();
				Log.Default.Enabled = prevLogState;
				PlayerConfig.Default.AutoWaitForControl = prevTimeout;
			}
			if (resControl == null || !resControl.Exists)
			{
				Log.Default.Warning("Wait timed out for the control '" + name + "'", exceptionMessage,
									EntryVerbosity.Internal);
			}
			return resControl;

		}

		/// <summary>
		/// Waits for a specific child by ID
		/// </summary>
		/// <param name="id">The ID of the child control</param>
		/// <param name="timeout">The maximum time (in miliseconds) to wait for the control</param>
		/// <returns>If a child with the given name appears within timeout then the child is returned, if timeout is reached null is returned</returns>
		public virtual UIControlBase WaitForControlByID(string id, int timeout)
		{
			int prevTimeout = PlayerConfig.Default.AutoWaitForControl;
			bool prevLogState = Log.Default.Enabled;
			string exceptionMessage = string.Empty;
			PlayerConfig.Default.AutoWaitForControl = 100;
			Log.Default.Enabled = false;
			Stopwatch stopWatch = new Stopwatch();
			UIControlBase resControl = new UINullControl();
			stopWatch.Start();
			try
			{
				do
				{
					resControl = this["", id, true];
					if (resControl.Exists)
					{
						break;
					}
					System.Threading.Thread.Sleep(50);
				} while (stopWatch.ElapsedMilliseconds < timeout);
				
			}
			catch (Exception ex)
			{
				exceptionMessage = ex.Message;
			}
			finally
			{
				stopWatch.Stop();
				Log.Default.Enabled = prevLogState;
				PlayerConfig.Default.AutoWaitForControl = prevTimeout;
			}
			if (resControl == null || !resControl.Exists)
			{
				Log.Default.Warning("Wait timed out for the control '" + id + "'", exceptionMessage,
									EntryVerbosity.Internal);
			}
			return resControl;

		}

		/// <summary>
		/// Waits for a specific property
		/// </summary>
		/// <example>
		/// <code>
		///  System.Diagnostics.Process.Start("calc");
		///    //Wait for calc app to appear
		///    UIAWindow calcMainWin = Desktop.UIA.WaitForControl(@"Calculator",2000) as UIAWindow;
		///    //Minimizes calc if it is visible 
		///    if (calcMainWin.WaitProperty("Visible", true, 10000))
		///			calcMainWin.Minimize();
		/// </code>
		/// </example>
		/// <param name="propertyName">The property to look for</param>
		/// <param name="val">The value of the property</param>
		/// <param name="timeout">The time (in miliseconds) to wait for the property to be equal to the value</param>
		/// <returns>True if the property is equal to val within timeout, else false</returns>
		/// <remarks>Pay attention that the type of the val matches the type of the property</remarks>
		public virtual bool WaitForProperty(string propertyName, object val, int timeout)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
			PropertyDescriptor prop = props.Find(propertyName, true);
			if (prop != null)
			{
				Stopwatch watch = new Stopwatch();
				watch.Start();
				while (watch.ElapsedMilliseconds < timeout)
				{
					object runtimeVal = prop.GetValue(this);
					if (runtimeVal.Equals(val))
					{
						return true;
					}
				}
				Logger.Log.Default.Warning(string.Format("Timeout while waiting for property {0}", propertyName), CodePath, QAliber.Logger.EntryVerbosity.Internal);
				return false;
			}
			Logger.Log.Default.Warning(string.Format("Property {0} does not exist", propertyName), CodePath, QAliber.Logger.EntryVerbosity.Internal);
			return false;
		}
		/// <summary>
		/// Search for a control within requested scope relative to this control, and the requested properties values.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAPane programManager = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"] as UIAPane;
		///    string [] props = new FindProperties[]{"Name" , "UIType"};
		///    object [] vals = new object[]{"My Documents","UIAListItem"};
		///    //Since My Document is 2 level below the scope is Descendants
		///    UIAListItem myDocs = programManager.Find(
		///						 System.Windows.Automation.TreeScope.Descendants,
		///						  props, vals) as UIAListItem;
		/// 
		///    if (myDocs != null)//found above
		///		   myDocs.DoubleClick();
		///    else
		///		   MessageBox.Show("My Documents Not found");
		/// </code>
		/// </example>
		/// <param name="scope">How to search the controls tree</param>
		/// <param name="properties">One or more properties of the control to search by</param>
		/// <param name="values">List of the expected value for each of the requested properties</param>
		/// <returns>UIControlBase with the matching properties found withing the expected scope or null if control not found</returns>
		public virtual UIControlBase Find(TreeScope scope, string[] properties, object[] values)
		{
			if (properties.Length != values.Length)
				throw new ArgumentException("Properties array must be equal to Values array");
			List<UIControlBase> findList = new List<UIControlBase>();
			switch (scope)
			{
				case TreeScope.Ancestors:
					findList = GetAnscestors();
					break;
				case TreeScope.Children:
					findList = Children;
					break;
				case TreeScope.Subtree:
				case TreeScope.Descendants:
					findList = GetDescendants(100);
					break;
				case TreeScope.Element:
					findList = new List<UIControlBase>();
					findList.Add(this);
					break;
				case TreeScope.Parent:
					findList = new List<UIControlBase>();
					findList.Add(Parent);
					break;
				default:
					break;
			}
			foreach (UIControlBase control in findList)
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(control);
				bool allMatch = true;
				for (int i = 0; i < properties.Length; i++)
				{
					PropertyDescriptor prop = props.Find(properties[i], true);
					if (prop == null || prop.GetValue(control)== null || !prop.GetValue(control).Equals(values[i]))
					{
						allMatch = false;
						break;
					}
				}
				if (allMatch)
					return control;
			}
			return null;
		}

		/// <summary>
		/// Search for a control within requested scope relative to this control, and the requested property value.
		/// </summary>
		/// <example>
		/// <code>
		///    UIAPane programManager = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"] as UIAPane;
		///    //Since My Document is 2 level below the scope is Descendants
		///    UIAListItem myDocs = programManager.Find(
		///						 System.Windows.Automation.TreeScope.Descendants,
		///						 "Name", "My Documents") as UIAListItem;
		///    if (myDocs != null)//not found above
		///		   myDocs.DoubleClick();
		///    else
		///		   MessageBox.Show("My Documents Not found");
		/// </code>
		/// </example>
		/// <param name="scope">Level of search up or down the conrols tree</param>
		/// <param name="property">Property of the control to search by</param>
		/// <param name="val">The expected value of the requested property</param>
		/// <returns>UIControlBase with the matching property found withing the expected scope or null if control not found</returns>
		public UIControlBase Find(TreeScope scope, string property, object val)
		{
			return Find(scope, new string[] { property }, new object[] { val });
		}
		/// <summary>
		/// Retrieve list of controls within requested scope relative to this control, and has the requested property value.
		/// </summary>
		/// <example>
		/// <code>
		///   UIAPane programManager = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"] as UIAPane;
		///    //Since shortcuts is 2 level below the scope is Descendants
		///    UIControl [] myShortcuts = programManager.FindAll(
		///						 System.Windows.Automation.TreeScope.Descendants,
		///						 FindProperties.Type, "UIAListItem") ;
		///
		///    if (myShortcuts != null)//not found above
		///		   myShortcuts[0].DoubleClick();
		///    else
		///		   MessageBox.Show("shortcut Not found");
		/// </code>
		/// </example>
		/// <param name="scope"></param>
		/// <param name="property"></param>
		/// <param name="val"></param>
		/// <returns>List of UIControlBase with the matching property found within the expected scope or null if controls not found</returns>
		public List<UIControlBase> FindAll(TreeScope scope, string property, object val)
		{
			return FindAll(scope, new string[] { property }, new object[] { val });
		}
		/// <summary>
		/// Retrieve list of controls within requested scope relative to this control, and has the requested properties values.
		/// </summary>
		/// <example>
		/// <code>
		///   UIAPane programManager = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"] as UIAPane;
		///    FindProperties [] props = new FindProperties[]{FindProperties.Visible,FindProperties.Type};
		///    object [] vals = new object[]{true,"UIAListItem"};
		///    //Since shortcuts is 2 level below the scope is Descendants
		///    UIControl [] myShortcuts = programManager.FindAll(
		///						 System.Windows.Automation.TreeScope.Descendants,
		///						 props, vals) ;
		/// 
		///    if (myShortcuts != null)//not found above
		///		   myShortcuts[0].DoubleClick();
		///    else
		///		   MessageBox.Show("shortcut Not found");
		/// </code>
		/// </example>
		/// <param name="scope">Level of search up or down the conrols tree</param>
		/// <param name="properties">Property of the control to search by</param>
		/// <param name="values">The expected value of the requested property</param>
		/// <returns>UIControl[] with the matching properties found withing the expected scope or null if controls not found</returns>
		public List<UIControlBase> FindAll(TreeScope scope, string[] properties, object[] values)
		{
			if (properties.Length != values.Length)
				throw new ArgumentException("Properties array must be equal to Values array");
			List<UIControlBase> res = new List<UIControlBase>();
			List<UIControlBase> findList = new List<UIControlBase>();
			switch (scope)
			{
				case TreeScope.Ancestors:
					findList = GetAnscestors();
					break;
				case TreeScope.Children:
					findList = Children;
					break;
				case TreeScope.Subtree:
				case TreeScope.Descendants:
					findList = GetDescendants(100);
					break;
				case TreeScope.Element:
					findList = new List<UIControlBase>();
					findList.Add(this);
					break;
				case TreeScope.Parent:
					findList = new List<UIControlBase>();
					findList.Add(Parent);
					break;
				default:
					break;
			}
			foreach (UIControlBase control in findList)
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(control);
				bool allMatch = true;
				for (int i = 0; i < properties.Length; i++)
				{
					PropertyDescriptor prop = props.Find(properties[i], true);
					if (prop == null || !prop.GetValue(control).Equals(values[i]))
					{
						allMatch = false;
						break;
					}
				}
				if (allMatch)
					res.Add(control);
			}
			return res;
		}

		public AmbiguityResult TestAmbiguity(string name, string classname, string id)
		{
			int matchCount = 0;
			foreach (UIControlBase child in Children)
			{
				if (child.Name == name && child.ClassName == classname && child.ID == id)
					matchCount++;
				if (matchCount > 1)
					return AmbiguityResult.Ambiguous;

			}
			if (matchCount == 0)
				return AmbiguityResult.Single;
			else
				return AmbiguityResult.None;
		}
		/// <summary>
		/// Simulate mouse click with the left button, at the center of the control
		/// </summary>
		/// <example>
		/// <code>
		///		Desktop.UIA.Click();
		/// </code>
		/// </example>
		/// <remarks>By default click is left mouse button, and is at the center of the specified control.</remarks>
		public virtual void Click()
		{
			Click(MouseButtons.Left);
		}

		/// <summary>
		/// Simulate mouse click at the center of the control
		/// </summary>
		/// <example>
		/// <code>
		///		Desktop.UIA.Click(MouseButtons.Right);
		/// </code>
		/// </example>
		/// <param name="button">The mouse button to be clicked</param>
		public virtual void Click(MouseButtons button)
		{
			Point rel = new Point(Layout.Width / 2, Layout.Height / 2);
			Click(button, rel);
		}

		/// <summary>
		/// Simulate mouse click on a certain given Point (upper left is (0,0))
		/// </summary>
		/// <example>
		/// <code>
		///		//click around the upper left corner
		///    Desktop.UIA.Click(MouseButtons.Right,new Point(50,50));
		/// </code>
		/// </example>
		/// <param name="button">The button to be clicked</param>
		/// <param name="rel">The point in pixels relative to the top left corner of the control</param>
		public virtual void Click(MouseButtons button, Point rel)
		{
			OnBeforeAnyAction(ControlActionType.Click);
			if (CheckExistence())
			{
				SetFocus();
				Point p = new Point(Layout.Left + rel.X, Layout.Top + rel.Y);
				Logger.Log.Default.Info("Clicking mouse on control " + CodePath, "Button : " + button + " , Coordinate : " + rel, QAliber.Logger.EntryVerbosity.Internal);
				Win32.LowLevelInput.Click(button, p);
			}
		}

		/// <summary>
		/// Simulate mouse double-click with the left button, at the center of the control
		/// </summary>
		public virtual void DoubleClick()
		{
			DoubleClick(MouseButtons.Left);
		}

		/// <summary>
		/// Simulate mouse double-click at the center of the control
		/// </summary>
		/// <param name="button">The button to be clicked</param>
		public virtual void DoubleClick(MouseButtons button)
		{
			Point rel = new Point(Layout.Width / 2, Layout.Height / 2);
			DoubleClick(button, rel);
		}

		/// <summary>
		/// Simulate mouse double-click on a certain point relative to upper left of the control (0,0)
		/// </summary>
		/// <example>
		/// <code>
		///		//click around the upper left corener of desktop
		///    Desktop.UIA.DoubleClick(MouseButtons.Right,new Point(50,50));
		/// </code>
		/// </example>
		/// <param name="button">The button to be clicked</param>
		/// <param name="rel">The point in pixels relative to the top left corner of the control</param>
		public virtual void DoubleClick(MouseButtons button, Point rel)
		{
			OnBeforeAnyAction(ControlActionType.DoubleClick);
			if (CheckExistence())
			{
				SetFocus();
				Point p = new Point(Layout.Left + rel.X, Layout.Top + rel.Y);
				Logger.Log.Default.Info("Double clicking mouse on control " + CodePath, "Button : " + button + " , Coordinate : " + rel, QAliber.Logger.EntryVerbosity.Internal);
				Win32.LowLevelInput.DoubleClick(button, p);
			}
		}
		
		/// <summary>
		/// Simulate mouse drag using the left button
		/// </summary>
		/// <example>
		/// Following example will locate calc and will drag it from titlebar (point (150,15)) to the middle of screen.
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    //Wait for calc app to appear
		///    UIAWindow calcMainWin = Desktop.UIA.WaitForControl(@"Calculator",2000) as UIAWindow;
		///
		///    if (calcMainWin != null)//not found above
		///		   calcMainWin.Drag(new Point(150, 15), new Point(300, 300));
		/// </code>
		/// </example>
		/// <param name="rel1">The point in pixels relative to the top left corner of the control, from which to start the drag</param>
		/// <param name="rel2">The point in pixels relative to the top left corner of the control, to end the drag</param>
		public virtual void Drag(Point rel1, Point rel2)
		{
			Drag(MouseButtons.Left, rel1, rel2);
		}

		/// <summary>
		/// Simulate mouse drag
		/// </summary>
		/// <example>
		/// Following example will locate will drag a shortcut from you desktop.
		/// Make sure your desktop is visible when you run this sample.
		/// Note the context menu opens in the end.
		/// <code>
		///		 UIAPane programManager = Desktop.UIA[@"Program Manager", @"Progman", @"UIAPane"] as UIAPane;
		///    //Since shortcuts is 2 level below the scope is Descendants
		///    UIControl[] myShortcuts = programManager.FindAll(
		///						 System.Windows.Automation.TreeScope.Descendants,
		///						 FindProperties.Visible, true);
		///    if (myShortcuts != null)//not found above
		///		   myShortcuts[0].Drag(MouseButtons.Right,new Point(30, 30), new Point(300, 300));
		///    else
		///		   MessageBox.Show("shortcut Not found");
		/// </code>
		/// </example>
		/// <param name="button">The button to perform the drag with</param>
		/// <param name="rel1">The point in pixels relative to the top left corner of the control, from which to start the drag</param>
		/// <param name="rel2">The point in pixels relative to the top left corner of the control, to end the drag</param>
		public virtual void Drag(MouseButtons button, Point rel1, Point rel2)
		{
			OnBeforeAnyAction(ControlActionType.Drag);
			if (CheckExistence())
			{
				SetFocus();
				Point p1 = new Point(Layout.Left + rel1.X, Layout.Top + rel1.Y);
				Point p2 = new Point(Layout.Left + rel2.X, Layout.Top + rel2.Y);
				Logger.Log.Default.Info("Dragging mouse on control " + CodePath, "Button : " + button + " , From : " + rel1 + " To : " + rel2, QAliber.Logger.EntryVerbosity.Internal);
				Win32.LowLevelInput.DragMouse(button, p1, p2);
			}
		}

		/// <summary>
		/// Simulate a mouse move, this is good way to test hover functionality.
		/// </summary>
		/// <example>
		/// <code>
		///		 Desktop.UIA.MoveMouseTo(new Point(300,300));
		/// </code>
		/// </example>
		/// <param name="to">The point in pixels relative to the top left corner of the control, to move the mouse to</param>
		public virtual void MoveMouseTo(Point to)
		{
			OnBeforeAnyAction(ControlActionType.MoveMouse);
			if (CheckExistence())
			{
				SetFocus();
				Point p = new Point(Layout.Left + to.X, Layout.Top + to.Y);
				Win32.LowLevelInput.MoveMouse(p);
			}
		}

		/// <summary>
		/// Sets the focus of the control
		/// </summary>
		/// <example>
		/// Open calc and hide it below windows before running this sample.
		/// <code>
		///		 Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"].SetFocus();
		/// </code>
		/// The call to SetFocus should bring it to front
		/// </example>
		/// <remarks>
		/// In UIAWindow it should activate the window
		/// </remarks>
		public virtual void SetFocus()
		{
			;
		}

		/// <summary>
		/// Simulate key strokes using the keyboard.  
		/// </summary>
		/// <example>
		/// This example will open notepad and send "Hello WORLD" keys , since it is expected to be focused.
		/// <code>
		///    System.Diagnostics.Process.Start("notepad");
		///    UIAWindow notepadWin = Desktop.UIA[@"Untitled - Notepad", @"Notepad", @"UIAWindow"] as UIAWindow;
		///    notepadWin.Write("({RightShift}h)ello ({RightShift}world)");
		/// </code>
		/// </example>
		/// <param name="text">Text to send</param>
		/// <remarks>Unicode is supported, to group keys together use '()' braces, to send special keys enclose them with '{}' braces</remarks>
		/// <see href="http://msdn.microsoft.com/en-us/library/system.windows.input.key.aspx">
		/// List of special keys
		/// </see>
		public virtual void Write(string text)
		{
			OnBeforeAnyAction(ControlActionType.Write);
			if (CheckExistence())
			{
				SetFocus();
				Logger.Log.Default.Info("Sending keys to control " + CodePath, "Keys : " + text, QAliber.Logger.EntryVerbosity.Internal);
				//System.Threading.Thread.Sleep(200);
				Win32.LowLevelInput.SendKeystrokes(text);
			}
		}

		/// <summary>
		/// Release cached properties associated with the control.
		/// Cachable properties are :
		/// Children
		/// Parent
		/// CodePath
		/// ID
		/// </summary>
		/// <example>
		/// Try run this code without the Refresh(). See the children is not updated till the refresh is called.
		/// <code>
		///    int openApps = Desktop.UIA.Children.Length;
		///    Console.Writeline("Number of open applications before are : " + openApps);
		///    System.Diagnostics.Process.Start("notepad");
		///    System.Threading.Thread.Sleep(2000);
		///    Desktop.UIA.Refresh();
		///    openApps = Desktop.UIA.Children.Length;
		///    Console.Writeline("Number of open applications after are : " + openApps);
		/// </code>
		/// </example>
		/// <remarks>
		/// If the control is changed during automation it is recommended to refresh the control (or its parent), before using it
		/// </remarks>
		public virtual void Refresh()
		{
			children = null;
		}

		/// <summary>
		/// Get the bitmap of the control.
		/// </summary>
		/// <example>
		/// Make sure calc is running in scientific mode
		/// In this example take the image of the hex A in decimal mode (disabled) and in Hex mode (enabled)
		/// Save the images for further analysis (e.g. comaprison)
		/// <code>
		///    System.Diagnostics.Process.Start("calc");
		///    UIAWindow calcWin = Desktop.UIA[@"Calculator", @"SciCalc", @"UIAWindow"] as UIAWindow;
		///    //Get disabled button image
		///    UIAButton hexAButton = calcWin[@"A", @"Button", @"134"] as UIAButton;
		///    System.Drawing.Image disabledButton = hexAButton.GetImage();
		///    disabledButton.Save(@"C:\disabledButton.bmp");
		///    UIARadioButton @hexSelect = calcWin[@"Hex", @"Button", @"306"] as UAIRadioButton;
		///    hexSelect.Click();
		///    // before accessing image,refresh() is recomended
		///    hexAButton.Refresh();
		///    disabledButton = hexAButton.GetImage();
		///    disabledButton.Save(@"C:\enabledButton.bmp");
		///    //See the difference
		///    System.Diagnostics.Process.Start(@"C:\enabledButton.bmp");
		///    System.Diagnostics.Process.Start(@"C:\disabledButton.bmp");
		/// </code>
		/// </example>
		/// QAliber comes with tools to help you work with images, Compare, OCR and more by simply right click
		/// the the image control on the UI Control Browser.
		/// <returns>The bitmap of the control</returns>
		public System.Drawing.Bitmap GetImage()
		{
			SetFocus();
			System.Threading.Thread.Sleep(1000);
			return Win32.GDI32.GetImage(Layout);
		}

		/// <summary>
		/// Gets partial bitmap of the control
		/// </summary>
		/// <remarks>bounds must be within the control's layout</remarks>
		/// <param name="bounds">A rectangle relative to the top left corner of the control to take the image from</param>
		/// <returns>A partial bitmap of the control</returns>
		public System.Drawing.Bitmap GetPartialImage(Rect bounds)
		{
			System.Drawing.Bitmap image = GetImage();
			if (bounds.X + bounds.Width > Layout.Width || bounds.Y + bounds.Height > Layout.Height)
				throw new ArgumentException(
					string.Format("Cannot get partial box of {0} from total of {1}", bounds, Layout));
			System.Drawing.Rectangle drawBounds = new System.Drawing.Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
			System.Drawing.Bitmap partialImage = image.Clone(drawBounds, image.PixelFormat);
			return partialImage;
		}

		
		protected List<UIControlBase> GetAnscestors()
		{
			List<UIControlBase> res = new List<UIControlBase>();
			UIControlBase cparent = Parent;
			while (cparent != null)
			{
				res.Add(cparent);
				cparent = cparent.Parent;
			}
			return res;
		}

		protected List<UIControlBase> GetDescendants(int depth)
		{
			List<UIControlBase> res = new List<UIControlBase>();
			res = GetDescendantsRecursively(this, depth, res);
			return res;
		}

		private List<UIControlBase> GetDescendantsRecursively(UIControlBase control, int depth, List<UIControlBase> list)
		{
			if (depth <= 0)
				return list;
			foreach (UIControlBase child in control.Children)
			{
				list.Add(child);
				list = GetDescendantsRecursively(child, depth - 1, list);
			}
			return list;
		}

		#endregion

		#region Private/Protected Methods

	   
		protected bool CheckExistence()
		{
			if (Layout == Rect.Empty || Layout.X < -Layout.Width || Layout.Y < -Layout.Height)
			{
				QAliber.Logger.Log.Default.Error("Control '" + Name + "' was not found on screen", CodePath, QAliber.Logger.EntryVerbosity.Internal);
				return false;
			}
		   
			if (!Enabled)
				QAliber.Logger.Log.Default.Warning("Control '" + Name + "' is disabled", CodePath, QAliber.Logger.EntryVerbosity.Internal);
			return true;
		}

		protected virtual void OnBeforeAnyAction(ControlActionType action)
		{
			if (BeforeAnyAction != null)
			{
				BeforeAnyAction(this, new UIControlEventArgs(action));
			}
		}
		#endregion

		#region Private Fields

		protected List<UIControlBase> children = null;
		protected Dictionary<string, object> extendedProperties = new Dictionary<string,object>();

		internal static readonly string tmpFile = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\qaliberTmpObj.bin";
		#endregion

		#region Comparers
		class IDSorter : IComparer<UIControlBase>
		{
			#region IComparer<UIControlBase> Members

			public int Compare(UIControlBase x, UIControlBase y)
			{
				int compRes = string.Compare(x.UIType, y.UIType);
				if (compRes == 0)
				{
					compRes = string.Compare(x.ID, y.ID);
					if (compRes == 0)
						return x.Index - y.Index;
				}
				return compRes;
			}

			#endregion
		}
		#endregion

		#region Events
		/// <summary>
		/// This event is fired whenever a real action on a control (mouse click, keyboard activity) is about to be made
		/// </summary>
		public static event EventHandler<UIControlEventArgs> BeforeAnyAction;
		#endregion

		#region ICustomTypeDescriptor Members

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection props = new PropertyDescriptorCollection(null);
			foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this, attributes, true))
			{
				props.Add(prop);
			}
			Attribute[] attrs = new Attribute[] { new CategoryAttribute("Additional Properites") };
			Dictionary<string, object>.Enumerator i = extendedProperties.GetEnumerator();
			while (i.MoveNext())
			{
				props.Add(new ExtendedPropertyDescriptor(i.Current.Key, i.Current.Value, attrs));
			}
			return props;
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		#endregion
	}

	public class UIControlEventArgs : EventArgs
	{
		public UIControlEventArgs(ControlActionType action)
		{
			this.action = action;
		}

		private ControlActionType action;

		public ControlActionType Action
		{
			get { return action; }
		}

	}

	public class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		public ExtendedPropertyDescriptor(string name, object val, Attribute[] attrs)
			: base(name, attrs)
		{
			this.val = val;
		}

		public override Type ComponentType
		{
			get { return typeof(UIControlBase); }
		}

		public override bool IsReadOnly
		{
			get { return false; }
		}

		public override Type PropertyType
		{
			get { return val.GetType(); }
		}

		public override bool CanResetValue(object component)
		{
			return true;
		}

		public override object GetValue(object component)
		{
			return val;
		}

		public override void SetValue(object component, object value)
		{
			val = value.ToString();
			OnValueChanged(component, EventArgs.Empty);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}

		public override void ResetValue(object component)
		{
			val = null;
		}

		private object val;

	}

	class IDControlSorter : IComparer<UIControlBase>
	{
		#region IComparer<UIControlBase> Members

		public int Compare(UIControlBase x, UIControlBase y)
		{
			return string.Compare(x.ID, y.ID);
		}

		#endregion
	}

	
}
