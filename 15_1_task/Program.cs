//Вывести на экран все отрицательные трехзначные числа, заменив их на противоположное значение

using System;


class Program
{
    public static int[] Input(string file_name)
    {
        using (StreamReader FileIn = new StreamReader(file_name))
        {
            char[] sep = {' ', ',', '.', ';','\n','\r'};
            string[] file = FileIn.ReadToEnd().Split(sep, StringSplitOptions.RemoveEmptyEntries);
            return Array.ConvertAll(file, int.Parse);
        }
    }
   
    public static void Main()
    {
        string file_name = "C:/Users/Kirill_WinLap/C#_projects/15_1_task/Input.txt";
        int[] numbers_array = Input(file_name);
        var numbers =
            from num in numbers_array
            where  !(num <= -100 && num >= -999)
            select Math.Abs(num);
        
    
    foreach (var item in numbers)
    {
        Console.Write("{0} ", item);
    }
    
}
}