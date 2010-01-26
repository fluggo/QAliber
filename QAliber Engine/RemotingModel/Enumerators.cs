using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.RemotingModel
{
	/// <summary>
	/// The test case result
	/// </summary>
	public enum TestCaseResult
	{
		/// <summary>
		/// Don't care about the result
		/// </summary>
		None,
		/// <summary>
		/// The test case passed
		/// </summary>
		Passed,
		/// <summary>
		/// The test case failed
		/// </summary>
		Failed
	}

	/// <summary>
	/// The execution state of a test scenario
	/// </summary>
	public enum ExecutionState
	{
		/// <summary>
		/// The test scenario was not executed (after load or new)
		/// </summary>
		NotExecuted,
		/// <summary>
		/// The test scenario is paused
		/// </summary>
		Paused,
		/// <summary>
		/// The test scenario is currently running
		/// </summary>
		InProgress,
		/// <summary>
		/// The test scenario was executed
		/// </summary>
		Executed
	}

}
