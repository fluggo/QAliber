using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QAliber.TestModel;
using System.ComponentModel;

namespace QAliber.Builder.Presentation.Commands
{
	public class PropertyChangedCommand : ICommand
	{
		public PropertyChangedCommand(QAliberTreeNode sourceNode, PropertyGrid pg, PropertyDescriptor property, object oldVal, object newVal)
		{
			this.sourceNode = sourceNode;
			this.pg = pg;
			this.property = property;
			this.oldVal = oldVal;
			this.newVal = newVal;
		}

		#region ICommand Members

		public void Do()
		{
			TestCase testcase = sourceNode.Testcase;
			if (testcase != null)
			{
				property.SetValue(testcase, newVal);
				if (property.Name == "Name")
				{
					sourceNode.Text = newVal.ToString();
				}
				if (sourceNode.TreeView != null)
					sourceNode.TreeView.SelectedNode = sourceNode;
				pg.Refresh();
			}
		}

		public void Undo()
		{
			TestCase testcase = sourceNode.Testcase;
			if (testcase != null && oldVal.GetType().Equals(property.PropertyType))
			{
				property.SetValue(testcase, oldVal);
				if (property.Name == "Name")
				{
					sourceNode.Text = oldVal.ToString();
				}
				if (sourceNode.TreeView != null)
					sourceNode.TreeView.SelectedNode = sourceNode;
				pg.Refresh();
			}
		}

		public void Redo()
		{
		}

		public QAliberTreeNode SourceNode
		{
			get
			{
				return sourceNode;
			}
			set
			{
				sourceNode = value;
			}
		}
		
		#endregion

		private PropertyGrid pg;
		private QAliberTreeNode sourceNode;
		private PropertyDescriptor property;
		private object oldVal;
		private object newVal;
	}
}
