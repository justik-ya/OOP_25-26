using System.Text;

namespace Task2
{
    public class Task2
    {

        /*
         * В этих заданиях рекомендуется всюду использовать класс StringBuilder.
         * Документация: https://docs.microsoft.com/ru-ru/dotnet/api/system.text.stringbuilder?view=net-6.0
         */

        /*
         * Задание 2.1. Дана непустая строка S и целое число N (> 0). Вернуть строку, содержащую символы
         * строки S, между которыми вставлено по N символов «*» (звездочка).
         */
        internal static String FillWithAsterisks( String s, Int32 n )
        {
            string s1 = s[0].ToString();
            
            for (int i = 1; i < s.Length; i++)
            {
                
                
                for (int j = 0; j < n; j++)
                {
                    s1 += "*";
                }
                s1 += s[i];
            }

            return s1;
        }

        /*
         * Задание 2.2. Сформировать таблицу квадратов чисел от 1 до заданного числа N.
         * Например, для N=4 должна получиться следующая строка:
        
        1  1
        2  4
        3  9
        4 16
        
         * Обратите внимание на выравнивание: числа в первом столбце выравниваются по левому краю,
         * а числа во втором -- по правому, причём между числами должен оставаться как минимум один
         * пробел. В решении можно использовать функции Pad*.
         */
        internal static String TabulateSquares( Int32 n )
        {
            int rightWidth = (n * n).ToString().Length;
            int leftWidth = n.ToString().Length;
            int totalWidth = leftWidth + 1 + rightWidth;

            var sb = new StringBuilder();

            for (int i = 1; i <= n; i++)
            {
                string left  = i.ToString().PadRight(leftWidth); 
                string right = (i * i).ToString().PadLeft(rightWidth);
                
                string res = left + " " + right;
                sb.AppendLine(res);
            }

            return sb.ToString();
        }

        public static void Main( String[] args )
        {
            Console.WriteLine( FillWithAsterisks( "abc", 2 ) );
            Console.WriteLine( TabulateSquares( 10 ) );
        }
    }
}
