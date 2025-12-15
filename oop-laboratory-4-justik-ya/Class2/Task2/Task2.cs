namespace Task2
{
    abstract class Animal
    {
        internal abstract void Talk();
    }

    class Cat : Animal
    {
        internal override void Talk()
        {
            Console.WriteLine( "Кошка мяучит 'мяу-мяу'" );
        }
    }

    class Dog : Animal
    {
        internal override void Talk()
        {
            Console.WriteLine( "Собака гавкает 'гав-гав-гав'" );
        }
    }

    class Goose : Animal
    {
        internal override void Talk()
        {
            Console.WriteLine( "Гусь гогочет 'га-га-га'" );
        }
    }

    public class Task2
    {
        public static void Main( String[] args )
        {
            RunTest();
        }

        internal static void RunTest()
        {
            foreach( var animal in new List<Animal> { new Cat(), new Dog(), new Goose() } )
            {
                animal.Talk();
            }
        }
    }
}
