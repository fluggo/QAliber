using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace QAliber.Repository.CommonTestCases {
	static class NativeMethods {
		const int MAX_PATH = 260;
		const int FILE_ATTRIBUTE_DIRECTORY = 0x10;

		[DllImport("shlwapi.dll", ExactSpelling=true, CharSet=CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool PathRelativePathToW(
			StringBuilder pszPath,
			string pszFrom,
			int dwAttrFrom,
			string pszTo,
			int dwAttrTo );

		/// <summary>
		/// Gets a relative path from one path to another.
		/// </summary>
		/// <param name="fromPath">File or directory where the search starts.</param>
		/// <param name="fromIsDirectory">True if <paramref name="fromPath"/> refers to a directory,
		///	  or false if it refers to a file.</param>
		/// <param name="toPath">File or directory to refer to.</param>
		/// <param name="toIsDirectory">True if <paramref name="toPath"/> refers to a directory,
		///	  or false if it refers to a file.</param>
		/// <returns>A relative path if one can be made from <paramref name='fromPath'/> to
		///   <paramref name='toPath'/>, or null if one couldn't be made.</returns>
		public static string MakeRelativePath( string fromPath, bool fromIsDirectory, string toPath, bool toIsDirectory ) {
			StringBuilder result = new StringBuilder( MAX_PATH );

			bool success = PathRelativePathToW( result, fromPath,
				fromIsDirectory ? FILE_ATTRIBUTE_DIRECTORY : 0,
				toPath,
				toIsDirectory ? FILE_ATTRIBUTE_DIRECTORY : 0 );

			if( !success )
				return null;

			return result.ToString();
		}
	}
}
