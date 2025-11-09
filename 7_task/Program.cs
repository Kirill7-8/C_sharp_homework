class Program
{
    static int[,] Input(out int n, out int m)
    {
        Console.WriteLine("введите размерность массива");
        Console.Write("n = ");
        n = int.Parse(Console.ReadLine());
        Console.Write("m = ");
        m = int.Parse(Console.ReadLine());
        int[,] a = new int[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                Console.Write("a[{0},{1}]= ", i, j);
                a[i, j] = int.Parse(Console.ReadLine());
            }
        return a;
    }
    static void Print(int[,] a, int n, int m)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write("{0,5} ", a[i, j]);
            Console.WriteLine();
        }
    }
    static void Delete(int[,] a, ref int n, int m, int k)
    {
        for (int i = k; i < n - 1; i++) //сдвиг строк вверх начиная с k-той строки
            for (int j = 0; j < m; j++) //сдвигаемы строки обрабатываются поэлементно
                a[i, j] = a[i + 1, j];
        --n; //уменьшаем текущее количество строк в массиве
    }
    static void Main()
    {
        int n, m;
        int[,] a = Input(out n, out m);
        Console.WriteLine("Исходный массив:");
        Print(a, n, m);
        Console.WriteLine("Введите номер строки для удаления:");
        int k = int.Parse(Console.ReadLine());
        Delete(a, ref n, m, k);
        Console.WriteLine("Измененный массив:");
        Print(a, n, m);
    }
}