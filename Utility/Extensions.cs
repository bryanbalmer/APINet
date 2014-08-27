using System.Collections.Generic;
using System.Text;
using System.Web;

namespace APINet.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Appends StringBuilder with a URL encoded key-value pair.
        /// </summary>
        /// <param name="sb">The StringBuilder.</param>
        /// <param name="kvp">The Key-Value Pair.</param>
        /// <returns></returns>
        public static StringBuilder UrlAppend(this StringBuilder sb, KeyValuePair<string, string> kvp)
        {
            sb.Append(kvp.Key.UrlEncode()).Append("=").Append(kvp.Value.UrlEncode());
            return sb;
        }

        /// <summary>
        /// Appends StringBuilder with a URL encoded
        ///  key-value pair with an '&'
        /// </summary>
        /// <param name="sb">The StringBuilder.</param>
        /// <param name="kvp">The Key-Value Pair.</param>
        /// <returns></returns>
        public static StringBuilder UrlAndAppend(this StringBuilder sb, KeyValuePair<string, string> kvp)
        {
            sb.Append("&").UrlAppend(kvp);
            return sb;
        }

        /// <summary>
        /// Extension to url encode a string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns></returns>
        public static string UrlEncode(this string s)
        {
            return HttpUtility.UrlEncode(s);
        }
    }
}
