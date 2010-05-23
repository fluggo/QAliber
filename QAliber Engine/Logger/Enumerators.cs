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

namespace QAliber.Logger
{
	/// <summary>
	/// The types a log message can be.
	/// </summary>
	public enum EntryType
	{
		/// <summary>
		/// User actions
		/// </summary>
		Event,
		/// <summary>
		/// General information logging
		/// </summary>
		Info,
		/// <summary>
		/// Things were not as expected, but are not errors
		/// </summary>
		Warning,
		/// <summary>
		/// Something went wrong
		/// </summary>
		Error
		
	}

	/// <summary>
	/// How a message is treated
	/// </summary>
	internal enum BodyType
	{
		Text,
		Picture,
		Link
	}

	/// <summary>
	/// The verbosity level of a message
	/// Internal - for QAliber engine messages
	/// Debug - for debugging information
	/// Normal - the default verbosity
	/// Critical - for messages that should not be avoided
	/// </summary>
	public enum EntryVerbosity
	{
		/// <summary>
		/// QAliber engine messages
		/// </summary>
		Internal,
		/// <summary>
		/// Debugging information
		/// </summary>
		Debug,
		/// <summary>
		/// The default verbosity
		/// </summary>
		Normal,
		/// <summary>
		/// Messages that should not be avoided
		/// </summary>
		Critical
	}
}
