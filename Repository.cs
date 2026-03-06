using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Repository<T> where T:ICloneable,IComparable<T>
    {
        private readonly List<T> _list;
        public Repository()
        {
            _list = new();
        }

        public void Add(T item)
        {
            if (item == null) return;
            _list.Add(item);
        }

        public IEnumerable<T> GetAll() => _list;

        public void Sort()
        {
            _list.Sort();
        }

        public object Clone()
        {
            var copy = new Repository<T>();
            foreach (var item in _list)
            {
                copy.Add((T)item.Clone());
            }

            return copy;
        }
    }
}
