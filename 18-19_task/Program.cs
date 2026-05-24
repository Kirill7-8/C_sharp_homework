[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Newtonsoft.Json")]

namespace MyProgram;

using System;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main()
    {
        
        Function[] testFunctions =
    {
            new Line(3, 4),
            new Kub(1, -2, 1),
            new Hyperbola(5, 2),
            new Line(-1, 7)
    };
        string jsonFile = "functions.json";
        string resultsFile = "results.txt";
        double x = 2.0;

        Function[] functions;

        if (!File.Exists(jsonFile) || new FileInfo(jsonFile).Length == 0)
        {
            Console.WriteLine("JSON файл не существует или пуст. Создаём с тестовыми функциями.");
            functions = testFunctions;
            SaveToJson(functions, jsonFile);
        }
        else
        {
            Console.WriteLine("JSON файл существует. Загружаем функции.");
            functions = LoadFromJson(jsonFile);
            if (functions == null)
            {
                Console.WriteLine("Используем тестовые.");
                functions = testFunctions;
            }
        }
        functions.Sort();

        WriteResultsToFile(functions, x, resultsFile);
    }

    

    static void SaveToJson(Function[] functions, string path)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };
        string json = JsonConvert.SerializeObject(functions, settings);
        File.WriteAllText(path, json);
    }

    static Function[]? LoadFromJson(string path)
    {
        if (!File.Exists(path)) return null;
        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json)) return null;
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        return JsonConvert.DeserializeObject<Function[]>(json, settings);
    }

    static void WriteResultsToFile(Function[] functions, double x, string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine($"Значения функций в точке x = {x}");
            foreach (var f in functions)
            {
                try
                {
                    double value = f.getValue(x);
                    writer.WriteLine($"{f}  =>  f({x}) = {value}");
                }
                catch (Exception ex)
                {
                    writer.WriteLine($"{f}  =>  Ошибка: {ex.Message}");
                }
            }
        }
    }
}