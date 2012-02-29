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

using ManagedWinapi.Windows;
using QAliber.Engine.Patterns;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ManagedInjector;
using System.Threading;
using System.Linq;
using QAliber.Logger;

namespace QAliber.Engine.Controls.UIA
{
	/// <summary>
	/// Abstract class. represents a control wrapper over UIAutomation.
	/// All UIA Desktop elements derive from this class.
	/// </summary>
	[Serializable]
	public class UIAControl : UIControlBase
	{
		Dictionary<string, object> _extendedProperties = new Dictionary<string,object>();
		internal static readonly CacheRequest SearchCache;

		static UIAControl() {
			// Caches those properties we expect in the UIAControl
			SearchCache = new CacheRequest();
			SearchCache.AutomationElementMode = AutomationElementMode.Full;
			SearchCache.TreeScope = TreeScope.Element;
			SearchCache.Add( AutomationElement.AutomationIdProperty );
			SearchCache.Add( AutomationElement.NameProperty );
			SearchCache.Add( AutomationElement.ClassNameProperty );
			SearchCache.Add( AutomationElement.ControlTypeProperty );
			SearchCache.Add( AutomationElement.NativeWindowHandleProperty );
			SearchCache.Add( AutomationElement.ProcessIdProperty );

			// Look for the pattern-available properties, too
			SearchCache.Add( AutomationElement.IsDockPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsExpandCollapsePatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsGridItemPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsGridPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsInvokePatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsItemContainerPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsMultipleViewPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsRangeValuePatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsScrollItemPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsScrollPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsSelectionItemPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsSelectionPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsSynchronizedInputPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsTableItemPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsTablePatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsTextPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsTogglePatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsTransformPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsValuePatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsVirtualizedItemPatternAvailableProperty );
			SearchCache.Add( AutomationElement.IsWindowPatternAvailableProperty );
		}

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

		public override UIControlBase[] GetChildren() {
			List<UIControlBase> children = new List<UIControlBase>();

			AutomationElementCollection elements;

			using( SearchCache.Activate() ) {
				elements = automationElement.FindAll(
					TreeScope.Children, new PropertyCondition( AutomationElement.IsControlElementProperty, true ) );
			}

			foreach (AutomationElement element in elements)
			{
				UIAControl control = UIAControl.GetControlByType(element);
				if (control != null)
				{
					control.SetIndex( children.Count );
					children.Add(control);
					control._parent = this;
				}
			}

			return children.ToArray();
		}

		int _index;

		private void SetIndex( int index ) {
			_index = index;
		}

