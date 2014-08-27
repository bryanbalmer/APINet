using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using APINet.Api;

namespace APINet.Data
{
    public class XmlProcessor : IDataProcessor
    {
        private XmlNode _nodes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlProcessor" /> class.
        /// </summary>
        /// <param name="data">The XML data to be processed.</param>
        public XmlProcessor(string data)
        {
            try
            {
                var doc = new XmlDocument();
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

        public List<ApiResponseItem> FindRelevantResults(ICollection<string> parentNodes, string resultName,
            ICollection<string> resultNodes)
        {
            var results = new List<ApiResponseItem>();
            ParseNodes(parentNodes);

            foreach (XmlNode node in _nodes)
            {
                var apiResponseItem = new ApiResponseItem(resultNodes);
                foreach (string resultNode in resultNodes)
                {
                    XmlNode selectSingleNode = node.SelectSingleNode(resultNode);
                    if (selectSingleNode != null)
                        apiResponseItem.SetSubItem(resultNode, selectSingleNode.InnerText);
                }
                results.Add(apiResponseItem);
            }

            return results;
        }


        public List<ApiResponseItem<TR>> FindRelevantResults<TR>(ICollection<string> parentNodes, string resultName)
            where TR : struct
        {
            var results = new List<ApiResponseItem<TR>>();
            ParseNodes(parentNodes);

            var responseValues = (TR[]) Enum.GetValues(typeof (TR));
            foreach (XmlNode node in _nodes)
            {
                var item = new ApiResponseItem<TR>();
                foreach (TR r in responseValues)
                {
                    XmlNode selectSingleNode = node.SelectSingleNode(r.ToString());
                    if (selectSingleNode != null)
                        item.SetSubItem(r, selectSingleNode.InnerText);
                }
                results.Add(item);
            }

            return results;
        }

        /// <summary>
        ///     Moves to the desired result node.
        /// </summary>
        /// <param name="parents">The parents of the result node.</param>
        private void ParseNodes(IEnumerable<string> parents)
        {
            foreach (string s in parents)
                if (_nodes != null) _nodes = _nodes.SelectSingleNode(s);
        }
    }
}