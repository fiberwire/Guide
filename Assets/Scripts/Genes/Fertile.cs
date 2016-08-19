using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fertile : Gene {

        float splitChance;

        public Fertile(Organism org) {
            splitChance = Mathf.Max(1, org.geneticSplitChance * Random.Range(1f, 1.01f));

            apply = () => {
                org.geneticSplitChance += splitChance.absDiff(org.geneticSplitChance);
            };
        }
    }
}
