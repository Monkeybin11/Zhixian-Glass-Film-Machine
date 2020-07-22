using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCData
{
    [Serializable]
    public struct NumberData<T> : IComparable<NumberData<T>>, IEquatable<NumberData<T>>
    {
        public int Num { get; set; }

        public T Value { get; set; }

        public NumberData(int num, T value)
        {
            this.Num = num;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("Num:{0},{1}", Num, Value);
        }

        public bool Equals(NumberData<T> other)
        {
            return this.Num == other.Num;
        }

        public int CompareTo(NumberData<T> other)
        {
            return this.Num - other.Num;
        }
    }



}
