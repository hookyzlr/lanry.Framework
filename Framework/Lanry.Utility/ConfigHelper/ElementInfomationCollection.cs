using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Lanry.Utility
{
    /// <summary>
    /// ConfigToObjectCollection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElementInfomationCollection<T> : IEnumerable
    {
        private List<T> _collection;
        private int _count;

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementInfomationCollection()
        {
            _collection = new List<T>();
        }

        /// <summary>
        /// Count
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }

        /// <summary>
        /// Add One Element
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            _collection.Add(t);
            _count++;
        }

        /// <summary>
        /// Remove one element
        /// </summary>
        /// <param name="t"></param>
        public void Remove(T t)
        {
            _collection.Remove(t);
        }

        /// <summary>
        /// Remove all element
        /// </summary>
        /// <param name="predicate"></param>
        public void RemoveAll(Predicate<T> predicate)
        {
            _collection.RemoveAll(predicate);
        }

        /// <summary>
        /// Get one entity by name and vlaue
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public T GetByNameAndValue(string name, string value)
        {
            return _collection.Find(t => { return t.GetType().GetProperty(name).GetValue(t, null).ToString() == value; });
        }

        /// <summary>
        /// 默认获取第一个
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            return _collection[0];
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return _collection[index]; }
        }

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
