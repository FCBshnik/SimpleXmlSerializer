using System;
using System.Reflection;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    public class CompositeWithAllPrimitives
    {
        public char Char { get; set; }

        public string String { get; set; }

        public byte Byte { get; set; }

        public sbyte Sbyte { get; set; }
        
        public short Short { get; set; }

        public ushort Ushort { get; set; }
        
        public int Int { get; set; }

        public int? NullableInt { get; set; }

        public uint Uint { get; set; }
        
        public long Long { get; set; }

        public ulong Ulong { get; set; }
        
        public float Float { get; set; }
        
        public double Double { get; set; }

        public decimal Decimal { get; set; }
        
        public bool Bool { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public DateTime DateTime { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }

        public BindingFlags Enum { get; set; }

        public Uri Uri { get; set; }
        
        public Guid Guid { get; set; }

        public Type Type { get; set; }
    }
}