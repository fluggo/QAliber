using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Builder.Presentation.Commands
{
	public interface ICommand
	{
		void Do();
		void Undo();
		void Redo();
	}
}
