using System;
using System.Collections.Generic;

namespace Assets.Scripts.Genes {
    public class Chromosome {
        public int Count { get { return genes.Count; } }
        public List<Gene> genes;

        public Chromosome() {
            genes = new List<Gene>();
        }

        public void Add(Gene gene) {
            genes.Add(gene);
        }
    }
}