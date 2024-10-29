namespace LinkedList;

class Program
{
    static void Main(string[] args)
    {
        Node uzlik = new Node(8);
        
    }

    class Node
    {
        public Node(int value)
        {
            Value = value;
        }
        public int Value { get; }
        public Node Next { get; set; }
    }

    class LinkedList
    {
        public Node Head { get; set; }
        public void Add(int value)
        {
            Node newHead = new Node(value);
            if (Head != null)
            {
                newHead.Next = Head;
            }
            Head = newHead;
        }

        public bool Find(int value)
        {
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Value == value)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public int FindMinimum()
        {
            // O(n)

            int minimum;
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (minimum == null | currentNode.Value < minimum)
                {
                    minimum = currentNode.Value;
                }
                currentNode = currentNode.Next;
            }
            return minimum;
        }

        public void Sort()
        {

        }
    }

    /*
    1. Nalezení minima ve spojovém seznamu (30b)
    2. Seřazení hodnot ve spojovém seznamu vzestupně (50b)
    3. Destruktivní průnik dvou seznamů (70b)Destruktivní průnik znamená, že z prvků obou seznamů vytvoříte jeden seznam obsahující právě všechny společné prvky a to každý pouze jednou. Funkce nevytváří žádné nové prvky, ale používá pouze ty již existující.

    Upozornění: V jednom seznamu se hodnoty mohou opakovat. Seznamy mohou být i prázdné.

    BONUS (+10b): Určete časovou složitost všech vašich implementací (n je počet prvků v poli).
    */
}
