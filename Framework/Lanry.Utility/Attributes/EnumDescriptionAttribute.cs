using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Lanry.Utility
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] Descriptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptions"></param>
        public EnumDescriptionAttribute(params string[] descriptions)
        {
            this.Descriptions = descriptions;
        }
    }
}
