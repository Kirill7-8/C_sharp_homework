using System;
using System.Collections.Generic;
using System.IO;

namespace Example
{
    public class Graph
    {
        private class Node
        {
            private int[,] array;

            public int this[int i, int j]
            {
                get { return array[i, j]; }
                set { array[i, j] = value; }
            }

            public int Size
            {
                get { return array.GetLength(0); }
            }

            private bool[] nov;

            public void NovSet()
            {
                for (int i = 0; i < Size; i++)
                {
                    nov[i] = true;
                }
            }

            public void AddDirectedEdge(int a, int b, int weight = 1)
            {
                if (a < 0 || a >= Size || b < 0 || b >= Size)
                    throw new ArgumentOutOfRangeException("Вершина выходит за пределы графа");
                
                if (weight <= 0)
                    throw new ArgumentException("Вес дуги должен быть положительным");
                
                array[a, b] = weight;
            }
            public Node(int[,] a)
            {
                if (a == null)
                    throw new ArgumentNullException(nameof(a));
                array = a;
                nov = new bool[a.GetLength(0)];
            }

            // Обход в глубину (DFS)
            public void Dfs(int v)
            {
                Console.Write("{0} ", v);
                nov[v] = false;

                for (int u = 0; u < Size; u++)
                {
                    if (array[v, u] != 0 && nov[u])
                    {
                        Dfs(u);
                    }
                }
            }

            // Обход в ширину (BFS)
            public void Bfs(int v)
            {
                Queue<int> q = new Queue<int>();
                q.Enqueue(v);
                nov[v] = false;

                while (q.Count > 0)
                {
                    v = q.Dequeue();
                    Console.Write("{0} ", v);

                    for (int u = 0; u < Size; u++)
                    {
                        if (array[v, u] != 0 && nov[u])
                        {
                            q.Enqueue(u);
                            nov[u] = false;
                        }
                    }
                }
            }

            // Алгоритм Дейкстры
            public long[] Dijkstra(int v, out int[] p)
            {
                nov[v] = false;

                // Матрица весов c
                long[,] c = new long[Size, Size];
                for (int i = 0; i < Size; i++)
                {
                    for (int u = 0; u < Size; u++)
                    {
                        if (array[i, u] == 0 || i == u)
                        {
                            c[i, u] = long.MaxValue;
                        }
                        else
                        {
                            c[i, u] = array[i, u];
                        }
                    }
                }

                long[] d = new long[Size];
                p = new int[Size];

                for (int u = 0; u < Size; u++)
                {
                    if (u != v)
                    {
                        d[u] = c[v, u];
                        p[u] = v;
                    }
                }
                d[v] = 0;

                for (int i = 0; i < Size - 1; i++)
                {
                    long min = long.MaxValue;
                    int w = -1;

                    for (int u = 0; u < Size; u++)
                    {
                        if (nov[u] && min > d[u])
                        {
                            min = d[u];
                            w = u;
                        }
                    }

                    if (w == -1) break;

                    nov[w] = false;

                    for (int u = 0; u < Size; u++)
                    {
                        if (c[w, u] == long.MaxValue) continue;

                        long distance = d[w] + c[w, u];
                        if (nov[u] && d[u] > distance)
                        {
                            d[u] = distance;
                            p[u] = w;
                        }
                    }
                }

                return d;
            }

            // Восстановление пути для Дейкстры
            public void WayDijkstra(int a, int b, int[] p, ref Stack<int> items)
            {
                items.Push(b);
                if (a == p[b])
                {
                    items.Push(a);
                }
                else
                {
                    WayDijkstra(a, p[b], p, ref items);
                }
            }

