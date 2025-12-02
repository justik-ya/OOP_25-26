using System;
using System.IO;
using NUnit.Framework;

namespace Task1
{
    public class Tests
    {
        private readonly TextWriter _stdout = Console.Out;
        private StringWriter _stringWriter = new();

        [SetUp]
        public void Setup()
        {
            _stringWriter = new StringWriter();
            Console.SetOut( _stringWriter );
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut( _stdout );
            _stringWriter.Close();
        }

        private void TestCase( Char headChar, Int32 headCharCount,
                               String middle,
                               Char tailChar, Int32 tailCharCount,
                               String expected )
        {
            var collection = new CrazyCollection( headChar, headCharCount, middle, tailChar, tailCharCount );

            foreach( Char c in collection )
                Console.Write( c );

            Assert.That( _stringWriter.ToString(), Is.EqualTo( expected ) );
        }

        private void TestWithException( Int32 headCharCount, Int32 tailCharCount )
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var collection = new CrazyCollection( '1', headCharCount, "2", '3', tailCharCount );
            }
            catch( Exception e )
            {
                _stdout.WriteLine( e.Message );
                return;
            }

            Assert.Fail( "Must be exception!" );
        }

        [Test]
        public void TestEmptyAll()
        {
            TestCase( '\0', 0, String.Empty, '\0', 0, String.Empty );
            TestCase( '\0', 0, null, '\0', 0, String.Empty );
        }

        [Test]
        public void TestEmptyHead()
        {
            TestCase( '\0', 0, "abc", '*', 5, "abc*****" );
        }

        [Test]
        public void TestEmptyMiddle()
        {
            TestCase( '<', 1, String.Empty, '*', 3, "<***" );
        }

        [Test]
        public void TestEmptyTail()
        {
            TestCase( '=', 4, "1234", 'k', 0, "====1234" );
        }

        [Test]
        public void TestTag1()
        {
            TestCase( '<', 1, "html", '>', 1, "<html>" );
        }

        [Test]
        public void TestExceptionNegativeHeadCount()
        {
            TestWithException( -3, 2 );
        }

        [Test]
        public void TestExceptionNegativeTailCount()
        {
            TestWithException( 3, -1 );
        }
    }
}
