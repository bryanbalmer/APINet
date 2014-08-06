using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINet.Data
{
    public interface IDataRequest
    {
        /// <summary>
        /// Gets the data from the data source specified.
        /// </summary>
        /// <param name="dataSource">The data source connection string.</param>
        /// <returns></returns>
        Task<string> GetData(string dataSource);
    }
}
