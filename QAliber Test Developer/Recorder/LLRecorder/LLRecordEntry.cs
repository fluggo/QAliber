using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace QAliber.Recorder
{
	public class LLRecordEntry : IRecordEntry
	{
		public LLRecordEntry(string path, bool isMouseUp,
			System.Windows.Forms.MouseButtons button, Point relPoint, Point absPoint, string type)
			: this()
		{
			codePath = path;
			
			IsMouseUp = isMouseUp;
			Button = button;
			RelativePoint = relPoint;
			AbsPoint = absPoint;
			this.type = type;
		}

		public LLRecordEntry(string path, bool isKeyUp,
			System.Windows.Input.Key key, string type)
			: this()
		{
			codePath = path;
			IsKeyUp = isKeyUp;
			Key = key;
			this.type = type;
		}

		public LLRecordEntry(string path, string action, params string[] actionParams)
			: this()
		{
			codePath = path;
			this.action = action;
			this.actionParams = actionParams;
		}

		public LLRecordEntry()
		{
			time = DateTime.Now;
			index = statIndex;
			statIndex++;
		}

		private string name;
		private string codePath;
		private string action;
		private string[] actionParams;
		private DateTime time;
		private int index;
		
		public bool IsMouseUp;
		public Point RelativePoint;
		public Point AbsPoint;
		public System.Windows.Forms.MouseButtons Button;
		private string type;

		public bool IsKeyUp;
		public System.Windows.Input.Key Key;
		public IntPtr InputHandle;

		private static int statIndex = 0;

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
				return codePath;
			}
			set
			{
				codePath = value;
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
	}

	
}
