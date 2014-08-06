using APINet.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APINet.Data
{
    internal interface IDataProcessor
    {
        /// <summary>
        /// Finds the relevant results in a set of data.
        /// </summary>
        /// <param name="parentNodes">The parent nodes of the result node.</param>
        /// <param name="resultName">Name of the result node.</param>
        /// <param name="resultNodes">The result node's child nodes.</param>
        /// <returns></returns>
        List<ApiResponseItem> FindRelevantResults(IEnumerable<string> parentNodes, string resultName, IEnumerable<string> resultNodes);

        /// <summary>
        /// Finds the relevant results in a set of data.
        /// </summary>
        /// <typeparam name="R">Enum containing names of the result node's children.</typeparam>
        /// <param name="parentNodes">The parent nodes of the result node.</param>
        /// <param name="resultName">Name of the result node.</param>
        /// <returns></returns>
        List<ApiResponseItem<R>> FindRelevantResults<R>(IEnumerable<string> parentNodes, string resultName)
            where R : struct;
    }
}
