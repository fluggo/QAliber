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

using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;
using System.Collections;
using QAliber.TestModel.Variables;
using QAliber.Logger;
using QAliber.RemotingModel;

namespace QAliber.TestModel
{
	/// <summary>
	/// The base class for all test cases implementations
	/// </summary>
	[Serializable]
	public abstract class TestCase : ICloneable
	{
		
		#region Virtuals
		/// <summary>
		/// The entry point for all the initializations prior to running the test case
		/// </summary>
		public virtual void Setup() { }

		/// <summary>
		/// The entry point for the 'heart' of the test case.
		/// </summary>
		public virtual void Body() { }

		/// <summary>
		/// The entry point for the clean-up of the test case in case it is ended by unhandled exception
		/// </summary>
		/// <remarks>The default behavior is to log the exception as error, and set the actual result to failed</remarks>
		/// <param name="e"></param>
		public virtual void Cleanup(Exception e) 
		{
			if (e.InnerException != null)
			{
				Log.Default.Error(e.GetType().Name + " Caught", e.InnerException.Message, EntryVerbosity.Internal);
			}
			else
			{
				Log.Default.Error(e.GetType().Name + " Caught", e.Message, EntryVerbosity.Internal);
			}
			Log.Default.Info("Stack Trace", e.StackTrace, EntryVerbosity.Debug);
			actualResult = TestCaseResult.Failed;
		}
		
		/// <summary>
		/// The entry point for the clean-up of the test case in case it was ended without exceptions
		/// </summary>
		public virtual void Cleanup() { }

		protected virtual void SetVariables()
		{
			if (scenario != null)
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
				if (outputProperties != null)
				{
					PropertyDescriptor foundProp = null;
					foreach (string name in outputProperties.Keys)
					{
						foreach (PropertyDescriptor prop in props)
						{
							if (prop.DisplayName == outputProperties[name])
							{
								foundProp = prop;
								break;
							}

						}
						if (foundProp != null)
						{
							object val = foundProp.GetValue(this);
							ICollection list = val as ICollection;
							if (list != null)
							{
								ScenarioList l = scenario.Lists[name];
								if (l != null)
									l.Value = list;
								else
									scenario.Lists.AddOrReplaceByName(new ScenarioList(name, list, this));

							}
							else if (val != null)
							{
								ScenarioVariable v = scenario.Variables[name];
								if (v != null)
									v.Value = val.ToString();
								else
									scenario.Variables.AddOrReplaceByName(new ScenarioVariable(name, val.ToString(), this));
							}

						}
					}
				}
			}
		}

		protected virtual void GetVariables()
		{
			if (scenario != null)
			{
				if (changedProperties == null)
					changedProperties = new Dictionary<string, string>();
				changedProperties.Clear();
				foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
				{
					object val = prop.GetValue(this);
					if (val != null)
					{
						string strVal = val.ToString();
						string replacementVal = scenario.ReplaceAllVars(strVal);
						if (string.Compare(replacementVal, strVal) != 0)
						{
							if (prop.PropertyType.Equals(typeof(string)))
							{
								changedProperties.Add(prop.Name, strVal);
								prop.SetValue(this, replacementVal);
							}
						}
					}
				}
			}
		}

		protected virtual void RestoreVariables()
		{
			if (scenario != null)
			{
				Dictionary<string, string>.Enumerator i = changedProperties.GetEnumerator();
				while (i.MoveNext())
				{
					PropertyDescriptor prop = TypeDescriptor.GetProperties(this).Find(i.Current.Key, true);
					if (prop != null)
					{
						prop.SetValue(this, i.Current.Value);
					}
				}
			}
		}

		#endregion

		#region Data Structure
		
		protected TestCase parent;

		/// <summary>
		/// The parent of the test case
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public TestCase Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		protected TestScenario scenario;

		/// <summary>
		/// The test scenario that this test case belongs to
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public TestScenario Scenario
		{
			get { return scenario; }
			set { scenario = value; }
		}
		#endregion

		#region Flow Control
		protected bool markedForExecution = true;

		/// <summary>
		/// Tells the QAliber Runner whether to run this tes case
		/// </summary>
		[Browsable(false)]
		public bool MarkedForExecution
		{
			get { return markedForExecution; }
			set { markedForExecution = value; }
		}

		protected bool exitOnError;

