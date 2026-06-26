namespace Example;
public class AVLTree
{
    public AVLNode Root;

    /// <summary>
/// Преобразует дерево в идеально сбалансированное (по количеству узлов)
/// и возвращает количество удалённых узлов.
/// </summary>
public int MakePerfectlyBalanced()
{
    int totalBefore = Root?.Count ?? 0;
    Root = MakePerfectlyBalanced(Root);
    int totalAfter = Root?.Count ?? 0;
    return totalBefore - totalAfter;
}

private AVLNode MakePerfectlyBalanced(AVLNode node)
{
    if (node == null) return null;
    
    // Рекурсивно балансируем детей
    node.Left = MakePerfectlyBalanced(node.Left);
    node.Right = MakePerfectlyBalanced(node.Right);
    
    // Получаем максимальные размеры для детей (уже после их балансировки)
    int leftMax = node.Left?.Count ?? 0;
    int rightMax = node.Right?.Count ?? 0;
    
    int newLeftCount, newRightCount;
    
    if (Math.Abs(leftMax - rightMax) <= 1)
    {
        // Уже хорошо, ничего не отрезаем
        newLeftCount = leftMax;
        newRightCount = rightMax;
    }
    else if (leftMax > rightMax)
    {
        // Левое слишком велико – оставляем в нём только rightMax + 1 узлов
        newLeftCount = rightMax + 1;
        newRightCount = rightMax;
        node.Left = TrimSubtree(node.Left, newLeftCount);
    }
    else
    {
        // Правое слишком велико
        newRightCount = leftMax + 1;
        newLeftCount = leftMax;
        node.Right = TrimSubtree(node.Right, newRightCount);
    }
    
    // Обновляем поля узла (важно для корректной работы Count выше)
    node.Count = 1 + newLeftCount + newRightCount;
    node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    
    return node;
}

/// <summary>
/// Обрезает поддерево так, чтобы в нём осталось ровно need узлов
/// (включая корень). Возвращает новый корень обрезанного поддерева.
/// </summary>
private AVLNode TrimSubtree(AVLNode node, int need)
{
    if (node == null || need <= 0) return null;
    if (need == 1)
    {
        // Оставляем только корень, отрезая всех детей
        node.Left = null;
        node.Right = null;
        node.Count = 1;
        node.Height = 1;
        return node;
    }
    
    int leftMax = node.Left?.Count ?? 0;
    int rightMax = node.Right?.Count ?? 0;
    int childrenNeed = need - 1;
    
    // Распределяем childrenNeed между левым и правым поддеревьями с разницей не более 1
    int leftNeed = childrenNeed / 2;
    int rightNeed = childrenNeed - leftNeed;
    
    // Корректируем, если доступных узлов не хватает
    if (leftNeed > leftMax)
    {
        rightNeed += leftNeed - leftMax;
        leftNeed = leftMax;
    }
    if (rightNeed > rightMax)
    {
        leftNeed += rightNeed - rightMax;
        rightNeed = rightMax;
    }
    // Повторная балансировка (разница не более 1)
    if (Math.Abs(leftNeed - rightNeed) > 1)
    {
        if (leftNeed > rightNeed + 1) leftNeed = rightNeed + 1;
        else if (rightNeed > leftNeed + 1) rightNeed = leftNeed + 1;
    }
    
    node.Left = TrimSubtree(node.Left, leftNeed);
    node.Right = TrimSubtree(node.Right, rightNeed);
    
    node.Count = 1 + (node.Left?.Count ?? 0) + (node.Right?.Count ?? 0);
    node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    return node;
}
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