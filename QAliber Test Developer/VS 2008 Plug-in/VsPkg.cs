// VsPkg.cs : Implementation of RecordStopPackage
//

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio;

namespace QAliber.VS2005.Plugin
{
	
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the registration utility (regpkg.exe) that this class needs
	// to be registered as package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// A Visual Studio component can be registered under different regitry roots; for instance
	// when you debug your package you want to register it in the experimental hive. This
	// attribute specifies the registry root to use if no one is provided to regpkg.exe with
	// the /root switch.
	[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0Exp")]
	// This attribute is used to register the informations needed to show the this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration(true, "#110", "#112", "1.0", IconResourceID = 400)]
	// In order be loaded inside Visual Studio in a machine that has not the VS SDK installed, 
	// package needs to have a valid load key (it can be requested at 
	// http://msdn.microsoft.com/vstudio/extend/). This attributes tells the shell that this 
	// package has a load key embedded in its resources.
	[ProvideLoadKey("Standard", "1.0", "QAliber Test Developer", "QAlibers", 111)]
	// This attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource(1000, 1)]
	// This attribute registers a tool window exposed by this package.
	[ProvideToolWindow(typeof(SpyToolWindow))]
	//[ProvideOptionPage(typeof(QAliber.Recorder.RecorderConfig), "QAliber Test Developer", "Recorder", 101, 106, true)]
	//[ProvideOptionPage(typeof(QAliber.VS2005.Plugin.Options.PlayerOptions), "QAliber Test Developer", "Player", 101, 106, true)]
	[Guid(GuidList.guidUITestingPackagePkgString)]
	public sealed class QAliberTestingPackage : Package, IVsInstalledProduct
	{
		const int bitmapResourceID = 300;
		
		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public QAliberTestingPackage()
		{
			Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
		}

		System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			string file = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + args.Name.Split(',')[0] + ".dll";
			if (System.IO.File.Exists(file))
			{
				try
				{
					return System.Reflection.Assembly.LoadFrom(file);
				}
				catch
				{
				}
			}

			return null;
		}

		/// <summary>
		/// This function is called when the user clicks the menu item that shows the 
		/// tool window. See the Initialize method to see how the menu item is associated to 
		/// this function using the OleMenuCommandService service and the MenuCommand class.
		/// </summary>
		private void ShowToolWindow(object sender, EventArgs e)
		{
			// Get the instance number 0 of this tool window. This window is single instance so this instance
			// is actually the only one.
			// The last flag is set to true so that if the tool window does not exists it will be created.
			ToolWindowPane window = this.FindToolWindow(typeof(SpyToolWindow), 0, true);
			if ((null == window) || (null == window.Frame))
			{
				throw new COMException(Resources.CanNotCreateWindow);
			}
			IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
		}

		private void RecordCallback(object sender, EventArgs e)
		{
			Statics.Commands[(int)Commands.CommandType.Record].Invoke();
		}

		private void StopRecordCallback(object sender, EventArgs e)
		{
			Statics.Commands[(int)Commands.CommandType.StopRecord].Invoke();
		}

		private void RecordCommandInvoked(object sender, EventArgs e)
		{
			menuRecord.Enabled = false;
			menuStopRecord.Enabled = true;
		}

		private void StopRecordCommandInvoked(object sender, EventArgs e)
		{
			menuRecord.Enabled = true;
			menuStopRecord.Enabled = false;
		}


		/////////////////////////////////////////////////////////////////////////////
		// Overriden Package Implementation
		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initilaization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			Trace.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
			base.Initialize();

			// Add our command handlers for menu (commands must exist in the .ctc file)
			OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if ( null != mcs )
			{
				// Create the command for the tool window
				CommandID toolwndCommandID = new CommandID(GuidList.guidUITestingPackageCmdSet, (int)PkgCmdIDList.cmdidUIControlsBrowser);
				MenuCommand menuToolWin = new MenuCommand( new EventHandler(ShowToolWindow), toolwndCommandID);
				CommandID recordCommandID = new CommandID(GuidList.guidUITestingPackageCmdSet, (int)PkgCmdIDList.cmdidUIRecord);
				menuRecord = new MenuCommand(new
				  EventHandler(RecordCallback),
				  recordCommandID);
				CommandID stopRecordCommandID = new CommandID(GuidList.guidUITestingPackageCmdSet, (int)PkgCmdIDList.cmdidUIStopRecord);
				menuStopRecord = new MenuCommand(new
				  EventHandler(StopRecordCallback),
				  stopRecordCommandID);

				Statics.Commands[(int)Commands.CommandType.Record].Invoked += new EventHandler(RecordCommandInvoked);
				Statics.Commands[(int)Commands.CommandType.StopRecord].Invoked += new EventHandler(StopRecordCommandInvoked);
			   
				mcs.AddCommand(menuToolWin);
				mcs.AddCommand(menuRecord);
				mcs.AddCommand(menuStopRecord);
			}
		}
		#endregion

		#region IVsInstalledProduct Members

		public int IdBmpSplash(out uint pIdBmp)
		{
			pIdBmp = 0;
			return VSConstants.S_OK;
		}

		public int IdIcoLogoForAboutbox(out uint pIdIco)
		{
			pIdIco = 400;
			return VSConstants.S_OK;
		}

		public int OfficialName(out string pbstrName)
		{
			pbstrName = "QAliber Visual Studio Plug In";
			return VSConstants.S_OK;

		}

		public int ProductDetails(out string pbstrProductDetails)
		{
			pbstrProductDetails = "QAliber VS plug-in will allow you to create and deliver fast system tests, much like unit tests";
			return VSConstants.S_OK;
		}

		public int ProductID(out string pbstrPID)
		{
			pbstrPID = "1.0";
			return VSConstants.S_OK;
		}

		#endregion

		private MenuCommand menuRecord;
		private MenuCommand menuStopRecord;

	}
}