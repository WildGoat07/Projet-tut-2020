using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ToStringOverrider<T>
    {
        private Func<string> fct;

        public ToStringOverrider(T val)
        {
            Value = val;
            fct = () => Value == null ? "null" : Value.ToString() ?? "null";
        }

        public ToStringOverrider(T val, string toString)
        {
            Value = val;
            fct = () => toString;
        }

        public ToStringOverrider(T val, Func<string> toString)
        {
            Value = val;
            fct = toString;
        }

        public T Value { get; }

        public override bool Equals(object? obj) => Equals(Value, obj is ToStringOverrider<T> overrider ? overrider.Value : null);

        public override int GetHashCode() => Value == null ? 0 : Value.GetHashCode();

        public override string ToString() => fct();
    }
}