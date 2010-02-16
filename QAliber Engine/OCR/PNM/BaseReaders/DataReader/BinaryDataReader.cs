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
using System.Text;

namespace QAliber.ImageHandling.DataReader
{
	/// <summary>
	/// Summary description for BinaryDataReader.
	/// </summary>
	internal class BinaryDataReader:IPNMDataReader
	{
		private BinaryReader br; 
		private char EndLine = '\n';
		public BinaryDataReader(FileStream fs)
		{			
			br = new BinaryReader((Stream)fs);
		}

		public string ReadLine()
		{
			byte cur;
			StringBuilder sb = new StringBuilder();
		
			cur =(byte) br.ReadByte();
			while(cur!=EndLine)
			{
				sb.Append(((char)cur).ToString());
				cur = br.ReadByte();
			}

			return sb.ToString();
		}

		public byte ReadByte()
		{
			return br.ReadByte();
		}

		public void Close()
		{
			br.Close();
		}
	}
}

