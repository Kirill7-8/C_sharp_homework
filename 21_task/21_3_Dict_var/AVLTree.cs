namespace Ex21_13;

public class AVLTree
{
    public AVLNode Root;

    public void Insert(int value)
    {
        Root = Insert(Root, value);
    }

    private AVLNode Insert(AVLNode node, int value)
    {
        if (node == null) 
            return new AVLNode(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else
            node.Right = Insert(node.Right, value);

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

        int balance = GetBalance(node);

        if (balance > 1 && value < node.Left.Value)
            return RotateRight(node);
        if (balance < -1 && value > node.Right.Value)
            return RotateLeft(node);
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }

    private int GetHeight(AVLNode node)
    {
        return node == null ? 0 : node.Height;
    }

    private int GetCount(AVLNode node)
    {
        return node == null ? 0 : node.Count;
    }

    private int GetBalance(AVLNode node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }

    private AVLNode RotateRight(AVLNode y)
    {
        AVLNode x = y.Left;
        AVLNode T2 = x.Right;
        x.Right = y;
        y.Left = T2;

        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

        y.Count = 1 + GetCount(y.Left) + GetCount(y.Right);
        x.Count = 1 + GetCount(x.Left) + GetCount(x.Right);

        return x;
    }

    private AVLNode RotateLeft(AVLNode x)
    {
        AVLNode y = x.Right;
        AVLNode T2 = y.Left;
        y.Left = x;
        x.Right = T2;

        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        x.Count = 1 + GetCount(x.Left) + GetCount(x.Right);
        y.Count = 1 + GetCount(y.Left) + GetCount(y.Right);

        return y;
    }
}