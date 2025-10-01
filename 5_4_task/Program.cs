class Stars
{
    static void Main()
    {
        Console.WriteLine("Введите n: ");
        int n = int.Parse(Console.ReadLine());
        func(n, 1);
    }
    static void str(int n, char s)
    {
        for (int i = 1; i <= n; i++)
        {
            Console.Write(s);
        }
    }
    static void func(int n, int i)
    {
        if (n == 0)
        {
            str(i * 2, '*');
            Console.WriteLine();
        }
        if (n > 0)
        {
            str(i, '*');
            str(n, ' ');
            str(i, '*');
            Console.WriteLine();
            func(n - 2, i + 1);
            str(i, '*');
            str(n, ' ');
            str(i, '*');
            Console.WriteLine();
        }
    }
}