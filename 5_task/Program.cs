//Разработать метод, который для заданного натурального числа N возвращает сумму его
//нечетных цифр. С помощью данного метода:

using System;
class Summator
{
    static void Main()
    {
        Console.WriteLine("а) Для каждого целого числа на отрезке [a, b] вывести на экран сумму его нечетных цифр;");
        Console.WriteLine("Введите начало отрезка: ");
        int a = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите конец отрезка: ");
        int b = int.Parse(Console.ReadLine());
        for (int i = a; i <= b; i++)
        {
            Console.WriteLine($"Число: {i}\n Сумма нечетных цифр: {summator(i)}");
        }
        separator();

        Console.WriteLine("вывести на экран только те целые числа отрезка [a, b], у которых сумма нечетных цифр числа равна заданному значению С");       
        int C = int.Parse(Console.ReadLine());
        for (int i = a; i <= b; i++){
            if (C == summator(i)){
                Console.WriteLine($"Число: {i}\n Сумма нечетных цифр: {summator(i)}");
            }
        } 
        separator();

        Console.WriteLine("вывести на экран только те целые числа отрезка [a, b], у которых сумма нечетных цифр является простым числом");
        for (int i = a; i <= b; i++){
            if (is_prime(summator(i))){
                Console.WriteLine($"Число: {i}\n Сумма нечетных цифр: {summator(i)}");
            }  
        }
        separator();

        Console.WriteLine("для заданного числа А вывести на экран ближайшее следующее по отношению к нему число, сумма нечетных цифр которого равна сумме нечетных цифр числа А");
        int A = int.Parse(Console.ReadLine());
        int B = A + 1;
        while (true){
            if (summator(A) == summator(B)) 
            {
                Console.WriteLine(B);
                break;
            }
            B = B + 1;
        }
    }

    static void separator(){
        Console.WriteLine(new string('-', 100));
    }
    static bool is_prime(int num){
        if (num < 2) return false;
        for (int i = 2; i * i <= num; i++){
            if (num % i == 0){
                return false;
            }
        }
        return true;
    }

    static int summator(int n)
    {
        int result_sum = 0;
        int num = Math.Abs(n);
        while (num != 0)
        {
            int last_digit = num % 10;
            if (last_digit % 2 != 0)
            {
                result_sum += last_digit;
            }
            num /= 10;
        }
        return result_sum;
    }
}