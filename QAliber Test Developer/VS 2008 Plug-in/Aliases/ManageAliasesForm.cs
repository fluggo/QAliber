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
using System.Windows.Automation;
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
			cmbAliasClass.Text = lastUsedClassName;
			if (Statics.DTE.Solution.IsOpen)
			{
				foreach (Project proj in Statics.DTE.Solution.Projects)
				{
					if (proj.Name.Contains("Test"))
					{
						openProject = proj;
						lang = Statics.Language;
						aliasPath = Path.GetDirectoryName(proj.FileName) + @"\Aliases\";
						if (lang == ProjectLanguage.VB)
						{
							FillComboAliasClasses(Directory.GetFiles(aliasPath, "*.vb"));
						}
						else
						{
							FillComboAliasClasses(Directory.GetFiles(aliasPath, "*.cs"));
						}
						if (cmbAliasClass.Items.Count > 0)
						{
							cmbAliasClass.SelectedIndex = 0;
						}
						initiated = true;
						return;
					}
				}
			}
			MessageBox.Show("To add an alias a solution must be open with a project named *Test*", "Can't Add Alias");
			
		}

		

		public bool Initiated { get { return initiated; } }

		private void FillComboAliasClasses(string[] getFiles)
		{
			foreach (var file in getFiles)
			{
				cmbAliasClass.Items.Add(Path.GetFileNameWithoutExtension(file));
			}
		}

		private void CreateAliasFileCS()
		{
			StringBuilder cSharpCodeText = new StringBuilder();
			cSharpCodeText.AppendLine("using System;");
			cSharpCodeText.AppendLine("using System.Windows.Automation;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls.UIA;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls.Web;");
			cSharpCodeText.AppendLine("using QAliber.Engine.Controls.WPF;");
			cSharpCodeText.AppendLine();
			cSharpCodeText.AppendLine("namespace " + openProject.Properties.Item("DefaultNamespace").Value.ToString());
			cSharpCodeText.AppendLine("{");
			cSharpCodeText.AppendLine("\tpublic static class " + cmbAliasClass.Text);
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

			vbCodeText.AppendLine("Imports System.Windows.Automation");
			vbCodeText.AppendLine();
			vbCodeText.AppendLine("Public Class " + cmbAliasClass.Text);
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
				string tabs = "\t\t";
				cSharpCodeText.AppendLine(tabs + "internal static " + uiType + " " + txtAlias.Text);
				cSharpCodeText.AppendLine(tabs + "{");
				tabs += "\t";
				cSharpCodeText.AppendLine(tabs + "get");
				cSharpCodeText.AppendLine(tabs + "{");
				tabs += "\t";
				if (chkSupressLog.Checked)
				{
					cSharpCodeText.AppendLine(tabs + "bool prevLogState = QAliber.Logger.Log.Default.Enabled;");
					cSharpCodeText.AppendLine(tabs + "QAliber.Logger.Log.Default.Enabled = false;");
					cSharpCodeText.AppendLine(tabs + "try");
					cSharpCodeText.AppendLine(tabs + "{");
					tabs += "\t";
				}
				cSharpCodeText.AppendLine(tabs + uiType + " control = " + codePath + " as " + uiType + ";");
				cSharpCodeText.AppendLine(tabs + "if (control == null || !control.Exists)");
				cSharpCodeText.AppendLine(tabs + "{");
				tabs += "\t";
				if (radioReturnNull.Checked)
				{
					cSharpCodeText.AppendLine(tabs + "return null;"); 
				}
				else
				{
					cSharpCodeText.AppendLine(tabs + "throw new ElementNotAvailableException(\"" + txtAlias.Text + "\");"); 
				}
				tabs = tabs.Remove(tabs.Length - 1);
				cSharpCodeText.AppendLine(tabs + "}");

				cSharpCodeText.AppendLine(tabs + "return control;");
				if (chkSupressLog.Checked)
				{
					tabs = tabs.Remove(tabs.Length - 1);
					cSharpCodeText.AppendLine(tabs + "}");
					cSharpCodeText.AppendLine(tabs + "finally");
					cSharpCodeText.AppendLine(tabs + "{");
					tabs += "\t";
					cSharpCodeText.AppendLine(tabs + "QAliber.Logger.Log.Default.Enabled = prevLogState;");
					tabs = tabs.Remove(tabs.Length - 1);
					cSharpCodeText.AppendLine(tabs + "}");
				}
				tabs = tabs.Remove(tabs.Length - 1);
				cSharpCodeText.AppendLine(tabs + "}");
				tabs = tabs.Remove(tabs.Length - 1);
				cSharpCodeText.AppendLine(tabs + "}");
				tabs = tabs.Remove(tabs.Length - 1);
				cSharpCodeText.AppendLine(tabs + "}");
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
				string vbCodePath = Recorder.RecordsDisplayer.ConvertCodePathToVB(codePath);
				vbCodeText.AppendLine("\tFriend Shared ReadOnly Property " + cmbAliasClass.Text + "() As " + uiType);
				vbCodeText.AppendLine("\t\tGet");
				if (chkSupressLog.Checked)
				{
					vbCodeText.AppendLine("\t\t\tDim prevLogState As Boolean = QAliber.Logger.Log.Default.Enabled");
					vbCodeText.AppendLine("\t\t\tQAliber.Logger.Log.Default.Enabled = False");
				}
				vbCodeText.AppendLine("\t\t\tTry");
				vbCodeText.AppendLine("\t\t\t\tDim control As " + uiType + " = " + vbCodePath);
				vbCodeText.AppendLine("\t\t\t\tReturn control");
				vbCodeText.AppendLine("\t\t\tCatch ex As InvalidCastException");
				if (radioReturnNull.Checked)
				{
					vbCodeText.AppendLine("\t\t\t\tReturn Nothing");
				}
				else
				{
					vbCodeText.AppendLine("\t\t\t\tThrow New ElementNotAvailableException(\"" + txtAlias.Text + "\")");
				}
				if (chkSupressLog.Checked)
				{
					vbCodeText.AppendLine("\t\t\tFinally");
					vbCodeText.AppendLine("\t\t\t\tQAliber.Logger.Log.Default.Enabled = prevLogState");
				}
				vbCodeText.AppendLine("\t\t\tEnd Try");
				vbCodeText.AppendLine("\t\tEnd Get");
				vbCodeText.AppendLine("\tEnd Property");
				vbCodeText.AppendLine("End Class");
				byte[] output = System.Text.Encoding.Default.GetBytes(vbCodeText.ToString());
				stream.Write(output, 0, output.Length);

			}
			
				
		
		}

		private bool ValidateText()
		{
			if (string.IsNullOrEmpty(txtAlias.Text) || string.IsNullOrEmpty(cmbAliasClass.Text))
				return false;
			Regex regexVar = new Regex("^[A-Za-z][A-Za-z0-9]*$");
			return regexVar.Match(txtAlias.Text).Success && regexVar.Match(cmbAliasClass.Text).Success;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (ValidateText())
			{
				lastUsedClassName = cmbAliasClass.Text;
				aliasFile = aliasPath + cmbAliasClass.Text;
				MethodInvoker createFile;
				MethodInvoker generateCode;
				if (lang == ProjectLanguage.VB)
				{
					aliasFile += ".vb";
					createFile = CreateAliasFileVB;
					generateCode = GenerateCodeVB;
				}
				else
				{
					aliasFile += ".cs";
					createFile = CreateAliasFileCS;
					generateCode = GenerateCodeCS;
				}
				if (!File.Exists(aliasFile))
				{
					createFile();
				}
				generateCode();
				Close();
			}
			else
			{
				MessageBox.Show("Please enter a valid variable and/or class name", "Input Is Not a Class/Variable Name");
			}
		}

		private bool initiated = false;
		private static string lastUsedClassName = "Aliases";
		private string aliasFile;
		private string aliasPath;
		private string codePath;
		private string uiType;
		private EnvDTE.Project openProject;
		private ProjectLanguage lang;

	}
}