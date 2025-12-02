using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Task3
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

        private void AssertResult( Int32 count )
        {
            StringBuilder expectedResult = new StringBuilder();
            for( Int32 i = 0; i < count; ++i )
                expectedResult.Append( "Thread 1\nThread 2\nThread 3\n" );
            String result = _stringWriter.ToString().Replace( "\r\n", "\n" );
            Assert.That( result, Is.EqualTo( expectedResult.ToString() ) );
        }

        private void Test( Int32[] threadStartOrder, Int32 msBeforeNextStart )
        {
            for( Int32 i = 0; i < 10; ++i )
            {
                ( new Task5() ).Start( threadStartOrder, msBeforeNextStart );
                AssertResult( i + 1 );
            }
        }

        // Note: Пока нет корректной реализации синхронизации данный тест может
        // как проходить так и падать, в зависимости от звезд на небе, от системы
        // под которой выполняется приложение, от количества ядер CPU, и т.д.
        // В отличае от остальных тестов, которые будут падать практически
        // гарантированно
        [Test]
        public void Test123()
        {
            Test( new[] { 0, 1, 2 }, 0 );
        }

        [Test]
        public void Test231()
        {
            Test( new[] { 1, 2, 0 }, 10 );
        }

        [Test]
        public void Test312()
        {
            Test( new[] { 2, 0, 1 }, 10 );
        }

        [Test]
        public void Test321()
        {
            Test( new[] { 2, 1, 0 }, 10 );
        }
    }
}
