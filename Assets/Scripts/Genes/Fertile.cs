using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fertile : Gene {

        float reproductionRate;

        public Fertile(Organism org, int magnitude) {
            reproductionRate = org.stats.geneticReproductionRate.absDiff(Mathf.Max(1, org.stats.geneticReproductionRate * Random.Range(1f, 1.01f)) * magnitude);

            apply = () => {
                org.stats.geneticReproductionRate += reproductionRate;
            };
        }
    }
}
