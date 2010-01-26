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

	public interface IToggle
	{
		void Check();
		void UnCheck();
		void Toggle();

		ToggleState CheckState { get; }
	}

	public interface ISelectable
	{
		void Select();
		void AddToSelection();
		void RemoveFromSelection();
		bool IsSelected { get; }
	}

	public interface ISelector
	{
		void Select(string name);
		void Select(int index);

		UIAControl[] SelectedItems { get; }
		string[] Items { get; }
		bool CanSelectMultiple { get; }
	}

	public interface ITable
	{
		int Columns { get; }
		int Rows { get; }
		UIAControl GetCell(int row, int column);
	}

	public interface IExpandable
	{
		void Expand();
		void Collapse();
		ExpandCollapseState ExpandCollapseState { get; }
	}

	public interface IScrollable
	{
		void Scroll(double horPercents, double verPercents);

		bool CanScrollHorizontal { get; }
		bool CanScrollVertical { get; }
		double HorizontalPercents { get; }
		double VericalPercents { get; }
	}

	public interface IInvokable
	{
		void Invoke();
	}

	public interface ITransform
	{
		void Move(double x, double y);
		void Resize(double width, double height);
		void Rotate(double degrees);

		bool CanMove { get; }
		bool CanResize { get; }
		bool CanRotate { get; }
	}

	public interface IRangeValue
	{
		double Value { get; }
		double MaximumValue { get; }
		double MinimumValue { get; }
	}


}
