using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINet.Api
{
    /// <summary>
    /// An response item from the data returned.
    /// </summary>
    public class ApiResponseItem
    {
        private Dictionary<string, string> _subItems = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponseItem"/> class.
        /// </summary>
        /// <param name="responseItemNames">The names of the response items.</param>
        public ApiResponseItem(IEnumerable<string> responseItemNames)
        {
            foreach (string s in responseItemNames)
            {
                _subItems.Add(s, string.Empty);
            }
        }

        /// <summary>
        /// Sets the value of the specific item.
        /// </summary>
        /// <param name="id">The item name</param>
        /// <param name="value">The item value.</param>
        public void SetSubItem(string id, string value)
        {
            if (_subItems.ContainsKey(id))
                _subItems[id] = value;
        }

        /// <summary>
        /// Gets a specific sub item.
        /// </summary>
        /// <param name="id">The sub item name.</param>
        /// <returns></returns>
        public string GetSubItem(string id)
        {
            if (_subItems.ContainsKey(id))
                return _subItems[id];
            else
                return string.Empty;
        }
    }

    /// <summary>
    /// Type safe Response item.
    /// </summary>
    /// <typeparam name="R">An enum containing response items.</typeparam>
    public class ApiResponseItem<R>
        where R : struct
    {
        private Dictionary<R, string> _subItems = new Dictionary<R, string>();

        public ApiResponseItem()
        {
            R[] items = (R[])Enum.GetValues(typeof(R));

            foreach (var item in items)
            {
                _subItems.Add(item, string.Empty);
            }
        }

        public void SetSubItem(R id, string value)
        {
            _subItems[id] = value;
        }

        public string GetSubItem(R id)
        {
            return _subItems[id];
        }
    }
}
