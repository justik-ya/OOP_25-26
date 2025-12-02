using System;

namespace class1
{
    public class Complex : IComplex
    {
        private readonly double _real;
        private readonly double _imag;

        public double Real => _real;
        public double Imag => _imag;

        public Complex(double realValue, double imagValue)
        {
            _real = realValue;
            _imag = imagValue;
        }
    }
}
