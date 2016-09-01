using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public Aging aging;
    public Organism organism;
    public Genetics genetics;

    //base stats
    public float maxHealth;
    public float longevity;
    public float energyReq;
    public float moveSpeed;
    public float reproductionRate;
    public float size;
    public float initialMaturity; //represents the minimum starting growthFactor

    //growth factors - represents the ratio between the minimum and maximum values based on age
    public float baseSizeGrowthFactor;
    public float baseHealthGrowthFactor;

    //decay factors - represents the ration between the maximum and final values based on age (organisms will get smaller and more frail in their old age)
    public float baseSizeDecayFactor;
    public float baseHealthDecayFactor;

    //final stats properties
    public float MaxHealth { get { return (maxHealth + genetics.maxHealth) * (HealthGrowthFactor - HealthDecayFactor); } }
    public float Longevity { get { return longevity + genetics.longevity; } }
    public float EnergyReq { get { return energyReq + genetics.energyReq; } }
    public float MoveSpeed { get { return moveSpeed + genetics.moveSpeed; } }
    public float ReproductionRate { get { return reproductionRate + genetics.reproductionRate; } }
    public float Size { get { return (size + genetics.size) * (SizeGrowthFactor - SizeDecayFactor); } }

    public float SizeGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, baseSizeGrowthFactor + genetics.sizeGrowthFactor, aging.growth);
        }
    }
    public float HealthGrowthFactor {
        get {
            return Mathf.Lerp(0.1f, baseHealthGrowthFactor + genetics.healthGrowthFactor, aging.growth);
        }
    }

    public float SizeDecayFactor {
        get {
            return Mathf.Lerp(0.1f, baseSizeDecayFactor + genetics.sizeDecayFactor, aging.decay);
        }
    }
    public float HealthDecayFactor {
        get {
            return Mathf.Lerp(0.1f, baseHealthDecayFactor + genetics.healthDecayFactor, aging.decay);
        }
    }

    void Start() {
        StartCoroutine(apply());
    }

    IEnumerator apply() {
        while (true) {
            transform.localScale = new Vector2(Size, Size);
            yield return null;
        }
    }
}
