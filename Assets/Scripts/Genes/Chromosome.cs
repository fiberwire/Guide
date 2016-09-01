using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Genes;

public class Chromosome {
    public string sequence;
    private Organism org;
    private static readonly string[] letters = new string[] {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
    };
    private static readonly string[] words = new string[] {
        "big", "fast", "fertile"
    };

    public List<Gene> genes {
        get {
            return newParseSequence(sequence);
        }
    }

    public Chromosome(Organism org, string sequence) {
        this.org = org;
        this.sequence = sequence;
    }

    public static Chromosome RandomChromosome(Organism org, int length) {
        return new Chromosome(org, RandomSequence(length));
    }

    public static string RandomSequence(int length) {
        var seq = "";

        length.times(i => {
            seq += letters.random();
        });

        return seq;
    }

    /*
     * returns genes based on whether (and how much of, and how many times) a word is spelled out in the sequence
     * each 'b' in the sequence is worth a Big(1), each 'bi' is worth a Big(2), each 'big' is worth a Big(3), etc.
     */
    List<Gene> ParseSequence(string seq) {
        List<Gene> g = new List<Gene>();

        foreach (var w in words) {
            /* the index of the inner lists represents the letter of the word, 
             * the ints in the inner lists represent the indexes of the letter of the word in the sequence
             * for instance, say the word is 'big' and the sequence is  'lkjlkbsdiasdfaiosdifnbbbigasd'
             * the list would look like this
             * [
             *      [5, 21, 22, 23],    // b indexes
             *      [8, 14, 18, 24],    // i indexes
             *      [25],                    // g indexes
             * ]
             * Once I have the indexes of each occurence of each letter in the word, 
             * I can compare the indexes and determine if letters occur sequentially
            */
            List<List<int>> indexes = new List<List<int>>();
            //initialize a list for each letter of the word w
            w.Length.times(i => { indexes.Add(new List<int>()); });

            w.Length.times(i => { //iterate through letters in the word 
                seq.Length.times(j => { //iterate through letters in sequence
                    if (w[i] == seq[j]) indexes[i].Add(j);
                });
            });

            var linkedIndexes = new LinkedList<List<int>>(indexes);

            foreach (var index in indexes[0]) { //iterate through indexes of occurences in the sequence of the first letter of the word
                //find how powerful the gene will be
                var mag = findSubsequency(linkedIndexes.First, index);

                g.Add(getGeneFromWord(w, mag));
            }
        }
        return g;
    }

    //determines how much of the word is present in the list, starting with the index of the first letter of the word in the sequence
    private int findSubsequency(LinkedListNode<List<int>> list, int i, int j = 1) {
        if (list.Value.Contains(i)) {
            if (list.Next != null)
                return findSubsequency(list.Next, i++, j++);
            else
                return j;
        } else {
            return j;
        }
    }

    List<Gene> newParseSequence(string seq) {
        var genes = new List<Gene>();

        foreach (var word in words) {
            //get all substrings of word
            var subs = word.Substrings();

            //find occurences of all substrings of word in seq
            var counts = new List<int>();
            foreach (var sub in subs) {
                counts.Add(seq.countOccurences(sub));
            }

            //trim duplicate counts (subtract longer substring occurences from shorter substring occurences)
            for (int i = counts.Count - 1; i > 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    counts[j] -= counts[i];
                }
            }

            //add genes
            counts.Count.times(i => {
                if (counts[i] > 0) {
                    counts[i].times(j => {
                        genes.Add(getGeneFromWord(word, i+1));
                    });
                }
            });
        }

        return genes;
    }

    //returns a gene that matches the word given, with the given magnitude
    private Gene getGeneFromWord(string word, int mag) {
        switch (word) {
            case "big":
                return new Big(org, mag);
            case "fast":
                return new Fast(org, mag);
            case "fertile":
                return new Fertile(org, mag);
            default: return null;
        }
    }

}