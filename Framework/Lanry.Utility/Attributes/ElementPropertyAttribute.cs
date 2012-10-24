using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 配置文件属性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,Inherited=true, AllowMultiple=true)]
    public class ElementPropertyAttribute : Attribute
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimary { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementPropertyAttribute()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public ElementPropertyAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isPrimary"></param>
        public ElementPropertyAttribute(string name, bool isPrimary)
        {
            Name = name;
            IsPrimary = isPrimary;
        }
    }
}
