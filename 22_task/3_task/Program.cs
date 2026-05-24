namespace Example;

class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");

        int N = int.Parse(lines[0]);
        var cities = new List<City>();
        var nameToIndex = new Dictionary<string, int>();

        for (int i  = 0; i < N; i++)
        {
            var parts = lines[1 + i].Split(' ');
            var name = parts[0];
            int x = int.Parse(parts[1]);
            int y = int.Parse(parts[2]);
            cities.Add(new City { Name = name, X = x, Y = y, Index = i });
            nameToIndex[name] = i;
        }

        double[,] weights = new double[N, N];
        int matStart = 1 + N;
        for (int i = 0; i < N; i++)
        {
            var parts = lines[matStart + i].Split(' ');
            for (int j = 0; j < N; j++)
            {
                int val = int.Parse(parts[j]);
                if (val == 1)
                    weights[i, j] = Distance(cities[i], cities[j]);
                else
                    weights[i, j] = double.PositiveInfinity;
            }
        }

        Console.WriteLine("Введите название города A:");
        string cityA = Console.ReadLine();
        Console.WriteLine("Введите название города B:");
        string cityB = Console.ReadLine();
        Console.WriteLine("Введите названия запрещённых городов через пробел (или пустую строку):");
        string forbiddenInput = Console.ReadLine();
        var forbidden = new HashSet<int>();
        if (!string.IsNullOrWhiteSpace(forbiddenInput))
        {
            foreach (var f in forbiddenInput.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                if (nameToIndex.ContainsKey(f))
                    forbidden.Add(nameToIndex[f]);
        }

        int start = nameToIndex[cityA];
        int end = nameToIndex[cityB];
        forbidden.Remove(start); 
        forbidden.Remove(end);

        var path = Dijkstra(N, weights, start, end, forbidden);

        if (path == null)
            Console.WriteLine("Путь не найден!");
        else
        {
            Console.WriteLine("Кратчайший путь:");
            foreach (var idx in path)
                Console.Write($"{cities[idx].Name} ");
            double total = 0;
            for (int i = 1; i < path.Count; i++)
                total += weights[path[i - 1], path[i]];
            Console.WriteLine($"\nДлина пути: {total:F2}");
        }
    }

    static double Distance(City a, City b)
    {
        int dx = a.X - b.X, dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    static List<int> Dijkstra(int N, double[,] weights, int start, int end, HashSet<int> forbidden)
    {
        double[] dist = new double[N];
        int[] prev = new int[N];
        bool[] used = new bool[N];
        
        foreach (int forbiddenIndex in forbidden)
            used[forbiddenIndex] = true;
        for (int i = 0; i < N; i++) { dist[i] = double.PositiveInfinity; prev[i] = -1; }

        dist[start] = 0;

        for (int k = 0; k < N; k++)
        {
            int u = -1;
            double minDist = double.PositiveInfinity;
            for (int i = 0; i < N; i++)
            {
                if (!used[i] && dist[i] < minDist)
                {
                    minDist = dist[i];
                    u = i;
                }
            }
            if (u == -1) break;
            used[u] = true;
            for (int v = 0; v < N; v++)
            {
                if (!used[v] && weights[u, v] < double.PositiveInfinity)
                {
                    double newDist = dist[u] + weights[u, v];
                    if (newDist < dist[v])
                    {
                        dist[v] = newDist;
                        prev[v] = u;
                    }
                }
            }
        }

        if (dist[end] == double.PositiveInfinity)
            return null;

        var path = new List<int>();
        for (int v = end; v != -1; v = prev[v])
            path.Add(v);
        path.Reverse();
        return path;
    }
}