namespace Example;

class Program{
    static void Main()
    {
        Graph g = new Graph("input.txt");
        g.HamiltonianCycle();
    }
}
