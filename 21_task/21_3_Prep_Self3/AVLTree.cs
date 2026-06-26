using System;
using System.ComponentModel;

namespace Example;
public class AVLTree
{
    public AVLNode Root;
    List<int> deletedValues = new List<int>();

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
    
    public void FixBalanceByCount()
    {
        Root = FixNode(Root);
    }

    private AVLNode FixNode(AVLNode node)
    {
        if (node == null) return null;

        node.Left = FixNode(node.Left);
        node.Right = FixNode(node.Right);

        UpdateNode(node);

        while (Math.Abs(GetCount(node.Left) - GetCount(node.Right)) > 1)
        {
            if (GetCount(node.Left) > GetCount(node.Right))
                node.Left = RemoveDeepestLeaf(node.Left);
            else
                node.Right = RemoveDeepestLeaf(node.Right);

            UpdateNode(node);
            node = Balance(node);
        }

        return node;
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
            deletedValues.Add(node.Value);
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

    
    public List<int> Task()
    {
        FixBalanceByCount();
        return deletedValues;
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