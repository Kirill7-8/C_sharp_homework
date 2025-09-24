class Program
{
    static void Main()
    {
        for (int i = 11; i <= 99; i++)
        {
            if ((i % 10) % 2 != 0 & (i / 10) % 2 != 0)
            {
                Console.WriteLine(i);
            }
        }
    }
}
