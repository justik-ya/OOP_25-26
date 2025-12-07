using System.Reflection;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task2.Task2;

namespace Task2;

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
        AssertOut( "Кошка мяучит 'мяу-мяу'\n" +
                   "Собака гавкает 'гав-гав-гав'\n" +
                   "Гусь гогочет 'га-га-га'" );
    }

    [Test]
    public void AnimalIsAbstractTest()
    {
        var assembly = Assembly.LoadFrom( "Task2.dll" );
        var type = assembly.GetType( "Task2.Animal" );
        That( type, Is.Not.Null );
        That( type!.IsAbstract, Is.True );
    }

    [Test]
    public void NoOverridingMethodsTest()
    {
        Multiple( () =>
        {
            AssertNoFunctionsDeclared( "Cat" );
            AssertNoFunctionsDeclared( "Dog" );
            AssertNoFunctionsDeclared( "Goose" );
        } );
    }

    private void AssertOut( String expected )
    {
        String result = _stringWriter.ToString()
                                     .Replace( "\r\n", "\n" )
                                     .TrimEnd( '\r', '\n' );

        That( result, Is.EqualTo( expected ) );
    }

    private void AssertNoFunctionsDeclared( String className )
    {
        var assembly = Assembly.LoadFrom( "Task2.dll" );
        var type = assembly.GetType( $"Task2.{className}" );
        That( type, Is.Not.Null );
        That( type!
              .GetMethods( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance )
              .Select( m => m.Name )
              .Count( s => !s.StartsWith( "get_" ) ),
              Is.EqualTo( 0 ) );
    }
}
