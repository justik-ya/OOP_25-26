using System;
using System.Linq;
using System.Collections.Generic;

namespace class1
{
    public static class SMatrix
    {
        // Функция должна парсить CSV строчку (Comma Separated Value) и
        // конструировать объект типа Complex
        // Format: rows, columns, a11, a12, ..., aRowsColumns
        // Результатом разбора должна стать матрица с rows строк и colums столбцов, вида
        // | a11 a12 ... a1C | , где R = rows, C = columns 
        // | a21 a22 ... a2C |
        // | ............... |
        // | aR1 aR2 ... aRC |
        // Если при разборе строки возникает ошибка необходимо выбросить исключение
        // Логика должна быть толерантна ко всем пробельным символам (см. пример для SComplex)
       public static IMatrix ParseCsv(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Empty");

            // Разбиваем всё только по запятым
            string[] parts = value.Split(',');

            var tokens = new List<string>();
            foreach (var part in parts)
            {
                string t = part.Trim();   // убираем пробелы, \t, \n, \r вокруг
                if (t.Length > 0)
                    tokens.Add(t);
            }

            // Нужно минимум 3 числа: rows, cols и хотя бы один элемент
            if (tokens.Count < 3)
                throw new Exception("Not enough data");

            // Парсим размеры матрицы
            if (!int.TryParse(tokens[0], out int rows) ||
                !int.TryParse(tokens[1], out int cols) ||
                rows <= 0 || cols <= 0)
                throw new Exception("Bad matrix dimensions");

            // Проверяем количество элементов
            if (tokens.Count != 2 + rows * cols)
                throw new Exception("Data size mismatch");

            double[,] data = new double[rows, cols];

            int idx = 2; // индекс первого элемента после rows, cols
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    string num = tokens[idx++];

                    if (!double.TryParse(num, out double val))
                        throw new Exception("Invalid number");

                    data[r, c] = val;
                }
            }

            return new Matrix(data);
        }


        // Функция должна складывать две матрицы и возвращать результат в новом объекте
        // Если операция не выполнима, необходимо выбросить исключение
        public static IMatrix Add( IMatrix x, IMatrix y )
        {
            if (x.Rows != y.Rows || x.Columns != y.Columns)
                throw new Exception("Different matrix sizes");

            var result = new Matrix(x.Rows, x.Columns);

            for (int r = 0; r < x.Rows; r++)
                for (int c = 0; c < x.Columns; c++)
                    result[r, c] = x[r, c] + y[r, c];

            return result;
        }

        // Функция должна перемножать две матрицы и возвращать результат в новом объекте
        // Если операция не выполнима, необходимо выбросить исключение
        public static IMatrix Mul( IMatrix x, IMatrix y )
        {
            if (x.Columns != y.Rows)
                throw new Exception("Invalid dimensions");

            var result = new Matrix(x.Rows, y.Columns);

            for (int i = 0; i < x.Rows; i++)
            {
                for (int j = 0; j < y.Columns; j++)
                {
                    double sum = 0;

                    for (int t = 0; t < x.Columns; t++)
                        sum += x[i, t] * y[t, j];

                    result[i, j] = sum;
                }
            }

            return result;
        }
    }
}   
