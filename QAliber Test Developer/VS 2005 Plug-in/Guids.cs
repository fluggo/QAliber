// Guids.cs
// MUST match guids.h
using System;

namespace QAliber.VS2005.Plugin
{
	static class GuidList
	{
		public const string guidUITestingPackagePkgString = "118c6e7d-352f-4a92-8113-eb60385f959c";
		public const string guidUITestingPackageCmdSetString = "9ab4a099-b7fb-4a7f-9395-27fdfe62991c";
		public const string guidToolWindowPersistanceString = "c1447f67-c464-4053-b6b3-19684aeadf62";

		public static readonly Guid guidUITestingPackagePkg = new Guid(guidUITestingPackagePkgString);
		public static readonly Guid guidUITestingPackageCmdSet = new Guid(guidUITestingPackageCmdSetString);
		public static readonly Guid guidToolWindowPersistance = new Guid(guidToolWindowPersistanceString);
	};
}