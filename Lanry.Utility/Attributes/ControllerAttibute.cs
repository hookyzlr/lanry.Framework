using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
    public class ControllerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }
}
