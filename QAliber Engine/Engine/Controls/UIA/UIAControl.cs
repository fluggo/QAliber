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
using System.Globalization;
using QAliber.Engine.Win32;

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
		static TypeMapEntry[] __idToElementName;
		string _xpathElementName;

		class TypeMapEntry {
			public TypeMapEntry( ControlType type, string elementName, string uiTypeName ) {
				ControlType = type;
				ElementName = elementName;
				UITypeName = uiTypeName;
			}

			public ControlType ControlType { get; set; }
			public string ElementName { get; set; }
			public string UITypeName { get; set; }
		}

		static UIAControl() {
			// Caches those properties we expect in the UIAControl
			SearchCache = new CacheRequest();
			SearchCache.AutomationElementMode = AutomationElementMode.Full;
			SearchCache.TreeScope = TreeScope.Element;
			SearchCache.Add( AutomationElement.AutomationIdProperty );
			SearchCache.Add( AutomationElement.NameProperty );
			SearchCache.Add( AutomationElement.BoundingRectangleProperty );
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

			// Build a table of XPath element names to UI automation IDs
			__idToElementName = new TypeMapEntry[] {
				new TypeMapEntry( ControlType.Button, "button", "UIAButton" ),
				new TypeMapEntry( ControlType.Calendar, "calendar", "UIATable" ),
				new TypeMapEntry( ControlType.CheckBox, "checkbox", "UIACheckBox" ),
				new TypeMapEntry( ControlType.ComboBox, "combobox", "UIAComboBox" ),
				new TypeMapEntry( ControlType.Custom, "custom", "UIALabel" ),
				new TypeMapEntry( ControlType.DataGrid, "datagrid", "UIATable" ),
				new TypeMapEntry( ControlType.DataItem, "dataitem", "UIAListItem" ),
				new TypeMapEntry( ControlType.Document, "document", "UIADocument" ),
				new TypeMapEntry( ControlType.Edit, "edit", "UIAEditBox" ),
				new TypeMapEntry( ControlType.Group, "group", "UIAGroup" ),
				new TypeMapEntry( ControlType.Header, "header", "UIALabel" ),
				new TypeMapEntry( ControlType.HeaderItem, "headeritem", "UIALabel" ),
				new TypeMapEntry( ControlType.Hyperlink, "hyperlink", "UIALabel" ),
				new TypeMapEntry( ControlType.Image, "image", "UIAButton" ),
				new TypeMapEntry( ControlType.List, "list", "UIAListBox" ),
				new TypeMapEntry( ControlType.ListItem, "listitem", "UIAListItem" ),
				new TypeMapEntry( ControlType.Menu, "menu", "UIAMenu" ),
				new TypeMapEntry( ControlType.MenuBar, "menubar", "UIAMenu" ),
				new TypeMapEntry( ControlType.MenuItem, "menuitem", "UIAMenuItem" ),
				new TypeMapEntry( ControlType.Pane, "pane", "UIAPane" ),
				new TypeMapEntry( ControlType.ProgressBar, "progressbar", "UIARange" ),
				new TypeMapEntry( ControlType.RadioButton, "radiobutton", "UIARadioButton" ),
				new TypeMapEntry( ControlType.ScrollBar, "scrollbar", "UIAButton" ),
				new TypeMapEntry( ControlType.Separator, "separator", "UIAButton" ),
				new TypeMapEntry( ControlType.Slider, "slider", "UIARange" ),
				new TypeMapEntry( ControlType.Spinner, "spinner", "UIARange" ),
				new TypeMapEntry( ControlType.SplitButton, "splitbutton", "UIAButton" ),
				new TypeMapEntry( ControlType.StatusBar, "statusbar", "UIALabel" ),
				new TypeMapEntry( ControlType.Tab, "tab", "UIATab" ),
				new TypeMapEntry( ControlType.TabItem, "tabitem", "UIATabItem" ),
				new TypeMapEntry( ControlType.Table, "table", "UIATable" ),
				new TypeMapEntry( ControlType.Text, "text", "UIALabel" ),
				new TypeMapEntry( ControlType.Thumb, "thumb", "UIALabel" ),
				new TypeMapEntry( ControlType.TitleBar, "titlebar", "UIAButton" ),
				new TypeMapEntry( ControlType.ToolBar, "toolbar", "UIAToolbar" ),
				new TypeMapEntry( ControlType.ToolTip, "tooltip", "UIAButton" ),
				new TypeMapEntry( ControlType.Tree, "tree", "UIATree" ),
				new TypeMapEntry( ControlType.TreeItem, "treeitem", "UIATreeItem" ),
				new TypeMapEntry( ControlType.Window, "window", "UIAWindow" ),
			};
		}

		#region Constructores
		/// <summary>
		/// Abstract class Ctor, used to initiate the base UI control with the UI Automation element.
		/// </summary>
		/// <param name="element">
		/// Reperesents UI Automation element in the UI Automation tree,and contains
		/// values used as identifiers by UI Automation client applications.
		///</param>
		protected UIAControl(AutomationElement element)
		{
			automationElement = element;

			if( automationElement == null ) {
				_xpathElementName = "null";
			}
			else {
				ControlType controlType = automationElement.Cached.ControlType;

				foreach( var map in __idToElementName ) {
					if( map.ControlType == controlType ) {
						_xpathElementName = map.ElementName;
						break;
					}
				}
			}
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
				UIAControl control = new UIAControl( element );
				if (control != null)
				{
					control.SetIndex( children.Count );
					children.Add(control);
					control._parent = this;
				}
			}

			return children.ToArray();
		}

		public string XPathElementName {
			get {
				return _xpathElementName;
			}
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

		public override string ID {
			get {
				object value = automationElement.GetCachedPropertyValue( AutomationElement.AutomationIdProperty, true );

				if( value == AutomationElement.NotSupported )
					return null;

				return (string) value;
			}
		}

		public override string UIType {
			get {
				// Supports the old style of finding elements by their class type;
				// this is a map from control types to the old class names
				ControlType type = automationElement.Cached.ControlType;
				TypeMapEntry entry = __idToElementName.FirstOrDefault( m => m.ControlType == type );

				if( entry != null )
					return entry.UITypeName;

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
						prefix = "uia:";
					else
						prefix = parent.CodePath;

					prefix += "/" + XPathElementName;

					if( Name == null && ID == null ) {
						_codePath = prefix + "[" + Index.ToString( CultureInfo.InvariantCulture ) + "]";
					}
					else {
						List<string> conditions = new List<string>();

						// The name, which we leave out for title bars because it's redundant
						// We also leave it out for the edits of combo boxes, because it tends to be
						// just the contents of the combo box
						if( !string.IsNullOrEmpty( Name )
								&& automationElement.Cached.ControlType != ControlType.TitleBar
								&& !(automationElement.Cached.ControlType == ControlType.Edit &&
									automationElement.Cached.AutomationId == "1001" &&
									((UIAControl) Parent).automationElement.Cached.ControlType == ControlType.ComboBox) )
							conditions.Add( "@Name=\'" + XPath.EscapeLiteral( Name ) + "\'" );

						if( (automationElement.Cached.ControlType == ControlType.Window || ID == null) && ClassName != null )
							conditions.Add( "@ClassName=\'" + XPath.EscapeLiteral( ClassName ) + "\'" );

						// The ID, which we leave out for scroll bars because it keeps changing
						if( ID != null ) {
							bool useId = !(automationElement.Cached.ControlType == ControlType.Pane && Name == "Horizontal Scroll Bar")
								&& !(automationElement.Cached.ControlType == ControlType.Pane && Name == "Vertical Scroll Bar");

							if( useId )
								conditions.Add( "@ID=\'" + XPath.EscapeLiteral( ID ) + "\'" );
						}

						if( conditions.Count != 0 )
							_codePath = prefix + "[" + string.Join( " and ", conditions ) + "]";
						else
							_codePath = prefix;
					}
				}
				return _codePath;
			}
		}

		/// <summary>
		/// Returns the first control identified by the given XPath expression.
		/// </summary>
		/// <param name="xpath">XPath expression to evaluate.</param>
		/// <returns>A <see cref="UIAControl"/> if the expression evaluates to one,
		///   or a <see cref="UIANullControl"/> if the result returns empty.</returns>
		public static UIAControl FindControlByXPath( string xpath ) {
			XPathExpression exp = XPath.Parse( xpath, true );
			UIAXPathEvaluator evaluator = new UIAXPathEvaluator();

			IXPathNode[] expResult = evaluator.Evaluate( exp ) as IXPathNode[];

			if( expResult == null )
				throw new ArgumentException( "XPath did not evaluate to a control." );

			if( expResult.Length == 0 )
				return new UIANullControl();

			XPathAdapter adapter = expResult[0] as XPathAdapter;

			if( adapter == null )
				throw new ArgumentException( "XPath evaluated to a non-control node." );

			return adapter.Owner;
		}

		public override Rect Layout
		{
			get { return automationElement.Cached.BoundingRectangle; }
		}

		public override string Name {
			get {
				object value = automationElement.GetCachedPropertyValue( AutomationElement.NameProperty, true );

				if( value == AutomationElement.NotSupported )
					return null;

				return (string) value;
			}
		}

		/// <summary>
		/// Determines whether the window is marked topmost (always-on-top).
		/// </summary>
		/// <value>True if the window is topmost, false otherwise.</value>
		public bool AlwaysOnTop {
			get {
				return User32.GetAlwaysOnTop( (IntPtr) automationElement.Cached.NativeWindowHandle );
			}
		}

		public override string ClassName
		{
			get {
				object value = automationElement.GetCachedPropertyValue( AutomationElement.ClassNameProperty, true );

				if( value == AutomationElement.NotSupported )
					return null;

				return (string) value;
			}
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
		public override void SetFocus() {
			automationElement.SetFocus();
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

			{
				ISelectionItemPattern pattern = GetControlInterface<ISelectionItemPattern>();

				if( pattern != null ) {
					props.Add( MakePropertyDescriptor( "IsSelected", () => pattern.IsSelected ) );
				}
			}

			{
				ITogglePattern pattern = GetControlInterface<ITogglePattern>();

				if( pattern != null ) {
					props.Add( MakePropertyDescriptor( "ToggleState", () => pattern.ToggleState ) );
				}
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
			return new UIAControl( element.GetUpdatedCache( SearchCache ) );
		}

		private void UpdateCache() {
			automationElement = automationElement.GetUpdatedCache( SearchCache );
			Refresh();
		}

		public override T GetControlInterface<T>() {
			T result = GetControlInterface( typeof(T) ) as T;
			return result ?? base.GetControlInterface<T>();
		}

		private object GetControlInterface( Type type ) {
			if( type == typeof(IText) ) {
				// Qaliber's IText covers both automations Text and Value patterns
				ValuePattern valuePattern = null;
				TextPattern textPattern = null;

				if( (bool) automationElement.GetCachedPropertyValue( AutomationElement.IsValuePatternAvailableProperty ) )
					valuePattern = (ValuePattern) automationElement.GetCurrentPattern( ValuePattern.Pattern );

				if( (bool) automationElement.GetCachedPropertyValue( AutomationElement.IsTextPatternAvailableProperty ) )
					textPattern = (TextPattern) automationElement.GetCurrentPattern( TextPattern.Pattern );

				if( valuePattern == null && textPattern == null )
					return null;

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

			if( type == typeof(ISelectionItemPattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsSelectionItemPatternAvailableProperty )) )
					return null;

				SelectionItemPattern pattern = (SelectionItemPattern) automationElement.GetCurrentPattern( SelectionItemPattern.Pattern );
				return new SelectionItemPatternImpl( pattern );
			}

			if( type == typeof(ITogglePattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsTogglePatternAvailableProperty )) )
					return null;

				TogglePattern pattern = (TogglePattern) automationElement.GetCurrentPattern( TogglePattern.Pattern );
				return new TogglePatternImpl( pattern );
			}

			if( type == typeof(IInvokePattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsInvokePatternAvailableProperty )) )
					return null;

				InvokePattern pattern = (InvokePattern) automationElement.GetCurrentPattern( InvokePattern.Pattern );
				return new InvokePatternImpl( pattern );
			}

			if( type == typeof(IValuePattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsValuePatternAvailableProperty )) )
					return null;

				ValuePattern pattern = (ValuePattern) automationElement.GetCurrentPattern( ValuePattern.Pattern );
				return new ValuePatternImpl( pattern );
			}

			if( type == typeof(IGridPattern) ) {
				if( (bool) automationElement.GetCachedPropertyValue( AutomationElement.IsGridPatternAvailableProperty ) ) {
					// Return the real thing
					return new GridPatternImpl( automationElement,
						(GridPattern) automationElement.GetCurrentPattern( GridPattern.Pattern ) );
				}

				// Pull up the .NET DataGridView control (which does not have UI automation support) if we can
				if( automationElement.Cached.Name == "DataGridView" && automationElement.Cached.ControlType == ControlType.Table )
					return new DataGridViewGridImpl( automationElement );

				return null;
			}

			if( type == typeof(IScrollItemPattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsScrollItemPatternAvailableProperty )) )
					return null;

				ScrollItemPattern pattern = (ScrollItemPattern) automationElement.GetCurrentPattern( ScrollItemPattern.Pattern );
				return new ScrollItemPatternImpl( this, pattern );
			}

			if( type == typeof(IExpandCollapsePattern) ) {
				if( !((bool) automationElement.GetCachedPropertyValue( AutomationElement.IsExpandCollapsePatternAvailableProperty )) )
					return null;

				ExpandCollapsePattern pattern = (ExpandCollapsePattern) automationElement.GetCurrentPattern( ExpandCollapsePattern.Pattern );
				return new ExpandCollapsePatternImpl( pattern );
			}

			if( type == typeof(IListPattern) ) {
				ControlType controlType = automationElement.Cached.ControlType;
				UIAControl listControl = this;

				if( controlType != ControlType.List && controlType != ControlType.ComboBox )
					return null;

				if( controlType == ControlType.ComboBox ) {
					IExpandCollapsePattern expCollapse = listControl.GetControlInterface<IExpandCollapsePattern>();

					if( expCollapse != null && expCollapse.ExpandCollapseState != Patterns.ExpandCollapseState.Expanded )
						expCollapse.Expand();

					listControl = listControl.GetChildren().Cast<UIAControl>()
						.Where( c => c.automationElement.Cached.ControlType == ControlType.List )
						.FirstOrDefault();

					if( expCollapse != null )
						expCollapse.Collapse();

					if( listControl == null )
						throw new ElementNotAvailableException( "Couldn't find the list associated with the combo box." );
				}

				// Finally, listControl should be of type List
				return new ListPatternImpl( listControl );
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
					new UIAControl( target ).Click();
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
					new UIAControl( target ).Click();
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
						return _pattern.Current.GetSelection().Select( e => new UIAControl( e ) ).ToArray();
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

		class SelectionItemPatternImpl : ISelectionItemPattern {
			SelectionItemPattern _pattern;

			public SelectionItemPatternImpl( SelectionItemPattern pattern ) {
				_pattern = pattern;
			}

			public bool IsSelected {
				get { return _pattern.Current.IsSelected; }
			}

			public void AddToSelection() {
				_pattern.AddToSelection();
			}

			public void RemoveFromSelection() {
				_pattern.RemoveFromSelection();
			}

			public void Select() {
				_pattern.Select();
			}
		}

		class TogglePatternImpl : ITogglePattern {
			TogglePattern _pattern;

			public TogglePatternImpl( TogglePattern pattern ) {
				_pattern = pattern;
			}

			public QAliber.Engine.Patterns.ToggleState ToggleState
				{ get { return (QAliber.Engine.Patterns.ToggleState)(int) _pattern.Current.ToggleState; } }

			public void Toggle() {
				_pattern.Toggle();
			}
		}

		class InvokePatternImpl : IInvokePattern {
			InvokePattern _pattern;

			public InvokePatternImpl( InvokePattern pattern ) {
				_pattern = pattern;
			}

			public void Invoke() {
				_pattern.Invoke();
			}
		}

		class ValuePatternImpl : IValuePattern {
			ValuePattern _pattern;

			public ValuePatternImpl( ValuePattern pattern ) {
				_pattern = pattern;
			}

			public bool IsReadOnly {
				get { return _pattern.Current.IsReadOnly; }
			}

			public string Value {
				get { return _pattern.Current.Value; }
			}

			public void SetValue( string value ) {
				_pattern.SetValue( value );
			}
		}

		class GridPatternImpl : IGridPattern {
			AutomationElement _element;
			GridPattern _pattern;

			static CacheRequest __gridCacheRequest;

			static GridPatternImpl() {
				__gridCacheRequest = new CacheRequest();
				__gridCacheRequest.Add( AutomationElement.ControlTypeProperty );
				__gridCacheRequest.Add( AutomationElement.NameProperty );
				__gridCacheRequest.Add( AutomationElement.OrientationProperty );
				__gridCacheRequest.Add( AutomationElement.IsTablePatternAvailableProperty );
				__gridCacheRequest.Add( ValuePattern.ValueProperty );
				__gridCacheRequest.Add( TablePattern.Pattern );
				__gridCacheRequest.Add( TablePattern.ColumnHeadersProperty );
				__gridCacheRequest.TreeScope = TreeScope.Element | TreeScope.Children | TreeScope.Descendants;
				__gridCacheRequest.AutomationElementMode = AutomationElementMode.Full;
			}

			public GridPatternImpl( AutomationElement element, GridPattern pattern ) {
				_element = element;
				_pattern = pattern;
				_element = element.GetUpdatedCache( __gridCacheRequest );
			}

			public int ColumnCount {
				get { return _pattern.Current.ColumnCount; }
			}

			public int RowCount {
				get { return _pattern.Current.RowCount; }
			}

			public UIControlBase GetItem( int row, int column ) {
				return GetControlByType( _pattern.GetItem( row, column ) );
			}

			public string[][] CaptureGrid( out string[] headers ) {
				AutomationElement element = _element.GetUpdatedCache( __gridCacheRequest );
				TablePattern tablePattern = null;

				if( (bool) element.GetCachedPropertyValue( AutomationElement.IsTablePatternAvailableProperty ) )
					tablePattern = (TablePattern) element.GetCachedPattern( TablePattern.Pattern );

				for( int i = 0; i < 10; i++ ) {
					bool complete = false;

					try {
						// The structure of a our enhanced DataGridView is:
						//		header[@Orientation=Horizontal]
						//			headeritem["@Name="H"]
						//			...
						//		group[@Name="Row 0"]
						//			// headeritem[@Name="Row 0"] - not yet
						//			dataitem[@Name="H Row 0"]
						//			...
						//		group[@Name="Row 1"]
						//			headeritem[@Name="Row 1"]
						//			dataitem[@Name="H Row 1"]
						//			...
						headers = null;
						AutomationElement[] children = element.CachedChildren.Cast<AutomationElement>().ToArray();

						if( tablePattern != null ) {
							// Try just getting the headers directly
							var headerItems = tablePattern.Cached.GetColumnHeaders();

							if( headerItems != null && headerItems.Length != 0 )
								headers = headerItems.Select( item => item.Cached.Name ).ToArray();
						}

						if( headers == null ) {
							AutomationElement headerRow = children.FirstOrDefault( child =>
								child.Cached.ControlType == ControlType.Header &&
								child.Cached.Orientation == OrientationType.Horizontal );

							headers = headerRow.CachedChildren.Cast<AutomationElement>()
								.Select( child => child.Cached.Name ).ToArray();
						}

						AutomationElement[][] rows = children
							.Where( child => child.Cached.ControlType == ControlType.Group )
							.Select( row => row.CachedChildren.Cast<AutomationElement>()
								.Where( rowItem => rowItem.Cached.ControlType == ControlType.DataItem )
								.ToArray() )
							.ToArray();

						if( rows.Length == 0 )
							return new string[0][];

						int columnCount = rows.Select( row => row.Length ).Max();
						int rowCount = rows.Length;

						// Try to verify we have the whole thing
						complete = (headers == null || columnCount == headers.Length) &&
							rows.All( row => row.Length == columnCount && row.All( cell => cell != null ) );

						if( !complete ) {
							if( (headers != null && columnCount != headers.Length) )
								Debug.WriteLine( "Missing column names" );
							else if( rows.Any( row => row.Length != columnCount ) )
								Debug.WriteLine( "Row incomplete, " + rows.Min( row => row.Length ).ToString() + " vs " + columnCount.ToString() );
							else if( rows.Any( row => row.Any( cell => cell == null ) ) )
								Debug.WriteLine( "Cell missing" );
						}

						if( !complete ) {
							if( i == 9 )
								break;
						}

						// Now walk through the rows
						return children.Where( child => child.Cached.ControlType == ControlType.Group )
							.Select( row => row.CachedChildren.Cast<AutomationElement>()
								.Where( rowItem => rowItem.Cached.ControlType != ControlType.HeaderItem )
								.Select( cell => {
									if( cell == null )
										return null;

									object value = cell.GetCachedPropertyValue( ValuePattern.ValueProperty );

									if( value == AutomationElement.NotSupported )
										return null;

									return (string) value;
								} ).ToArray() ).ToArray();
					}
					catch( ElementNotAvailableException ) {
						Debug.WriteLine( "Caught ElementNotAvailableException" );
					}
				}

				throw new InvalidOperationException( "Tried to capture the grid ten times. Could not get a complete copy of the grid." );
			}
		}

		/// <summary>
		/// Implements the <see cref="IGridPattern"/> for <see cref="DataGridView"/> controls that
		/// don't implement their own.
		/// </summary>
		class DataGridViewGridImpl : IGridPattern {
			AutomationElement _element;
			AutomationElement[] _headers;
			AutomationElement[][] _cells;
			string[] _columnNames;
			int _columnCount, _rowCount;
			string[][] _capturedValues;

			static CacheRequest __gridCacheRequest;

			static DataGridViewGridImpl() {
				__gridCacheRequest = new CacheRequest();
				__gridCacheRequest.Add( AutomationElement.ControlTypeProperty );
				__gridCacheRequest.Add( AutomationElement.NameProperty );
				__gridCacheRequest.Add( ValuePattern.ValueProperty );
				__gridCacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;
				__gridCacheRequest.AutomationElementMode = AutomationElementMode.None;
			}

			public DataGridViewGridImpl( AutomationElement element ) {
				// We don't have much of a choice; we have to capture the whole
				// grid to know anything about what's going on
				_element = element;

				for( int i = 0; i < 10; i++ ) {
					bool complete = false;

				try {
					// Here's the deal: The contents of the DataGridView could be changing right
					// underneath us. We will make five attempts to get the whole grid, as best
					// we can.

					// The structure of a raw DataGridView is:
					//		custom[@Name="Top Row"]
					//			header["@Name="H"]
					//			...
					//		custom[@Name="Row 0"]
					//			header[@Name="Row 0"]
					//			custom[@Name="H Row 0"]
					//			...
					//		custom[@Name="Row 1"]
					//			header[@Name="Row 1"]
					//			custom[@Name="H Row 1"]
					//			...
					//
					// The following code assumes that all rows are present and appear sequentially in the tree.
					// It also assumes every row has a complete set of cells, and that the cells and row headers
					// are the only things in the row and they're in order.
					AutomationElement[] rows;

					using( __gridCacheRequest.Activate() ) {
						rows = _element.FindAll( TreeScope.Children,
								new PropertyCondition( AutomationElement.IsControlElementProperty, true ) )
							.Cast<AutomationElement>()
							.ToArray();
					}

					// Now figure out the columns
					AutomationElement topRow = rows.FirstOrDefault( elem => elem.Cached.Name == "Top Row" );

					if( topRow != null ) {
						_headers = topRow.CachedChildren.Cast<AutomationElement>()
							.Where( cell => cell.Cached.ControlType == ControlType.Header )
							.ToArray();

						_columnNames = _headers.Select( cell => cell.Cached.Name ).ToArray();
					}

					_cells = rows
						.Where( row => row.Cached.Name.StartsWith( "Row " ) )
						.Select( row => row.CachedChildren.Cast<AutomationElement>()
							.Where( cell => cell.Cached.ControlType != ControlType.Header )
							.ToArray() )
						.ToArray();

					_columnCount = _cells.Select( row => row.Length ).Max();
					_rowCount = _cells.Length;

					// Try to verify we have the whole thing
					complete = (_columnNames == null || _columnCount == _columnNames.Length) &&
						_cells.All( row => row.Length == _columnCount && row.All( cell => cell != null ) );

					if( !complete ) {
						if( (_columnNames != null && _columnCount != _columnNames.Length) )
							Debug.WriteLine( "Missing column names" );
						else if( _cells.Any( row => row.Length != _columnCount ) )
							Debug.WriteLine( "Row incomplete, " + _cells.Min( row => row.Length ).ToString() + " vs " + _columnCount.ToString() );
						else if( _cells.Any( row => row.Any( cell => cell == null ) ) )
							Debug.WriteLine( "Cell missing" );
					}

					_capturedValues = _cells.Select( row =>
							row.Select( cell => {
								if( cell == null )
									return null;

								object value = cell.GetCachedPropertyValue( ValuePattern.ValueProperty );

								if( value == AutomationElement.NotSupported )
									return null;

								return (string) value;
							} ).ToArray()
						).ToArray();
				}
				catch( ElementNotAvailableException ) {
					Debug.WriteLine( "Caught ElementNotAvailableException" );
				}

					if( !complete ) {
						if( i == 9 )
							throw new InvalidOperationException( "Tried to capture the grid ten times. Could not get a complete copy of the grid." );
						else
							continue;
					}

					break;
				}
			}

			public int ColumnCount {
				get { return _columnCount; }
			}

			public int RowCount {
				get { return _rowCount; }
			}

			public UIControlBase GetItem( int row, int column ) {
				if( row < 0 || row >= _rowCount )
					throw new ArgumentOutOfRangeException( "row" );

				if( column < 0 || column >= _columnCount )
					throw new ArgumentOutOfRangeException( "column" );

				AutomationElement[] rowCells = _cells[row];
				AutomationElement cell = null;

				if( column < rowCells.Length )
					cell = rowCells[column];

				if( cell == null )
					return new UIANullControl();

				return GetControlByType( cell );
			}

			public string[][] CaptureGrid( out string[] headers ) {
				// We already have everything, just fill in the numbers
				headers = _columnNames;
				return _capturedValues;
			}
		}

		class ScrollItemPatternImpl : IScrollItemPattern {
			ScrollItemPattern _pattern;
			UIAControl _owner;

			public ScrollItemPatternImpl( UIAControl owner, ScrollItemPattern pattern ) {
				_owner = owner;
				_pattern = pattern;
			}

			public void ScrollIntoView() {
				_pattern.ScrollIntoView();
				_owner.UpdateCache();
			}
		}

		class ListPatternImpl : IListPattern {
			// The list pattern isn't a pattern offered by UIA;
			// we offer it as a way of discovering list items in
			// a convenient, common way. The owner should be of type List.
			UIAControl _owner;
			UIAControl[] _items;
			string[] _itemList;

			public ListPatternImpl( UIAControl owner ) {
				_owner = owner;
				_items = Array.ConvertAll( owner.GetChildren(), c => (UIAControl) c );
				_itemList = Array.ConvertAll( _items, c => c.Name );
			}

			public string[] CaptureList() {
				return _itemList;
			}

			public UIAControl GetItem( string name ) {
				int index = Array.IndexOf( _itemList, name );

				if( index == -1 )
					return null;

				return _items[index];
			}
		}

		class ExpandCollapsePatternImpl : IExpandCollapsePattern {
			ExpandCollapsePattern _pattern;

			public ExpandCollapsePatternImpl( ExpandCollapsePattern pattern ) {
				_pattern = pattern;
			}

			public void Collapse() {
				_pattern.Collapse();
			}

			public void Expand() {
				_pattern.Expand();
			}

			public QAliber.Engine.Patterns.ExpandCollapseState ExpandCollapseState {
				get { return (QAliber.Engine.Patterns.ExpandCollapseState)(int) _pattern.Current.ExpandCollapseState; }
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
