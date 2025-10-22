//Удалить все строки, в которых среднее арифметическое элементов является двузначным числом

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите n: ");
        int n = int.Parse(Console.ReadLine());
        int[][] matrix = Input(n);
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
    //static int[][] Func(int[][] matrix){}
    static int[][] Input(int n)
    {
        int[][] matrix = new int[n][];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Впишите {i + 1} строку, а затем через пробел элементы строки");
            String[] digit = Console.ReadLine(); 

            Console.WriteLine(new string('-', 20));
        }
        return matrix;
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