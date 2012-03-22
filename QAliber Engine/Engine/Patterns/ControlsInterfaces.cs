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
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;

namespace QAliber.Engine.Patterns
{
	public interface IText
	{
		string Text { get; }
	}

	public interface ISelector
	{
		void Select(string name);
		void Select(int index);

		UIAControl[] SelectedItems { get; }
		string[] Items { get; }
		bool CanSelectMultiple { get; }
	}

	public interface IWindowPattern {
		bool CanMinimize { get; }
		bool CanMaximize { get; }

		void Close();

		WindowVisualState State { get; }
		void SetState( WindowVisualState state );
	}

	public enum WindowVisualState {
		Normal = System.Windows.Automation.WindowVisualState.Normal,
		Maximized = System.Windows.Automation.WindowVisualState.Maximized,
		Minimized = System.Windows.Automation.WindowVisualState.Minimized,
	}

	public interface ITransformPattern {
		bool CanMove { get; }
		bool CanResize { get; }
		bool CanRotate { get; }

		void Move( double x, double y );
		void Resize( double width, double height );
		void Rotate( double degrees );
	}

	public interface ISelectionItemPattern {
		bool IsSelected { get; }

		void AddToSelection();
		void RemoveFromSelection();
		void Select();
	}

	public enum ToggleState {
		Off = System.Windows.Automation.ToggleState.Off,
		On = System.Windows.Automation.ToggleState.On,
		Indeterminate = System.Windows.Automation.ToggleState.Indeterminate,
	}

	public interface ITogglePattern {
		ToggleState ToggleState { get; }
		void Toggle();
	}

	public interface IInvokePattern {
		void Invoke();
	}

	public interface IValuePattern {
		bool IsReadOnly { get; }
		string Value { get; }

		void SetValue( string value );
	}

	public interface IGridPattern {
		int ColumnCount { get; }
		int RowCount { get; }

		UIControlBase GetItem( int row, int column );

		/// <summary>
		/// Captures the values in the grid in one sweep.
		/// </summary>
		/// <param name="headers">On return, contains the names in the header row if one was found,
		///   otherwise <see langword='null'/>.</param>
		/// <returns>A jagged object array that contains the values in each row of the grid.
		///   If there's a header row, it will be the first row returned.</returns>
		string[][] CaptureGrid( out string[] headers );
	}

	public interface IScrollItemPattern {
		void ScrollIntoView();
	}

	public enum ExpandCollapseState {
		Collapsed = System.Windows.Automation.ExpandCollapseState.Collapsed,
		Expanded = System.Windows.Automation.ExpandCollapseState.Expanded,
		PartiallyExpanded = System.Windows.Automation.ExpandCollapseState.PartiallyExpanded,
		LeafNode = System.Windows.Automation.ExpandCollapseState.LeafNode,
	}

	public interface IExpandCollapsePattern {
		void Collapse();
		void Expand();
		ExpandCollapseState ExpandCollapseState { get; }
	}
}
