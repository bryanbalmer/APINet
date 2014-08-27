using System;
using System.Collections.Generic;
using System.Diagnostics;
using APINet.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APINet.Data
{
    public class JsonProcessor : IDataProcessor
    {
        private JToken _tokens;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonProcessor" /> class.
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

        public List<ApiResponseItem> FindRelevantResults(ICollection<string> parentNodes, string resultName,
            ICollection<string> resultNodes)
        {
            var results = new List<ApiResponseItem>();

            ParseTokens(parentNodes);

            foreach (JToken token in _tokens)
            {
                var item = new ApiResponseItem(resultNodes);
                foreach (string resultNode in resultNodes)
                {
                    item.SetSubItem(resultNode, token.SelectToken(resultNode).ToString());
                }
                results.Add(item);
            }

            return results;
        }


        public List<ApiResponseItem<TR>> FindRelevantResults<TR>(ICollection<string> parentNodes, string resultName)
            where TR : struct
        {
            var results = new List<ApiResponseItem<TR>>();
            ParseTokens(parentNodes);

            var responseValues = (TR[]) Enum.GetValues(typeof (TR));
            foreach (JToken token in _tokens)
            {
                var item = new ApiResponseItem<TR>();
                foreach (TR responseValue in responseValues)
                {
                    item.SetSubItem(responseValue, token.SelectToken(responseValue.ToString()).ToString());
                }
                results.Add(item);
            }

            return results;
        }

        /// <summary>
        ///     Moves to the desired result node.
        /// </summary>
        /// <param name="parents">The parents of the result node.</param>
        private void ParseTokens(IEnumerable<string> parents)
        {
            foreach (string s in parents)
                _tokens = _tokens.SelectToken(s);
        }
    }
}