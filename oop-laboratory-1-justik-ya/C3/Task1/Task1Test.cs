using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

public class Tests
{
    [Test]
    public void StringLengthTest()
    {
        That( StringLength( "hello" ), Is.EqualTo( 5 ) );
        That( StringLength( "" ), Is.EqualTo( 0 ) );
        That( StringLength( new String( 'x', 1000 ) ), Is.EqualTo( 1000 ) );
    }

    [Test]
    public void TestCountDigits()
    {
        That( CountDigits( "xxx" ), Is.EqualTo( 0 ) );
        That( CountDigits( "x1x2x3" ), Is.EqualTo( 3 ) );
        That( CountDigits( "1a2b3c" ), Is.EqualTo( 3 ) );
        That( CountDigits( "0-1/2+3@4#5$6%7^8&9" ), Is.EqualTo( 10 ) );
    }

    [Test]
    public void FirstLastCodeTest()
    {
        //That( FirstLastCodes( String.Empty ), Is.EqualTo( new Tuple<Int32?, Int32?>( null, null ) ) );
        That( FirstLastCodes( " " ), Is.EqualTo( new Tuple<Int32?, Int32?>( 32, 32 ) ) );
        That( FirstLastCodes( "xxxxxxxxyyyyyyyy" ), Is.EqualTo( new Tuple<Int32?, Int32?>( 'x', 'y' ) ) );
    }

    [Test]
    public void CountDigits2Test()
    {
        That( CountDigits2( "xxx" ), Is.EqualTo( 0 ) );
        That( CountDigits2( "x1x2x3" ), Is.EqualTo( 3 ) );
        That( CountDigits2( "1a2b3c" ), Is.EqualTo( 3 ) );
        That( CountDigits2( "0-1/2+3@4#5$6%7^8&9" ), Is.EqualTo( 10 ) );
    }

    [Test]
    public void CalcDigitsTest()
    {
        That( CalcDigits( "3-3" ), Is.EqualTo( 0 ) );
        That( CalcDigits( "4+7-2-8" ), Is.EqualTo( 1 ) );
        That( CalcDigits( "2+8-4-7" ), Is.EqualTo( -1 ) );
    }

    [Test]
    public void ReplaceWithStringTest()
    {
        That( ReplaceWithString( "Миру мир", "мир", "пир" ), Is.EqualTo( "Миру пир" ) );
        That( ReplaceWithString( "abcd", "ab", "xxx" ), Is.EqualTo( "xxxcd" ) );
        That( ReplaceWithString( "abcdab", "ab", "xxx" ), Is.EqualTo( "xxxcdab" ) );
        That( ReplaceWithString( "abcdab", "yy", "xxx" ), Is.EqualTo( "abcdab" ) );
        That( ReplaceWithString( "abcdab", "bcd", "" ), Is.EqualTo( "aab" ) );
    }
}
