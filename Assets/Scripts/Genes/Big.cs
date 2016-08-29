
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Big : Gene {
        float size;
        float energyReq; //increase energy requirement once it is added as a stat
        float maxHealth; //increase max health once it is added as a stat
        float moveSpeed;
        
        public Big(Organism org, int magnitude) {
            size = org.stats.geneticSize.absDiff(Mathf.Max(0.1f, org.stats.geneticSize * Random.Range(1f, 1.05f)));
            moveSpeed = org.stats.geneticMoveSpeed * Random.Range(0f, 0.01f);
            apply = () => {
                org.stats.geneticSize += size;
                org.stats.geneticMoveSpeed -= moveSpeed;
            };
        }
    }
}
