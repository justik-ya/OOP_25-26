namespace Task3
{
    abstract class Creature
    {
    }

    class Human : Creature
    {
        internal String Greeting() => "Привет, я человек!";
    }

    class Dog : Creature
    {
        internal String Bark() => "Гав!";
    }

    class Alien : Creature
    {
        internal String Command() => "Ты меня не видишь";
    }

    public class Task3
    {
        internal static void PrintMessageFrom( Creature creature )
        {
            throw new NotImplementedException();
        }

        static List<Dog> FindDogs( List<Creature> creatures ) => throw new NotImplementedException();

        public static void Main( String[] args )
        {
            RunTest();
        }

        internal static void RunTest()
        {
            var creatures = new List<Creature> { new Alien(), new Dog(), new Human(), new Dog() };
            Console.WriteLine( "Все сообщения:" );

            foreach( var creature in creatures )
            {
                PrintMessageFrom( creature );
            }

            Console.WriteLine();
            Console.WriteLine( "Сообщения только от собак:" );

            foreach( var dog in FindDogs( creatures ) )
            {
                Console.WriteLine( dog.Bark() );
            }
        }
    }
}
