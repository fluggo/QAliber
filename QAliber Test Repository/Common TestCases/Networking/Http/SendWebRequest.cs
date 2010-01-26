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
	public class SendWebRequest : global::QAliber.TestModel.TestCase
	{
		public SendWebRequest()
		{
			name = "Send Web Request";
			icon = Properties.Resources.Http;
		}

		public override void Body()
		{
			// used on each read operation
			byte[] buf = new byte[8192];

			// prepare the web page we will be asking for
			HttpWebRequest request = (HttpWebRequest)
				WebRequest.Create(url);

			// execute the request
			HttpWebResponse response = (HttpWebResponse)
				request.GetResponse();

			if (response.StatusCode != expectedCode)
			{
				Log.Default.Error("Http status code was different than expected", "Actual : " + response.StatusCode + "\nExpected : " + expectedCode);
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}
			// we will read data via the response stream
			Stream resStream = response.GetResponseStream();
			using (StreamWriter writer = new StreamWriter(file))
			{
				string tempString = null;
				int count = 0;

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

		private string file;

		[DisplayName("2) Response File")]
		[Category("Http")]
		[Description("The file to store the web response to")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string File
		{
			get { return file; }
			set { file = value; }
		}

		private HttpStatusCode expectedCode;

		[DisplayName("3) Response Code")]
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
				return "Sending web request to '" + url + "' and storing it to '" + file + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	

	}
}
