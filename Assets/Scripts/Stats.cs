using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public Aging aging;
    public Organism organism;
    public Genetics genetics;

    //base stats
    public float maxHealth;
    public float maxEnergy;
    public float longevity;
    public float metabolicRate;
    public float moveSpeed;
    public float reproductionRate;
    public float size;
    public float initialMaturity; //represents the minimum starting growthFactor
    public float healthRegen;

    //growth factors - represents the ratio between the minimum and maximum values based on age
    public float sizeGrowthFactor;
    public float healthGrowthFactor;
    public float energyGrowthFactor;
    public float moveSpeedGrowthFactor;
    public float healthRegenGrowthFactor;

    //decay factors - represents the ratio between the maximum and final values based on age (organisms will get smaller and more frail in their old age)
    public float sizeDecayFactor;
    public float healthDecayFactor;
    public float energyDecayFactor;
    public float moveSpeedDecayFactor;
    public float healthRegenDecayFactor;

    //stats properties
    public float MaxHealth { get { return (maxHealth + genetics.maxHealth) * (HealthGrowthFactor - HealthDecayFactor); } }
    public float MaxEnergy { get { return (maxEnergy + genetics.maxEnergy) * (EnergyGrowthFactor - EnergyDecayFactor); } }
    public float Longevity { get { return longevity + genetics.longevity; } }
    public float EnergyReq { get { return metabolicRate + genetics.metabolicRate; } }
    public float MoveSpeed { get { return (moveSpeed + genetics.moveSpeed) * (MoveSpeedGrowthFactor - MoveSpeedDecayFactor); } }
    public float ReproductionRate { get { return reproductionRate + genetics.reproductionRate; } }
    public float Size { get { return (size + genetics.size) * (SizeGrowthFactor - SizeDecayFactor); } }
    public float HealthRegen { get { return (healthRegen + genetics.healthRegen) * (HealthRegenGrowthFactor - HealthRegenDecayFactor); } }

    //aging factor properties
    public float HealthGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, healthGrowthFactor + genetics.healthGrowthFactor, aging.growth);
        }
    }
    public float HealthDecayFactor {
        get {
            return Mathf.Lerp(0.1f, healthDecayFactor + genetics.healthDecayFactor, aging.decay);
        }
    }

    public float HealthRegenGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, healthRegenGrowthFactor + genetics.healthRegenGrowthFactor, aging.growth);
        }
    }
    public float HealthRegenDecayFactor {
        get {
            return Mathf.Lerp(0.1f, healthRegenDecayFactor + genetics.healthRegenDecayFactor, aging.decay);
        }
    }

    public float SizeGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, sizeGrowthFactor + genetics.sizeGrowthFactor, aging.growth);
        }
    }
    public float SizeDecayFactor {
        get {
            return Mathf.Lerp(0.1f, sizeDecayFactor + genetics.sizeDecayFactor, aging.decay * 0.66f);
        }
    }

    public float EnergyGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, energyGrowthFactor + genetics.energyGrowthFactor, aging.growth);
        }
    }
    public float EnergyDecayFactor {
        get {
            return Mathf.Lerp(0.1f, energyDecayFactor + genetics.energyDecayFactor, aging.decay);
        }
    }

    public float MoveSpeedGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, moveSpeedGrowthFactor + genetics.moveSpeedGrowthFactor, aging.growth);
        }
    }
    public float MoveSpeedDecayFactor {
        get {
            return Mathf.Lerp(0.1f, moveSpeedDecayFactor + genetics.moveSpeedDecayFactor, aging.decay);
        }
    }

    void Start() {
        StartCoroutine(apply());
    }

    IEnumerator apply() {
        while (true) {
            transform.localScale = new Vector2(Size, Size);
            applyHealthAndEnergy();
            yield return null;
        }
    }

    void applyHealthAndEnergy() {
        if (organism.initializedHealthAndEnergy) {
            //scale health and energy with maxHealth and maxEnergy so that they maintain their ratio
            float healthRatio = MaxHealth / organism.maxHealth;
            float energyRatio = MaxEnergy / organism.maxEnergy;
            organism.maxHealth *= healthRatio;
            organism.health *= healthRatio;
            organism.maxEnergy *= energyRatio;
            organism.energy *= energyRatio;
        } else if (MaxHealth != 0 && MaxEnergy != 0) {
            organism.maxHealth = MaxHealth;
            organism.maxEnergy = MaxEnergy;
            organism.health = organism.maxHealth;
            organism.energy = organism.maxEnergy;
            organism.initializedHealthAndEnergy = true;
        }
    }
}
