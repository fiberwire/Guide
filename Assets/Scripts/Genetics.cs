using UnityEngine;
using System.Collections;

public class Genetics : MonoBehaviour {

    public Stats stats;
    public Aging aging;
    public Organism organism;

    //genetic bonuses
    public float maxHealth;
    public float maxEnergy;
    public float longevity;
    public float metabolicRate;
    public float moveSpeed;
    public float reproductionRate;
    public float size;
    public float initialMaturity;
    public float healthRegen;

    //growth factors
    public float healthGrowthFactor;
    public float sizeGrowthFactor;
    public float energyGrowthFactor;
    public float moveSpeedGrowthFactor;
    public float healthRegenGrowthFactor;

    //decay factors
    public float healthDecayFactor;
    public float sizeDecayFactor;
    public float energyDecayFactor;
    public float moveSpeedDecayFactor;
    public float healthRegenDecayFactor;

    // Use this for initialization
    void Start() {

    }

    //apply genetic bonuses, whatever they may be
    public void apply(Genome gen) {
        reset();

        foreach (var g in gen.genes) {
            g.apply();
        }
    }

    //zero out genetic bonuses
    private void reset() {
        maxHealth = 0;
        maxEnergy = 0;
        longevity = 0;
        metabolicRate = 0;
        moveSpeed = 0;
        reproductionRate = 0;
        size = 0;
        initialMaturity = 0;
        healthRegen = 0;
    }
}
