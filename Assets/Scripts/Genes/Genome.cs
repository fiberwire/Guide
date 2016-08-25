using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Genome {
    public List<Chromosome> chromosomes;

    public Genome() {
        chromosomes = new List<Chromosome>();
        chromosomes.Add(new Chromosome());
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

    public void Add(Gene gene) {
        int i = Random.Range(0, chromosomes.Count - 1);

        if (chromosomes[i].Count < 50) {
            chromosomes[i].Add(gene);
        } else if (chromosomes.Count < 50) {
            chromosomes.Add(new Chromosome());
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

    public Genome Clone() {
        return new Genome {
            chromosomes = chromosomes
        };
    }
}
