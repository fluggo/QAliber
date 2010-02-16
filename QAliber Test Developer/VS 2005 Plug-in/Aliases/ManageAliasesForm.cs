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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.CodeDom;
using System.Text.RegularExpressions;
using QAliber.VS2005.Plugin.Commands;
using EnvDTE;

namespace QAliber.VS2005.Plugin.Aliases
{
	public partial class ManageAliasesForm : Form
	{
		private ManageAliasesForm()
		{
			InitializeComponent();
		}

		public ManageAliasesForm(string codePath, string type) : this()
		{
			this.codePath = codePath;
			this.uiType = type;
			if (Statics.DTE.Solution.IsOpen)
			{
				foreach (Project proj in Statics.DTE.Solution.Projects)
				{
					if (proj.Name.Contains("Test"))
					{
						openProject = proj;
						lang = Statics.Language;
						if (lang == ProjectLanguage.VB)
							aliasFile = Path.GetDirectoryName(proj.FileName) + @"\Aliases\Aliases.vb";
						else
							aliasFile = Path.GetDirectoryName(proj.FileName) + @"\Aliases\Aliases.cs";
						initiated = true;
						return;
					}
				}
			}
			MessageBox.Show("To add an alias a solution must be open with a project named *Test*", "Can't Add Alias");
			
		}

		public bool Initiated { get { return initiated; } }

		private void CreateAliasFileCS()
		{
			StringBuilder cSharpCodeText = new StringBuilder();
			cSharpCodeText.AppendLine("using System;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls.UIA;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls.Web;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls.WPF;");
			cSharpCodeText.AppendLine();
			cSharpCodeText.AppendLine("namespace " + openProject.Properties.Item("DefaultNamespace").Value.ToString());
			cSharpCodeText.AppendLine("{");
			cSharpCodeText.AppendLine("\tpublic static class Aliases");
			cSharpCodeText.AppendLine("\t{");
			cSharpCodeText.AppendLine("\t}");
			cSharpCodeText.AppendLine("}");

			using (StreamWriter writer = new StreamWriter(aliasFile))
			{
				writer.Write(cSharpCodeText.ToString());
			}

			openProject.ProjectItems.AddFromFile(aliasFile);
		}

		private void CreateAliasFileVB()
		{
			StringBuilder vbCodeText = new StringBuilder();
			vbCodeText.AppendLine("Imports QAliber.Engine.Controls");
			vbCodeText.AppendLine("Imports QAliber.Engine.Controls.UIA");
			vbCodeText.AppendLine("Imports QAliber.Engine.Controls.Web");
			vbCodeText.AppendLine("Imports QAliber.Engine.Controls.WPF");
			vbCodeText.AppendLine();
			vbCodeText.AppendLine("Public Class Aliases");
			vbCodeText.AppendLine("End Class");

			using (StreamWriter writer = new StreamWriter(aliasFile))
			{
				writer.Write(vbCodeText.ToString());
			}

			openProject.ProjectItems.AddFromFile(aliasFile);
		}

		private void GenerateCodeCS()
		{
		   StringBuilder cSharpCodeText = new StringBuilder();
			
		   using (FileStream stream = File.Open(aliasFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				byte[] ch = new byte[1];
				int count = 0;
				stream.Seek(-1, SeekOrigin.End);
				while (count < 2 && stream.Position > 0)
				{
					stream.Read(ch, 0, 1);
					if (ch[0] == '}')
						count++;
					stream.Seek(-2, SeekOrigin.Current);
				}
				cSharpCodeText.AppendLine("\t\tinternal static " + uiType + " " + txtAlias.Text);
				cSharpCodeText.AppendLine("\t\t{");
				cSharpCodeText.AppendLine("\t\t\tget");
				cSharpCodeText.AppendLine("\t\t\t{");
				cSharpCodeText.AppendLine("\t\t\t\treturn (" + uiType + ")" + codePath + ";");
				cSharpCodeText.AppendLine("\t\t\t}");
				cSharpCodeText.AppendLine("\t\t}");
				cSharpCodeText.AppendLine("\t}");
				cSharpCodeText.AppendLine("}");
				byte[] output = System.Text.Encoding.Default.GetBytes(cSharpCodeText.ToString());
				stream.Write(output, 0, output.Length);

			}
			
				
		
		}

		private void GenerateCodeVB()
		{
			StringBuilder vbCodeText = new StringBuilder();
			if (!File.Exists(aliasFile))
			{
				CreateAliasFileVB();
			}
		   using (FileStream stream = File.Open(aliasFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				byte[] ch = new byte[1];
				int count = 0;
				stream.Seek(-1, SeekOrigin.End);
				while (count < 1 && stream.Position > 0)
				{
					stream.Read(ch, 0, 1);
					if (ch[0] == 'E')
						count++;
					stream.Seek(-2, SeekOrigin.Current);
				}
				vbCodeText.AppendLine("\tFriend Shared ReadOnly Property " + txtAlias.Text + "() As " + uiType);
				vbCodeText.AppendLine("\t\tGet");
				vbCodeText.AppendLine("\t\t\tReturn " + Recorder.RecordsDisplayer.ConvertCodePathToVB(codePath));
				vbCodeText.AppendLine("\t\tEnd Get");
				vbCodeText.AppendLine("\tEnd Property");
				vbCodeText.AppendLine("End Class");
				byte[] output = System.Text.Encoding.Default.GetBytes(vbCodeText.ToString());
				stream.Write(output, 0, output.Length);

			}
			
				
		
		}

		private bool ValidateText()
		{
			if (string.IsNullOrEmpty(txtAlias.Text))
				return false;
			Regex regexVar = new Regex("^[A-Za-z][A-Za-z0-9]*$");
			return regexVar.Match(txtAlias.Text).Success;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (ValidateText())
			{
				if (!File.Exists(aliasFile))
				{
					if (lang == ProjectLanguage.VB)
						CreateAliasFileVB();
					else
						CreateAliasFileCS();
				}
				if (lang == ProjectLanguage.VB)
					GenerateCodeVB();
				else
					GenerateCodeCS();
				Close();
			}
			else
			{
				MessageBox.Show("Please enter a valid variable name", "Input Is Not a Variable");
			}
		}

		private bool initiated = false;
		private string aliasFile;
		private string codePath;
		private string uiType;
		private EnvDTE.Project openProject;
		private ProjectLanguage lang;

		

	   

		
		

		
	}
}