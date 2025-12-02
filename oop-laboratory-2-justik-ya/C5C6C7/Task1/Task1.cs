using System;
using System.Collections;
using System.Collections.Generic;

namespace Task1
{
    /*
     * Задача: Реализовать коллекцию и энумератор к ней при итерировании которой мы увидем
     * _headCharCount раз повторенный символ _headChar, затем все символы строки _middle по
     * порядку, после чего _tailCharCount раз повторенный символ _tailChar.
     *
     * Допишите проверки входных данных конструктора так, чтобы при отрицательном количестве
     * символов выбрасывалось исключение - текст любой.
     * Строка middle может быть пустой или null'евой. В этом случае энумератор должен вернуть
     * только нужное количество символов из головы и из хвоста.
     *
     * Пример:
     *   foreach( Char c in new CrezyCollection('<', 3, "Hello", '>', 2))
     *       Console.Write( c );
     *   Выведет в консоль строку "<<<Hello>>"
     *
     * FYI: ключевое слово readonly запрещает изменение значения переменной после
     *      ее инициализации в конструкторе
     */
    public class CrazyCollection : IEnumerable<Char>
    {
        private readonly Char _headChar;
        private readonly Int32 _headCharCount;
        private readonly String _middle;
        private readonly Char _tailChar;
        private readonly Int32 _tailCharCount;

        public CrazyCollection( Char headChar, Int32 headCharCount, String middle, Char tailChar, Int32 tailCharCount )
        {
            if (headCharCount < 0)
                throw new ArgumentException("Head count cannot be negative");

            if (tailCharCount < 0)
                throw new ArgumentException("Tail count cannot be negative");
            
            _headChar = headChar;
            _headCharCount = headCharCount;
            _middle = middle ?? String.Empty;
            _tailChar = tailChar;
            _tailCharCount = tailCharCount;
        }

        #region IEnumerable<Char>

        public IEnumerator<Char> GetEnumerator()
        {
            return new CrazyEnumerator(_headChar, _headCharCount, _middle, _tailChar, _tailCharCount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class CrazyEnumerator : IEnumerator<Char>
    {
        private readonly char _headChar;
        private readonly int _headCount;
        private readonly string _middle;
        private readonly char _tailChar;
        private readonly int _tailCount;

        private int _position = -1;
        
        public CrazyEnumerator(char headChar, int headCount, string middle, char tailChar, int tailCount)
        {
            _headChar = headChar;
            _headCount = headCount;
            _middle = middle;
            _tailChar = tailChar;
            _tailCount = tailCount;
        }
        
        #region IEnumerator<Char>

        public Boolean MoveNext()
        {
            _position++;
            return _position < _headCount + _middle.Length + _tailCount;
        }

        public void Reset()
        {
            _position = -1;
        }

        public Char Current 
        { 
            get
            {
                if (_position < _headCount)
                    return _headChar;

                if (_position < _headCount + _middle.Length)
                    return _middle[_position - _headCount];

                return _tailChar;
            }
        }

        Object IEnumerator.Current => Current;

        public void Dispose() { }

        #endregion
    }
}
