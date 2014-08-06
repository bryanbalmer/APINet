using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APINet.Data
{
    public class WebRequest : IDataRequest
    {
        public async Task<string> GetData(string url)
        {
            string downloadedString = string.Empty;

            using (WebClient client = new WebClient())
            {
                try
                {
                    downloadedString = await client.DownloadStringTaskAsync(url);
                }
                catch (WebException ex)
                {
                    Debug.WriteLine("Network error with {0} downloading string: {1}", ex.Source, ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unspecified error: {0}", ex.ToString());
                }
            }

            return downloadedString;
        }
    }
}
