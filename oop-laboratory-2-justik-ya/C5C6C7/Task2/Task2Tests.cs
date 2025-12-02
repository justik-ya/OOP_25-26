using System;
using System.IO;
using NUnit.Framework;

namespace Task2
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

        private void AssertResult( String expectedStdout )
        {
            String result = _stringWriter.ToString().Replace( "\r\n", "\n" );
            Assert.That( result, Is.EqualTo( expectedStdout ) );
        }

        [Test]
        public void TestOneResource()
        {
            using( ResourcePool pool = new ResourcePool() )
            using( IManagedResource r1 = pool.Rent() )
            {
                Console.WriteLine( r1.ResourceId );
            }

            String expectedStdout = "Resource 1 created\n" +
                                    "Resource 1 acquired\n" +
                                    "1\n" +
                                    "Resource 1 released\n" +
                                    "Resource 1 disposed\n";

            AssertResult( expectedStdout );
        }

        [Test]
        public void TestOneLeakedResource()
        {
            using( ResourcePool pool = new ResourcePool() )
            {
                IManagedResource r1 = pool.Rent();
                Console.WriteLine( r1.ResourceId );
            }

            String expectedStdout = "Resource 1 created\n" +
                                    "Resource 1 acquired\n" +
                                    "1\n" +
                                    "Resource 1 disposed\n";

            AssertResult( expectedStdout );
        }

        [Test]
        public void TestTwoResources()
        {
            using( ResourcePool pool = new ResourcePool() )
            using( IManagedResource r1 = pool.Rent() )
            using( IManagedResource r2 = pool.Rent() )
            {
                Console.WriteLine( r1.ResourceId );
                Console.WriteLine( r2.ResourceId );
            }

            String expectedStdout = "Resource 1 created\n" +
                                    "Resource 1 acquired\n" +
                                    "Resource 2 created\n" +
                                    "Resource 2 acquired\n" +
                                    "1\n" +
                                    "2\n" +
                                    "Resource 2 released\n" +
                                    "Resource 1 released\n" +
                                    "Resource 2 disposed\n" +
                                    "Resource 1 disposed\n";

            AssertResult( expectedStdout );
        }

        [Test]
        public void TestRerentResource()
        {
            using( ResourcePool pool = new ResourcePool() )
            {
                using( IManagedResource r1 = pool.Rent() )
                    Console.WriteLine( r1.ResourceId );
                using( IManagedResource r1 = pool.Rent() )
                    Console.WriteLine( r1.ResourceId );
            }

            String expectedStdout = "Resource 1 created\n" +
                                    "Resource 1 acquired\n" +
                                    "1\n" +
                                    "Resource 1 released\n" +
                                    "Resource 1 acquired\n" +
                                    "1\n" +
                                    "Resource 1 released\n" +
                                    "Resource 1 disposed\n";

            AssertResult( expectedStdout );
        }

        [Test]
        public void Test123()
        {
            using( ResourcePool pool = new ResourcePool() )
            {
                IManagedResource r1 = pool.Rent();
                IManagedResource r2 = pool.Rent();
                IManagedResource r3 = pool.Rent();
                r1.Dispose();
                r2.Dispose();
                r3.Dispose();
            }

            String expectedStdout = "Resource 1 created\n" +
                                    "Resource 1 acquired\n" +
                                    "Resource 2 created\n" +
                                    "Resource 2 acquired\n" +
                                    "Resource 3 created\n" +
                                    "Resource 3 acquired\n" +
                                    "Resource 1 released\n" +
                                    "Resource 2 released\n" +
                                    "Resource 3 released\n" +
                                    "Resource 1 disposed\n" +
                                    "Resource 2 disposed\n" +
                                    "Resource 3 disposed\n";

            AssertResult( expectedStdout );
        }
    }
}
