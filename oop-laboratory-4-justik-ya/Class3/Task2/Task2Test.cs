using System.Collections.Immutable;
using System.Reflection;
using NUnit.Framework;
using static NUnit.Framework.Assert;

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
    public void CheckTalkableTest()
    {
        var assembly = Assembly.LoadFrom( "Task2.dll" );
        var type = assembly.GetType( "Task2.Talkable" );
        That( type, Is.Not.Null, "There should be Talkable interface" );
        That( type!.IsAbstract, Is.True );
        That( type.GetMembers( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance )
                  .Select( info => info.Name ).Where( name => !name.StartsWith( "get_" ) ).ToImmutableSortedSet(),
              Is.EquivalentTo( new SortedSet<String> { "Sound", "Talk" } ) );
    }

    [Test]
    public void CheckAbstractAnimalTest()
    {
        Multiple( () =>
        {
            var assembly = Assembly.LoadFrom( "Task2.dll" );
            var abstractAnimal = assembly.GetType( "Task2.AbstractAnimal" );
            That( abstractAnimal, Is.Not.Null );

            if( abstractAnimal == null )
            {
                return;
            }

            That( abstractAnimal.IsAbstract, Is.True );

            That( abstractAnimal.GetInterfaces(), Does.Contain( assembly.GetType( "Task2.Talkable" ) ),
                  "AbstractAnimal must implement Talkable" );
            var soundProperty = abstractAnimal.GetProperty( "Sound" );

            if( soundProperty == null || soundProperty.GetMethod == null )
            {
                return;
            }

            That( soundProperty.GetMethod.IsAbstract,
                  Is.True,
                  "The `Sound` property shouldn't be overridden in AbstractAnimal:" );
        } );
    }

    [Test]
    public void CheckAnimalsTest()
    {
        Multiple( () =>
        {
            var assembly = Assembly.LoadFrom( "Task2.dll" );

            foreach( var animalName in new List<String> { "Cat", "Dog", "Goose" } )
            {
                var animal = assembly.GetType( $"Task2.{animalName}" );
                That( animal, Is.Not.Null );

                if( animal == null )
                {
                    return;
                }

                That( animal.IsAbstract, Is.False );

                That( animal.IsSubclassOf( assembly.GetType( "Task2.AbstractAnimal" ) ), Is.True,
                      $"animal {animal} must extend AbstractAnimal:" );

                That( animal.GetInterfaces(),
                      Does.Contain( assembly.GetType( "Task2.Talkable" ) ),
                      $"animal {animal} must implement Talkable" );

                That( animal.GetMethods( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance )
                            .Select( info => info.Name ).Where( name => name.Equals( "Talk" ) ),
                      Is.Empty, $"The `Talk` function shouldn't be overridden in animal {animal}:" );
                That( animal.GetMembers( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance )
                            .Select( info => info.Name ).Where( name => name.Equals( "Sound" ) ),
                      Is.Not.Empty, $"The `Sound` property should be overridden in animal {animal}:" );
            }
        } );
    }

    [Test]
    public void CheckRobotTest()
    {
        Multiple( () =>
        {
            var assembly = Assembly.LoadFrom( "Task2.dll" );
            var robot = assembly.GetType( "Task2.RobotVacuum" );
            That( robot, Is.Not.Null );

            if( robot == null )
            {
                return;
            }

            That( robot.IsAbstract, Is.False );

            That( robot.IsSubclassOf( assembly.GetType( "Task2.AbstractAnimal" ) ), Is.False,
                  "Robot must not extend AbstractAnimal" );

            That( robot.GetInterfaces(),
                  Does.Contain( assembly.GetType( "Task2.Talkable" ) ),
                  $"animal {robot} must implement Talkable" );
        } );
    }

    [Test]
    public void CheckTalksTest()
    {
        var enumerable = new List<AbstractAnimal> { new Cat(), new Dog(), new Goose() };

        foreach( var animal in enumerable )
        {
            animal.Talk();
        }

        var assembly = Assembly.LoadFrom( "Task2.dll" );
        var robot = assembly.GetType( "Task2.RobotVacuum" );
        That( robot, Is.Not.Null );

        if( robot == null )
        {
            return;
        }

        CallFunOnInstance( robot, "Talk" );

        AssertOut( "мяу-мяу-мяу\n" +
                   "гав-гав-гав\n" +
                   "га-га-га\n" +
                   "ур-ур-ур-ур" );
    }

    private static Object? CallFunOnInstance( Type type, String funName, params Object[] args )
    {
        var constructorInfo = type.GetConstructor( Array.Empty<Type>() );
        That( constructorInfo, Is.Not.Null, $"No suitable constructor found in class {type.Name}" );

        if( constructorInfo == null )
        {
            return null;
        }

        var instance = constructorInfo.Invoke( args );
        var methodInfo = instance.GetType().GetMethod( funName,
                                                       BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, Array.Empty<Type>() );
        That( methodInfo, Is.Not.Null, $"No suitable method {funName} found in class {type.Name}" );
        return methodInfo == null ? null : methodInfo.Invoke( instance, Array.Empty<Object>() );
    }

    [Test]
    public void AnimalIsAbstractTest()
    {
        var assembly = Assembly.LoadFrom( "Task2.dll" );
        var type = assembly.GetType( "Task2.AbstractAnimal" );
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

    private static void AssertNoFunctionsDeclared( String className )
    {
        var assembly = Assembly.LoadFrom( "Task2.dll" );
        var type = assembly.GetType( $"Task2.{className}" );
        That( type, Is.Not.Null );
        That( type!.GetMethods( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance )
                   .Select( info => info.Name ).Where( name => !name.StartsWith( "get_" ) ),
              Is.Empty );
    }
}
