using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task3.Task3;

namespace Task3;

public class Tests
{
    private readonly TextWriter _standartOut = Console.Out;
    private StringWriter _stringWriter = new();

    [SetUp]
    public void Setup()
    {
        var stringWriter = new StringWriter();
        _stringWriter = stringWriter;
        Console.SetOut( _stringWriter );
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetOut( _standartOut );
        _stringWriter.Close();
    }

    [Test]
    public void Part1Test()
    {
        Part1();
        AssertOut( "=== Часть 1: класс Box ===\n" +
                   "Box b: 6\n" +
                   "ERROR: Box is not empty\n" +
                   "ERROR: Box is empty\n" +
                   "ERROR: Box is empty\n" +
                   "Box b: 0\n" +
                   "Box b2: hello\n" +
                   "String's length is 5\n" +
                   "String's length is 3" );
    }

    [Test]
    public void Part2Test()
    {
        Part2();
        AssertOut( "=== Часть 2: функция Convert ===\n" +
                   "Box stringBox: 42!\n" +
                   "ERROR: Box is empty\n" +
                   "Box intBox2: 3\n" +
                   "Box intBox3: 4" );
    }

    [Test]
    public void Part3Test()
    {
        Part3();
        AssertOut( "=== Часть 3: класс BoxList ===\n" +
                   "[[1], [2], [3], [4], [5], [6], [7], [8]]\n" +
                   "[[a], [b], [c], [d], [e], [f], [g], [h]]" );
    }

    private void AssertOut( String expected )
    {
        String result = _stringWriter.ToString()
                                     .Replace( "\r\n", "\n" )
                                     .TrimEnd( '\r', '\n' );

        That( result, Is.EqualTo( expected ) );
    }
}
