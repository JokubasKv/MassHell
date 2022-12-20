using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.Iterator
{
    public interface IIterator<T>
    {
        T Next();
        T Current { get; }
        bool IsLeft();
    }

    public class Iterator<T> : IIterator<T>
    {
        private readonly IAggregate<T> _aggregate;
        int index = 0;

        public Iterator(IAggregate<T> aggregate)
        {
            this._aggregate = aggregate;
        }

        public T Current => _aggregate[index];

        public bool IsLeft() => index < _aggregate.Count;

        public T Next()
        {
            index++;
            return IsLeft() ? _aggregate[index] : default;
        }
    }
}
