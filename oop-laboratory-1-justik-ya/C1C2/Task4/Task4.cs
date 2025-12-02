using System.Globalization;

namespace Task4
{
    public class Task4
    {
        /*
         * В решениях следующих заданий предполагается использование циклов.
         */

        /*
         * Задание 4.1. Пользуясь циклом for, посимвольно напечатайте рамку размера width x height,
         * состоящую из символов frameChar. Можно считать, что width>=2, height>=2.
         * Например, вызов printFrame(5,3,'+') должен напечатать следующее:
         *
         * +++++
         * +   +
         * +++++
         *
         */
        internal static void PrintFrame( Int32 width, Int32 height, Char frameChar = '*' )
        {
            for (int j = 0; j < width; j++)
                Console.Write(frameChar);
            Console.WriteLine();

            for (int i = 0; i < height-2; i++)
            {
                Console.Write(frameChar);
                for (int k = 0; k < width - 2; k++)
                    Console.Write(" "); 
                Console.Write(frameChar);
                
                Console.WriteLine();  
            }
            for (int j = 0; j < width; j++)
                Console.Write(frameChar);
            Console.WriteLine();
            
        }

        /*
         * Задание 4.2. Выполните предыдущее задание, пользуясь циклом while.
         */
        internal static void PrintFrame2( Int32 width, Int32 height, Char frameChar = '*' )
        {
            int j = 0;
            while (j < width)
            {
                Console.Write(frameChar);
                j++;
            }
            Console.WriteLine();

            int i = 0;
            while (i < height-2)
            {
                int k = 0;
                Console.Write(frameChar);
                while (k < width - 2)
                {
                    Console.Write(" ");
                    k++;
                }
                Console.Write(frameChar);
                
                Console.WriteLine();
                i++;
            }

            int l = 0;
            while (l < width)
            {
                Console.Write(frameChar);
                l++;
            }
            Console.WriteLine();
        }

        /*
         * Задание 4.3. Даны целые положительные числа A и B. Найти их наибольший общий делитель (НОД),
         * используя алгоритм Евклида:
         * НОД(A, B) = НОД(B, A mod B),    если B ≠ 0;        НОД(A, 0) = A,
         * где «mod» обозначает операцию взятия остатка от деления.
         */
        internal static Int64 Gcd( Int64 a, Int64 b )
        {
            if (b == 0)
                return a;

            return Gcd(b, a % b);
        }

        /*
         * Задание 4.4. Дано вещественное число X и целое число N (> 0). Найти значение выражения
         * 1 + X + X^2/(2!) + … + X^N/(N!), где N! = 1·2·…·N.
         * Полученное число является приближенным значением функции exp в точке X.
         */
        internal static Double ExpTaylor( Double x, Int32 n )
        {
            if (n <= 0) return 1.0;

            double result = 1.0;
            double term = 1.0;
            
            for (int i = 1; i <= n; i++)
            {
                term *= x / i;   
                result += term;
                
                if (term == 0.0) break;
            }
            return result;
        }

        public static void Main( String[] args )
        {
            PrintFrame( 5, 4, '+' );
            PrintFrame2(9, 3, '#');
            Console.WriteLine( Gcd( 4, 2 ) );
            Console.WriteLine( ExpTaylor( 1, 100 ) );
            
        }
    }
}
