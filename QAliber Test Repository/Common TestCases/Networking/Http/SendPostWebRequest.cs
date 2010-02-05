using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Net;

namespace QAliber.Repository.CommonTestCases.Networking.Http
{
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"Networking\Http")]
	public class SendPostWebRequest : global::QAliber.TestModel.TestCase
	{
		public SendPostWebRequest()
		{
			name = "Send Post Web Request";
			icon = Properties.Resources.Http;
		}

		public override void Body()
		{
			// used on each read operation
			byte[] buf = new byte[8192];

			// prepare the web page we will be asking for
			HttpWebRequest request = (HttpWebRequest)
				WebRequest.Create(url);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			int count = 0;
			using (StreamReader reader = new StreamReader(inFile))
			{
				request.ContentLength = reader.BaseStream.Length;
				Stream reqStream = request.GetRequestStream();
				while ((count = reader.BaseStream.Read(buf, 0, buf.Length)) > 0)
					reqStream.Write(buf, 0, count);
				reqStream.Close();
			}

			// execute the request
			HttpWebResponse response = (HttpWebResponse)
				request.GetResponse();

			if (response.StatusCode != expectedCode)
			{
				Log.Default.Error("Http status code was different than expected", "Actual : " + response.StatusCode + "\nExpected : " + expectedCode);
				actualResult = global::QAliber.RemotingModel.TestCaseResult.Failed;
			}
			// we will read data via the response stream
			Stream resStream = response.GetResponseStream();
			using (StreamWriter writer = new StreamWriter(outFile))
			{
				string tempString = null;
				

				do
				{
					// fill the buffer with data
					count = resStream.Read(buf, 0, buf.Length);

					// make sure we read some data
					if (count != 0)
					{
						// translate from bytes to ASCII text
						tempString = Encoding.ASCII.GetString(buf, 0, count);

						writer.Write(tempString);
					}
				}
				while (count > 0); // any more data to read?
			}
		}

		private string url;

		[DisplayName("1) URL")]
		[Category("Http")]
		[Description("The url to retrieve")]
		public string URL
		{
			get { return url; }
			set { url = value; }
		}

		private string inFile;

		[DisplayName("2) Request File")]
		[Category("Http")]
		[Description("The file that holds the request data")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string InFile
		{
			get { return inFile; }
			set { inFile = value; }
		}

		private string outFile;

		[DisplayName("3) Response File")]
		[Category("Http")]
		[Description("The file to store the web response to")]
		[Editor(typeof(UITypeEditors.FileSaveTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string OutFile
		{
			get { return outFile; }
			set { outFile = value; }
		}

		private HttpStatusCode expectedCode;

		[DisplayName("4) Response Code")]
		[Category("Http")]
		[Description("The expected http response code")]
		public HttpStatusCode ExpectedResponseCode
		{
			get { return expectedCode; }
			set { expectedCode = value; }
		}

		public override string Description
		{
			get
			{
				return "Sending web request to '" + url + "' with data from '" + inFile + "' and storing it to '" + outFile + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	

	}
}
