﻿using UnityEngine;
using System.Collections;

public class Aging : MonoBehaviour {

    public enum LifeStage { Young, Adult, Old }

    public Organism organism;
    public Stats stats;
    private SpriteRenderer sr;

    public float growth, decay;

    public LifeStage stage;
    public float age;

    //life stage colors
    public Color young;
    public Color adult;
    public Color old;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        stage = LifeStage.Young;
        StartCoroutine(Age());
        StartCoroutine(AgeColor());
        StartCoroutine(updateGrowthAndDecay());
    }

    IEnumerator Age() {
        while (true) {
            age += 1;

            switch (stage) {
                case LifeStage.Young:
                    if (age / stats.longevity > 1f / 3f) stage = LifeStage.Adult;
                    break;
                case LifeStage.Adult:
                    if (age / stats.longevity > 2f / 3f) stage = LifeStage.Old;
                    break;
                case LifeStage.Old:
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator AgeColor() {
        while (true) {
            switch (stage) {
                case LifeStage.Young:
                    var t = age / (stats.longevity / 3);
                    sr.color = new Color(
                        Mathf.Lerp(young.r, adult.r, t),
                        Mathf.Lerp(young.g, adult.g, t),
                        Mathf.Lerp(young.b, adult.b, t)
                    );
                    break;
                case LifeStage.Adult:
                    break;
                case LifeStage.Old:
                    t = (age - (stats.longevity * 2 / 3)) / (stats.longevity / 3);
                    sr.color = new Color(
                        Mathf.Lerp(adult.r, old.r, t),
                        Mathf.Lerp(adult.g, old.g, t),
                        Mathf.Lerp(adult.b, old.b, t)
                    );
                    break;
            }
            yield return null;
        }
    }

    IEnumerator updateGrowthAndDecay() {
        while (true) {
            if (stage == LifeStage.Young) {
                growth = age / (stats.longevity / 3f);
            } else {
                growth = 1f;
            }

            if (stage == LifeStage.Old) {
                decay = (age - (stats.longevity * 2f / 3f)) / (stats.longevity / 3f);
            } else {
                decay = 0f;
            }
            
            
            yield return null;
        }
    }

}
