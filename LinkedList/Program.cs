namespace LinkedList;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    class Node
    {
        public Node(int value)
        {
            Value = value;
        }
        public int Value { get; };
        public Node Next { get; set };
}