            // Алгоритм Флойда
            public long[,] Floyd(out int[,] p)
            {
                int i, j, k;
                long[,] a = new long[Size, Size];
                p = new int[Size, Size];

                for (i = 0; i < Size; i++)
                {
                    for (j = 0; j < Size; j++)
                    {
                        if (i == j)
                        {
                            a[i, j] = 0;
                        }
                        else if (array[i, j] == 0)
                        {
                            a[i, j] = long.MaxValue;
                        }
                        else
                        {
                            a[i, j] = array[i, j];
                        }
                        p[i, j] = -1;
                    }
                }

                for (k = 0; k < Size; k++)
                {
                    for (i = 0; i < Size; i++)
                    {
                        if (a[i, k] == long.MaxValue) continue;

                        for (j = 0; j < Size; j++)
                        {
                            if (a[k, j] == long.MaxValue) continue;

                            long distance = a[i, k] + a[k, j];
                            if (a[i, j] > distance)
                            {
                                a[i, j] = distance;
                                p[i, j] = k;
                            }
                        }
                    }
                }

                return a;
            }

            // Восстановление пути для Флойда
            public void WayFloyd(int a, int b, int[,] p, ref Queue<int> items)
            {
                int k = p[a, b];
                if (k != -1)
                {
                    WayFloyd(a, k, p, ref items);
                    items.Enqueue(k);
                    WayFloyd(k, b, p, ref items);
                }
            }
        }

        private Node graph;

        public Graph(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя файла не может быть пустым");

            using (StreamReader file = new StreamReader(name))
            {
                string firstLine = file.ReadLine();
                if (firstLine == null)
                    throw new InvalidDataException("Файл пуст");

                int n = int.Parse(firstLine);
                int[,] a = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string line = file.ReadLine();
                    if (line == null)
                        throw new InvalidDataException("Недостаточно строк в файле");

                    string[] mas = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < n; j++)
                    {
                        a[i, j] = int.Parse(mas[j]);
                    }
                }
                graph = new Node(a);
            }
        }
        public void AddDirectedEdge(int a, int b, int weight = 1)
        {
            graph.AddDirectedEdge(a, b , weight);
        }
        public void Show()
        {
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    Console.Write("{0,4}", graph[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Dfs(int v)
        {
            if (v < 0 || v >= graph.Size)
                throw new ArgumentOutOfRangeException(nameof(v));

            graph.NovSet();
            graph.Dfs(v);
            Console.WriteLine();
        }

        public void Bfs(int v)
        {
            if (v < 0 || v >= graph.Size)
                throw new ArgumentOutOfRangeException(nameof(v));

            graph.NovSet();
            graph.Bfs(v);
            Console.WriteLine();
        }

        public void Dijkstra(int v)
        {
            if (v < 0 || v >= graph.Size)
                throw new ArgumentOutOfRangeException(nameof(v));

            graph.NovSet();
            int[] p;
            long[] d = graph.Dijkstra(v, out p);

            Console.WriteLine("Кратчайшие пути от вершины {0}:", v);
            for (int i = 0; i < graph.Size; i++)
            {
                if (i != v)
                {
                    Console.Write("  до вершины {0} длина = {1}, путь: ", i, d[i] == long.MaxValue ? "∞" : d[i].ToString());
                    if (d[i] != long.MaxValue)
                    {
                        Stack<int> items = new Stack<int>();
                        graph.WayDijkstra(v, i, p, ref items);
                        while (items.Count > 0)
                        {
                            Console.Write("{0} ", items.Pop());
                        }
                    }
                    else
                    {
                        Console.Write("пути не существует");
                    }
                    Console.WriteLine();
                }
            }
        }

        public void Floyd()
        {
            int[,] p;
            long[,] a = graph.Floyd(out p);

            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    if (i != j)
                    {
                        if (a[i, j] == long.MaxValue)
                        {
                            Console.WriteLine("Пути из вершины {0} в вершину {1} не существует", i, j);
                        }
                        else
                        {
                            Console.Write("Кратчайший путь из {0} в {1} длина = {2}, путь: ", i, j, a[i, j]);
                            Queue<int> items = new Queue<int>();
                            items.Enqueue(i);
                            graph.WayFloyd(i, j, p, ref items);
                            items.Enqueue(j);

                            while (items.Count > 0)
                            {
                                Console.Write("{0} ", items.Dequeue());
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}