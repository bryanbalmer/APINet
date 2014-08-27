using APINet.Data;
using System.Collections.Generic;

namespace APINet.Api
{
    /// <summary>
    /// Abstract class for a site with a query string api.
    /// </summary>
    public abstract class SiteBase
    {
        protected ApiRequestBuilder RequestBuild;
        protected IDataRequest DataRequest;
        protected DataHandler DataHandler = new DataHandler();

        /// <summary>
        /// Gets or sets the parent nodes.
        /// </summary>
        /// <value>
        /// The parent nodes are the topmost nodes leading to the desired data.
        /// </value>
        public ICollection<string> ParentNodes { get; set; }

        /// <summary>
        /// Gets or sets the name of the response.
        /// </summary>
        /// <value>
        /// The name of the node containing the desired data.
        /// </value>
        public string ResponseName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteBase"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="arguments">The arguments required by the query string</param>
        protected SiteBase(string baseUrl, IEnumerable<string> arguments)
        {
            RequestBuild = new ApiRequestBuilder(baseUrl, arguments);
            DataRequest = new WebRequest();
        }
    }
}
