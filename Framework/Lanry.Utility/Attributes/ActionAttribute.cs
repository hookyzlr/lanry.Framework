using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class ActionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ActionAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public ActionAttribute(string path)
        {
            this.Path = path;
        }
    }
}
