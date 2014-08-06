using APINet.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace APINet.Data
{
    public class JsonProcessor : IDataProcessor
    {
        private JToken _tokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonProcessor"/> class.
        /// </summary>
        /// <param name="data">The JSON data be processed.</param>
        public JsonProcessor(string data)
        {
            try
            {
                _tokens = JToken.Parse(data);
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine("Error in {0} reading JSON: {1}", ex.Source, ex.Message);
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine("Serialization Error in {0}: {1}", ex.Source, ex.Message);
            }
            catch (JsonException ex)
            {
                Debug.WriteLine("Error in {0} with JSON: {1}", ex.Source, ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unspecified error: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Moves to the desired result node.
        /// </summary>
        /// <param name="parents">The parents of the result node.</param>
        private void ParseTokens(IEnumerable<string> parents)
        {
            foreach (string s in parents)
                _tokens = _tokens.SelectToken(s);
        }

        public List<ApiResponseItem> FindRelevantResults(IEnumerable<string> parentNodes, string resultName, IEnumerable<string> resultNodes)
        {
            List<ApiResponseItem> results = new List<ApiResponseItem>();

            ParseTokens(parentNodes);

            foreach (JToken token in _tokens)
            {
                ApiResponseItem item = new ApiResponseItem(resultNodes);
                foreach (string s in resultNodes)
                {
                    item.SetSubItem(s, token.SelectToken(s).ToString());
                }
                results.Add(item);
            }

            return results;
        }


        public List<ApiResponseItem<R>> FindRelevantResults<R>(IEnumerable<string> parentNodes, string resultName)
            where R : struct
        {
            List<ApiResponseItem<R>> results = new List<ApiResponseItem<R>>();
            ParseTokens(parentNodes);

            R[] responseValues = (R[])Enum.GetValues(typeof(R));
            foreach (JToken token in _tokens)
            {
                ApiResponseItem<R> item = new ApiResponseItem<R>();
                foreach (R r in responseValues)
                {
                    item.SetSubItem(r, token.SelectToken(r.ToString()).ToString());
                }
                results.Add(item);
            }

            return results;
        }
    }
}
