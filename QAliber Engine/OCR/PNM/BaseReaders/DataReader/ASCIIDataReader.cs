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
using System.Collections;

namespace QAliber.ImageHandling.DataReader
{
	/// <summary>
	/// Summary description for ASCIIDataReader.
	/// </summary>
	internal class ASCIIDataReader:IPNMDataReader
	{
		private char[] WhiteSpaces = new char[]{' ','\t','\r','\n'};
		private StreamReader sr; 
		private Queue toks;		

		public ASCIIDataReader(FileStream fs)
		{
			toks = new Queue();
			sr = new StreamReader((Stream)fs,System.Text.ASCIIEncoding.ASCII);
		}
		
		public string ReadLine()
		{
			return sr.ReadLine();
		}
		
		public byte ReadByte()
		{	
			if(toks!=null &&  toks.Count>0)
			{						
				return byte.Parse(toks.Dequeue().ToString());
			}

			string line  = sr.ReadLine();

			if(line == null) 
				throw new Exception("Unexpected end of file");

			string[] data =line.Split(WhiteSpaces);
	
			foreach(string str in data)
			{
				//put only non trivial tokens
				if(str.Length>0)
					toks.Enqueue(str);
			}

			//recurrsion
			return ReadByte();
		}

		public void Close()
		{
			sr.Close();
		}
	}
}
