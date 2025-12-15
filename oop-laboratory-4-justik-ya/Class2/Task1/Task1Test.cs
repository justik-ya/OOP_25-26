using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

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
    public void RunTestTest()
    {
        RunTest();
        AssertOut( "age: 21\n" +
                   "age: 21\n" +
                   "age: 22\n" +
                   "age: 23\n" +
                   "age: 24\n" +
                   "System.ArgumentException: Property name is immutable\n" +
                   "System.FormatException: For input string: \"?\"\n" +
                   "System.ArgumentException: Incorrect JSON property format: 'age = 10'\n" +
                   "JSON value of 'age' has been set 4 time(s)\n" +
                   "count: 0\n" +
                   "JSON value of 'count' has been set 1 time(s)\n" +
                   "Class 'JsonIntProperty' instance has been created 2 time(s)" );
    }

    private void AssertOut( String expected )
    {
        String result = _stringWriter.ToString()
                                     .Replace( "\r\n", "\n" )
                                     .TrimEnd( '\r', '\n' );

        That( result, Is.EqualTo( expected ) );
    }
}
