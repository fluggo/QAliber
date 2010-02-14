using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace QAliber.Repository.CommonTestCases.UI.Dialogs
{
	public partial class Notifier : Component
	{
		private Notifier()
		{
			InitializeComponent();
		}

		private Notifier(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		internal static Notifier Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Notifier();
					instance.notifyIcon.Icon = Properties.Resources.QAliberTarget;
				}
				return instance;
			}
		}

		private static Notifier instance = null;
	}
}
