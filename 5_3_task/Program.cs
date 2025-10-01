//для перевода числа из двоичной системы счисления в десятичную
using System;
using System.ComponentModel;
class Converter
{
    static void Main()
    {
        Console.WriteLine("Введите число в двоичной СС: ");
        long num = long.Parse(Console.ReadLine());

        Console.WriteLine(binary_converter(num));
        Console.WriteLine(binary_converter_old(num));
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