		public override int Index {
			get {
				return _index;
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

		string _id;

		public override string ID
		{
			get 
			{
				if (_id == null)
				{
					if (automationElement.Cached.AutomationId == "" ||
						IsIdHandle(automationElement.Cached.AutomationId))
					{
						_id = UIType;
					}
					else
						_id = automationElement.Cached.AutomationId;

				}
				return _id;
			}
		}

		public override string UIType {
			get {
				// Supports the old style of finding elements by their class type;
				// this is a map from control types to the old class names
				ControlType type = automationElement.Cached.ControlType;

				if( type == ControlType.Button ||
						type == ControlType.Image ||
						type == ControlType.Separator ||
						type == ControlType.ScrollBar ||
						type == ControlType.SplitButton ||
						type == ControlType.TitleBar ||
						type == ControlType.ToolTip )
					return "UIAButton";

				if( type == ControlType.Calendar ||
						type == ControlType.DataGrid ||
						type == ControlType.Table )
					return "UIATable";

				if( type == ControlType.CheckBox )
					return "UIACheckBox";

				if( type == ControlType.ComboBox )
					return "UIAComboBox";

				if( type == ControlType.Custom ||
						type == ControlType.Header ||
						type == ControlType.HeaderItem ||
						type == ControlType.Hyperlink ||
						type == ControlType.StatusBar ||
						type == ControlType.Text ||
						type == ControlType.Thumb )
					return "UIALabel";

				if( type == ControlType.DataItem ||
						type == ControlType.ListItem )
					return "UIAListItem";

				if( type == ControlType.Document )
					return "UIADocument";

				if( type == ControlType.Edit )
					return "UIAEditBox";

				if( type == ControlType.Group )
					return "UIAGroup";

				if( type == ControlType.List )
					return "UIAListBox";

				if( type == ControlType.Menu ||
						type == ControlType.MenuBar )
					return "UIAMenu";

				if( type == ControlType.MenuItem )
					return "UIAMenuItem";

				if( type == ControlType.Pane )
					return "UIAPane";

				if( type == ControlType.ProgressBar ||
						type == ControlType.Slider ||
						type == ControlType.Spinner )
					return "UIARange";

				if( type == ControlType.RadioButton )
					return "UIARadioButton";

				if( type == ControlType.Tab )
					return "UIATab";

				if( type == ControlType.TabItem )
					return "UIATabItem";

				if( type == ControlType.ToolBar )
					return "UIAToolbar";

				if( type == ControlType.Tree )
					return "UIATree";

				if( type == ControlType.TreeItem )
					return "UIATreeItem";

				if( type == ControlType.Window )
					return "UIAWindow";

				return string.Empty;
			}
		}

		string _codePath;

		public override string CodePath
		{
			get
			{
				if (_codePath == null)
				{
					string prefix = String.Empty;
					UIControlBase parent = Parent;
					if (parent == null || (parent is UIAControl) && ((UIAControl)parent).UIAutomationElement.Equals(AutomationElement.RootElement))
						prefix = "code:Desktop.UIA";
					else
						prefix = parent.CodePath;
					string tmpName = Name;
					string tmpID = ID;
					if (tmpName == "" && tmpID.StartsWith("UI"))
						_codePath = prefix + "[@\"" + tmpID + "\", " + IDIndex + "]";
					else
						_codePath = prefix + "[@\"" + tmpName + "\", @\"" + ClassName + "\", @\"" + tmpID + "\"]";
				}
				return _codePath;
			}
		}

		public override Rect Layout
		{
			get { return automationElement.Current.BoundingRectangle; }
		}

		public override string Name
		{
			get { return automationElement.Cached.Name; }
		}

		
		public override string ClassName
		{
			get { return automationElement.Cached.ClassName; }
		}

		public override IntPtr Handle
		{
			get
			{
				return new IntPtr( automationElement.Cached.NativeWindowHandle );
			}
		}

		UIAControl _parent;

		public override UIControlBase Parent
		{
			get
			{
				if (_parent == null)
				{
					AutomationElement element = walker.GetParent(automationElement);
					if (element != null)
					{
						_parent = UIAControl.GetControlByType(element);
					}
				}
				return _parent;
			}
		}

		public override Process Process
		{
			get { return System.Diagnostics.Process.GetProcessById(automationElement.Cached.ProcessId); }
		}

		/// <summary>
		/// Indicates whether the control is a winforms control
		/// </summary>
		[Browsable(false)]
		public bool IsWinForms
		{
			get { return ClassName.StartsWith("WindowsForms"); }
		}

		[ReadOnly(true)]
		[Category("Identifiers")]
		[Description("The index of the ID to uniquely identify controls with same IDs among their siblings")]
		[DisplayName("ID Index")]
		public int IDIndex
		{
			get
			{
				if (idIndex == -1)
				{
					idIndex = 0;
					UIControlBase p = Parent;
					if (p != null)
					{
						List<UIControlBase> siblings = new List<UIControlBase>();
						siblings.AddRange(p.GetChildren());
						if (siblings.Count > 0)
						{
							siblings.Sort(new IDControlSorter());
							((UIAControl)siblings[0]).IDIndex = 0;
							int tmpIndex = 0;
							for (int i = 1; i < siblings.Count; i++)
							{
								UIAControl prevSibling = siblings[i - 1] as UIAControl;
								UIAControl sibling = siblings[i] as UIAControl;
								if (sibling != null)
								{
									if (sibling.ID == prevSibling.ID)
										tmpIndex++;
									else
										tmpIndex = 0;
									if (this.Layout.Equals(sibling.Layout) && this.Handle == sibling.Handle)
										idIndex = tmpIndex;
									else
										sibling.IDIndex = tmpIndex;
								}
							}
						}
					}
				}
				return idIndex;
			}
			set
			{
				idIndex = value;
			}
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

		public UIAControl FindFirstChildByIdIndex( string id, int idIndex ) {
			if (Exists)
			{
				Stopwatch watch = new Stopwatch();
				watch.Start();
				while (watch.ElapsedMilliseconds < PlayerConfig.Default.AutoWaitForControl)
				{
					foreach (QAliber.Engine.Controls.UIA.UIAControl child in GetChildren()) {
						if (child.ID == id && child.IDIndex == idIndex)
							return child;
					}
					System.Threading.Thread.Sleep(50);
				}
				QAliber.Logger.Log.Default.Warning(
					string.Format("Cannot find control [{0}, {1}] for control {2}",
									id, idIndex, CodePath), "", QAliber.Logger.EntryVerbosity.Internal);
			}
			return new UIANullControl();
		}

		public override void Refresh()
		{
			base.Refresh();
			idIndex = -1;
			_codePath = null;
			_id = null;
			_parent = null;
		}

		/// <summary>
		/// Applicable only on WinFormsControl (you can query it by the IsWinForms property), gets the serializable properties of the control with types that are commonly references
		/// </summary>
		public void GetWinFormsProperties()
		{
			if (IsWinForms)
			{
				File.Delete(UIControlBase.tmpFile);
				Injector.Launch(new IntPtr(UIAutomationElement.Cached.NativeWindowHandle),
							this.GetType().Assembly.Location, 
							typeof(UIAControl).FullName, "QueryWinForms",
							UIAutomationElement.Cached.NativeWindowHandle.ToString());
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
					_extendedProperties = bf.Deserialize(fs) as Dictionary<string, object>;

				}
			}
		}

		public override PropertyDescriptorCollection GetProperties( Attribute[] attributes ) {
			// BJC: Adds in the properties that would have been grabbed from the WinForms spy DLL.
			// I prefer not to do things that way, so I'm planning to add in all the UI Automation properties
			// I can find.
			PropertyDescriptorCollection props = base.GetProperties( attributes );

			Attribute[] attrs = new Attribute[] { new CategoryAttribute( "Additional Properties" ) };

			foreach( var kv in _extendedProperties ) {
				props.Add( new ExtendedPropertyDescriptor( kv.Key, kv.Value, attrs ) );
			}

			return props;
		}

		/// <summary>
		/// Converts a UIAutomation element to QAliber UIAControl
		/// </summary>
		/// <param name="element">The UIAutomation element to convert</param>
		/// <returns></returns>
		public static UIAControl GetControlByType(AutomationElement element)
		{
			return new UIAControl( element );
		}

		public override T GetControlInterface<T>() {
			T result = GetControlInterface( typeof(T) ) as T;
			return result ?? base.GetControlInterface<T>();
		}

		private object GetControlInterface( Type type ) {
			if( type == typeof(IText) ) {
				// Qaliber's IText covers both automations Text and Value patterns
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsTextPatternAvailableProperty )) &&
					!((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsValuePatternAvailableProperty )) )
					return null;

				ValuePattern valuePattern = (ValuePattern) automationElement.GetCurrentPattern( ValuePattern.Pattern );
				TextPattern textPattern = (TextPattern) automationElement.GetCurrentPattern( TextPattern.Pattern );

				return new TextPatternImpl( textPattern, valuePattern );
			}

