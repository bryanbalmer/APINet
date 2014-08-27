using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APINet.Api
{
    /// <summary>
    /// Type-safe api site search.
    /// </summary>
    /// <typeparam name="TA">Enum containing query string argument names.</typeparam>
    /// <typeparam name="TR">Enum containing response item names.</typeparam>
    public class ApiSite<TA, TR> : SiteBase
        where TA : struct
        where TR : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSite{A, R}"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        public ApiSite(string baseUrl)
            : base(baseUrl, Enum.GetNames(typeof(TA)))
        {

        }

        /// <summary>
        /// Sets the value of a given argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ApiSite<TA, TR> SetArgumentValue(TA argument, string value)
        {
            RequestBuild.SetArgumentValue(argument.ToString(), value);
            return this;
        }

        /// <summary>
        /// Searches the site with the query string built
        /// from the arguments and values. Returns a list
        /// of <see cref="ApiResponseItem" />/>s using the
        /// given response Enum.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiResponseItem<TR>>> Search()
        {
            string data = await DataRequest.GetData(RequestBuild.SearchUrl());
            DataHandler.HandleData(data);
            return DataHandler.GetResults<TR>(ParentNodes, ResponseName);
        }
    }
}
