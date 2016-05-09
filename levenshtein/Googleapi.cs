using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace levenshtein
{
    class Googleapi
    {
        public string api(String cdmdesc)
            {
                 // Create a request for the URL. 
               // String cdm_description = "predisone";
                String url = "http://clients1.google.com/complete/search?hl=en&output=toolbar&q=" + cdmdesc;
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
               // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
              //  Console.WriteLine(responseFromServer);
                StringBuilder output = new StringBuilder();
                using (XmlReader xmlreader = XmlReader.Create(new StringReader(responseFromServer)))
                {
                    xmlreader.ReadToFollowing("suggestion");
                    xmlreader.MoveToFirstAttribute();
                    string data = xmlreader.Value;
                    output.AppendLine(data);

                    }
              
                reader.Close();
                response.Close();
            return output.ToString();
        }
    }
}
