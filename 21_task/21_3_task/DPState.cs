namespace Example;

public class DPState
{
    public int KeptCount;
    public int Removals;    
    public List<int> DeletedNodes;

    public DPState(int keptCount, int removals, List<int> deletedNodes)
    {
        KeptCount = keptCount;
        Removals = removals;
        DeletedNodes = deletedNodes;
    }
}