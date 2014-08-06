using APINet.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace APINet.Data
{
    public class XmlProcessor : IDataProcessor
    {
        private XmlNode _nodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlProcessor"/> class.
        /// </summary>
        /// <param name="data">The XML data to be processed.</param>
        public XmlProcessor(string data)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                _nodes = doc;
            }
            catch (XmlException ex)
            {
                Debug.WriteLine("Error with {0} loading the Xml Document: {1}", ex.Source, ex.Message);
            }
            catch (XPathException ex)
            {
                Debug.WriteLine("Error in {0} finding XPath expression: {1}", ex.Source, ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Null reference in {0} parsing XML: {1}", ex.TargetSite.Name, ex.Message);
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
        private void ParseNodes(IEnumerable<string> parents)
        {
            foreach (string s in parents)
                _nodes = _nodes.SelectSingleNode(s);
        }

        public List<ApiResponseItem> FindRelevantResults(IEnumerable<string> parentNodes, string resultName, IEnumerable<string> resultNodes)
        {
            List<ApiResponseItem> results = new List<ApiResponseItem>();
            ParseNodes(parentNodes);

            foreach (XmlNode node in _nodes)
            {
                ApiResponseItem item = new ApiResponseItem(resultNodes);
                foreach (string s in resultNodes)
                {
                    item.SetSubItem(s, node.SelectSingleNode(s).InnerText);
                }
                results.Add(item);
            }

            return results;
        }


        public List<ApiResponseItem<R>> FindRelevantResults<R>(IEnumerable<string> parentNodes, string resultName)
            where R : struct
        {
            List<ApiResponseItem<R>> results = new List<ApiResponseItem<R>>();
            ParseNodes(parentNodes);

            R[] responseValues = (R[])Enum.GetValues(typeof(R));
            foreach (XmlNode node in _nodes)
            {
                ApiResponseItem<R> item = new ApiResponseItem<R>();
                foreach (R r in responseValues)
                {
                    item.SetSubItem(r, node.SelectSingleNode(r.ToString()).InnerText);
                }
                results.Add(item);
            }

            return results;
        }
    }
}
