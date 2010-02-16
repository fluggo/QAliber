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
using System.Windows;

namespace QAliber.Recorder
{
	public class AnalyzedLLRecordEntry : IRecordEntry
	{
		public AnalyzedLLRecordEntry(string path, string action,
			System.Windows.Forms.MouseButtons button, Point relPoint, DateTime time, string type, string name)
		{
			controlPath = path;
			this.name = name;
			this.time = time;
			this.button = button;
			this.relativePoint1 = relPoint;
			this.action = action;
			if (relPoint.X >= 0 && relPoint.Y >= 0)
				actionParams = new string[2] { "MouseButtons." + button.ToString(), "new Point(" + relPoint.X + ", " + relPoint.Y + ")" };
			else
				actionParams = new string[1] { "MouseButtons." + button.ToString() };
			this.type = type;
			index = statIndex;
			statIndex++;
		}

		public AnalyzedLLRecordEntry(string path, string keys, DateTime time, string type, string name)
		{
			controlPath = path;
			this.name = name;
			this.time = time;
			actionParams = new string[] { "\"" + keys  + "\"" };
			action = "Write";
			this.type = type;
			index = statIndex;
			statIndex++;
		}

		public AnalyzedLLRecordEntry(string path, DateTime time, string action, string name, params string[] actionParams)
		{
			controlPath = path;
			this.name = name;
			this.action = action;
			this.actionParams = actionParams;
			index = statIndex;
			statIndex++;
		}

		public Point RelativePoint1
		{

			get { return relativePoint1; }
			set 
			{ 
				relativePoint1 = value;
				if (value.X >=0 && value.Y >=0)
					actionParams[1] = "new Point(" + value.X + ", " + value.Y + ")";
			}
		}

		public Point RelativePoint2
		{

			get { return relativePoint2; }
			set
			{
				relativePoint2 = value;
				actionParams = new string[] { actionParams[0], actionParams[1], "new Point(" + value.X + ", " + value.Y + ")" };
			}
		}

		public System.Windows.Forms.MouseButtons Button
		{
			get { return button; }
			set
			{
				button = value;
				actionParams[0] = "MouseButtons." + value.ToString();
			}
		}
	
		#region IRecordEntry Members

		public int Index
		{
			get
			{
				return index;
			}
		}

		public string CodePath
		{
			get
			{
				return controlPath;
			}
			set
			{
				controlPath = value;
			}
		}

		public DateTime Time
		{
			get
			{
				return time;
			}
			set
			{
				time = value;
			}
		}

		public string Action
		{
			get
			{
				return action;
			}
			set
			{
				action = value;
			}
		}

		public string Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		public string[] ActionParams
		{
			get
			{
				return actionParams;
			}
			set
			{
				actionParams = value;
			}
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		#endregion

		private string controlPath;
		private DateTime time;

		private Point relativePoint1;
		private Point relativePoint2;
		private System.Windows.Forms.MouseButtons button;

		private string[] actionParams;
		private string action;
		private string type;
		private string name;

		private static int statIndex = 0;
		private int index;
	}
}
