using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class DBMappingConfig : ElementInfomation
    {
        /// <summary>
        /// Name
        /// </summary>
        [ElementProperty("Name")]
        public string Name { set; get; }

        /// <summary>
        /// AssemblyName
        /// </summary>
        [ElementProperty("AssemblyName")]
        public string AssemblyName { set; get; }

        /// <summary>
        /// TypeName
        /// </summary>
        [ElementProperty("TypeName")]
        public string TypeName { set; get; }
    }
}
