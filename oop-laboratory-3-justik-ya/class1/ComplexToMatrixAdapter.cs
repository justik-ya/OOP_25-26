using System;

namespace class1
{
    public class ComplexToMatrixAdapter : IMatrix
    {
        private readonly IComplex src;

        public int Rows => 2;
        public int Columns => 2;

        public double this[int r, int c]
        {
            get
            {
                if (r == 0 && c == 0) return src.Real;
                if (r == 0 && c == 1) return -src.Imag;
                if (r == 1 && c == 0) return src.Imag;
                if (r == 1 && c == 1) return src.Real;

                throw new Exception("Invalid index");
            }
        }

        public ComplexToMatrixAdapter(IComplex src)
        {
            this.src = src;
        }

    }
}
