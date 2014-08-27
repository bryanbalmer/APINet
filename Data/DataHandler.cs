using System;
using System.Collections.Generic;
using APINet.Api;

namespace APINet.Data
{
    public class DataHandler
    {
        private IDataProcessor _processor;

        /// <summary>
        ///     Parses the data to determine if it is
        ///     JSON or XML and registers the appropriate
        ///     <see cref="IDataProcessor" />
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentException">Data for processing not valid or not supported.</exception>
        public void HandleData(string data)
        {
            data = data.Trim();
            if ((data.StartsWith("{") && data.EndsWith("}")) ||
                data.StartsWith("[") && data.EndsWith("]"))
                _processor = new JsonProcessor(data);
            else if (data.StartsWith("<") && data.EndsWith(">"))
                _processor = new XmlProcessor(data);
            else
                throw new ArgumentException("Data for processing not valid or not supported.");
        }

        /// <summary>
        ///     Gets the results from data processor.
        /// </summary>
        /// <param name="parentNodes">The parent nodes of the result node.</param>
        /// <param name="resultName">Name of the result node.</param>
        /// <param name="resultNodes">The child names of the result node.</param>
        /// <returns></returns>
        public List<ApiResponseItem> GetResults(ICollection<string> parentNodes, string resultName,
            ICollection<string> resultNodes)
        {
            return _processor.FindRelevantResults(parentNodes, resultName, resultNodes);
        }

        /// <summary>
        ///     Gets the results.
        /// </summary>
        /// <typeparam name="TR">Enum of the child names of the result node.</typeparam>
        /// <param name="parentNodes">The parent nodes of the result node.</param>
        /// <param name="resultName">Name of the result node.</param>
        /// <returns></returns>
        public List<ApiResponseItem<TR>> GetResults<TR>(ICollection<string> parentNodes, string resultName)
            where TR : struct
        {
            return _processor.FindRelevantResults<TR>(parentNodes, resultName);
        }
    }
}