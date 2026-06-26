namespace Example;

class Program
{
    
    static void Main(string[] args)
    {
        string file = "input3.txt";

        if (!File.Exists(file))
        {
            Console.WriteLine("Файл input.txt не найден.");
            return;
        }

        var parts = File.ReadAllText(file)
            .Split(new char[] { ' ', '\n', '\r' },
                   StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        if (parts.Count < 2)
        {
            Console.WriteLine("Некорректный формат входных данных.");
            return;
        }

        int maxRemovals = parts[0];

        var numbers = parts.Skip(1).ToList();

        AVLTree tree = new AVLTree();

        foreach (int num in numbers){
            tree.Insert(num);
        }
        tree.Task();
    }
}