using System.Reflection;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Task3;

public class Tests
{
    [Test]
    public void CheckCreatureInterfaceTest()
    {
        var assembly = Assembly.LoadFrom( "Task3.dll" );
        var type = assembly.GetType( "Task3.Creature" );
        That( type, Is.Not.Null, "There should be Creature interface" );

        if( type == null )
        {
            return;
        }

        That( type.GetProperty( "Message" ),
              Is.Not.Null,
              "There should be Creature interface with the `Message` property" );
    }

    [Test]
    public void CheckAdaptersTest()
    {
        var assembly = Assembly.LoadFrom( "Task3.dll" );
        var creature = assembly.GetType( "Task3.Creature" );
        That( creature, Is.Not.Null, "There should be Creature interface" );
        if( creature == null ) return;
        Multiple( () =>
        {
            var human = new Human();
            CheckAdapter( creature, assembly, human, human.Greeting, "HumanAdapter" );
            var dog = new Dog();
            CheckAdapter( creature, assembly, dog, dog.Bark, "DogAdapter" );
            var alien = new Alien();
            CheckAdapter( creature, assembly, alien, alien.Command, "AlienAdapter" );
            var robotVacuum = Assembly.LoadFrom( "Task2.dll" ).GetType( "Task2.RobotVacuum" ).GetConstructor( Array.Empty<Type>() )
                                      .Invoke( Array.Empty<Object>() );
            CheckAdapter( creature, assembly, robotVacuum, "ур", "RobotVacuumAdapter" );
        } );
    }

    private static void CheckAdapter( Type creature, Assembly assembly, Object origObj, String origMessage,
                                      String adapterClassName )
    {
        var adapterType = assembly.GetType( $"Task3.{adapterClassName}" );
        That( adapterType, Is.Not.Null, $"No adapter class found {adapterClassName}" );

        if( adapterType == null )
        {
            return;
        }

        That( adapterType.GetInterfaces().Contains( creature ), Is.True,
              $"Adapter class {adapterClassName} must implement Creature" );

        var constructorInfos = adapterType.GetConstructors( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );
        That( constructorInfos, Has.Length.EqualTo( 1 ), $"Adapter class {adapterClassName} must contain only one constructor" );

        if( constructorInfos.Length != 1 )
        {
            return;
        }

        That( constructorInfos[0].GetParameters().Length, Is.EqualTo( 1 ) );
        That( constructorInfos[0].GetParameters()[0].ParameterType, Is.EqualTo( origObj.GetType() ) );
        var constructorInfo = constructorInfos[0];
        var adapter = constructorInfo.Invoke( new[] { origObj } );
        var messageProperty = adapterType.GetProperty( "Message" );
        That( messageProperty, Is.Not.Null );
        That( origMessage, Is.EqualTo( messageProperty?.GetValue( adapter ) ),
              "Adapter's message should be same as one of the original object" );
    }
}
