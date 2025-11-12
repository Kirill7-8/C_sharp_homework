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
        char[] separator = {' ',',', '\n', '\t', '\r'};
        int index = 1;
        using (StreamReader fileIn1 = new StreamReader(file1, Encoding.GetEncoding(1251)))
        {
            using (StreamReader fileIn2 = new StreamReader(file2, Encoding.GetEncoding(1251)))
            {
                string[] file1array = fileIn1.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                string[] file2array = fileIn2.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                int file1Length = file1array.Length;
                int file2Length = file2array.Length;
                
                for (int i = 0; i < Math.Min(file1Length, file2Length); i++)
                {
                    if (!file1array[i].Equals(file2array[i])) return index;
                    index++;
                }
                if (file1Length != file2Length) return index;
            }
        }

        return -1;
    }
}