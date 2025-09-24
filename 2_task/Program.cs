class Program
{
    static void Main()
    {
        string x = Console.ReadLine();
        bool y = (x[0] == x[1] & x[1] == x[2]) ? true : false;
        Console.WriteLine(y);
    }
}