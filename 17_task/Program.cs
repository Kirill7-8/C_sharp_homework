using System;
using System.Collections.Generic;
using System.IO;

class Triangle
{
    private int a, b, c;
//
    public int A
    {
        get { return a; }
        set { a = value; }
    }
    public int B
    {
        get { return b; }
        set { b = value; }
    }
    public int C
    {
        get { return c; }
        set { c = value; }
    }
//
    private bool IsExist
    {
        get
        {
            return (a + b > c && a + c > b && b + c > a);
        }
    }

  
    public Triangle()
    {
        a = b = c = 1;
    }

    //
    public Triangle(int a, int b, int c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
//
        if (!IsExist)
        {
            Console.WriteLine("Ошибка: треугольник с такими сторонами не существует.");
            this.a = this.b = this.c = 1;
        }
    }

    public Triangle(Triangle t)
    {
        a = t.a;
        b = t.b;
        c = t.c;
    }


    public int Perimeter()
    {
        return a + b + c;
    }

    public double Area()
    {
        double p = Perimeter() / 2.0;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }


    public void Scale(int k)
    {
        a *= k;
        b *= k;
        c *= k;
    }
    
    public override string ToString()
    {
        return $"Triangle: a={a}, b={b}, c={c}";
    }

    public override int GetHashCode()
    {
        return a ^ b ^ c;
    }
//
    public override bool Equals(object obj)
    {
        if (obj is Triangle t)
            return a == t.a && b == t.b && c == t.c;

        return false;
    }

    
    public int this[int index]
    {
        get
        {
            if (index == 0) return a;
            if (index == 1) return b;
            if (index == 2) return c;
            throw new Exception("Неверный индекс! Допустимо 0..2");
        }
        set
        {
            if (index == 0) a = value;
            else if (index == 1) b = value;
            else if (index == 2) c = value;
            else throw new Exception("Неверный индекс! Допустимо 0..2");
        }
    }

//
    public static Triangle operator ++(Triangle t)
    {
        t.a++; t.b++; t.c++;
        return t;
    }
//
    public static Triangle operator --(Triangle t)
    {
        t.a--; t.b--; t.c--;
        return t;
    }

    
    public static bool operator true(Triangle t)
    {
        return t.IsExist;
    }

    public static bool operator false(Triangle t)
    {
        return !t.IsExist;
    }

    
    public static Triangle operator *(Triangle t, int k)
    {
        return new Triangle(t.a * k, t.b * k, t.c * k);
    }
}

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
        list.Add(new Triangle(list[0]) * 2); 

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

