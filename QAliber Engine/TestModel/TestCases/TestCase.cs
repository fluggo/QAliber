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
using System.Linq;

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
using System.Data;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace QAliber.TestModel
{
	/// <summary>
	/// The base class for all test cases implementations
	/// </summary>
	[XmlType("TestStep", Namespace=Util.XmlNamespace)]
	public abstract class TestCase : ICloneable, INotifyPropertyChanged {
		private string _defaultName;

		protected TestCase( string name ) {
			ExitBranchOnError = ExitBranchOnErrorDefaultValue;
			AlwaysRun = AlwaysRunDefaultValue;

			_defaultName = name;
		}

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
				Log.Error(e.GetType().Name + " Caught", e.InnerException.Message, EntryVerbosity.Internal);
			}
			else
			{
				Log.Error(e.GetType().Name + " Caught", e.Message, EntryVerbosity.Internal);
			}
			Log.Info("Exception Details", e.ToString(), EntryVerbosity.Debug);
			_actualResult = TestCaseResult.Failed;
		}
		
		/// <summary>
		/// The entry point for the clean-up of the test case in case it was ended without exceptions
		/// </summary>
		public virtual void Cleanup() { }

		protected virtual void SetVariables()
		{
			if (_scenario != null)
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
				if (_outputProperties != null)
				{
					PropertyDescriptor foundProp = null;
					foreach (string name in _outputProperties.Keys)
					{
						foundProp = props.Find( name, false );
						string variableName = _outputProperties[name];

						if (foundProp != null)
						{
							object val = foundProp.GetValue(this);

							DataTable table = val as DataTable;
							ICollection list = val as ICollection;

							if( table != null ) {
								ScenarioVariable<DataTable> tableVar = _scenario.Tables[variableName];

								if( tableVar != null )
									tableVar.Value = table;
								else
									_scenario.Tables.AddOrReplace( new ScenarioTable( variableName, table, this ) );
							}
							else if( list != null ) {
								string[] stringList = list.Cast<object>().Select(
									obj => (obj == null) ? "(null)" : obj.ToString() ).ToArray();

								ScenarioVariable<string[]> l = _scenario.Lists[variableName];
								if (l != null)
									l.Value = stringList;
								else
									_scenario.Lists.AddOrReplace(new ScenarioVariable<string[]>(variableName, stringList, this));

							}
							else if (val != null)
							{
								ScenarioVariable<string> v = _scenario.Variables[variableName];
								if (v != null)
									v.Value = val.ToString();
								else
									_scenario.Variables.AddOrReplace(new ScenarioVariable<string>(variableName, val.ToString(), this));
							}

						}
					}
				}
			}
		}

		protected virtual void GetVariables()
		{
			if (_scenario != null)
			{
				if (_changedProperties == null)
					_changedProperties = new Dictionary<string, string>();
				_changedProperties.Clear();
				foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(this))
				{
					object val = prop.GetValue(this);
					if (val != null)
					{
						string strVal = val.ToString();
						string replacementVal = _scenario.ReplaceAllVars(strVal);
						if (string.Compare(replacementVal, strVal) != 0)
						{
							if (prop.PropertyType.Equals(typeof(string)))
							{
								_changedProperties.Add(prop.Name, strVal);
								prop.SetValue(this, replacementVal);
							}
						}
					}
				}
			}
		}

		protected virtual void RestoreVariables()
		{
			if (_scenario != null)
			{
				Dictionary<string, string>.Enumerator i = _changedProperties.GetEnumerator();
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

		/// <summary>
		/// Raised when a property changes.
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged( string propertyName ) {
			PropertyChangedEventHandler handler = PropertyChanged;

			if( handler != null )
				handler( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#region Data Structure

		private TestCase _parent;

		/// <summary>
		/// The parent of the test case
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public TestCase Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		private TestScenario _scenario;

		/// <summary>
		/// The test scenario that this test case belongs to
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public TestScenario Scenario
		{
			get { return _scenario; }
			set { _scenario = value; }
		}
		#endregion

		#region Flow Control
		private bool _markedForExecution = true;

		/// <summary>
		/// Tells the QAliber Runner whether to run this tes case
		/// </summary>
		[Browsable(false)]
		[DefaultValue(true)]
		[XmlAttribute]
		public bool MarkedForExecution
		{
			get { return _markedForExecution; }
			set { _markedForExecution = value; }
		}

		private bool _exitBranchOnError = true;

		/// <summary>
		/// Tells the QAliber Runner whether to continue the scenario on the next branch, if this test case fails
		/// </summary>
		[Category("Test Step Flow Control")]
		[DisplayName("Stop Parent on Fail")]
		[Description("Should the current tree branch terminate if the current test case fails ?")]
		[XmlAttribute]
		public bool ExitBranchOnError
		{
			get { return _exitBranchOnError; }
			set { _exitBranchOnError = value; }
		}

		/// <summary>
		/// Gets the default value of <see cref="ExitBranchOnError"/>.
		/// </summary>
		/// <value>The base class returns true. Override if you want to provide a different default.</value>
		[Browsable(false), XmlIgnore]
		protected virtual bool ExitBranchOnErrorDefaultValue {
			get { return true; }
		}

		/// <summary>
		/// Determines whether <see cref="ExitBranchOnError"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeExitBranchOnError() {
			return ExitBranchOnError != ExitBranchOnErrorDefaultValue;
		}

		/// <summary>
		/// Resets <see cref="ExitBranchOnError"/> to its default value.
		/// </summary>
		public void ResetExitBranchOnError() {
			ExitBranchOnError = ExitBranchOnErrorDefaultValue;
		}

		private bool _exitOnError;

		/// <summary>
		/// Tells the QAliber Runner whether to quit the scenario, if this test case fails
		/// </summary>
		[Category("Test Step Flow Control")]
		[DisplayName("Stop All on Fail")]
		[Description("Should the current test case stop the entire scenario if it fails ?")]
		[DefaultValue(false)]
		public bool ExitOnError
		{
			get { return _exitOnError; }
			set { _exitOnError = value; }
		}

		private bool _alwaysRun;

		[Category("Test Step Flow Control")]
		[DisplayName("Always Run")]
		[Description("Run this step even if the folder has already failed.")]
		[XmlAttribute]
		public bool AlwaysRun {
			get { return _alwaysRun; }
			set { _alwaysRun = value; }
		}

		/// <summary>
		/// Gets the default value of <see cref="AlwaysRun"/>.
		/// </summary>
		/// <value>The base class returns false. Override if you want to provide a different default.</value>
		[Browsable(false), XmlIgnore]
		protected virtual bool AlwaysRunDefaultValue {
			get { return false; }
		}

		/// <summary>
		/// Determines whether <see cref="AlwaysRun"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeAlwaysRun() {
			return AlwaysRun != AlwaysRunDefaultValue;
		}

		/// <summary>
		/// Resets <see cref="AlwaysRun"/> to its default value.
		/// </summary>
		public void ResetAlwaysRun() {
			AlwaysRun = AlwaysRunDefaultValue;
		}

		private int _numOfRetries;

		/// <summary>
		/// Tells the QAliber Runner how many times to retry this test case, if this test case fails
		/// </summary>
		[Category("Test Step Flow Control")]
		[DisplayName("Number of Retries")]
		[Description("How many times to retry in case of failure")]
		[DefaultValue(0)]
		[XmlAttribute]
		public int NumOfRetries
		{
			get { return _numOfRetries; }
			set { _numOfRetries = value; }
		}

		private TakeScreenshotOption _screenshotOption;

		/// <summary>
		/// Tells the QAliber runner when to take screenshots in the test case life cycle
		/// </summary>
		[Category("Test Step Results")]
		[DisplayName("Take Screenshot?")]
		[Description("When to take a screenshot during the execution of the test case")]
		[DefaultValue(TakeScreenshotOption.No)]
		[XmlAttribute]
		public TakeScreenshotOption ScreenshotOption
		{
			get { return _screenshotOption; }
			set { _screenshotOption = value; }
		}

		private VideoOptions _videoOptions = new VideoOptions();

		[Category("Test Step Results")]
		[DisplayName("Video Settings")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public VideoOptions VideoOptions
		{
			get { return _videoOptions; }
			set { _videoOptions = value; }
		}

		/// <summary>
		/// Determines whether <see cref="VideoOptions"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeVideoOptions() {
			return !VideoOptions.Equals( new VideoOptions() );
		}

		/// <summary>
		/// Resets <see cref="VideoOptions"/> to its default value.
		/// </summary>
		public void ResetVideoOptions() {
			VideoOptions = new VideoOptions();
		}

		private bool _hasBreakPoint;

		[Browsable(false)]
		[XmlIgnore]
		public bool HasBreakPoint
		{
			get { return _hasBreakPoint; }
			set { _hasBreakPoint = value; }
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
		protected virtual string DefaultName {
			get { return _defaultName; }
		}

		protected virtual void OnDefaultNameChanged() {
			if( _name == null )
				OnPropertyChanged( "Name" );
		}

		private string _name;

		/// <summary>
		/// The logical name of the test case
		/// </summary>
		[Category("Test Step Details")]
		[Description("The name of the test case")]
		public string Name
		{
			get { return _name ?? DefaultName; }
			set {
				if( string.IsNullOrWhiteSpace( value ) )
					_name = null;
				else
					_name = value;

				OnPropertyChanged( "Name" );
			}
		}

		/// <summary>
		/// Determines whether <see cref="Name"/> has its default value.
		/// </summary>
		/// <returns>False if it has its default value, true otherwise.</returns>
		public bool ShouldSerializeName() {
			return Name != DefaultName;
		}

		/// <summary>
		/// Resets <see cref="Name"/> to its default value.
		/// </summary>
		public void ResetName() {
			_name = null;
		}

		private string _notes = string.Empty;

		[Category("Test Step Details")]
		[Description("User-provided description of the step.")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		public string Notes
		{
			get { return _notes; }
			set { _notes = value; }
		}

		/// <summary>
		/// The description of the test csae, the description will be logged as a remark by the QAliber runner.
		/// A good practice is to set it according to the parameters the user chosen for the test case
		/// </summary>
		[Category("Test Step Details")]
		[Description("Automatic description of the step.")]
		[XmlIgnore, ReadOnly(true)]
		public virtual string Description
		{
			get { return string.Empty; }
		}

		private string _originalPath;

		/// <summary>
		/// The description of the test csae, the description will be logged as a remark by the QAliber runner.
		/// A good practice is to set it according to the parameters the user chosen for the test case
		/// </summary>
		[Category("Test Step Details")]
		[Description("The origin of the test case")]
		[DisplayName("Repository Location")]
		[ReadOnly(true)]
		[XmlIgnore]
		public virtual string RepositoryLocation
		{
			get { return _originalPath; }
			set { _originalPath = value; }
		}

		private TestCaseResult _expectedResult = TestCaseResult.Passed;

		/// <summary>
		/// The expected result for this test case, for support of both positive and negative tests in the same test case
		/// </summary>
		[Category("Test Step Results")]
		[DisplayName("Expected Result")]
		[Description("The expected result from this test case")]
		[DefaultValue(TestCaseResult.Passed)]
		public TestCaseResult ExpectedResult
		{
			get { return _expectedResult; }
			set { _expectedResult = value; }
		}

		private TestCaseResult _actualResult;

		/// <summary>
		/// The actual result of the test case.
		/// <remarks>It is recommended to set the actual result on any code path inside the Body method (much like return)</remarks>
		/// </summary>
		[ReadOnly(true)]
		[Category("Test Step Results")]
		[DisplayName("Actual Result")]
		[Description("The actual result this test case returned")]
		[XmlIgnore]
		public TestCaseResult ActualResult
		{
			get { return _actualResult; }
			set { _actualResult = value; }
		}

		private TestCaseResult expectedVsActual
		{
			get
			{
				if (_expectedResult == TestCaseResult.None)
					return TestCaseResult.Passed;
				else if (_expectedResult != _actualResult)
					return TestCaseResult.Failed;
				else
					return TestCaseResult.Passed;
			}
		}

		[Category("Test Step Results")]
		[DisplayName("Output To Variables")]
		[Description("Choose here the parameters you want to save for future use")]
		public OutputPropertiesMap OutputProperties
		{
			get { return _outputProperties; }
			set { _outputProperties = value; }
		}

		private int _id;

		/// <summary>
		/// A unique identifier of the test case, when in a scenario
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public int ID
		{
			get 
			{
				return _id;
			}
			set 
			{ 
				_id = value;
			}
		}

		private Bitmap _icon;

		/// <summary>
		/// A bitmap by the size of 16x16 to be shown next to the test case in the QAliber Test Builder, this should be set in the constructor of your test case
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public Bitmap Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		private Color _color = Color.Black;

		/// <summary>
		/// The color the test case in the QAliber Test Builder, for better visualization
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}
	
		#endregion

		#region ICloneable Members

		/// <summary>
		/// Clones the test case.
		/// </summary>
		/// <returns>A clone of the test case.</returns>
		/// <remarks>This method performs a memberwise clone. In your subclass,
		///   be sure to clone any subtests you have, and clone any other important
		///   object references.</remarks>
		public virtual object Clone() {
			TestCase result = (TestCase) MemberwiseClone();

			result._actualResult = TestCaseResult.None;
			result._hasBreakPoint = false;
			result._outputProperties = null;
			result._changedProperties = new Dictionary<string,string>();
			result._videoOptions = (VideoOptions) _videoOptions.Clone();
			result.UpdateIDs();

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
			if (!_markedForExecution || exitTotally)
				return;
			currenTestCase = this;
			TestController.Default.RaiseStepStarted(_id);
			if (_hasBreakPoint)
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
			if (_expectedResult != TestCaseResult.None && _actualResult != _expectedResult)
			{
				Log.Result(TestCaseResult.Failed);
				Log.Current.EndFolder();

				if (_screenshotOption == TakeScreenshotOption.OnError)
				{
					Log.Image( Logger.Slideshow.ScreenCapturer.Capture(), "Error - " + Name );
				}
				
				if (_currentRetryNumber < _numOfRetries)
				{
					_currentRetryNumber++;
					Run();
				}
				
			}
			else
			{
				Log.Result(TestCaseResult.Passed);
				Log.Current.EndFolder();
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
			if (_expectedResult == TestCaseResult.Failed)
				Log.Info(message, extra);
			else
				Log.Error(message, extra);

		}

		/// <summary>
		/// Posts info/error message to the log according to the expected result.
		/// If expected result is fail it will log error, otherwise it will log info
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="extra">And additional info to log</param>
		protected void LogPassedByExpectedResult(string message, string extra)
		{
			if (_expectedResult != TestCaseResult.Failed)
				Log.Info(message, extra);
			else
				Log.Error(message, extra);

		}

		private void InitRun()
		{
			_actualResult = TestCaseResult.None;
			GetVariables();

			string name = Name;

			if( _currentRetryNumber > 0 )
				name += " - Retry #" + _currentRetryNumber.ToString();

			XmlTypeAttribute attr = (XmlTypeAttribute) Attribute.GetCustomAttribute( GetType(), typeof(XmlTypeAttribute) );
			Log.Current.StartTestStep( attr.Namespace, attr.TypeName, name, Description );

			if( !string.IsNullOrWhiteSpace( _notes ) )
				Log.Info( "Notes", _notes );

			if (_videoOptions != null && _videoOptions.CaptureVideo
				&& !Logger.Slideshow.SlideshowRecorder.Default.IsCapturing)
			{
				_capturing = true;
				Logger.Slideshow.SlideshowRecorder.Default.Interval = _videoOptions.Interval;
				Logger.Slideshow.SlideshowRecorder.Default.Start( Name );
			}
			if (_screenshotOption == TakeScreenshotOption.BeforeTestCase || _screenshotOption == TakeScreenshotOption.Both)
			{
				Log.Image( Logger.Slideshow.ScreenCapturer.Capture(), "Begin - " + Name );
			}
		}

		private void FinalizeRun()
		{
			HandleResult();
			SetVariables();
			RestoreVariables();
			TestController.Default.RaiseStepResultArrived(expectedVsActual);
			if (_capturing)
			{
				_capturing = false;
				Logger.Slideshow.SlideshowRecorder.Default.Stop();
			}
			if (_screenshotOption == TakeScreenshotOption.AfterTestCase || _screenshotOption == TakeScreenshotOption.Both)
			{
				Log.Image( Logger.Slideshow.ScreenCapturer.Capture(), "End - " + Name );
			}
			System.Windows.Forms.Application.DoEvents();
		}

		private void UpdateIDs()
		{
			_id = maxID;
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

		private OutputPropertiesMap _outputProperties;
		private Dictionary<string, string> _changedProperties = new Dictionary<string,string>();
		private bool _capturing;
		private int _currentRetryNumber = 0;

	}
}
