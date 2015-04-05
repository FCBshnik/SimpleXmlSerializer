using System;
using System.Reflection;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    public class CompositeWithAllPrimitiveTypes
    {
        public char Char { get; set; }

        public string String { get; set; }

        public byte Byte { get; set; }
        
        public short Short { get; set; }
        
        public int Int { get; set; }
        
        public long Long { get; set; }
        
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