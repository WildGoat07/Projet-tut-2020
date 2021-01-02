using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.filters
{
    public interface IFilter
    {
        string Name { get; }
    }

    public record Values : IFilter
    {
        public string Name { get; init; } = "";
        public string[] Filters { get; init; } = Array.Empty<string>();
    }
    public record ListValues : IFilter
    {
        public string Name { get; init; } = "";
        public string[] Values { get; init; } = Array.Empty<string>();
        public int[] Filters { get; init; } = Array.Empty<int>();
    }

    public record IntRange : IFilter
    {
        public string Name { get; init; } = "";
        public int? Min { get; init; } = null;
        public int? Max { get; init; } = null;
    }

    public record FloatRange : IFilter
    {
        public string Name { get; init; } = "";
        public float? Min { get; init; } = null;
        public float? Max { get; init; } = null;
    }
}