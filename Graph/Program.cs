using System.ComponentModel;
using System.Text;

namespace Graph
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many people:");
            int count = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Friends:");
            string? friends = Console.ReadLine();

            Console.WriteLine("Trying to find a chain between:");
            string? chainBetween = Console.ReadLine();

            FriendTableFind table = new FriendTableFind(count, friends, chainBetween);

            string chain = table.FindConnectionBFS();
            Console.WriteLine();
            Console.WriteLine("Chain:");
            Console.WriteLine(chain);
        }
    }

    class FriendTableFind
    {
        // I ain't doing inheritance for this (FriendTable < FriendTableWithFind) or anything similar..

        public bool[,] FriendTable { get; init; }
        public int Count { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public int?[] Parents { get; set; } // Once a node is found in the tree, it saves it's parent for backwards chain construction purposes
        public Dictionary<int, bool> CurrentCycle { get; set; }

        public FriendTableFind(int count, string friends, string chainBetween)
        {
            Count = count;
            FriendTable = new bool[Count, Count];
            
            // Populate the FriendTable
            foreach (string pair in friends.Split(' '))
            {
                string[] splitPair = pair.Split('-');
                int person1 = Convert.ToInt16(splitPair[0]) - 1;
                int person2 = Convert.ToInt16(splitPair[1]) - 1;

                FriendTable[person1, person2] = true;
                FriendTable[person2, person1] = true;
            }

            // Convert wannabe friends to a usable format
            string[] wantedPair = chainBetween.Split(' ');
            Start = Convert.ToInt16(wantedPair[0]) - 1;
            End = Convert.ToInt16(wantedPair[1]) - 1;

            // Prepare for the BFS algorithm
            CurrentCycle = new Dictionary<int, bool>
            {
                [Start] = true
            };

            Parents = new int?[Count];
            Parents[Start] = Start;
        }

        public void ClearParents()
        {
            Parents = new int?[Count];
            Parents[Start] = Start;
        }

        public string GetChain()
        {
            if (Parents[End] == null)
                return "Chain not possible!";

            StringBuilder chain = new StringBuilder();
            chain.Append(End + 1);
            chain.Append(' ');

            int? currentParent = Parents[End];
            while (currentParent != null && currentParent != Start)
            {
                chain.Append(currentParent + 1);    // +1 to add to a humanly-readable format
                chain.Append(' ');
                currentParent = Parents[(int) currentParent];
            }

            chain.Append(Start + 1);

            return new string(chain.ToString().Reverse().ToArray());
            //return chain.ToString().Reverse().ToString();
        }

        public bool FindConnectionOneCycle()
        {
            Dictionary<int, bool> nextCycle = new Dictionary<int, bool>();

            foreach (int door in CurrentCycle.Keys)
            {
                if (FriendTable[door, End] == true)
                {
                    Parents[End] = door;
                    return true;    // Return true if End found
                }

                // Get the values to check in the next cycle
                for (int i = 0; i < Count; i++)
                {
                    if (FriendTable[door, i] == true && Parents[i] == null)
                    {
                        Parents[i] = door;  // The first door to reach the i'th person
                        nextCycle[i] = true;
                    }
                }
            }

            CurrentCycle = nextCycle;
            return false;
        }

        public string FindConnectionBFS()
        {
            bool found = false;
            while (CurrentCycle.Count > 0 && found != true)  // At least the Dictionary.Count is O(1)...
            {
                found = FindConnectionOneCycle();
            }

            string chain = GetChain();
            ClearParents();

            return chain;
        }
    }
}