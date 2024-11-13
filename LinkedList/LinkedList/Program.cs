namespace Spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList list1 = new LinkedList();
            list1.Add(2);
            list1.Add(3);
            list1.Add(2);
            list1.Add(0);
            list1.Add(1);
            list1.Add(4);

            list1.PrintLinkedList();
            Console.WriteLine();

            LinkedList list2 = new LinkedList();
            list2.Add(8);
            list2.Add(1);
            list2.Add(2);
            list2.Add(0);

            list2.PrintLinkedList();
            Console.WriteLine();

            list1.DestructiveAddition(list2);

            list1.PrintLinkedList();
            Console.WriteLine();
        }

        class Node
        {
            public Node(int value)
            {
                Value = value;
            }

            public int Value { get; set; }

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
                AddNode(newNode);
            }

            public void AddNode(Node node)
            {
                if (Head == null)
                {
                    Tail = node;
                }
                else
                {
                    Head.Prev = node;
                    node.Next = Head;
                }

                Head = node;
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

            public int? FindMinimum()   // O(n)
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

            public void PrintLinkedList()   // O(n)
            {
                Node? node = Head;

                while (node != null)
                {
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
            }

            public void Remove(Node node)   // O(1)
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

            public void InsertAfter(Node node, Node prevNode)    // O(1)
            {
                node.Prev = prevNode;
                node.Next = prevNode.Next;

                if (prevNode != Tail)
                    prevNode.Next.Prev = node;
                else
                    Tail = node;

                prevNode.Next = node;
            }

            public void InsertBefore(Node node, Node nextNode)  // O(1)
            {
                node.Next = nextNode;
                node.Prev = nextNode.Prev;

                if (nextNode != Head)
                    nextNode.Prev.Next = node;
                else
                    Head = node;

                nextNode.Prev = node;
            }

            public void SortLinkedList()    // O(n^2)
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
                            InsertAfter(node, node.Next);
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

            public bool RemoveAll(int value)    // O(n)
            {
                // Returns whether there was node of given value

                bool found = false;
                Node? node = Head;

                while (node != null)
                {
                    if (node.Value == value)
                    {
                        found = true;
                        Remove(node);
                    }
                    node = node.Next;
                }

                return found;
            }

            public void RemoveDuplicates()  // O(n^2)
            {
                Node? node = Head;
                while (node != null)
                {
                    Node? nextNode = node.Next;
                    RemoveAll(node.Value);
                    AddNode(node);
                    node = nextNode;
                }
            }

            public void DestructivePenetration(LinkedList list2)    // O(k*n)
            {
                Node? node = Head;
                while (node != null)
                {
                    bool found = list2.RemoveAll(node.Value);

                    if (found == false)
                    {
                        Remove(node);
                    }

                    node = node.Next;
                }
            }

            public void DestructiveAddition(LinkedList list2)   // O(n^2 + k^2) <= O(k + (n+k)^2)
            {
                // I am to tired to write optimized code idc anymore
                Node? list2Head = list2.Head;
                while (list2Head != null)
                {
                    list2.Remove(list2Head);
                    AddNode(list2Head);
                    list2Head = list2.Head;
                }

                RemoveDuplicates();
            }

            public void AddLongNumber(LinkedList list2)   // O(n)
            {
                Node? node1 = Tail;
                Node? node2 = list2.Tail;

                int overflow = 0;
                while (node2 != null)
                {
                    if (node1 == null)
                        Add(0);
                    
                    int sum = node1.Value + node2.Value + overflow;
                    node1.Value = sum % 10;
                    overflow = (sum - node1.Value) / 10;

                    node1 = node1.Prev;
                    node2 = node2.Prev;
                }

                if (overflow != 0)
                {
                    Add(overflow);
                }
            }
        }
    }
}
