namespace Task3
{
    class Box<T>
    {
        internal Box<U> Convert<U>( Func<T, U> mapper )
        {
            throw new NotImplementedException();
        }
    }

    class BoxList<T> : List<T>
    {

    }

    public class Task3
    {
        internal static void Part1()
        {
            Console.WriteLine( "=== Часть 1: класс Box ===" );
            throw new NotImplementedException( "Раскомментируйте код ниже и реализуйте требуемую функциональность в классе Box" );

            /*
            var b = new Box<int>( 6 );
            Console.WriteLine( $"Box b: {b.View()}" );

            try
            {
                b.Put( 6 );
            }
            catch( InvalidOperationException e )
            {
                Console.WriteLine( $"ERROR: {e.Message}" );
            }

            b.Get();

            try
            {
                Console.WriteLine( $"Box b: {b.View()}" );
            }
            catch( InvalidOperationException e )
            {
                Console.WriteLine( $"ERROR: {e.Message}" );
            }

            try
            {
                b.Get();
            }
            catch( InvalidOperationException e )
            {
                Console.WriteLine( $"ERROR: {e.Message}" );
            }

            b.Put( 0 );
            Console.WriteLine( $"Box b: {b.View()}" );

            var b2 = new Box<string>( "hello" );
            Console.WriteLine( $"Box b2: {b2.View()}" );
            var s = b2.Get();
            Console.WriteLine( $"String's length is {s.Length}" );
            b2.Put( "bye" );
            var s2 = b2.Get();
            Console.WriteLine( $"String's length is {s2.Length}" );
            */
        }

        internal static void Part2()
        {
            Console.WriteLine();
            Console.WriteLine( "=== Часть 2: функция Convert ===" );
            throw new NotImplementedException( "Раскомментируйте код ниже и реализуйте требуемую функциональность в функции Convert класса Box" );

            /*
            var intBox = new Box<int>( 42 );
            var stringBox = intBox.Convert( i => $"{i}!" );
            Console.WriteLine( $"Box stringBox: {stringBox.View()}" );

            try
            {
                Console.WriteLine( $"Box intBox: {intBox.View()}" );
            }
            catch( InvalidOperationException e )
            {
                Console.WriteLine( $"ERROR: {e.Message}" );
            }

            var intBox2 = stringBox.Convert( s => s.Length );
            Console.WriteLine( $"Box intBox2: {intBox2.View()}" );
            var intBox3 = intBox2.Convert( i => i + 1 );
            Console.WriteLine( $"Box intBox3: {intBox3.View()}" );
            */
        }

        internal static void Part3()
        {
            Console.WriteLine();
            Console.WriteLine( "=== Часть 3: класс BoxList ===" );
            throw new NotImplementedException( "Раскомментируйте код ниже и реализуйте требуемую функциональность в классе BoxList" );

            /*
            var intBoxes = new BoxList<int>();

            for( int i = 1; i <= 8; i++ )
            {
                intBoxes.Add( i );
            }

            Console.WriteLine( intBoxes );

            var stringBoxes = new BoxList<String>();

            for( char c = 'a'; c <= 'h'; c++ )
            {
                stringBoxes.Add( c.ToString() );
            }

            Console.WriteLine( stringBoxes );
            */
        }

        public static void Main( String[] args )
        {
            Part1();
            Part2();
            Part3();
        }
    }
}
