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
using System.IO;

namespace QAliber.ImageHandling.DataWriter
{
	/// <summary>
	/// Summary description for BinaryDataWriter.
	/// </summary>
	internal class BinaryDataWriter:IPNMDataWriter
	{
		private BinaryWriter bw;
		public BinaryDataWriter(FileStream fs)
		{
			bw = new BinaryWriter((Stream)fs);
		}

		#region IPNMDataWriter Members

		public void WriteLine(string line)
		{
			bw.Write(System.Text.Encoding.ASCII.GetBytes(line+"\n"));			
		}

		public void WriteByte(byte data)
		{
			bw.Write(data);
		}

		public void Close()
		{
			bw.Flush();
			bw.Close();
		}

		#endregion
	}
}
