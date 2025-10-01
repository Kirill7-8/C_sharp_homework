class Stars
{
    static void Main()
    {
        Console.WriteLine("Введите n: ");
        //int n = int.Parse(Console.ReadLine());
        int n = 12;
        func(n, 0);
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
            str(i * 2 + 2, '*');
            Console.WriteLine();
        }
        if (n > 0)
        {
            str(i + 1, '*');
            str(n, ' ');
            str(i + 1, '*');
            Console.WriteLine();
            func(n - 2, i + 1);
            str(i + 1, '*');
            str(n, ' ');
            str(i + 1, '*');
            Console.WriteLine();
        }
    }
}