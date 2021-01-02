using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Order
    {
        public string Header { get; init; } = "";
        public string Value { get; init; } = "";

        public override string ToString() => Header;
    }
}