			if( type == typeof(ISelector) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsScrollPatternAvailableProperty )) &&
						automationElement.Cached.ControlType != ControlType.ComboBox &&
						automationElement.Cached.ControlType != ControlType.Menu &&
						automationElement.Cached.ControlType != ControlType.MenuBar &&
						automationElement.Cached.ControlType != ControlType.MenuItem &&
						automationElement.Cached.ControlType != ControlType.List &&
						automationElement.Cached.ControlType != ControlType.Tree &&
						automationElement.Cached.ControlType != ControlType.Tab )
					return null;

				SelectionPattern selectionPattern = (SelectionPattern) automationElement.GetCurrentPattern( SelectionPattern.Pattern );

				return new SelectorPatternImpl( automationElement, selectionPattern );
			}

			if( type == typeof(IWindowPattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsWindowPatternAvailableProperty )) )
					return null;

				WindowPattern windowPattern = (WindowPattern) automationElement.GetCurrentPattern( WindowPattern.Pattern );
				return new WindowPatternImpl( windowPattern );
			}

			if( type == typeof(ITransformPattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsTransformPatternAvailableProperty )) )
					return null;

				TransformPattern transformPattern = (TransformPattern) automationElement.GetCurrentPattern( TransformPattern.Pattern );
				return new TransformPatternImpl( transformPattern );
			}

			return null;
		}

		class TextPatternImpl : IText {
			TextPattern _textPattern;
			ValuePattern _valuePattern;

			public TextPatternImpl( TextPattern textPattern, ValuePattern valuePattern ) {
				_textPattern = textPattern;
				_valuePattern = valuePattern;
			}

			public string Text {
				get {
					if( _textPattern != null )
						return _textPattern.DocumentRange.GetText( -1 );

					return _valuePattern.Current.Value;
				}
			}
		}

		private static OrCondition __itemPropertyCondition = new OrCondition(
			new PropertyCondition( AutomationElement.ControlTypeProperty, ControlType.ListItem ),
			new PropertyCondition( AutomationElement.ControlTypeProperty, ControlType.MenuItem ),
			new PropertyCondition( AutomationElement.ControlTypeProperty, ControlType.DataItem ),
			new PropertyCondition( AutomationElement.ControlTypeProperty, ControlType.TabItem ),
			new PropertyCondition( AutomationElement.ControlTypeProperty, ControlType.TreeItem ) );

		class SelectorPatternImpl : ISelector {
			// BJC: Not at all a fan of this interface. Not one bit.
			AutomationElement _element;
			SelectionPattern _pattern;

			public SelectorPatternImpl( AutomationElement element, SelectionPattern pattern ) {
				_element = element;
				_pattern = pattern;
			}

			public void Select( string name ) {
				// The PatternsExecutor goes through the children and tries
				// to find one with the right name
				CacheRequest cache = new CacheRequest();
				cache.AutomationElementMode = AutomationElementMode.Full;
				cache.TreeScope = TreeScope.Element;
				cache.Add( AutomationElement.NameProperty );
				cache.Add( AutomationElement.IsSelectionItemPatternAvailableProperty );

				AutomationElementCollection children;

				using( cache.Activate() ) {
					children = _element.FindAll( TreeScope.Descendants, __itemPropertyCondition );
				}

				AutomationElement target = children.Cast<AutomationElement>()
					.FirstOrDefault( child => StringComparer.InvariantCultureIgnoreCase.Equals( child.Cached.Name, name ) );

				if( target == null ) {
					Log.Default.Error( "Could not find name " + name + " in '" + _element.Cached.Name + "'", "", EntryVerbosity.Internal );
					return;
				}

				if( ((bool) target.GetCachedPropertyValue( AutomationElement.IsSelectionItemPatternAvailableProperty )) ) {
					var pattern = (SelectionItemPattern) target.GetCurrentPattern( SelectionItemPattern.Pattern );
					pattern.Select();
				}
				else {
					// Try to click it
					QAliber.Logger.Log.Default.Warning( "Could not find the item selectable, trying to click it", "", EntryVerbosity.Internal );
					GetControlByType( target ).Click();
				}
			}

			public void Select( int index ) {
				CacheRequest cache = new CacheRequest();
				cache.AutomationElementMode = AutomationElementMode.Full;
				cache.TreeScope = TreeScope.Element;
				cache.Add( AutomationElement.IsSelectionItemPatternAvailableProperty );

				AutomationElementCollection children;

				using( cache.Activate() ) {
					children = _element.FindAll( TreeScope.Children, __itemPropertyCondition );
				}

				AutomationElement target = children.Cast<AutomationElement>().ElementAtOrDefault( index );

				if( target == null ) {
					Log.Default.Error( "Could not find index " + index + " in '" + _element.Cached.Name + "'", "", EntryVerbosity.Internal );
					return;
				}

				if( ((bool) target.GetCachedPropertyValue( AutomationElement.IsSelectionItemPatternAvailableProperty )) ) {
					var pattern = (SelectionItemPattern) target.GetCurrentPattern( SelectionItemPattern.Pattern );
					pattern.Select();
				}
				else {
					// Try to click it
					QAliber.Logger.Log.Default.Warning( "Could not find the item selectable, trying to click it", "", EntryVerbosity.Internal );
					GetControlByType( target ).Click();
				}
			}

			public UIAControl[] SelectedItems {
				get {
					if( _pattern == null ) {
						Log.Default.Error( "Could not get selection for " + _element.Cached.ControlType.ProgrammaticName +
							" '" + _element.Cached.Name + "'", "", EntryVerbosity.Internal);

						return new UIAControl[0];
					}

					using( SearchCache.Activate() ) {
						return _pattern.Current.GetSelection().Select( e => GetControlByType( e ) ).ToArray();
					}
				}
			}

			public string[] Items {
				get {
					CacheRequest cache = new CacheRequest();
					cache.Add( AutomationElement.NameProperty );

					using( cache.Activate() ) {
						return _element.FindAll( TreeScope.Descendants, __itemPropertyCondition )
							.Cast<AutomationElement>()
							.Select( e => e.Cached.Name )
							.ToArray();
					}
				}
			}

			public bool CanSelectMultiple {
				get {
					if( _pattern == null ) {
						Log.Default.Error( "Selection pattern not supported on " + _element.Cached.ControlType.ProgrammaticName +
							" '" + _element.Cached.Name + "'", "", EntryVerbosity.Internal);

						return false;
					}

					return _pattern.Current.CanSelectMultiple;
				}
			}
		}

		class WindowPatternImpl : IWindowPattern {
			WindowPattern _pattern;

			public WindowPatternImpl( WindowPattern pattern ) {
				_pattern = pattern;
			}

			public bool CanMinimize {
				get { return _pattern.Current.CanMinimize; }
			}

			public bool CanMaximize {
				get { return _pattern.Current.CanMaximize; }
			}

			public void Close() {
				_pattern.Close();
			}

			public QAliber.Engine.Patterns.WindowVisualState State
				{ get { return (QAliber.Engine.Patterns.WindowVisualState)(int) _pattern.Current.WindowVisualState; } }

			public void SetState( QAliber.Engine.Patterns.WindowVisualState state ) {
				_pattern.SetWindowVisualState( (System.Windows.Automation.WindowVisualState)(int) state );
			}
		}

		class TransformPatternImpl : ITransformPattern {
			TransformPattern _pattern;

			public TransformPatternImpl( TransformPattern pattern ) {
				_pattern = pattern;
			}

			public bool CanMove {
				get { return _pattern.Current.CanMove; }
			}

			public bool CanResize {
				get { return _pattern.Current.CanResize; }
			}

			public bool CanRotate {
				get { return _pattern.Current.CanRotate; }
			}

			public void Move( double x, double y ) {
				_pattern.Move( x, y );
			}

			public void Resize( double width, double height ) {
				_pattern.Resize( width, height );
			}

			public void Rotate( double degrees ) {
				_pattern.Rotate( degrees );
			}
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
		protected int idIndex = -1;

		protected static TreeWalker walker = TreeWalker.ControlViewWalker;
		#endregion

	}

	

	

	
}
