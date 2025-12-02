using System;

namespace class1
{
    public interface IMatrix
    {
        Int32 Rows { get; }
        Int32 Columns { get; }
        
        Double this[Int32 r, Int32 c] { get; }
    }
}
