using System.Collections.Generic;
using APINet.Api;

namespace APINet.Data
{
    internal interface IDataProcessor
    {
        /// <summary>
        ///     Finds the relevant results in a set of data.
        /// </summary>
        /// <param name="parentNodes">The parent nodes of the result node.</param>
        /// <param name="resultName">Name of the result node.</param>
        /// <param name="resultNodes">The result node's child nodes.</param>
        /// <returns></returns>
        List<ApiResponseItem> FindRelevantResults(ICollection<string> parentNodes, string resultName,
            ICollection<string> resultNodes);

        /// <summary>
        ///     Finds the relevant results in a set of data.
        /// </summary>
        /// <typeparam name="TR">Enum containing names of the result node's children.</typeparam>
        /// <param name="parentNodes">The parent nodes of the result node.</param>
        /// <param name="resultName">Name of the result node.</param>
        /// <returns></returns>
        List<ApiResponseItem<TR>> FindRelevantResults<TR>(ICollection<string> parentNodes, string resultName)
            where TR : struct;
    }
}