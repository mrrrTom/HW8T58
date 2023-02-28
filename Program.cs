// Задача 58: Задайте две матрицы. Напишите программу, которая будет находить произведение двух матриц.
// Например, даны 2 матрицы:
// 2 4 | 3 4
// 3 2 | 3 3
// Результирующая матрица будет:
// 18 20
// 15 18

namespace HW58
{
    class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to matrixs multiplier!");
            var m1 = new MatrixBuilder(2);
            m1.SetRandomValues();
            var m2 = new MatrixBuilder(2);
            m2.SetRandomValues();
            Console.WriteLine($"{m1.ToString()}x\n{m2.ToString()}");
            var result = m1 * m2;
            Console.WriteLine("Multiplication result:");
            Console.WriteLine(result.ToString());
        }
    }

    public class MatrixBuilder
    {
        private double[,] _arr;
        private bool _isInitialized = false;
        public int Size { get; private set; }

        public MatrixBuilder(int size)
        {
            _arr = new double[size, size];
            _isInitialized = true;
            Size = size;
        }

        public override string ToString()
        {
            return _arr.ToArrString();
        }

        public double this[int row, int col]
        {
            get {
                return _arr[row, col];
            }

            set {
                if (!_isInitialized) return;
                _arr[row, col] = value;
            }
        }

#region operators
        public static bool operator == (MatrixBuilder m1, MatrixBuilder m2)
        {
            return !m1.Equals(null) && !m2.Equals(null) && m1.Size == m2.Size;
        }

        public static bool operator != (MatrixBuilder m1, MatrixBuilder m2)
        {
            return m1.Equals(null) || m2.Equals(null) || m1.Size != m2.Size;
        }

        public static MatrixBuilder operator * (MatrixBuilder m1, MatrixBuilder m2)
        {
            if (m1 != m2)
            {
                return default(MatrixBuilder);
            }

            var result = new MatrixBuilder(m1.Size);
            for (var i = 0; i < m1.Size; i++)
            {
                for (var j = 0; j < m1.Size; j++)
                {
                    var newValue = default(double);
                    for (var k = 0; k < m1.Size; k++)
                    {
                        newValue += Math.Round((m1[i, k] * m2[k, j]), 2); 
                    }

                    result[i, j] = newValue; 
                }
            }

            return result;
        }
#endregion operators

        public void SetRandomValues()
        {
            var rnd = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var signPow = rnd.Next(1, 3);
                    var tenPow = rnd.Next(0, 3);
                    var doubleValue = rnd.NextDouble();
                    var sign = ((double)Math.Pow(-1, signPow));
                    var tens = ((double)Math.Pow(10, tenPow));
                    var roundCount = rnd.Next(0, 3);
                    var arrInput =  Math.Round(doubleValue * sign * tens, roundCount);
                    _arr[i, j] = arrInput;
                }
            }
        }
    }

    public static class ArrExtension
    {
        public static string ToArrString(this double[,] arr)
        {
            var result = string.Empty;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    result += arr[i, j] + "\t";
                }

                result += "\n";
            }

            return result;
        }
    }
}