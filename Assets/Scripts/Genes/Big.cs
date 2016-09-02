
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Big : Gene {
        float size;
        float metabolicRate; //increase energy requirement once it is added as a stat
        float maxHealth; //increase max health once it is added as a stat
        float moveSpeed;

        public Big(Organism org, float magnitude) {
            size = org.genetics.size.absDiff(Mathf.Max(0.1f, org.genetics.size * Random.Range(1f, 1.05f)) * magnitude);
            moveSpeed = org.genetics.moveSpeed.absDiff(Mathf.Max(0.1f, org.genetics.moveSpeed * Random.Range(0f, 0.01f) * magnitude));
            maxHealth = org.genetics.maxHealth.absDiff(Mathf.Max(0.1f, org.genetics.maxHealth * Random.Range(0f, 0.01f) * magnitude));
            metabolicRate = org.genetics.metabolicRate.absDiff(Mathf.Max(0.1f, org.genetics.metabolicRate * Random.Range(0f, 0.01f) * magnitude));
            apply = () => {
                org.genetics.size += size;
                org.genetics.moveSpeed -= moveSpeed;
                org.genetics.maxHealth += maxHealth;
                org.genetics.metabolicRate += metabolicRate;
            };
        }
    }
}
