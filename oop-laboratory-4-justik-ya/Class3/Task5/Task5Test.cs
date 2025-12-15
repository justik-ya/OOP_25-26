using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task5.Task5;

namespace Task5;

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
    public void Part1Test1()
    {
        Part1( new Logger() );
        AssertOut( "First message\n" +
                   "Second message\n" +
                   "Third message" );
    }

    [Test]
    public void Part1Test2()
    {
        Part1( new LoggerDecorator( new Logger() ) );
        AssertOut( "Message 1: First message\n" +
                   "Message 2: Second message\n" +
                   "Message 3: Third message" );
    }

    private void AssertOut( String expected )
    {
        String result = _stringWriter.ToString()
                                     .Replace( "\r\n", "\n" )
                                     .TrimEnd( '\r', '\n' );

        That( result, Is.EqualTo( expected ) );
    }
}
