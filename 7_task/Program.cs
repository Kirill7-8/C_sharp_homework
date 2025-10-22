
//для каждого столбца найти номер последнего нечетного элемента и записать данные в
//новый массив
class Program
{
    static void Main()
    {
        Console.WriteLine("Введите n: ");
        int n = int.Parse(Console.ReadLine());
        int[,] matrix = Input(n);
        if (n == matrix.GetLength(0) && matrix.GetLength(1) == n)
        {
            Print(matrix);
            Console.WriteLine("Результат:");
            Print(Func(matrix));
        }
        else
        {
            Console.WriteLine("Ошибка!");
        }
    }
    static int[] Func(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        int[] res = new int[n];
        for (int i = 0; i < n; i++)
        {
            res[i] = -1;
            for (int j = n - 1; j >= 0; j--)
            {
                
                if (matrix[j, i] % 2 != 0)
                {
                    res[i] = j;
                    break;
                }
               
                
            }
        }

        return res;
    }
    static int[,] Input(int n)
    {
        int[,] matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Впишите {i + 1} строку");
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine(new string('-', 20));
        }
        return matrix;
    }
    static void Print(int[] a)
    {
        int n = a.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            Console.Write("{0} ", a[i]);
        }
    }
    static void Print(int[,]a)
    {
        int n = a.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write("{0} ", a[i, j]);
            }
            Console.WriteLine();
            
        }
        
    }
}