; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{3A880920-8CCB-4847-A1BD-A97644FD18B3}
AppName=QAliber Test Suite
AppVerName=QAliber Test Suite 1.0
AppPublisher=QAlibers (c)
AppPublisherURL=http://www.qaliber.net/
AppSupportURL=http://www.qaliber.net/
AppUpdatesURL=http://www.qaliber.net/
CreateAppDir=no
LicenseFile=..\QAliber Test Builder\Presentation\COPYING.txt
OutputBaseFilename=QAliberTestSuiteSetup
Compression=lzma
SolidCompression=yes
WizardImageFile=QAliberSetupLogo.bmp
WizardSmallImageFile=QAliberLogo55.bmp

[Types]
Name: "full"; Description: "Full installation"
Name: "custom"; Description: "Custom installation"; Flags: iscustom

[Components]
Name: "installvs2005"; Description: "Install Visual Studio 2005 Plug-in (QAlibet Test Developer)"; Types: full custom; Check: IsVS2005Installed
Name: "installvs2008"; Description: "Install Visual Studio 2008 Plug-in (QAlibet Test Developer)"; Types: full custom; Check: IsVS2008Installed
Name: "installbuilder"; Description: "Install QAliber Test Builder"; Types: full custom;


[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "QAliberTestBuilderSetup.msi"; DestDir: "{win}"; Flags: ignoreversion deleteafterinstall
Source: "QAliberVS2005PluginSetup.msi"; DestDir: "{win}"; Flags: ignoreversion deleteafterinstall; Check: IsVS2005Installed
Source: "QAliberVS2008PluginSetup.msi"; DestDir: "{win}"; Flags: ignoreversion deleteafterinstall; Check: IsVS2008Installed
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Run]
Filename: "msiexec"; Parameters: "/i ""{win}\QAliberTestBuilderSetup.msi"""; WorkingDir: "{win}"; StatusMsg: "Installing QAliber Test Builder..."; Check: IsComponentSelected('installbuilder')
Filename: "msiexec"; Parameters: "/i ""{win}\QAliberVS2005PluginSetup.msi"""; WorkingDir: "{win}"; StatusMsg: "Installing VS 2005 Plug-in..."; Check: IsComponentSelected('installvs2005')
Filename: "msiexec"; Parameters: "/i ""{win}\QAliberVS2008PluginSetup.msi"""; WorkingDir: "{win}"; StatusMsg: "Installing VS 2008 Plug-in..."; Check: IsComponentSelected('installvs2008')

[UnInstallRun]
Filename: "msiexec"; Parameters: "/x {{20D197D0-8E7B-42A5-B58E-8E510350F352}"; WorkingDir: "{win}"; StatusMsg: "Un-installing QAliber Test Builder..."; Check: IsComponentSelected('installbuilder')
Filename: "msiexec"; Parameters: "/x {{5CD9EE21-8C36-45BD-93AC-B090D2ED7A8F}"; WorkingDir: "{win}"; StatusMsg: "Un-installing VS 2005 Plug-in..."; Check: IsComponentSelected('installvs2005')
Filename: "msiexec"; Parameters: "/x {{105E14C1-C2C6-486F-81B0-3217DFDA1086}"; WorkingDir: "{win}"; StatusMsg: "Un-installing VS 2008 Plug-in..."; Check: IsComponentSelected('installvs2008')

[Code]
function IsVS2005Installed() : Boolean;
begin
   Result := RegValueExists(HKEY_LOCAL_MACHINE, 'Software\Microsoft\VisualStudio\8.0', 'InstallDir') or RegValueExists(HKEY_LOCAL_MACHINE, 'Software\Wow6432Node\Microsoft\VisualStudio\8.0', 'InstallDir');
end;

function IsVS2008Installed() : Boolean;
begin
   Result := RegValueExists(HKEY_LOCAL_MACHINE, 'Software\Microsoft\VisualStudio\9.0', 'InstallDir') or RegValueExists(HKEY_LOCAL_MACHINE, 'Software\Wow6432Node\Microsoft\VisualStudio\9.0', 'InstallDir');
end;

function IsVSInstalled() : Boolean;
begin
    Result := IsVS2005Installed() or IsVS2008Installed();
end;



