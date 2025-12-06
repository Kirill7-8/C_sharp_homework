

using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main()
    {
        string inputFile = "triangles.txt";
        string outputFile = "result.txt";

        List<Triangle> list = new List<Triangle>();

        if (File.Exists(inputFile))
        {
            string[] lines = File.ReadAllLines(inputFile);

            foreach (string line in lines)
            {
                string[] p = line.Split(' ');

                if (p.Length == 3)
                {
                    int a = int.Parse(p[0]);
                    int b = int.Parse(p[1]);
                    int c = int.Parse(p[2]);

                    list.Add(new Triangle(a, b, c));
                }
            }
        }

        list.Add(new Triangle(3, 4, 5));
        list.Add(new Triangle());
        list.Add(new Triangle(list[0])); 
        list.Add(new Triangle(--list[0]));
        Triangle x = 2 * list[0]; 
        Console.WriteLine(list[0] );
        Console.WriteLine(x);


        using (StreamWriter sw = new StreamWriter(outputFile))
        {
            foreach (Triangle t in list)
            {
                sw.WriteLine(t.ToString());
                sw.WriteLine("Периметр: " + t.Perimeter());
                sw.WriteLine("Площадь: " + t.Area());
                sw.WriteLine("Существует? " + (t ? "Да" : "Нет"));

                sw.WriteLine();
            }
        }

        Console.WriteLine("Готово! Результат записан в файл result.txt");
    }
}

