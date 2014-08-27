using System;
using System.Collections.Generic;

namespace APINet.Api
{
    /// <summary>
    /// An response item from the data returned.
    /// </summary>
    public class ApiResponseItem
    {
        private readonly Dictionary<string, string> _subItems = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponseItem"/> class.
        /// </summary>
        /// <param name="responseItemNames">The names of the response items.</param>
        public ApiResponseItem(IEnumerable<string> responseItemNames)
        {
            foreach (var responseItem in responseItemNames)
            {
                _subItems.Add(responseItem, string.Empty);
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
            return _subItems.ContainsKey(id) ? _subItems[id] : string.Empty;
        }
    }

    /// <summary>
    /// Type safe Response item.
    /// </summary>
    /// <typeparam name="TR">An enum containing response items.</typeparam>
    public class ApiResponseItem<TR>
        where TR : struct
    {
        private readonly Dictionary<TR, string> _subItems = new Dictionary<TR, string>();

        public ApiResponseItem()
        {
            var items = (TR[])Enum.GetValues(typeof(TR));

            foreach (var item in items)
            {
                _subItems.Add(item, string.Empty);
            }
        }

        public void SetSubItem(TR id, string value)
        {
            _subItems[id] = value;
        }

        public string GetSubItem(TR id)
        {
            return _subItems[id];
        }
    }
}
