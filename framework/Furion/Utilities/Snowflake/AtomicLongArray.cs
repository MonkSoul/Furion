using System.Collections.Generic;
using System.Threading;

namespace Furion.Utilities
{
    public class AtomicLongArray
    {
        private volatile long[] _value;

        public IReadOnlyCollection<long> Value => _value;

        public long this[int index] => _value[index];

        public AtomicLongArray(int capacity) => _value = new long[capacity];

        public bool CompareAndSet(int index, long currentValue, long newValue)
            => Interlocked.CompareExchange(ref _value[index], newValue, currentValue) == currentValue;
    }
}