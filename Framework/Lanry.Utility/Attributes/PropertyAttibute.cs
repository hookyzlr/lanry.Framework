using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanry.Utility
{
    /// <summary>
    /// 类字段属性
    /// </summary>
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field, Inherited = true,AllowMultiple = true)]
    public class PropertyAttibute : Attribute
    {
        private string _name;
        private bool _isPrimaryKey;
        private bool _isIdentity;
        private string _description;
        private bool _noMapping;

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPrimaryKey
        {
            set { _isPrimaryKey = value; }
            get { return _isPrimaryKey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsIdentity
        {
            set { _isIdentity = value; }
            get { return _isIdentity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool NoMapping
        {
            set { _noMapping = value; }
            get { return _noMapping; }
        }
        /// <summary>
        /// 
        /// </summary>
        public PropertyAttibute()
        {
            _description = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public PropertyAttibute(string fieldName)
        {
            _name = fieldName;
            _isIdentity = false;
            _isPrimaryKey = false;
            _description = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="desc"></param>
        public PropertyAttibute(string fieldName,string desc)
        {
            _name = fieldName;
            _isIdentity = false;
            _isPrimaryKey = false;
            _description = desc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="pKey"></param>
        public PropertyAttibute(string fieldName,bool pKey)
        {
            _name = fieldName;
            _isIdentity = false;
            _isPrimaryKey = pKey;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="pKey"></param>
        /// <param name="iden"></param>
        public PropertyAttibute(string fieldName, bool pKey, bool iden)
        {
            _name = fieldName;
            _isIdentity = iden;
            _isPrimaryKey = pKey;
        }
    }
}
