using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum HttpVerbs
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        Get = 1 << 0,
        /// <summary>
        /// 
        /// </summary>
        Post = 1 << 1,
        /// <summary>
        /// 
        /// </summary>
        GetPost = Get | Post,
        /// <summary>
        /// 
        /// </summary>
        Put = 1 << 2,
        /// <summary>
        /// 
        /// </summary>
        Delete = 1 << 3,
        /// <summary>
        /// 
        /// </summary>
        Head = 1 << 4
    }
}
