using System;
using System.ComponentModel;

namespace Example;
public class AVLTree
{
    public AVLNode Root;
    private List<int> deletedValues = new List<int>();

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

    if (balance > 1)
    {
        if (value < node.Left.Value)
            return RotateRight(node);           
        else
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);          
        }
    }
    else if (balance < -1)
    {
        if (value >= node.Right.Value)
            return RotateLeft(node);         
        else
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);           
        }
    }

    return node;
}
    

    private int GetHeight(AVLNode node)
    {
        return node == null ? 0 : node.Height;
    }


   
   
private void UpdateNode(AVLNode node)
{
    if (node == null) return;
    node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);
}
private AVLNode RemoveDeepestLeaf(AVLNode node)
{
    if (node.Left == null && node.Right == null)
    {
        Console.WriteLine($"  Удаляем лист {node.Value}");
        return null;
    }


    if (GetCount(node.Right) >= GetCount(node.Left))
    {
        node.Right = RemoveDeepestLeaf(node.Right);
    }
    else
    {
        node.Left = RemoveDeepestLeaf(node.Left);
    }
    UpdateNode(node);
    return Balance(node);
}

    

public void Delete(int value)
{
    Root = Delete(Root, value);
}

private AVLNode Delete(AVLNode node, int value)
{
    if (node == null) return null;

    if (value < node.Value)
        node.Left = Delete(node.Left, value);
    else if (value > node.Value)
        node.Right = Delete(node.Right, value);
    else
    {
        // узел найден
        if (node.Left == null) return node.Right;
        if (node.Right == null) return node.Left;

        // два потомка – заменяем на минимальный из правого поддерева
        AVLNode minNode = FindMin(node.Right);
        node.Value = minNode.Value;
        node.Right = Delete(node.Right, minNode.Value);
    }

    UpdateNode(node);
    return Balance(node);
}

private AVLNode FindMin(AVLNode node)
{
    while (node.Left != null) node = node.Left;
    return node;
}
public bool IsPerfectlyBalanced()
{
    return IsPerfectlyBalanced(Root);
}

private bool IsPerfectlyBalanced(AVLNode node)
{
    if (node == null) return true;
    int diff = Math.Abs(GetCount(node.Left) - GetCount(node.Right));
    if (diff > 1) return false;
    return IsPerfectlyBalanced(node.Left) && IsPerfectlyBalanced(node.Right);
}
public AVLNode FindDeepestInHeavySubtree()
{
    return FindDeepestInHeavySubtree(Root);
}

private AVLNode FindDeepestInHeavySubtree(AVLNode node)
{
    if (node == null) return null;
    while (node.Left != null || node.Right != null)
    {
        int leftCount = GetCount(node.Left);
        int rightCount = GetCount(node.Right);

        if (leftCount > rightCount)
            node = node.Left;
        else 
            node = node.Right;
        
    }
    return node;
}
public List<int> RemoveUpToN(int n)
{
    var removed = new List<int>();
    int attempts = 0;

    while (attempts < n && !IsPerfectlyBalanced())
    {
        AVLNode deepest = FindDeepestInHeavySubtree();
        if (deepest == null) break; // на всякий случай

        int value = deepest.Value;
        Delete(value);
        removed.Add(value);
        attempts++;
    }

    if (!IsPerfectlyBalanced())
    {
        Console.WriteLine($"Не удалось достичь идеального баланса за {n} удалений");
    }
    else
    {
        Console.WriteLine($"Достигнут идеальный баланс за {removed.Count} удалений");
    }

    return removed;
}

private AVLNode Balance(AVLNode node)
{
    int balance = GetBalance(node);

    if (balance > 1)
    {
        if (GetBalance(node.Left) >= 0)
            return RotateRight(node);
        else
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
    }
    if (balance < -1)
    {
        if (GetBalance(node.Right) <= 0)
            return RotateLeft(node);
        else
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
    }
    return node;
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