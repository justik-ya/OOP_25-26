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
    public void RunTestTest()
    {
        RunTest();
        AssertOut( "Все сообщения:\n" +
                   "Ты меня не видишь\n" +
                   "Гав!\n" +
                   "Привет, я человек!\n" +
                   "Гав!\n" +
                   "\n" +
                   "Сообщения только от собак:\n" +
                   "Гав!\n" +
                   "Гав!" );
    }

    [Test]
    public void PrintMessageFromWorkForDogTest()
    {
        var dog = new Dog();
        PrintMessageFrom( dog );
        AssertOut( dog.Bark() );
    }

    [Test]
    public void PrintMessageFromWorkForHumanTest()
    {
        var human = new Human();
        PrintMessageFrom( human );
        AssertOut( human.Greeting() );
    }

    [Test]
    public void PrintMessageFromWorkForAlienTest()
    {
        var alien = new Alien();
        PrintMessageFrom( alien );
        AssertOut( alien.Command() );
    }

    private void AssertOut( String expected )
    {
        String result = _stringWriter.ToString()
                                     .Replace( "\r\n", "\n" )
                                     .TrimEnd( '\r', '\n' );

        That( result, Is.EqualTo( expected ) );
    }
}
