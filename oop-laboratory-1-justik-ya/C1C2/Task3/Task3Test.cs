using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task3.Task3;

namespace Task3;

public class Tests
{
    [Test]
    public void FTest()
    {
        That( F( 0.0 ), Is.EqualTo( 1.0 ).Within( 1e-5 ) );
        That( F( 2.7 ), Is.EqualTo( 1.0 ).Within( 1e-5 ) );
        That( F( -5.6 ), Is.EqualTo( 0 ).Within( 1e-5 ) );
        That( F( 3.2 ), Is.EqualTo( -1.0 ).Within( 1e-5 ) );
        That( F( 9.0 ), Is.EqualTo( -1.0 ).Within( 1e-5 ) );
        That( F( 8.5 ), Is.EqualTo( 1.0 ).Within( 1e-5 ) );
    }

    [Test]
    public void NumberOfDaysTest()
    {
        That( NumberOfDays( 2021 ), Is.EqualTo( 365 ) );
        That( NumberOfDays( 0 ), Is.EqualTo( 365 ) );
        That( NumberOfDays( 2024 ), Is.EqualTo( 366 ) );
        That( NumberOfDays( 2000 ), Is.EqualTo( 366 ) );
        That( NumberOfDays( 1900 ), Is.EqualTo( 365 ) );
    }

    [Test]
    public void Rotate2Test()
    {
        That( Rotate2( 'С', 1, -1 ), Is.EqualTo( 'С' ) );
        That( Rotate2( 'Ю', 1, 2 ), Is.EqualTo( 'З' ) );
        That( Rotate2( 'В', -1, -1 ), Is.EqualTo( 'З' ) );
        That( Rotate2( 'З', 2, 2 ), Is.EqualTo( 'З' ) );
    }

    [Test]
    public void AgeDescriptionTest()
    {
        That( AgeDescription( 42 ), Is.EqualTo( "сорок два года" ) );
        That( AgeDescription( 20 ), Is.EqualTo( "двадцать лет" ) );
        That( AgeDescription( 21 ), Is.EqualTo( "двадцать один год" ) );
    }

    [Test]
    public void MainTest()
    {
        Main( Array.Empty<String>() );
    }
}
