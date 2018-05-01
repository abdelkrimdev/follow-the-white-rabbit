# Follow The White Rabbit
Solution to the TrustPilot "[follow the white rabbit](https://followthewhiterabbit.trustpilot.com)" coding challenge done in C#.

## Performance
The easy one could be found in just `2 secs`, the medium one took `50 secs` and the hard one should be resolved in about `30 mins`.

## Usage
```
$ dotnet run
Please type the key phrase: trustpilot wants you
Minimum word length: 2
Number of words per anagram: 4
Loading file...
Excluding impossible words...
Building words tree...
Jumping into the hole...
+------------------------------------------------+
|                                                |
         - printout stout yawls
         - ty outlaws printouts
         - wu lisp not statutory
|                                                |
+------------------------------------------------+
Saving results...
DONE!!!
```

## Strategy
* Filter down the wordlist to only contain words that could be part of the secret phrase.
* Store the wordlist into a prefix tree, so we can group words with the same prefix.
```
                             '^'
                            /   \
                           /     \
                         'h'     'w'
                          |       |
                          |       |
                         'e'     'o'
                          |        \
                          |         \
                         'l'        'r'
                          |        / | \
                          |       /  |  \
                         'l*'   'd*''k*''l'
                          |              |
                          |              |
                         'o*'           'd*'
```
* Iterate over all characters from the key phrase.
* Going through the `Trie` and try to build an anagram for the key phrase.
* Each time it consumes all characters from the key phrase, it adds a new candidate to the list. 
* Hash each of the anagrams and check if it matches.
