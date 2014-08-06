using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APINet.Utility;

namespace APINet.Api
{
    /// <summary>
    /// Class to buld api request url.
    /// </summary>
    public class ApiRequestBuilder
    {
        private string _baseUrl;
        private Dictionary<string, string> _arguments = new Dictionary<string,string>();
        public IEnumerable<string> Arguments
        {
            get { return _arguments.Keys.AsEnumerable(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRequestBuilder"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="arguments">The argument names.</param>
        public ApiRequestBuilder(string baseUrl, IEnumerable<string> arguments)
        {
            _baseUrl = baseUrl;
            foreach (string s in arguments)
                _arguments.Add(s, string.Empty);
        }

        /// <summary>
        /// Sets an argument value if it exists.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="value">The argument's value.</param>
        public void SetArgumentValue(string argument, string value)
        {
            if (_arguments.ContainsKey(argument))
                _arguments[argument] = value;
        }

        /// <summary>
        /// Gets the built search url.
        /// </summary>
        /// <returns>System.String</returns>
        public string SearchUrl()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(_baseUrl);

            if (_arguments.Count > 0)
                sb.UrlAppend(_arguments.ElementAt(0));

            for (int i = 1; i < _arguments.Count; i++)
            {
                sb.UrlAndAppend(_arguments.ElementAt(i));
            }

            return sb.ToString();
        }
    }
}
