using System;

namespace class1
{
    public class Matrix : IMatrix
    {
        private double[,] data;

        public int Rows => data.GetLength(0);
        public int Columns => data.GetLength(1);

        public double this[int r, int c]
        {
            get { return data[r, c]; }
            set { data[r, c] = value; }
        }

        public Matrix(int rows, int columns)
        {
            data = new double[rows, columns];
        }

        public Matrix(double[,] source)
        {
            data = source;
        }
    }
}
