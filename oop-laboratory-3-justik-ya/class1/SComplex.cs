using System;

namespace class1
{
    public static class SComplex
    {
        // Функция должна парсить CSV строчку (Comma Separated Value) и
        // конструировать объект типа Complex
        // Format: real, image
        // Если при разборе строки возникает ошибка необходимо выбросить исключение
        // Логика должна быть толерантна ко всем пробельным символам, иными словами
        // строки "\n\t1 ,\n    15" и "1,15" обе корректны и должны возвращать объект
        // соответвующий числу 1 + 15i
        public static IComplex ParseCsv( String value )
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Value is null or empty");
            
            string[] parts = value.Split(',');

            if (parts.Length != 2)
                throw new Exception("Value is not in the correct format");
            
            string realStr = parts[0].Trim();
            string imagStr = parts[1].Trim();

            if (!double.TryParse(realStr, out double real))
                throw new Exception("Real part is not a valid number");

            if (!double.TryParse(imagStr, out double imag))
                throw new Exception("Imaginary part is not a valid number");

            return new Complex(real, imag);
        }

        // Функция должна складывать два комплексных числа и возвращать результат
        // в новом объекте
        public static IComplex Add( IComplex x, IComplex y )
        {
            return new Complex(
                x.Real + y.Real,
                x.Imag + y.Imag);  
        }

        // Функция должна перемножать два комплексных числа и возвращать результат
        // в новом объекте
        public static IComplex Mul(IComplex x, IComplex y)
        {
            double real = x.Real * y.Real - x.Imag * y.Imag;
            double imag = x.Real * y.Imag + x.Imag * y.Real;

            return new Complex(real, imag);
        }
    }
}
