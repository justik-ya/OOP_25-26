namespace Task3
{
    class Human
    {
        internal String Greeting => "Привет, я человек!";
    }

    class Dog
    {
        internal String Bark => "Гав!";
    }

    class Alien
    {
        internal String Command = "Ты меня не видишь";
    }

    // interface Creature

    // adapters

    public class Task3
    {
        public static void Main( String[] args )
        {
            var creatures = new List<Object> { }; // что нужно поменять в этой строке после добавления в список инстансов адаптеров ?

            Console.WriteLine( "Все сообщения:" );
            creatures.ForEach( creature => throw new NotImplementedException() );
        }
    }
}
