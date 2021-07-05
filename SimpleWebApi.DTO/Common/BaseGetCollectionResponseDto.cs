using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace SimpleWebApi.DTO.Common
{
    public class BaseGetCollectionResponseDto<T> where T : class
    {
        /// <summary>
        /// List of objects
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        /// Count of objects
        /// </summary>
        public int Count => Result.Count();
        
        //some other api info properties

    }
}