﻿//------------------------------------------------------------------------------
// <auto-generated>
//	   This code was generated by a tool.
//	   Runtime Version:2.0.50727.3607
//
//	   Changes to this file may cause incorrect behavior and will be lost if
//	   the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QAliber.Runner.Properties {
	
	
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
	internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
		
		private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
		
		public static Settings Default {
			get {
				return defaultInstance;
			}
		}
		
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("localhost")]
		public string QAliberCenterIP {
			get {
				return ((string)(this["QAliberCenterIP"]));
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("TestCases")]
		public string TestCasesAssemblyDir {
			get {
				return ((string)(this["TestCasesAssemblyDir"]));
			}
			set {
				this["TestCasesAssemblyDir"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("Logs")]
		public string LogLocation {
			get {
				return ((string)(this["LogLocation"]));
			}
			set {
				this["LogLocation"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("machine/dd-MM-yy HH_mm")]
		public string LogStructure {
			get {
				return ((string)(this["LogStructure"]));
			}
			set {
				this["LogStructure"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("True")]
		public bool AnimateCursor {
			get {
				return ((bool)(this["AnimateCursor"]));
			}
			set {
				this["AnimateCursor"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("250")]
		public uint DelayAfterAction {
			get {
				return ((uint)(this["DelayAfterAction"]));
			}
			set {
				this["DelayAfterAction"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("5000")]
		public uint ControlAutoWaitTimeout {
			get {
				return ((uint)(this["ControlAutoWaitTimeout"]));
			}
			set {
				this["ControlAutoWaitTimeout"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("False")]
		public bool ShowLogAfter {
			get {
				return ((bool)(this["ShowLogAfter"]));
			}
			set {
				this["ShowLogAfter"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("True")]
		public bool BlockUserInput {
			get {
				return ((bool)(this["BlockUserInput"]));
			}
			set {
				this["BlockUserInput"] = value;
			}
		}
		
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
		[global::System.Configuration.DefaultSettingValueAttribute("Data Source=ILQHFAATC1WS785\\SQLEXPRESS;Initial Catalog=Automation;User ID=Automat" +
			"ionAgentService;Password=letmein")]
		public string QAliberCentrerConnection {
			get {
				return ((string)(this["QAliberCentrerConnection"]));
			}
		}
	}
}
