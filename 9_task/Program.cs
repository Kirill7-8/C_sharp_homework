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
        int counter = 1;
        using (StreamReader fileIn1 = new StreamReader(file1, Encoding.GetEncoding(1251)))
        {
            using (StreamReader fileIn2 = new StreamReader(file2, Encoding.GetEncoding(1251)))
            {
                string line1;
                string line2;
                while ((line1 = fileIn1.ReadLine()) != null && (line2 = fileIn2.ReadLine()) != null)
                {
                    if (line1 != line2) return counter;
                    counter++;
                }
            }
        }

        return -1;
    }
}