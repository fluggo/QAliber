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

	public delegate void ExecutionStateChangedCallback(ExecutionState state);
	public delegate void StepStartedCallback(int id);
	public delegate void StepResultArrivedCallback(TestCaseResult result);
	public delegate void LogResultArrivedCallback(string logFile);
	public delegate void BreakPointReachedCallback();

	public interface IController
	{
		Type[] SupportedTypes { get; }
		string[] UserFiles { get; set;}
		string RemoteAssemblyDirectory { get; set; }
		string LogPath { get; set; }
		string LogDirectoryStructure { get; set; }

		void Run(string scenarioFile);
		void Run(object testcase);
		void ContinueFromBreakPoint();
		void Pause();
		void Resume();
		void Stop();

		event ExecutionStateChangedCallback OnExecutionStateChanged;
		event StepStartedCallback OnStepStarted;
		event StepResultArrivedCallback OnStepResultArrived;
		event LogResultArrivedCallback OnLogResultArrived;
		event BreakPointReachedCallback OnBreakPointReached;

	}

	public abstract class NotifyCallbackSink : MarshalByRefObject
	{
		
		public void FireExecutionStateChangedCallback(ExecutionState state)
		{
			OnExecutionStateChanged(state);
		}

		public void FireStepResultArrivedCallback(TestCaseResult result)
		{
			OnStepResultArrived(result);
		}

		public void FireStepStartedCallback(int id)
		{
			OnStepStarted(id);
		}

		public void FireLogResultArrivedCallback(string logFile)
		{
			OnLogResultArrived(logFile);
		}

		public void FireBreakPointReachedCallback()
		{
			OnBreakPointReached();
		}

		protected abstract void OnExecutionStateChanged(ExecutionState state);
		protected abstract void OnStepResultArrived(TestCaseResult result);
		protected abstract void OnStepStarted(int id);
		protected abstract void OnLogResultArrived(string logFile);
		protected abstract void OnBreakPointReached();
	}
}
