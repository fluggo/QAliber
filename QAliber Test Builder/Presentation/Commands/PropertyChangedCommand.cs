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
