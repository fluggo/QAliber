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
