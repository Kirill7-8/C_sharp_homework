//Найти все самые длинные слова сообщения
using System.Security.Principal;

class Program
{
    static void Main()
    {
        String s = Console.ReadLine().Replace(" ","");
        char[] separator = { ' ', ',', '.', '\t', '\r', '?', '!' };
        int maxLength = 0;
        

        foreach (String word in s.Split(separator))
        {
            if (word.Length > maxLength) maxLength = word.Length;
        }
        foreach (String word1 in s.Split(separator))
        {
            if (word1.Length == maxLength) Console.WriteLine(word1);
        }  
    }
}