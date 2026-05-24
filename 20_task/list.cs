﻿using System;
using System.IO;

public class LinkedList
{
    public Node head;
    public Node tail;
    public Node temp;

    public LinkedList()
    {
        head = null;
        tail = null;
        temp = null;
    }

    public void PushBack(int value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
    }

    public int PopFront()
    {
        if (head == null)
        {
            Console.WriteLine("Список пуст!");
            return -1;
        }
        temp = head;
        int value = temp.Data;
        head = head.Next;
        if (head == null)
            tail = null;
        return value;
    }

    public void Print(StreamWriter writer)
    {
        Node current = head;
        while (current != null)
        {
            writer.Write(current.Data + " ");
            current = current.Next;
        }
        writer.WriteLine();
    }

    public int FindMax()
    {
        if (head == null)
        {
            Console.WriteLine("Список пуст!");
            return 0;
        }
        int maxVal = head.Data;
        Node current = head;
        while (current != null)
        {
            if (current.Data > maxVal)
                maxVal = current.Data;
            current = current.Next;
        }
        return maxVal;
    }

    public void InsertBeforeMax(int maxVal, int x)
    {
        Node current = head;
        Node previous = null;
        while (current != null)
        {
            if (current.Data == maxVal)
            {
                Node newNode = new Node(x);
                newNode.Next = current;
                if (previous == null)
                {
                    head = newNode;
                }
                else
                {
                    previous.Next = newNode;
                }
                previous = newNode;
            }
            previous = current;
            current = current.Next;
        }
    }
}