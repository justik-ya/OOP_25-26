using System;

namespace class1
{
    public class MatrixToComplexAdapter : IComplex
    {
        private readonly double a;
        private readonly double b;

        public MatrixToComplexAdapter(IMatrix value)
        {
            if (value.Rows != 2 || value.Columns != 2)
                throw new Exception("Invalid matrix size");

            double a1 = value[0, 0];
            double minusB = value[0, 1];
            double b1 = value[1, 0];
            double a2 = value[1, 1];

            if (a1 != a2 || -b1 != minusB)
                throw new Exception("Invalid matrix");

            a = a1;
            b = b1;
        }
        
        public double Real => a;
        public double Imag => b;
    }
}
