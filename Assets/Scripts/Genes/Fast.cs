using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fast : Gene {

        float moveSpeed;

        public Fast(Organism org) {
            moveSpeed = Mathf.Max(1, org.geneticMoveSpeed * Random.Range(1f, 1.02f));
            apply = () => {
                org.geneticMoveSpeed += moveSpeed.absDiff(org.geneticMoveSpeed);
            };
        }
    }
}
