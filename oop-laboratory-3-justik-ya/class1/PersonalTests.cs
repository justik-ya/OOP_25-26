using NUnit.Framework;

namespace class1
{
    public class PersonalTests
    {
        [Test]
        public void ComplexToMatrixAdapter_BasicCheck()
        {
            IComplex z = new Complex(3, 4);
            IMatrix m = new ComplexToMatrixAdapter(z);

            Assert.That(m[0, 0], Is.EqualTo(3));
            Assert.That(m[0, 1], Is.EqualTo(-4));
            Assert.That(m[1, 0], Is.EqualTo(4));
            Assert.That(m[1, 1], Is.EqualTo(3));
        }

        [Test]
        public void MatrixToComplexAdapter_BasicCheck()
        {
            double[,] data = new double[,]
            {
                { 5, -7 },
                { 7,  5 }
            };

            IMatrix m = new Matrix(data);
            IComplex z = new MatrixToComplexAdapter(m);

            Assert.That(z.Real, Is.EqualTo(5));
            Assert.That(z.Imag, Is.EqualTo(7));
        }

        [Test]
        public void ParseCsv_MatrixBasic()
        {
            string text = "1,2,3\n4,5,6";
            IMatrix m = SMatrix.ParseCsv(text);

            Assert.That(m.Rows, Is.EqualTo(2));
            Assert.That(m.Columns, Is.EqualTo(3));

            Assert.That(m[0, 0], Is.EqualTo(1));
            Assert.That(m[0, 1], Is.EqualTo(2));
            Assert.That(m[0, 2], Is.EqualTo(3));

            Assert.That(m[1, 0], Is.EqualTo(4));
            Assert.That(m[1, 1], Is.EqualTo(5));
            Assert.That(m[1, 2], Is.EqualTo(6));
        }

        [Test]
        public void MatrixMul_BasicCheck()
        {
            double[,] a = { {1,2}, {3,4} };
            double[,] b = { {5,6}, {7,8} };

            IMatrix A = new Matrix(a);
            IMatrix B = new Matrix(b);

            IMatrix C = SMatrix.Mul(A, B);

            Assert.That(C[0, 0], Is.EqualTo(19));
            Assert.That(C[0, 1], Is.EqualTo(22));
            Assert.That(C[1, 0], Is.EqualTo(43));
            Assert.That(C[1, 1], Is.EqualTo(50));
        }

    }
}
