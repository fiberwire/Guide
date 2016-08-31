using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fast : Gene {

        float moveSpeed;

        public Fast(Organism org, int magnitude) {
            moveSpeed = org.stats.geneticMoveSpeed.absDiff(Mathf.Max(1, org.stats.geneticMoveSpeed * Random.Range(1f, 1.02f)) * magnitude);
            apply = () => {
                org.stats.geneticMoveSpeed += moveSpeed;
            };
        }
    }
}
