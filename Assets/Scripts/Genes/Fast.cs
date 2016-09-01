using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fast : Gene {

        float moveSpeed;

        public Fast(Organism org, int magnitude) {
            moveSpeed = org.genetics.moveSpeed.absDiff(Mathf.Max(1, org.genetics.moveSpeed * Random.Range(1f, 1.02f)) * magnitude);
            apply = () => {
                org.genetics.moveSpeed += moveSpeed;
            };
        }
    }
}
