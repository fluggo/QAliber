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
 
//------------------------------------------------------------------------------
// <auto-generated>
//	   This code was generated by a tool.
//	   Runtime Version:2.0.50727.3603
//
//	   Changes to this file may cause incorrect behavior and will be lost if
//	   the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QAliber.Builder.Presentation.Properties {
	
	
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
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("True")]
		public bool MinimizeOnRun {
			get {
				return ((bool)(this["MinimizeOnRun"]));
			}
			set {
				this["MinimizeOnRun"] = value;
			}
		}
		
		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("True")]
		public bool CopyIfNewerTestCasesAssemblies {
			get {
				return ((bool)(this["CopyIfNewerTestCasesAssemblies"]));
			}
			set {
				this["CopyIfNewerTestCasesAssemblies"] = value;
			}
		}
	}
}
