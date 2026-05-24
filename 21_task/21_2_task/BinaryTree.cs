using System;

namespace Example
{
    public class BinaryTree //класс, реализующий АТД «дерево бинарного поиска»
    {
        private class Node
        {
            public int inf; //информационное поле
            public Node left; //ссылка на левое поддерево
            public Node rigth; //ссылка на правое поддерево

            //конструктор вложенного класса, создает узел дерева
            public Node(int nodeInf)
            {
                inf = nodeInf;
                left = null;
                rigth = null;
            }

            //добавляет узел в дерево так, чтобы дерево оставалось деревом бинарного поиска
            public static void Add(ref Node r, int nodeInf)
            {
                if (r == null)
                {
                    r = new Node(nodeInf);
                }
                else
                {
                    if (((IComparable)r.inf).CompareTo(nodeInf) > 0)
                    {
                        Add(ref r.left, nodeInf);
                    }
                    else
                    {
                        Add(ref r.rigth, nodeInf);
                    }
                }
            }

            public static void Preorder(Node r) //прямой обход дерева
            {
                if (r != null)
                {
                    Console.Write("{0} ", r.inf);
                    Preorder(r.left);
                    Preorder(r.rigth);
                }
            }

            public static void Inorder(Node r) //симметричный обход дерева
            {
                if (r != null)
                {
                    Inorder(r.left);
                    Console.Write("{0} ", r.inf);
                    Inorder(r.rigth);
                }
            }

            public static void Postorder(Node r) //обратный обход дерева
            {
                if (r != null)
                {
                    Postorder(r.left);
                    Postorder(r.rigth);
                    Console.Write("{0} ", r.inf);
                }
            }
            public static int NodeCount(Node r, int k)
            {
                if (r == null)
                {
                    return 0;
                }
                if (k == 1)
                {
                    return 1;
                }
                else
                {
                    return NodeCount(r.left, k - 1) + NodeCount(r.rigth, k - 1);
                }
            }
            }
        

        Node tree; //ссылка на корень дерева

        //свойство позволяет получить доступ к значению информационного поля корня дерева
        public int Inf
        {
            set { tree.inf = value; }
            get { return tree.inf; }
        }

        public BinaryTree() //открытый конструктор
        {
            tree = null;
        }

        private BinaryTree(Node r) //закрытый конструктор
        {
            tree = r;
        }

        public void Add(int nodeInf) //добавление узла в дерево
        {
            Node.Add(ref tree, nodeInf);
        }

        //организация различных способов обхода дерева
        public void Preorder()
        {
            Node.Preorder(tree);
        }

        public int NodeCountOnLevel(int k)
        {
            if (tree == null)
                throw new InvalidOperationException("Дерево пусто");
            if (k < 1)
            {
                throw new InvalidOperationException("уровень начинается с единицы");
            }
            return Node.NodeCount(tree, k);
        }

        public void Inorder()
        {
            Node.Inorder(tree);
        }

        public void Postorder()
        {
            Node.Postorder(tree);
        }

        
    }
}