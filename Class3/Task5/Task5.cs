namespace Task5
{
    public class Task5
    {
        internal interface ILogger
        {
            void Info( String message );
        }

        internal class Logger : ILogger
        {
            public void Info( String message )
            {
                throw new NotImplementedException();
            }
        }

        internal class LoggerDecorator : ILogger
        {
            public LoggerDecorator( ILogger logger )
            {
                throw new NotImplementedException();
            }

            public void Info( String message )
            {
                throw new NotImplementedException();
            }
        }

        internal static void Part1( ILogger logger )
        {
            logger.Info( "First message" );
            logger.Info( "Second message" );
            logger.Info( "Third message" );
        }

        public static void Main( String[] args )
        {
            Part1( new Logger() );
            Part1( new LoggerDecorator( new Logger() ) );
        }
    }
}
