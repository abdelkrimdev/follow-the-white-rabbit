using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Follow_the_White_Rabbit
{
    class Program
    {
        static void Main()
        {
            var goals = new string[]
            {
                "e4820b45d2277f3844eac66c903e84be",
                "23170acc097c24edb98fc5488ab033fe",
                "665e5bcb0c20062fe8abaaf4628bb154"
            };

            Console.Write("Please type the key phrase: ");
            var phrase = Console.ReadLine().ToLower().Replace(" ", string.Empty);

            var minLength = ReadIntegerFromTheConsole("Minimum word length: ");
            var numWords = ReadIntegerFromTheConsole("Number of words per anagram: ");

            Console.WriteLine("Loading file...");
            var words = GetWordListFromFile(@"WordList")
                .Where(w => w.Length >= minLength).Distinct();

            Console.WriteLine("Excluding impossible words...");
            words = words.Where(w => IsIncluded(w, phrase));

            Console.WriteLine("Building words tree...");
            var tree = new Trie();
            foreach (var word in words)
            {
                tree.Insert(word);
            }

            Console.WriteLine("Jumping into the hole...");
            var anagrams = tree.Anagrams(phrase, numWords).ToList();

            WriteValidResultsToTheConsole(anagrams, goals);

            Console.WriteLine("Saving results...");
            WritePhrasesToFile(@"Results", anagrams);

            Console.WriteLine("DONE!!!");
            Console.ReadLine();
        }

        static IEnumerable<string> GetWordListFromFile(string path)
        {
            return File.ReadAllLines(path);
        }

        static void WritePhrasesToFile(string path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        static string GetHashCode(string input)
        {
            var result = new StringBuilder();

            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                for (var i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }
            }

            return result.ToString();
        }

        static bool IsIncluded(string word, string phrase)
        {
            while (word.Length > 0)
            {
                var position = phrase.IndexOf(word[0]);
                if (position >= 0)
                {
                    phrase = phrase.Remove(position, 1);
                    word = word.Remove(0, 1);
                }
                else return false;
            }
            return true;
        }

        static int ReadIntegerFromTheConsole(string message)
        {
            bool isValid; int input;

            do
            {
                Console.Write(message);
                isValid = int.TryParse(Console.ReadLine(), out input);
                if (!isValid) Console.WriteLine("Please type a valid number.");
            } while (!isValid);

            return input;
        }

        static void WriteValidResultsToTheConsole(IEnumerable<string> anagrams, string[] goals)
        {
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|                                                |");
            foreach (var item in anagrams)
            {
                if (goals.Contains(GetHashCode(item)))
                {
                    Console.WriteLine($"\t - {item}");
                }
            }
            Console.WriteLine("|                                                |");
            Console.WriteLine("+------------------------------------------------+");
        }
    }
}
