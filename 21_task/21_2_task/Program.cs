using Example;

class Program
{
    
    static void Main()
    {
        
        string input = "input.txt";
        string output = "output.txt";
        BinaryTree example = new BinaryTree();
        int[] numbers = Input(input);
        foreach(int num in numbers)
        {
            example.Add(num);
        }
        int level = int.Parse(Console.ReadLine());
        Console.WriteLine(example.NodeCountOnLevel(level));
        
    }
    public static int[] Input(string file_name)
    {
        using (StreamReader FileIn = new StreamReader(file_name))
        {
            char[] sep = {' ', ',', '.', ';','\n','\r','?','!'};
            string[] file = FileIn.ReadToEnd().Split(sep, StringSplitOptions.RemoveEmptyEntries);
            return Array.ConvertAll(file, int.Parse);
        }
    }
    }