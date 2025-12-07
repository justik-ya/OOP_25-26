namespace Task2
{
    abstract class AbstractAnimal
    {
        public abstract void Talk();
    }

    class Cat : AbstractAnimal
    {
        public override void Talk()
        {
            Console.WriteLine( "мяу-мяу-мяу" );
        }
    }

    class Dog : AbstractAnimal
    {
        public override void Talk()
        {
            Console.WriteLine( "гав-гав-гав" );
        }
    }

    class Goose : AbstractAnimal
    {
        public override void Talk()
        {
            Console.WriteLine( "га-га-га" );
        }
    }

    interface Talkable
    {
    }

    class RobotVacuum : Talkable
    {
        public void Talk()
        {
            Console.WriteLine( "ур-ур-ур-ур" );
        }
    }

    public class Task2
    {
        public static void Main( String[] args )
        {
            // На подумать и поэкспериментировать:
            // Что произойдёт, если добавить в список RobotVacuum?
            var animals = new List<AbstractAnimal> { new Cat(), new Dog(), new Goose() };

            // Что нужно поменять, чтобы код стал компилироваться после этого?
            // Сделайте это изменение и в тесте.
            foreach( var animal in animals )
            {
                animal.Talk();
            }
        }
    }
}
