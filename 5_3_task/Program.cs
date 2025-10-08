//для перевода числа из двоичной системы счисления в десятичную
using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
class Converter
{
    static void Main()
    {
        Console.WriteLine("Введите число в двоичной СС: ");
        long num = long.Parse(Console.ReadLine());
        if (check(num))
        {
            Console.WriteLine(binary_converter(num));
            Console.WriteLine(binary_converter_old(num));
        }
        else
        {
            Console.WriteLine("Ошибка!");
        }
    }
    static bool check(long num)
    {
        bool res = (num < 0) ? false : true;
        while (num > 0)
        {
            if (num % 10 > 1)
            {
                return false;
            }
            num /= 10;
        }
        return res;
     }
    static long binary_converter(long num)
    {
        if (num == 1) return 1L;
        if (num == 0) return 0L;
        return (binary_converter(num / 10) << 1) + binary_converter(num % 10);
    }
    static long binary_converter_old(long bin)
    {
        if (bin == 1) return 1;
        if (bin == 0) return 0;
        int digit_position = (int)Math.Log10(bin);
        int power = (int)Math.Pow(10, digit_position);
        return binary_converter_old(bin / power) * ((int)Math.Pow(2, digit_position)) + binary_converter_old(bin % power);
    }
}
