using System;
using NUnit.Framework;

namespace class1
{
    public class Tests
    {
        private const Double Epsilon = 1E-16D;
        private readonly Object _lock = new();

        [Test]
        public void ParseComplexTests()
        {
            lock( _lock )
            {
                using( TestReporter reporter = new TestReporter( "ParseComplexTests" ) )
                {
                    AssertCplx( SComplex.ParseCsv( "1,0" ), 1.0, 0.0 );
                    AssertCplx( SComplex.ParseCsv( "   \n   1.5,\t-4.99" ), 1.5, -4.99 );
                    Assert.That( () => SComplex.ParseCsv( String.Empty ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SComplex.ParseCsv( "," ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SComplex.ParseCsv( "1," ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SComplex.ParseCsv( "1,abc" ), Throws.InstanceOf<Exception>() );
                    reporter.SetPassed();
                }
            }
        }

        [Test]
        public void ParseMatrixTests()
        {
            lock( _lock )
            {
                using( TestReporter reporter = new TestReporter( "ParseMatrixTests" ) )
                {
                    AssertMtx( SMatrix.ParseCsv( "1,1,1" ), new[,] { { 1.0 } } );
                    AssertMtx( SMatrix.ParseCsv( "2,\t2,\n1.3,\t2,\n-3E+2,\t4" ), new[,] { { 1.3, 2.0 }, { -300.0, 4.0 } } );
                    Assert.That( () => SMatrix.ParseCsv( String.Empty ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SMatrix.ParseCsv( "," ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SMatrix.ParseCsv( "1,1" ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SMatrix.ParseCsv( "2,2,1,2,a,4" ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SMatrix.ParseCsv( "1,-1,1" ), Throws.InstanceOf<Exception>() );
                    Assert.That( () => SMatrix.ParseCsv( "0,0" ), Throws.InstanceOf<Exception>() );
                    reporter.SetPassed();
                }
            }
        }

        [Test]
        public void ComplexTests()
        {
            lock( _lock )
            {
                using( TestReporter reporter = new TestReporter( "ComplexTests" ) )
                {
                    IComplex x = SComplex.ParseCsv( "2,-1" );
                    IComplex y = SComplex.ParseCsv( "-1,1" );
                    AssertCplx( SComplex.Add( x, y ), 1, 0 );
                    AssertCplx( SComplex.Mul( x, y ), -1, 3 );
                    AssertCplx( SComplex.Mul( y, x ), -1, 3 );
                    reporter.SetPassed();
                }
            }
        }

        [Test]
        public void MatrixTests()
        {
            lock( _lock )
            {
                using( TestReporter reporter = new TestReporter( "MatrixTests" ) )
                {
                    IMatrix x = SMatrix.ParseCsv( "2,2,1,2,3,4" );
                    IMatrix y = SMatrix.ParseCsv( "2,2,-3,1,2,0" );
                    AssertMtx( SMatrix.Add( x, y ), new[,] { { -2.0, 3.0 }, { 5.0, 4.0 } } );
                    AssertMtx( SMatrix.Mul( x, y ), new[,] { { 1.0, 1.0 }, { -1.0, 3.0 } } );
                    AssertMtx( SMatrix.Mul( y, x ), new[,] { { 0.0, -2.0 }, { 2.0, 4.0 } } );
                    reporter.SetPassed();
                }
            }
        }

        [Test]
        public void AdaptersTest()
        {
            lock( _lock )
            {
                using( TestReporter reporter = new TestReporter( "AdaptersTest" ) )
                {
                    IComplex x = SComplex.ParseCsv( "3,-1.5" );
                    IComplex y = SComplex.ParseCsv( "0,0.87" );
                    IComplex z = SComplex.Mul( x, y );
                    IMatrix mx = new ComplexToMatrixAdapter( x ) as IMatrix;
                    Assert.That( mx is not null );
                    IMatrix my = new ComplexToMatrixAdapter( y ) as IMatrix;
                    Assert.That( my is not null );
                    IMatrix mzl = SMatrix.Mul( mx, my );
                    IMatrix mzr = SMatrix.Mul( my, mx );
                    IComplex zl = new MatrixToComplexAdapter( mzl ) as IComplex;
                    Assert.That( zl is not null );
                    IComplex zr = new MatrixToComplexAdapter( mzr ) as IComplex;
                    Assert.That( zr is not null );
                    AssertCplx( zl, z.Real, z.Imag );
                    AssertCplx( zr, z.Real, z.Imag );
                    reporter.SetPassed();
                }
            }
        }

        private static void AssertCplx( IComplex value, Double real, Double imag )
        {
            Assert.That( value.Real - real, Is.LessThanOrEqualTo( Epsilon ) );
            Assert.That( value.Imag - imag, Is.LessThanOrEqualTo( Epsilon ) );
        }

        private static void AssertMtx( IMatrix value, Double[,] matrix )
        {
            Assert.That( value.Rows, Is.EqualTo( matrix.GetLength( 0 ) ) );
            Assert.That( value.Columns, Is.EqualTo( matrix.GetLength( 1 ) ) );

            for( Int32 r = 0; r < value.Rows; ++r )
            {
                for( Int32 c = 0; c < value.Columns; ++c )
                    Assert.That( value[r, c] - matrix[r, c] <= Epsilon );
            }
        }

        private class TestReporter : IDisposable
        {
            private readonly String _name;
            private readonly DateTime _startTime;
            private Boolean _passed;

            public TestReporter( String name )
            {
                _name = name;
                _startTime = DateTime.Now;
                _passed = false;
            }

            public void SetPassed()
            {
                _passed = true;
            }

            public void Dispose()
            {
                TimeSpan delta = DateTime.Now - _startTime;
                String duration = delta.ToString( "c" );
                String status = _passed ? "PASSED" : "FAILED";
                Console.WriteLine( $"<--TEST-->|{duration}|{status}|{_name}" );
            }
        }
    }
}
