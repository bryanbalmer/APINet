using log4net;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace APINet.Data
{
    public class WebRequest : IDataRequest
    {
        ILog _log = LogManager.GetLogger(typeof(WebRequest));

        public async Task<string> GetData(string url)
        {
            string downloadedString = string.Empty;

            using (var client = new WebClient())
            {
                try
                {
                    downloadedString = await client.DownloadStringTaskAsync(url);
                }
                catch (WebException ex)
                {
                    _log.ErrorFormat("Network error with {0} downloading string: {1}", ex.Source, ex.Message);
                }
                catch (Exception ex)
                {
                    _log.ErrorFormat("Unspecified error: {0}", ex.ToString());
                }
            }

            return downloadedString;
        }
    }
}