using System.Collections.Generic;
using System.Threading.Tasks;

namespace APINet.Api
{
    /// <summary>
    /// A simple api site using string arguments.
    /// Extends SiteBase
    /// </summary>
    public class ApiSite : SiteBase
    {
        private readonly ICollection<string> _responseNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSite"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="arguments">The arguments required by the query string</param>
        /// <param name="responseNodes">The names of the nodes to get values for.</param>
        public ApiSite(string baseUrl, IEnumerable<string> arguments, ICollection<string> responseNodes)
            : base(baseUrl, arguments)
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
            RequestBuild.SetArgumentValue(argument, value);
            return this;
        }

        /// <summary>
        /// Performs search based on argument values registered and
        /// returns a list of <see cref="ApiResponseItem"/>s.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResponseItem>> Search()
        {
            string data = await DataRequest.GetData(RequestBuild.SearchUrl());
            DataHandler.HandleData(data);
            return DataHandler.GetResults(ParentNodes, ResponseName, _responseNodes);
        }
    }
}
