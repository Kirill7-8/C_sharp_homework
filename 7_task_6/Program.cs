//Удалить все строки, в которых среднее арифметическое элементов является двузначным числом

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите n: ");
        int n = int.Parse(Console.ReadLine());
        int[][] matrix = Input(n);
        if (n == matrix.Length)
        {
            Print(matrix, n);
            Console.WriteLine("Результат:");
            for (int i = matrix.Length - 1; i >= 0; i--){
            if (Check(matrix[i])) Delete(matrix, i, ref n);
            }
            Print(matrix, n);
        }
        else
        {
            Console.WriteLine("Ошибка!");
        }
    }
    static bool Check(int[] array)
    {
        int counter = 0;
        foreach (int num in array)
        {
            counter += num;
        }
        int average = counter / array.Length; 
        return average > 9 && average < 100;
    }
    static void Delete(int[][] matrix, int k, ref int n)
    {
        for (int i = k; i < n - 1; i++)
        {
            matrix[i] = matrix[i + 1];
        }
        --n;
    }
    
    static int[][] Input(int n)
    {
        int[][] matrix = new int[n][];

        for (int i = 0; i < n; i++)
        {
            matrix[i] = new int[n];
            Console.WriteLine($"Впишите {i + 1} строку");
            for (int j = 0; j < n; j++)
            {
                matrix[i][j] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine(new string('-', 20));
        }
        return matrix;
    }
    static void Print(int[][] a, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < a[i].Length; j++)
            {
                Console.Write("{0} ", a[i][j]);
            }
            Console.WriteLine();
        }
    }
}