using System;
using System.IO;

namespace Program1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "input.txt";
            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Файл input.txt не найден!");
                return;
            }

            string inputContent = File.ReadAllText(inputFile);
            string[] parts = inputContent.Split(new char[] { ' ', '\n', '\r', '\t', '!', ',','.','?' }, StringSplitOptions.RemoveEmptyEntries);

            if (!int.TryParse(parts[0], out int x))
            {
                Console.WriteLine("Первая строка не является корректным целым числом для x!");
                return;
            }

            LinkedList list = new LinkedList();
            for (int i = 1; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int num))
                {
                    list.PushBack(num);
                }
            }

            string outputFile = "output.txt";
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                int maxVal = list.FindMax();

                writer.WriteLine($"Максимальное число: {maxVal}");

                list.Print(writer);

                list.InsertBeforeMax(maxVal, x);

                list.Print(writer);
            }

            Console.WriteLine("Обработка завершена. Результат записан в файл output.txt");
        }
    }
}

      