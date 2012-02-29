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
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Patterns;
using System.Diagnostics;
using System.Xml.Serialization;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{

	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("RetrieveListItems", Namespace=Util.XmlNamespace)]
	public class RetrieveListItems : TestCase
	{
		public RetrieveListItems() : base( "Retrieve List Items" )
		{
			icon = Properties.Resources.Combobox;
		}

		private string control = "";


		[Category("Control")]
		[DisplayName("1) Control")]
		[Description("The control must be of type UIACombobox, UIAListBox or HTMLSelect")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string[] listItems;

		[Category("Control")]
		[DisplayName("2) Retrieved Items")]
		[Description("String of all items in the list")]
		public string[] ListItems
		{
			get { return listItems; }
		}




		public override void Body()
		{
			try
			{
				UIControlBase c = UIControlBase.FindControlByPath( control );

				if (!c.Exists)
				{
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					throw new InvalidOperationException("Control not found");
				}

				ISelector selectorPattern = c.GetControlInterface<ISelector>();

				if( selectorPattern != null ) {
					listItems = selectorPattern.Items;
					actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
				}
				else if (c is HTMLSelect)
				{
					HTMLOption[] ops = ((HTMLSelect)c).Options;
					listItems = new string[ops.Length];
					for (int idx = 0; idx < ops.Length; idx++)
					{
						listItems[idx] = ops[idx].Text;
					}

					actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
				}
				else
				{
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
					throw new InvalidOperationException("Control is not list type control");
				}

			}
			catch (Exception ex)
			{
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw ex;
			}
		}

		public override string Description
		{
			get
			{
				return "Retrieve items from list control " + control;
			}
		}

	}


}