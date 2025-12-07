using System.Reflection;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Task1;

public class Tests
{
    [Test]
    public void CheckComputerTest()
    {
        var assembly = Assembly.LoadFrom( "Task1.dll" );
        var type = assembly.GetType( "Task1.Computer" );
        That( type, Is.Not.Null );
        That( type!.IsAbstract, Is.True );
        That( type.GetMembers( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance )
                  .Select( info => info.Name ),
              Is.EquivalentTo( new List<String> { "CalculateAnswer" } ) );
    }

    [Test]
    public void CheckDesktopTest()
    {
        var assembly = Assembly.LoadFrom( "Task1.dll" );
        var computer = assembly.GetType( "Task1.Computer" )!;
        var desktop = assembly.GetType( "Task1.Desktop" )!;
        Multiple( () =>
        {
            That( desktop.GetInterfaces().Contains( computer ), Is.True, "Desktop must implement Computer" );
            That( CallFunOnInstance( desktop, "CalculateAnswer", 5 ), Is.EqualTo( 5 ) );
            That( CallFunOnInstance( desktop, "CalculateAnswer", 10 ), Is.EqualTo( 10 ) );
        } );
    }

    [Test]
    public void CheckSummingCloudTest()
    {
        var assembly = Assembly.LoadFrom( "Task1.dll" );
        var computer = assembly.GetType( "Task1.Computer" )!;
        var summingCloud = assembly.GetType( "Task1.SummingCloud" )!;
        Multiple( () =>
        {
            That( summingCloud.GetInterfaces(), Does.Contain( computer ), "SummingCloud must implement Computer" );
            That( CallFunOnInstance( summingCloud, "CalculateAnswer", 1 ), Is.EqualTo( 1 ) );
            That( CallFunOnInstance( summingCloud, "CalculateAnswer", 10 ), Is.EqualTo( 55 ) );
        } );
    }

    private static Object? CallFunOnInstance( Type type, String funName, params Object[] args )
    {
        var constructorInfo = type.GetConstructor( new[] { typeof( Int32 ) } );
        That( constructorInfo, Is.Not.Null, $"No suitable constructor found in class {type.Name}" );

        if( constructorInfo == null )
            return null;

        var instance = constructorInfo!.Invoke( args );
        var methodInfo = instance.GetType()
                                 .GetMethod( funName,
                                             BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, Array.Empty<Type>() );
        That( methodInfo, Is.Not.Null, $"No suitable method CalculateAnswer found in class {type.Name}" );
        return methodInfo == null ? null : methodInfo.Invoke( instance, Array.Empty<Object>() );
    }
}
