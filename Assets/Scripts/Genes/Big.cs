
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Big : Gene {
        float size;
        float energyReq; //increase energy requirement once it is added as a stat
        float moveSpeed;
        public Big(Organism org) {
            size = Mathf.Max(0.1f, org.geneticSize * Random.Range(1f, 1.01f));
            moveSpeed = Mathf.Max(0.5f, org.geneticMoveSpeed * Random.Range(0.95f, 1f));
            apply = () => {
                org.geneticSize += size.absDiff(org.geneticSize);
                org.geneticMoveSpeed += moveSpeed.absDiff(org.geneticMoveSpeed);
            };
        }
    }
}
