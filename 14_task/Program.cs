using System;
using System.IO;

class Program
{
    struct SPoint
    {
        public int x;
        public int y;
        public int z;

        public SPoint(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void Show()
        {
            Console.WriteLine("({0}, {1}, {2})", x, y, z);
        }
    }
    
    static bool isInSphere(SPoint center_point, int Radius, SPoint point)
    {
        double Ox = Math.Pow(center_point.x - point.x, 2);
        double Oy = Math.Pow(center_point.y - point.y, 2);
        double Oz = Math.Pow(center_point.z - point.z, 2); 
        return Math.Sqrt(Ox + Oy + Oz) <= Radius;
    }
    
    static SPoint[] Input(string FileName)
    {
        using (StreamReader fileIn = new StreamReader(FileName))
        {
            int n = int.Parse(fileIn.ReadLine());
            SPoint[] arr = new SPoint[n];
            for (int i = 0; i < n; i++)
            {
                string[] str = fileIn.ReadLine().Split(' ');
                arr[i] = new SPoint(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]));
            }
            return arr;
        }
    }
    
    static void findMin(SPoint[] points, int Radius)
    {
        int n = points.Length;
        int[] pointCounts = new int[n];

        for (int i = 0; i < n; i++)
        {
            int current_points = 0;
            foreach (SPoint point2 in points)
            {
                if (isInSphere(points[i], Radius, point2)) 
                    current_points++;
            }
            pointCounts[i] = current_points;
        }
        

        int min_point = pointCounts[0];
        for (int i = 1; i < n; i++)
        {
            if (pointCounts[i] < min_point)
            {
                min_point = pointCounts[i];
            }
        }
        

        Console.WriteLine("Минимальные точки (количество точек в сфере = {0}):", min_point);
        for (int i = 0; i < n; i++)
        {
            if (pointCounts[i] == min_point)
            {
                Console.Write("Точка: ");
                points[i].Show();
                Console.WriteLine("Количество точек в сфере радиуса {0}: {1}", Radius, min_point);
                Console.WriteLine();
            }
        }
    }
    
    static void Main()
    {
        string FileName = "C:/Users/Kirill_WinLap/C#_projects/14_task/test.txt";
        Console.Write("Введите радиус: ");
        int Radius = int.Parse(Console.ReadLine());
        SPoint[] InPoints = Input(FileName);
        findMin(InPoints, Radius);
    }
}