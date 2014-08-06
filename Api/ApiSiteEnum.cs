using APINet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINet.Api
{
    /// <summary>
    /// Type-safe api site search.
    /// </summary>
    /// <typeparam name="A">Enum containing query string argument names.</typeparam>
    /// <typeparam name="R">Enum containing response item names.</typeparam>
    public class ApiSite<A, R> : SiteBase
        where A : struct
        where R : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSite{A, R}"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        public ApiSite(string baseUrl)
            :base(baseUrl, Enum.GetNames(typeof(A)))
        {

        }

        /// <summary>
        /// Sets the value of a given argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ApiSite<A, R> SetArgumentValue(A argument, string value)
        {
            _requestBuild.SetArgumentValue(argument.ToString(), value);
            return this;
        }

        /// <summary>
        /// Searches the site with the query string built
        /// from the arguments and values. Returns a list
        /// of <see cref="ApiResponseItem<R>"/>s using the
        /// given response Enum.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResponseItem<R>>> Search()
        {
            string data = await _dataRequest.GetData(_requestBuild.SearchUrl());
            _dataHandler.HandleData(data);
            return _dataHandler.GetResults<R>(ParentNodes, ResponseName);
        }
    }
}
