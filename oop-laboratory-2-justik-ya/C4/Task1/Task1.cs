using System.Text.RegularExpressions;

namespace Task1
{
    public class Task1
    {
        /*
         * Перед выполнением заданий рекомендуется просмотреть туториал по регулярным выражениям:
         * https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expression-language-quick-reference
         */

        /*
         * Задание 3.1. Проверить, содержит ли заданная строка только цифры.
         */
        internal static Boolean AllDigits( String s ) => new Regex( "^\\d+$" ).IsMatch( s );

        /*
         * Задание 3.2. Проверить, содержит ли заданная строка подстроку, состоящую
         * из букв abc в указанном порядке, но в произвольном регистре.
         */
        internal static Boolean ContainsABC( String s ) => new Regex( "[aA][bB][cC]", RegexOptions.None ).IsMatch( s );

        /*
         * Задание 3.3. Найти первое вхождение подстроки, состоящей только из цифр,
         * и вернуть её в качестве результата. Вернуть пустую строку, если вхождения нет.
         */
        internal static String FindDigitalSubstring( String s )
        {
            Match match = new Regex( "\\d+" ).Match( s );

            if (match.Success)
                return match.Value;
            else
                return "";
        }

        /*
         * Задание 3.4. Заменить все вхождения подстрок строки S, состоящих только из цифр,
         * на заданную строку S1.
         */
        internal static String HideDigits( String s, String s1 )
        {
            string str = new Regex( "\\d+" ).Replace(s, s1);
            
            return str;
        }

        public static void Main( String[] args )
        {
            Console.WriteLine( AllDigits( "ab13mdj4" ) );
            Console.WriteLine( ContainsABC( "23sddaBcdk33" ) );
            Console.WriteLine( FindDigitalSubstring( "abcd123efg" ) );
            Console.WriteLine( HideDigits( "ab12cd34ef", "@@@" ) );
        }
    }
}
