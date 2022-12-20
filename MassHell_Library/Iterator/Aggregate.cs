using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.Iterator
{
    public interface IAggregate<T>
    {
        T this[int index] { get; set; }

        int Count { get; }
        IIterator<T> Iterator { get; }
    }

    public class Aggregate<T> : IAggregate<T>
    {
        private IIterator<T> _iterator;
        private List<T> _list = new List<T>(); //holds objects


        public T this[int index]
        {
            get { return _list[index]; }
            set { _list.Add(value); }
        }

        /// <summary>
        /// Responsible for iterating thought objects
        /// </summary>
        public IIterator<T> Iterator
        {
            get
            {
                if (this._iterator == null)
                {
                    _iterator = new Iterator<T>(this);
                }
                return _iterator;
            }
        }

        public int Count => _list.Count;
    }
}
