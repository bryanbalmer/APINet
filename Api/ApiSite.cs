using APINet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APINet.Api
{
    /// <summary>
    /// A simple api site using string arguments.
    /// Extends SiteBase
    /// </summary>
    public class ApiSite : SiteBase
    {
        private IEnumerable<string> _responseNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSite"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="arguments">The arguments required by the query string</param>
        /// <param name="responseNodes">The names of the nodes to get values for.</param>
        public ApiSite(string baseUrl, IEnumerable<string> arguments, IEnumerable<string> responseNodes)
            :base(baseUrl, arguments)
        {
            _responseNodes = responseNodes;
        }

        /// <summary>
        /// Sets the argument value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ApiSite SetArgumentValue(string argument, string value)
        {
            _requestBuild.SetArgumentValue(argument, value);
            return this;
        }

        /// <summary>
        /// Performs search based on argument values registered and
        /// returns a list of <see cref="ApiResponseItem"/>s.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResponseItem>> Search()
        {
            string data = await _dataRequest.GetData(_requestBuild.SearchUrl());
            _dataHandler.HandleData(data);
            return _dataHandler.GetResults(ParentNodes, ResponseName, _responseNodes);
        }
    }
}
