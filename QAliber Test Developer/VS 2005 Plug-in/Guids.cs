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