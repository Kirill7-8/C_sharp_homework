using System;

using Example;


class Program
{
    static void Main()
    {
        Graph g = new Graph("graph.txt");
        g.AddDirectedEdge(5, 1);
        g.Show();
    }
}