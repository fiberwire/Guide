using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fertile : Gene {

        float reproductionRate;

        public Fertile(Organism org, int magnitude) {
            reproductionRate = org.genetics.reproductionRate.absDiff(Mathf.Max(1, org.genetics.reproductionRate * Random.Range(1f, 1.01f)) * magnitude);

            apply = () => {
                org.genetics.reproductionRate += reproductionRate;
            };
        }
    }
}
