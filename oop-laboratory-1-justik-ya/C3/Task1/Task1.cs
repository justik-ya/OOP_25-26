using System;

namespace Task1
{
    public class Task1
    {
        /*
         * Задание 1.1. Дана строка. Верните строку, содержащую текст "Длина: NN",
         * где NN — длина заданной строки. Например, если задана строка "hello",
         * то результатом должна быть строка "Длина: 5".
         */
        internal static Int32 StringLength( String s )
        {
            return s.Length;
        }

        /*
         * Задание 1.2. Дана непустая строка. Вернуть коды ее первого и последнего символов.
         * Рекомендуется найти специальные функции для вычисления соответствующих символов и их кодов.
         */
        internal static Tuple<Int32?, Int32?> FirstLastCodes( String s )
        {
            return new Tuple<int?, int?>(Code(First(s)), Code(Last(s)));
        }

        private static Char? First(String s) =>s[0];
        private static Char? Last(String s) => s[s.Length - 1];
        private static Int32? Code(Char? c) => (int?)c;

        /*
         * Задание 1.3. Дана строка. Подсчитать количество содержащихся в ней цифр.
         * В решении необходимо воспользоваться циклом for.
         */
        internal static Int32 CountDigits( String s )
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                    count++;
            }

            return count;
        }

        /*
         * Задание 1.4. Дана строка. Подсчитать количество содержащихся в ней цифр.
         * В решении необходимо воспользоваться методом Count:
         * https://docs.microsoft.com/ru-ru/dotnet/api/system.linq.enumerable.count?view=net-6.0#system-linq-enumerable-count-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))
         * и функцией Char.IsDigit:
         * https://docs.microsoft.com/ru-ru/dotnet/api/system.char.isdigit?view=net-6.0
         */
        internal static Int32 CountDigits2( String s )
        {
            return s.Count(c => Char.IsDigit(c));
        }

        /*
         * Задание 1.5. Дана строка, изображающая арифметическое выражение вида «<цифра>±<цифра>±…±<цифра>»,
         * где на месте знака операции «±» находится символ «+» или «−» (например, «4+7−2−8»). Вывести значение
         * данного выражения (целое число).
         */
        internal static Int32 CalcDigits( String expr )
        {
            int sum = 0;
            int sign = 1;

            foreach (char c in expr)
            {
                if (c == '+') 
                    sign = 1;       
                else if (c == '-') 
                    sign = -1;      
                else if (char.IsDigit(c)) 
                    sum += (c - '0') * sign; 
            }

            return sum;
        }

        /*
         * Задание 1.6. Даны строки S, S1 и S2. Заменить в строке S первое вхождение строки S1 на строку S2.
         * Использовать стандартную функцию замены запрещено.
         */
        internal static String ReplaceWithString( String s, String s1, String s2 )
        {
            int pos = -1;
            
            for (int i = 0; i <= s.Length - s1.Length; i++)
            {
                bool match = true;
                
                if (s[i] == s1[0])
                {
                    for (int j = 0; j < s1.Length; j++)
                    {
                        if (s[i+j] != s1[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        pos = i;
                        break;
                    }
                        
                }
            }

            if (pos == -1)
                return s;
            else
            {
                string s3 = s[0..pos] + s2 + s[(pos + s1.Length)..];
                return s3;
            }
                
               
            
        }

        public static void Main( String[] args )
        {
            System.Console.WriteLine($"Длина: {StringLength("aba")}");
            System.Console.WriteLine(FirstLastCodes("abc"));
            System.Console.WriteLine(CountDigits("ab1c2d3"));
            System.Console.WriteLine(CountDigits2("ab1c2d3"));
            System.Console.WriteLine(CalcDigits("4+7−2−8"));
            System.Console.WriteLine(ReplaceWithString("abcdefg", "cde", "wwwww"));
        }
    }
}
