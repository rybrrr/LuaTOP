namespace Spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList list1 = new LinkedList();
            list1.Add(6);
            list1.Add(2);
            list1.Add(8);
            list1.Add(4);
            list1.Add(10);

            list1.PrintLinkedList();
            list1.SortLinkedList();
            Console.WriteLine();
            list1.PrintLinkedList();
            Console.WriteLine();

            LinkedList list2 = new LinkedList();
            list2.Add(3);
            list2.Add(5);
            list2.Add(2);
            list2.Add(4);
            list2.Add(2);
            list2.Add(5);

            list2.PrintLinkedList();
            Console.WriteLine();

            list1.DestructivePenetration(list2);
            list1.PrintLinkedList();
            Console.WriteLine();

            list2.PrintLinkedList();
            Console.WriteLine();
        }

        class Node
        {
            public Node(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public Node? Prev { get; set; }
            public Node? Next { get; set; }
        }

        class LinkedList
        {
            public Node? Head { get; set; }
            public Node? Tail { get; set; }

            public void Add(int value)  // O(1)
            {
                Node newNode = new Node(value);

                if (Head == null)
                {
                    Tail = newNode;
                }
                else
                {
                    Head.Prev = newNode;
                    newNode.Next = Head;
                }

                Head = newNode;
            }


            public bool Find(int value) // O(n)
            {
                Node? node = Head;

                while (node != null)
                {
                    if (node.Value == value)
                    {
                        return true;
                    }
                    node = node.Next;
                }
                return false;
            }

            public int? FindMinimum() // O(n)
            {
                if (Head == null)
                {
                    return null;
                }

                Node? node = Head;
                int minimum = Head.Value;

                while (node != null)
                {
                    if (minimum > node.Value)
                    {
                        minimum = node.Value;
                    }
                    node = node.Next;
                }

                return minimum;
            }

            public void PrintLinkedList() // O(n)
            {
                Node? node = Head;

                while (node != null)
                {
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
            }

            public void Remove(Node node)
            {
                if (node != Head)
                    node.Prev.Next = node.Next;
                else
                    Head = node.Next;

                if (node != Tail)
                    node.Next.Prev = node.Prev;
                else
                    Tail = node.Prev;
            }

            public void Insert(Node node, Node prevNode)
            {
                node.Prev = prevNode;
                node.Next = prevNode.Next;

                if (prevNode != Tail)
                    prevNode.Next.Prev = node;
                else
                    Tail = node;

                prevNode.Next = node;
            }

            public void SortLinkedList() // O(n^2)
            {
                if (Head == null || Head.Next == null)
                {
                    return;
                }

                while (true)
                {
                    bool sorted = true;
                    Node node = Head;

                    while (node.Next != null) // dokud nedojedeme na konec seznamu
                    {
                        if (node.Value > node.Next.Value)
                        {
                            Remove(node);
                            Insert(node, node.Next);
                            sorted = false;
                        } else
                        {
                            node = node.Next;
                        }
                    }

                    if (sorted == true)
                    {
                        return;
                    }
                }
            }

            public void DestructivePenetration(LinkedList list2)
            {
                Node? node = Head;
                while (node != null)
                {
                    bool found = false;
                    Node? node2 = list2.Head;
                    while (node2 != null)
                    {
                        if (node.Value == node2.Value)
                        {
                            found = true;
                            list2.Remove(node2);
                        }
                        node2 = node2.Next;
                    }

                    if (found == false)
                    {
                        Remove(node);
                    }

                    node = node.Next;
                }
            }
        }
    }
}
