namespace Ex21_13;

public class AVLNode
{
    public int Value;
    public AVLNode Left;
    public AVLNode Right;
    public int Height;
    public int Count; 

    public AVLNode(int value)
    {
        Value = value;
        Height = 1;
        Count = 1;
    }
}