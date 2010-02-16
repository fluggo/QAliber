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
