namespace Example;

class Program
{
    
    static void Main(string[] args)
    {
        string file = "input2.txt";

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
        var deletedValues = tree.Task();
        int countDeletedValues = deletedValues.Count;
        if (countDeletedValues == 0) 
        {
            Console.WriteLine("Дерево уже идельно сбалансированно");
        }
        else
        {
            if (countDeletedValues > maxRemovals)
            {
                Console.WriteLine("Невозможно с помощью {0} удалений, превратить АВЛ дерево в ИСБ дерево", maxRemovals);
                Console.WriteLine("Необходимо как минимум {0} удалений узлов", countDeletedValues);
                Console.WriteLine("Узлы, которые как миниум необходимо удалить: ");
                foreach (int n in deletedValues)
                {
                    Console.WriteLine(n);
                }
            }
            else
            {
                Console.WriteLine("Удалив не более {0} узлов можно превратить АВЛ дерево в ИСБ дерево", maxRemovals);
                Console.WriteLine("Узлы, которые как минимум необходимо удалить: ");
                foreach (int n in deletedValues)
                {
                    Console.WriteLine(n);
                }
            }
        }
    }
}