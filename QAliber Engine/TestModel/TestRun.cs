using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QAliber.TestModel {
	/// <summary>
	/// Provides access to the current test run.
	/// </summary>
	public class TestRun {
		volatile bool _canceled;
		TestRun _parent;
		int _branchesToBreak;

		public TestRun() {
		}

		public TestRun( TestRun parent ) {
			_parent = parent;
		}

		public void Cancel() {
			_canceled = true;
		}

		/// <summary>
		/// Gets a value that represents whether the run has been canceled.
		/// </summary>
		/// <value>True if the run should end at the nearest convenience, false otherwise.</value>
		public bool Canceled
			{ get { return _canceled; } }

		public int BranchesToBreak {
			get { return _branchesToBreak; }
			set { _branchesToBreak = value; }
		}
	}
}
