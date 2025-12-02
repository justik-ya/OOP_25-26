using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task2.Task2;

namespace Task2;

public class Tests
{
    [Test]
    public void FillWithAsterisksTest()
    {
        That( FillWithAsterisks( "ab", 3 ), Is.EqualTo( "a***b" ) );
        That( FillWithAsterisks( "abcd", 2 ), Is.EqualTo( "a**b**c**d" ) );
        That( FillWithAsterisks( "*", 5 ), Is.EqualTo( "*" ) );
    }

    [Test]
    public void TabulateSquaresTest()
    {
        That( TabulateSquares( 3 ).Replace( "\r\n", "\n" ),
              Is.EqualTo( "1 1\n" +
                          "2 4\n" +
                          "3 9\n" ) );

        That( TabulateSquares( 4 ).Replace( "\r\n", "\n" ),
              Is.EqualTo( "1  1\n" +
                          "2  4\n" +
                          "3  9\n" +
                          "4 16\n" ) );

        That( TabulateSquares( 10 ).Replace( "\r\n", "\n" ),
              Is.EqualTo( "1    1\n" +
                          "2    4\n" +
                          "3    9\n" +
                          "4   16\n" +
                          "5   25\n" +
                          "6   36\n" +
                          "7   49\n" +
                          "8   64\n" +
                          "9   81\n" +
                          "10 100\n" ) );
    }
}