		/// <summary>
		/// Tells the QAliber Runner whether to quit the scenario, if this test case fails
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("Exit On Error?")]
		[Description("Should the current test case stop the entire scenario if it fails ?")]
		public bool ExitOnError
		{
			get { return exitOnError; }
			set { exitOnError = value; }
		}

		protected bool exitBranchOnError;

		/// <summary>
		/// Tells the QAliber Runner whether to continue the scenario on the next branch, if this test case fails
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("Exit Current Branch On Error?")]
		[Description("Should the current tree branch terminate if the current test case fails ?")]
		public bool ExitBranchOnError
		{
			get { return exitBranchOnError; }
			set { exitBranchOnError = value; }
		}
	   
		protected int numOfRetries;

		/// <summary>
		/// Tells the QAliber Runner how many times to retry this test case, if this test case fails
		/// </summary>
		[Category("Test Case Flow Control")]
		[DisplayName("Number Of Retries")]
		[Description("How many times to retry in case of failure")]
		public int NumOfRetries
		{
			get { return numOfRetries; }
			set { numOfRetries = value; }
		}

		protected TakeScreenshotOption screenshotOption;

		/// <summary>
		/// Tells the QAliber runner when to take screenshots in the test case life cycle
		/// </summary>
		[Category("Test Case Results")]
		[DisplayName("Take Screenshot?")]
		[Description("When to take a screenshot during the execution of the test case")]
		public TakeScreenshotOption ScreenshotOption
		{
			get { return screenshotOption; }
			set { screenshotOption = value; }
		}

		protected VideoOptions videoOptions = new VideoOptions();

		[Category("Test Case Results")]
		[DisplayName("Video Settings")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public VideoOptions VideoOptions
		{
			get { return videoOptions; }
			set { videoOptions = value; }
		}

		[NonSerialized]
		private bool hasBreakPoint;

		[Browsable(false)]
		[XmlIgnore]
		public bool HasBreakPoint
		{
			get { return hasBreakPoint; }
			set { hasBreakPoint = value; }
		}
	
		/// <summary>
		/// A static property which indicates the QAliber runner to stop all subsequent test cases execution, setting this to true, should lead to an end of the test scenario
		/// </summary>
		public static bool ExitTotally
		{
			get { return exitTotally; }
			set { exitTotally = value; }
		}

		public static uint BranchesToBreak
		{
			get { return branchesToBreak; }
			set { branchesToBreak = value; }
		}
		#endregion

		#region Test Case Descriptors
		protected string name;

		/// <summary>
		/// The logical name of the test case
		/// </summary>
		[Category("Test Case Details")]
		[Description("The name of the test case")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		protected string description;

		/// <summary>
		/// The description of the test csae, the description will be logged as a remark by the QAliber runner.
		/// A good practice is to set it according to the parameters the user chosen for the test case
		/// </summary>
		[Category("Test Case Details")]
		[Description("The description of the test case")]
		public virtual string Description
		{
			get { return description; }
			set { description = value; }
		}

		protected string originalPath;

		/// <summary>
		/// The description of the test csae, the description will be logged as a remark by the QAliber runner.
		/// A good practice is to set it according to the parameters the user chosen for the test case
		/// </summary>
		[Category("Test Case Details")]
		[Description("The origin of the test case")]
		[DisplayName("Repository Location")]
		[ReadOnly(true)]
		public virtual string RepositoryLocation
		{
			get { return originalPath; }
			set { originalPath = value; }
		}

		protected TestCaseResult expectedResult = TestCaseResult.Passed;

		/// <summary>
		/// The expected result for this test case, for support of both positive and negative tests in the same test case
		/// </summary>
		[Category("Test Case Results")]
		[DisplayName("Expected Result")]
		[Description("The expected result from this test case")]
		public TestCaseResult ExpectedResult
		{
			get { return expectedResult; }
			set { expectedResult = value; }
		}

		protected TestCaseResult actualResult;

		/// <summary>
		/// The actual result of the test case.
		/// <remarks>It is recommended to set the actual result on any code path inside the Body method (much like return)</remarks>
		/// </summary>
		[ReadOnly(true)]
		[Category("Test Case Results")]
		[DisplayName("Actual Result")]
		[Description("The actual result this test case returned")]
		public TestCaseResult ActualResult
		{
			get { return actualResult; }
			set { actualResult = value; }
		}

		private TestCaseResult expectedVsActual
		{
			get
			{
				if (expectedResult == TestCaseResult.None)
					return TestCaseResult.Passed;
				else if (expectedResult != actualResult)
					return TestCaseResult.Failed;
				else
					return TestCaseResult.Passed;
			}
		}

		[Category("Test Case Results")]
		[DisplayName("Output To Variables")]
		[Description("Choose here the parameters you want to save for future use")]
		[Editor(typeof(TypeEditors.OutputPropertiesTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[XmlIgnore]
		public Dictionary<string, string> OutputProperties
		{
			get { return outputProperties; }
			set { outputProperties = value; }
		}

		protected int id;

		/// <summary>
		/// A unique identifier of the test case, when in a scenario
		/// </summary>
		[Browsable(false)]
		public int ID
		{
			get 
			{
				return id; 
			}
			set 
			{ 
				id = value; 
			}
		}

		protected Bitmap icon;

		/// <summary>
		/// A bitmap by the size of 16x16 to be shown next to the test case in the QAliber Test Builder, this should be set in the constructor of your test case
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public Bitmap Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		protected Color color = Color.Black;

		/// <summary>
		/// The color the test case in the QAliber Test Builder, for better visualization
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}
	
		#endregion

		#region ICloneable Members

		/// <summary>
		/// Clones the test case using serialization
		/// </summary>
		/// <remarks>All test csaes classes must be Serializable</remarks>
		/// <returns>A deep copy of the test case</returns>
		public object Clone()
		{
			TestCase result = null;
			using (MemoryStream memStream = new MemoryStream())
			{
				BinaryFormatter binFormatter = new BinaryFormatter();
				binFormatter.Serialize(memStream, this);
				memStream.Seek(0, SeekOrigin.Begin);
				result = binFormatter.Deserialize(memStream) as TestCase;
				result.UpdateIDs();
				memStream.Close();
			}
			return result;
		}

		#endregion

		/// <summary>
		/// The current test case being run by the QAliber runner
		/// </summary>
		public static TestCase Current
		{
			get { return currenTestCase; }
			set { currenTestCase = value; }
		}

		/// <summary>
		/// The method that executes the entire test case with all its parts (Setup, Body and Cleanup)
		/// </summary>
		public void Run()
		{
			if (!markedForExecution || exitTotally)
				return;
			currenTestCase = this;
			TestController.Default.RaiseStepStarted(id);
			if (hasBreakPoint)
			{
				try
				{
					TestController.Default.RaiseBreakPointReached();
					System.Threading.Thread.CurrentThread.Join();
				}
				catch (System.Threading.ThreadInterruptedException)
				{
				}
			}
			try
			{
				InitRun();
				Setup();
				Body();
				Cleanup();
			}
			catch (Exception e)
			{
				Cleanup(e);
			}
			finally
			{
				FinalizeRun();
			}
		}

		protected void HandleResult()
		{
			if (expectedResult != TestCaseResult.None && actualResult != expectedResult)
			{
				Log.Default.Result(TestCaseResult.Failed);
				if (screenshotOption == TakeScreenshotOption.OnError)
				{
					Log.Default.Image(Logger.Slideshow.ScreenCapturer.Capture(), "Error - " + name);
				}
				
				if (currentRetryNumber < numOfRetries)
				{
					currentRetryNumber++;
					Log.Default.IndentOut();
					Log.Default.Warning(name + " - Retry #" + currentRetryNumber);
					Run();
				}
				
			}
			else
			{
				Log.Default.Result(TestCaseResult.Passed);
			}
		}

		/// <summary>
		/// Posts info/error message to the log according to the expected result.
		/// If expected result is fail it will log info, otherwise it will log error
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">And additional info to log</param>
		protected void LogFailedByExpectedResult(string message, string extra)
		{
			if (expectedResult == TestCaseResult.Failed)
				Log.Default.Info(message, extra);
			else
				Log.Default.Error(message, extra);

		}

		/// <summary>
		/// Posts info/error message to the log according to the expected result.
		/// If expected result is fail it will log error, otherwise it will log info
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">And additional info to log</param>
		protected void LogPassedByExpectedResult(string message, string extra)
		{
			if (expectedResult != TestCaseResult.Failed)
				Log.Default.Info(message, extra);
			else
				Log.Default.Error(message, extra);

		}

		private void InitRun()
		{
			actualResult = TestCaseResult.None;
			GetVariables();
			Log.Default.IndentIn(name, Description, true);
			if (videoOptions != null && videoOptions.CaptureVideo
				&& !Logger.Slideshow.SlideshowRecorder.Default.IsCapturing)
			{
				capturing = true;
				Logger.Slideshow.SlideshowRecorder.Default.Interval = videoOptions.Interval;
				Logger.Slideshow.SlideshowRecorder.Default.Start(name);
			}
			if (screenshotOption == TakeScreenshotOption.BeforeTestCase || screenshotOption == TakeScreenshotOption.Both)
			{
				Log.Default.Image(Logger.Slideshow.ScreenCapturer.Capture(), "Begin - " + name);
			}
		}

		private void FinalizeRun()
		{
			HandleResult();
			SetVariables();
			RestoreVariables();
			TestController.Default.RaiseStepResultArrived(expectedVsActual);
			if (capturing)
			{
				capturing = false;
				Logger.Slideshow.SlideshowRecorder.Default.Stop();
			}
			if (screenshotOption == TakeScreenshotOption.AfterTestCase || screenshotOption == TakeScreenshotOption.Both)
			{
				Log.Default.Image(Logger.Slideshow.ScreenCapturer.Capture(), "End - " + name);
			}
			Log.Default.IndentOut();
		}

		private void UpdateIDs()
		{
			id = maxID;
			maxID++;
			if (this is FolderTestCase)
			{
				foreach (TestCase child in ((FolderTestCase)this).Children)
				{
					child.UpdateIDs();
				}
			}
		}

		protected static uint branchesToBreak;
		protected static bool exitTotally;
		private static TestCase currenTestCase;
		internal static int maxID = 1;

		protected Dictionary<string, string> outputProperties;
		protected Dictionary<string, string> changedProperties = new Dictionary<string,string>();
		private bool capturing;
		private int currentRetryNumber = 0;

		
	}
}
