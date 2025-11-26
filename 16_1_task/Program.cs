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
        string file_name = "C:/Users/Kirill_WinLap/C#_projects/16_1_task/Input.txt";
        int[] numbers_array = Input(file_name);
        var result = numbers_array.Where(num => num <= -100 && num >= -999).Select(Math.Abs);
        
    
    foreach (var item in result)
    {
        Console.Write("{0} ", item);
    }
    
}
}