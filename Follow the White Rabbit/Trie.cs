using System.Collections.Generic;

namespace Follow_the_White_Rabbit
{
    public class Trie
    {
        readonly Node _root;

        public Trie() => _root = new Node('^', string.Empty);

        public void Insert(string word)
        {
            var current = Prefix(word);

            for (var i = current.Path.Length; i < word.Length; i++)
            {
                var node = new Node(word[i], current.Path + word[i]);
                current.Children.Add(node);
                current = node;
            }

            current.IsWord = true;
        }

        public IEnumerable<string> Anagrams(string phrase, int numberOfWords)
        {
            var anagrams = new List<string>();
            Anagrams(phrase, numberOfWords - 1, string.Empty, anagrams);
            return anagrams;
        }

        void Anagrams(string phrase, int spaces, string candidate, ICollection<string> anagrams, Node node = null)
        {
            if (node == null) node = _root;

            if (phrase.Length == 0)
            {
                if (node.IsWord && spaces == 0)
                    anagrams.Add(candidate);
                return;
            }

            foreach (var child in node.Children)
            {
                var index = phrase.IndexOf(child.Value);
                if (index > -1)
                    Anagrams(phrase.Remove(index, 1), spaces, candidate + child.Value, anagrams, child);
            }

            if (node.IsWord && spaces > 0)
                Anagrams(phrase, spaces - 1, candidate + " ", anagrams);
        }

        Node Prefix(string word)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var item in word)
            {
                currentNode = currentNode.FindChildNode(item);
                if (currentNode == null) break;
                result = currentNode;
            }

            return result;
        }
    }
}
