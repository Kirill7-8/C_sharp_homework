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
        set { 
            if (value > 0) {
                a = value;
            }
            else
            {
                throw new ArgumentException("Сторона не может быть отрицательной или нулевой!");
            } 
            }
    }
    public int B
    {
        get { return b; }
        set { 
            if (value > 0) {
            b = value;
            }
            else
            {
                throw new ArgumentException("Сторона не может быть отрицательной или нулевой!");
            } 
            }
    }
    public int C
    {
        get { return c; }
        set { 
            if (value > 0) {
            c = value;
            }
            else
            {
                throw new ArgumentException("Сторона не может быть отрицательной или нулевой!");
            } 
            }
    }
//
    private bool IsExist(int a, int b, int c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

  
    public Triangle()
    {
        a = b = c = 1;
    }

    //
    public Triangle(int a, int b, int c)
    {
        if (IsExist(a, b, c))
        {
            A = a;
            B = b;
            C = c;
        }
        else
        {
            throw new ArgumentException("Не может существовать треугольника с такими сторонами!");
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

//
    public void Scale(int k)
    {
        if (k > 0)
        {
        A *= k;
        B *= k;
        C *= k;
        }
        else
        {
            throw new ArgumentException("Скаляр не может быть меньше или равен нулю!!");
        }
    }
    public Triangle Scaled(int k)
    {
    if (k <= 0) throw new ArgumentException("Скаляр должен быть больше 0!");
    return new Triangle(a * k, b * k, c * k);
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
    {
        int[] sides1 = [a, b, c];
        int[] sides2 = [t.a, t.b, t.c];

        Array.Sort(sides1);
        Array.Sort(sides2);

        return sides1[0] == sides2[0] &&
               sides1[1] == sides2[1] &&
               sides1[2] == sides2[2];
    }

    return false;
    }


    
    public int this[int index]
    {
        get
        {
            if (index == 0) return a;
            if (index == 1) return b;
            if (index == 2) return c;
            throw new Exception("Неверный индекс!");
        }
        set
        {
            if (index == 0) a = value;
            else if (index == 1) b = value;
            else if (index == 2) c = value;
            else throw new Exception("Неверный индекс!");
        }
    }

//
    public static Triangle operator ++(Triangle t)
    {
        Triangle obj = new Triangle(t);
        obj.A += 1;
        obj.B += 1;
        obj.C += 1;
        return obj;
    }
//
    public static Triangle operator --(Triangle t)
    {
        Triangle obj = new Triangle(t);
        obj.A -= 1;
        obj.B -= 1;
        obj.C -= 1;
        return obj;
    }

    
    public static bool operator true(Triangle t)
    {
        return t.IsExist(t.a, t.b, t.c);
    }

    public static bool operator false(Triangle t)
    {
        return !t.IsExist(t.a, t.b, t.c);
    }

    
    public static Triangle operator *(Triangle t, int k)
    {
        if (k > 0){
        return new Triangle(t.a * k, t.b * k, t.c * k);
        }
        else
        {
            throw new ArgumentException("Скаляр должен быть больше нуля!");
        }
    }
    public static Triangle operator *(int k, Triangle t)
    {
        return t * k;
    }
}