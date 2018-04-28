using System.Collections.Generic;

namespace Follow_the_White_Rabbit
{
    class Node
    {
        public char Value { get; private set; }
        public string Path { get; private set; }
        public bool IsWord { get; set; }
        public ICollection<Node> Children { get; set; }

        public Node(char value, string path)
        {
            Value = value;
            Path = path;
            IsWord = false;
            Children = new List<Node>();
        }

        public Node FindChildNode(char value)
        {
            foreach (var child in Children)
            {
                if (child.Value == value) return child;
            }
            return null;
        }
    }
}
