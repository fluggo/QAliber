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
using System.Windows.Automation;

namespace QAliber.Engine.Controls
{
	/// <summary>
	/// Represents a non existent control in a user-interface under windows OS.
	/// </summary>
	[Serializable]
	public class UINullControl : UIControlBase
	{
		public override bool Exists
		{
			get
			{
				return false;
			}
		}

		#region Equity Operators
		private static bool InternalEquals(UINullControl left, UINullControl right)
		{
			try
			{
				return left.GetHashCode() == right.GetHashCode();
			}
			catch (NullReferenceException)
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return true;
			}
			return base.Equals(obj);
		}

		public static bool operator ==(UINullControl left, UINullControl right)
		{
			return InternalEquals(left, right);
		}

		public static bool operator !=(UINullControl left, UINullControl right)
		{
			return !InternalEquals(left, right);
		}

		public override int GetHashCode()
		{
			return 0;
		}
		#endregion

		
	}

	/// <summary>
	/// Represents a non existent control in a user-interface under windows OS.
	/// </summary>
	[Serializable]
	public class UIANullControl : UIA.UIAControl
	{
		public UIANullControl() : base(null)
		{
		}

		public override bool Exists
		{
			get
			{
				return false;
			}
		}

		#region Equity Operators
		private static bool InternalEquals(UIANullControl left, UIANullControl right)
		{
			try
			{
				return left.GetHashCode() == right.GetHashCode();
			}
			catch (NullReferenceException)
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return true;
			}
			return base.Equals(obj);
		}

		public static bool operator ==(UIANullControl left, UIANullControl right)
		{
			return InternalEquals(left, right);
		}

		public static bool operator !=(UIANullControl left, UIANullControl right)
		{
			return !InternalEquals(left, right);
		}

		public override int GetHashCode()
		{
			return 0;
		}
		#endregion
	}

	/// <summary>
	/// Represents a non existent control in a user-interface under windows OS.
	/// </summary>
	[Serializable]
	public class WebNullControl : Web.WebControl
	{
		public WebNullControl()
			: base(null)
		{
		}

		public override bool Exists
		{
			get
			{
				return false;
			}
		}

		#region Equity Operators
		private static bool InternalEquals(WebNullControl left, WebNullControl right)
		{
			try
			{
				return left.GetHashCode() == right.GetHashCode();
			}
			catch (NullReferenceException)
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return true;
			}
			return base.Equals(obj);
		}

		public static bool operator ==(WebNullControl left, WebNullControl right)
		{
			return InternalEquals(left, right);
		}

		public static bool operator !=(WebNullControl left, WebNullControl right)
		{
			return !InternalEquals(left, right);
		}

		public override int GetHashCode()
		{
			return 0;
		}
		#endregion
	}
}
