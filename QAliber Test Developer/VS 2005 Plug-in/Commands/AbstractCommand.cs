using System;
using System.Collections.Generic;
using System.Text;
using QAliber.Recorder;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;
using QAliber.Engine;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace QAliber.VS2005.Plugin.Commands
{
	public abstract class Command
	{
		
		public event EventHandler Invoked;

		protected void OnInvoke()
		{
			if (Invoked != null)
				Invoked(this, EventArgs.Empty);
		}

		public abstract void Invoke();
		
	}

	public enum CommandType
	{
		Record = 0,
		StopRecord
	}


}
