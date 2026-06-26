namespace Ex21_13;

class Program
{
    static void Main(string[] args)
    {

        string file = "C:\\Users\\linag\\RiderProjects\\Ex21-13\\Ex21-13\\input.txt";
        if (!File.Exists(file))
        {
            Console.WriteLine("Файл input.txt не найден.");
            return;
        }

        var parts = File.ReadAllText(file)
            .Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s)).ToList();
        if (parts.Count < 2)
        {
            Console.WriteLine("Некорректный формат входных данных.");
            return;
        }

        int maxRemovals = parts[0];
        var numbers = parts.Skip(1).ToList();

        AVLTree tree = new AVLTree();
        foreach (var num in numbers)
            tree.Insert(num);

        BalanceDP dpSolver = new BalanceDP(tree.Root);
        var states = dpSolver.Process(tree.Root);

        DPState best = null;
        foreach (var state in states.Values)
        {
            if (state.KeptCount > 0 && (best == null || state.Removals < best.Removals))
                best = state;
        }

        if (best != null && best.Removals <= maxRemovals)
        {
            Console.WriteLine("Можно добиться идеально сбалансированного BST, удалив {0} узлов.", best.Removals);
            Console.WriteLine("Удаляемые узлы: {0}", string.Join(", ", best.DeletedNodes));
        }
        else
        {
            Console.WriteLine("Невозможно получить идеально сбалансированное BST при удалении не более {0} узлов.",
                maxRemovals);
        }
    }
}