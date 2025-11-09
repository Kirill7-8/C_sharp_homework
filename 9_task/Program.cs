using System;
using System.IO;
using System.Text;


class Program
{
    static void Main()

    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        string FileName1 = "C:/Users/Kirill_WinLap/C#_projects/9_task/test1.txt";
        string FileName2 = "C:/Users/Kirill_WinLap/C#_projects/9_task/test2.txt";
        string OutFileName = "C:/Users/Kirill_WinLap/C#_projects/9_task/testOut.txt";

        
        using (StreamWriter fileOut = new StreamWriter(OutFileName, false))
        {
            int result = GetDifference(FileName1, FileName2);
            if (result == -1)
            {
                fileOut.WriteLine("Компоненты совпадают");
            }
            else
            {
                fileOut.WriteLine("Номер первого отличного элемента: {0}", result);
            }
            
        }

        

    }

    static int GetDifference(string file1, string file2)
    {
        char[] separator = {' ',',', '\n', '\t'};
        int index = 1;
        using (StreamReader fileIn1 = new StreamReader(file1, Encoding.GetEncoding(1251)))
        {
            using (StreamReader fileIn2 = new StreamReader(file2, Encoding.GetEncoding(1251)))
            {
                string[] file1array = fileIn1.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                string[] file2array = fileIn2.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in file1array)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("------");
                foreach (string s in file2array)
                {
                    Console.WriteLine(s);
                }
                
                int min_length = Math.Min(file1array.Length, file2array.Length);
                for(int i = 0; i < min_length; i++)
                {
    
                    if (!file1array[index].Equals(file2array[index])) return index;
                    index++;

                }
            }
        }

        return -1;
    }
}