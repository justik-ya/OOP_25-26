namespace Task1
{
    class JsonIntProperty
    {
    }

    public class Task1
    {
        public static void Main( String[] args )
        {
            RunTest();
        }

        internal static void RunTest()
        {
            throw new NotImplementedException( "Раскомментируйте код ниже и реализуйте требуемую функциональность в классе JsonIntProperty" );
            /*
            var ageProperty = new JsonIntProperty( "age", 21 );
            Console.WriteLine( ageProperty );
            Console.WriteLine( ageProperty.StringRepresentation );
            ageProperty.Value += 1;
            Console.WriteLine( ageProperty );
            ageProperty.StringRepresentation = "age: 23";
            Console.WriteLine( ageProperty );
            ageProperty.StringRepresentation = "age   :   24";
            Console.WriteLine( ageProperty );

            try
            {
                ageProperty.StringRepresentation = "value : 10";
            }
            catch( Exception e )
            {
                Console.WriteLine( e.GetType().FullName + ": " + e.Message );
            }

            try
            {
                ageProperty.StringRepresentation = "age: ?";
            }
            catch( Exception e )
            {
                Console.WriteLine( e.GetType().FullName + ": " + e.Message );
            }

            try
            {
                ageProperty.StringRepresentation = "age = 10";
            }
            catch( Exception e )
            {
                Console.WriteLine( e.GetType().FullName + ": " + e.Message );
            }

            Console.WriteLine( $"JSON value of 'age' has been set {ageProperty.SetValueCounter} time(s)" );
            var countProperty = new JsonIntProperty( "count" );
            Console.WriteLine( countProperty );
            Console.WriteLine( $"JSON value of 'count' has been set {countProperty.SetValueCounter} time(s)" );
            Console.WriteLine( $"Class 'JsonIntProperty' instance has been created {JsonIntProperty.InstanceCounter} time(s)" );
            */
        }
    }
}
