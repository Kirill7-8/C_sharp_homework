namespace Example;

class Program
{
    
    static void Main(string[] args)
{
    string file = "input3.txt";
    var parts = File.ReadAllText(file)
        .Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();

    int n = parts[0];       
    var numbers = parts.Skip(1).ToList();

    AVLTree tree = new AVLTree();
    foreach (int num in numbers)
        tree.Insert(num);

 

    var removed = tree.RemoveUpToN(n);

    Console.WriteLine("Удалённые узлы: " + string.Join(", ", removed));

    
}
}