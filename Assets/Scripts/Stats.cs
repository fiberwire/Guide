using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public Aging aging;

    

    //base stats
    public float baseMaxHealth;
    public float baseLongevity;
    public float baseEnergyReq;
    public float baseMoveSpeed;
    public float baseSplitChance;
    public float baseSize;
    public float baseInitialMaturity; //represents the minimum starting growthFactor

    //genetic bonuses
    public float geneticMaxHealth;
    public float geneticLongevity;
    public float geneticEnergyReq;
    public float geneticMoveSpeed;
    public float geneticSplitChance;
    public float geneticSize;
    public float geneticInitialMaturity;

    //growth factors - represents the ratio between the minimum and maximum values based on age
    public float baseSizeGrowthFactor;
    public float geneticSizeGrowthFactor;
    public float baseHealthGrowthFactor;
    public float geneticHealthGrowthFactor;

    //decay factors - represents the ration between the maximum and final values based on age (organisms will get smaller and more frail in their old age)
    public float baseSizeDecayFactor;
    public float geneticSizeDecayFactor;
    public float baseHealthDecayFactor;
    public float geneticHealthDecayFactor;

    //final stats properties
    public float maxHealth { get { return (baseMaxHealth + geneticMaxHealth) * (healthGrowthFactor - healthDecayFactor); } }
    public float longevity { get { return baseLongevity + geneticLongevity; } }
    public float energyReq { get { return baseEnergyReq + geneticEnergyReq; } }
    public float moveSpeed { get { return baseMoveSpeed + geneticMoveSpeed; } }
    public float splitChance { get { return baseSplitChance + geneticSplitChance; } }
    public float size { get { return (baseSize + geneticSize) * (sizeGrowthFactor - sizeDecayFactor); } }
    public float sizeGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, baseSizeGrowthFactor + geneticSizeGrowthFactor, aging.growth);
        }
    }
    public float healthGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, baseHealthGrowthFactor + geneticHealthGrowthFactor, aging.growth);
        }
    }

    public float sizeDecayFactor {
        get {
            return Mathf.Lerp(0.1f, baseSizeDecayFactor + geneticSizeDecayFactor, aging.decay);
        }
    }

    public float healthDecayFactor {
        get {
            return Mathf.Lerp(0.1f, baseHealthDecayFactor + geneticHealthDecayFactor, aging.decay);
        }
    }

    //apply genetic bonuses, whatever they may be
    public void applyGenetics(Genome gen) {
        resetGenetics();

        foreach (var g in gen.genes) {
            g.apply();
        }

        transform.localScale = new Vector2(size, size);
    }

    //zero out genetic bonuses
    private void resetGenetics() {
        geneticMoveSpeed = 0;
        geneticSplitChance = 0;
        geneticSize = 0;
    }

}
