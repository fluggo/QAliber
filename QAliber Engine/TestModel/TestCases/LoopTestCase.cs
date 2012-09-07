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
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;

using QAliber.Logger;
using QAliber.TestModel.Attributes;

namespace QAliber.TestModel
{
	/// <summary>
	/// Loop through this test case's children N times
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control\Loops")]
	[XmlType("Loop", Namespace=Util.XmlNamespace)]
	public class LoopTestCase : FolderTestCase
	{
		public LoopTestCase() : base( "Loop" )
		{
			Icon = Properties.Resources.Loop;
		}

		protected int numOfLoops = 1;

		/// <summary>
		/// The number of loops to run through all the children
		/// </summary>
		[Category("Loop")]
		[DisplayName("Number Of Loops")]
		[Description("The number of loops all the descendants will run")]
		public int NumOfLoops
		{
			get { return numOfLoops; }
			set { numOfLoops = value; }
		}

		public override void Body()
		{
			Log log = Log.Current;

			for (int i = 0; i < numOfLoops; i++)
			{
				Scenario.Variables.AddOrReplace(new QAliber.TestModel.Variables.ScenarioVariable<string>("CurrentLoopNumber", ((int)(i+1)).ToString(), this));

				if( log != null )
					log.StartFolder( Name + " - Loop #" + (int)(i + 1), null );

				base.Body();

				if( log != null )
					log.EndFolder();

				if (exitTotally)
					break;
				if (branchesToBreak > 0)
				{
					branchesToBreak--;
					break;
				}
			}
		}

		public override string Description
		{
			get
			{
				return "Looping " + numOfLoops + " Times";
			}
		}

	}

}
