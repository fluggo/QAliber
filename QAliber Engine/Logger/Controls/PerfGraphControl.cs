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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;

namespace QAliber.Logger.Controls
{
	public partial class PerfGraphControl : UserControl
	{
		public PerfGraphControl()
		{
			InitializeComponent();
		}

		internal void Init(string logPath)
		{
			if (Directory.Exists(logPath))
			{
				foreach (string csvFile in Directory.GetFiles(logPath, "*.csv", SearchOption.AllDirectories))
				{
					this.csvFile = csvFile;
					FillCountersList();
					Visible = true;
					BringToFront();
					break;
				}
			}
		}

		private void FillCountersList()
		{
			using (StreamReader csvReader = new StreamReader(csvFile))
			{
				string line = csvReader.ReadLine();
				string[] counters = line.Split(',');
				for (int i = 1; i < counters.Length; i++)
				{
					string lviText = counters[i].Trim('"');
					string[] counterNameFields = lviText.Split('\\');
					if (counterNameFields.Length > 2)
					{
						lviText = string.Join("\\", 
							new string[] { counterNameFields[counterNameFields.Length - 2], counterNameFields[counterNameFields.Length - 1]} );
					}
					ListViewItem lvi = new ListViewItem(lviText);
					lvi.Name = counters[i].Trim('"');
					lvi.Tag = new PointPairList();
					listViewCounters.Items.Add(lvi);
					

				}
				line = csvReader.ReadLine();
				while (line != null)
				{
					string[] fields = line.Split(',');
					DateTime time = DateTime.Parse(fields[0].Trim('"'), new System.Globalization.CultureInfo("en-US"));
					
					for (int i = 1; i < fields.Length; i++)
					{
						string trimmedField = fields[i].Trim('"', ' ');
						if (trimmedField != string.Empty)
							((PointPairList)listViewCounters.Items[i - 1].Tag).Add(XDate.DateTimeToXLDate(time), double.Parse(trimmedField));
					}
					line = csvReader.ReadLine();
				}
			}
		}

		private LogViewerControl GetParentControl()
		{
			Control parent = Parent;
			while (parent != null)
			{
				if (parent is LogViewerControl)
					return (LogViewerControl)parent;
				parent = parent.Parent;
			}
			return null;
		}

		private void listViewCounters_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item.Checked)
			{
				if (colorIndex < graphColors.Length && colorIndex >= 0)
				{
					e.Item.ForeColor = graphColors[colorIndex];
					colorIndex++;
				}
			}
			else
			{
				e.Item.ForeColor = Color.Black;
				colorIndex--;
				if (colorIndex <= 0)
					colorIndex = 0;
			}
		}

		private void listViewCounters_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Checked && colorIndex >= graphColors.Length)
				e.NewValue = CheckState.Unchecked;
		}


		private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form form = FindForm();
			if (fullScreenItem.Checked)
			{
				parent = Parent;
				form.WindowState = FormWindowState.Maximized;
				Parent.Controls.Remove(this);
				form.Controls.Add(this);
				this.Bounds = new Rectangle(0, 0, form.Width, form.Height);
				this.BringToFront();
			}
			else
			{
				form.Controls.Remove(this);
				parent.Controls.Add(this);
				Dock = DockStyle.Fill;
				this.BringToFront();
			}
		}

		private void zedGraphControl_Click(object sender, EventArgs e)
		{
			CurveItem nearestCurve = null;
			int nearestIndex = 0;
			Point screenPoint = zedGraphControl.PointToClient(Cursor.Position);
			zedGraphControl.GraphPane.FindNearestPoint(
				new PointF(screenPoint.X, screenPoint.Y),
				out nearestCurve, out nearestIndex);
			if (nearestCurve != null && nearestIndex >= 0)
			{
				DateTime time = XDate.XLDateToDateTime(nearestCurve[nearestIndex].X);
				LogViewerControl lvc = GetParentControl();
				if (lvc != null)
				{
					lvc.SelectNodeByDate(time);
				}

			}
		}

		private void toolStripButtonPlot_Click(object sender, EventArgs e)
		{
			zedGraphControl.GraphPane.CurveList.Clear();
			zedGraphControl.GraphPane.Title.Text = "Resources Monitoring";
			zedGraphControl.GraphPane.XAxis.Title.Text = "Time";
			zedGraphControl.GraphPane.XAxis.Type = AxisType.Date;
			zedGraphControl.GraphPane.YAxis.Title.Text = "Value";
			zedGraphControl.IsShowPointValues = true;
			foreach (ListViewItem item in listViewCounters.Items)
			{
				if (item.Checked)
				{
					zedGraphControl.GraphPane.AddCurve(item.Name, (IPointList)item.Tag, item.ForeColor);
				}
			}
			zedGraphControl.AxisChange();
			zedGraphControl.Refresh();
		}

		private void zedGraphControl_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
		{
			if (fullScreenItem == null)
			{
				fullScreenItem = new ToolStripMenuItem("Full Screen", null, new EventHandler(fullScreenToolStripMenuItem_Click));
				fullScreenItem.CheckOnClick = true;
			}
			menuStrip.Items.Add(new ToolStripSeparator());
			menuStrip.Items.Add(fullScreenItem);
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < listViewCounters.Items.Count; i++)
			{
				if (i >= 8)
					listViewCounters.Items[i].Checked = false;
				listViewCounters.Items[i].Checked = true;
			}
		}

		private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < listViewCounters.Items.Count; i++)
			{
				listViewCounters.Items[i].Checked = false;
			}
		}

		

		private ToolStripMenuItem fullScreenItem = null;
		private Control parent;
		private List<DateTime> dateTimes = new List<DateTime>();
		private int colorIndex = 0;
		private string csvFile;
		private Color[] graphColors = new Color[] { Color.Blue, Color.Green,
			 Color.Red, Color.Orange, Color.Yellow, Color.Purple, Color.DarkBlue, Color.Black };

	   

	}

	
}
