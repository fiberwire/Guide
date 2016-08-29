using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Genome {
    public List<Chromosome> chromosomes;

    public Genome(Organism org) {
        chromosomes = new List<Chromosome>();
        chromosomes.Add(Chromosome.RandomChromosome(org));
    }

    public int Count {
        get {
            return genes.Count;
        }
    }

    public List<Gene> genes {
        get {
            return (from c in chromosomes
                    from g in c.genes
                    select g).ToList();
        }
    }

    public string sequence {
        get {
            var list = (from c in chromosomes select c.sequence).ToList();
            var seq = "";
            foreach (var s in list) seq += s;
            return seq;
        }
    }

    //get a random half of the genome, for reproduction purposes
    public List<Chromosome> inheritance {
        get {
            Queue<int> indexes = new Queue<int>();
            List<Chromosome> half = new List<Chromosome>();
            while (indexes.Count < chromosomes.Count) {
                int i = Random.Range(0, chromosomes.Count - 1);
                if (!indexes.Contains(i)) {
                    indexes.Enqueue(i);
                }
            }

            indexes.Count.times((i) => {
                half.Add(chromosomes[i]);
            });
            return half;
        }
    }

    public bool Equals(Genome gen) {
        bool equal = true;
        chromosomes.Count.times(i => {
            if (!gen.chromosomes[i].genes.SequenceEqual(chromosomes[i].genes)) {
                equal = false;
            }
        });
        return equal;
    }

    public Genome Clone(Organism org) {
        return new Genome(org) {
            chromosomes = chromosomes
        };
    }
}
