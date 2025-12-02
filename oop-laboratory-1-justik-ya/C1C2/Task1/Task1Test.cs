using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

public class Tests
{
    private readonly TextReader _standartIn = Console.In;
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
        Console.SetIn( _standartIn );
        Console.SetOut( _standartOut );
        _stringWriter.Close();
    }

    [Test]
    public void TestSayHello()
    {
        SayHello( "Vitaly" );
        AssertOut( "Hello Vitaly!" );
    }

    private void AssertOut( String expected )
    {
        String result =
            _stringWriter.ToString()
                         .Replace( "\r\n", "\n" )
                         .Trim();

        That( result, Is.EqualTo( expected ) );
    }

    [Test]
    public void TestSayHelloNl()
    {
        SayHello( "Vitaly\nBragilevsky" );
        AssertOut( "Hello Vitaly\nBragilevsky!" );
    }

    [Test]
    public void TestHelloUser()
    {
        Console.SetIn( new StringReader( "C#" ) );
        HelloUser();
        AssertOut( "Hello C#!" );
    }

    [Test]
    public void TestSquare()
    {
        RunOnInputCheckLastLine( "5", "25", Square );
        RunOnInputCheckLastLine( "-3", "9", Square );
    }

    [Test]
    public void TestSum()
    {
        RunOnInputCheckLastLine( "2\n3", "5", Sum );
        RunOnInputCheckLastLine( "100\n-100", "0", Sum );
        RunOnInputCheckLastLine( "50\n50", "100", Sum );
    }

    private void RunOnInputCheckLastLine( String input, String expected, Action action )
    {
        Console.SetIn( new StringReader( input ) );
        
        action.Invoke();
        
        String result =
            _stringWriter.ToString()
                         .Replace( "\r\n", "\n" )
                         .Trim()
                         .Split( '\n', StringSplitOptions.RemoveEmptyEntries )
                         .Last();
        
        That( result, Is.EqualTo( expected ) );
    }
}
