﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets.Scripts.Genes;

public class Organism : MonoBehaviour {

    public Genome genome;
    public delegate void GenomeChanged(Genome gen);
    public event GenomeChanged genomeChanged;

    //base stats
    public float baseMoveSpeed;
    public float baseSplitChance;
    public float baseSize;
    public int genomeCount;

    //genetic bonuses
    public float geneticMoveSpeed;
    public float geneticSplitChance;
    public float geneticSize;

    //final stats properties
    public float moveSpeed { get { return baseMoveSpeed + geneticMoveSpeed; } }
    public float splitChance { get { return baseSplitChance + geneticSplitChance; } }
    public float size { get { return baseSize + geneticSize; } }

    // Use this for initialization
    void Start() {
        if (genome == null) genome = new Genome();
        genomeChanged += applyGenetics;

        StartCoroutine(move());
        StartCoroutine(addGenes());
        StartCoroutine(split());
    }

    IEnumerator addGenes() {
        while (true) {
            genomeCount = genome.Count;
            addGene(new Fast(this));
            addGene(new Fertile(this));
            addGene(new Big(this));
            yield return new WaitForSeconds(1f);
        }
    }

    public void addGene(Gene g) {
        genome.Add(g);
        genomeChanged(genome);
    }

    //apply genetic bonuses, whatever they may be
    void applyGenetics(Genome gen) {
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

    /*
     * Movement works like this:
     * organism has a main target that it's trying to reach
     * it picks an intermediate target randomly
     * it checks if that target is closer to its main target than its current position
     * if it is, the organism moves towards its intermediate target until it reaches it, 
     * then it chooses another intermediate target, and moves towards it,
     * repeat until the organism arrives at its main target
     */
    IEnumerator move() {
        while (true) {
            var target = getTarget(); //overall goal of movement
            var move = randomTarget(); //intermediate goal on way to target
            var time = Time.realtimeSinceStartup; //time when target is chosen

            //if move is closer to target than transform.position
            if ((move - target).sqrMagnitude < ((Vector2)transform.position - target).sqrMagnitude) {
                //move to move
                while (Vector2.Distance(transform.position, move) > Random.Range(0.05f, 0.5f)) {

                    MoveTowards(move);

                    //give up on target if it takes too long to get there
                    if (time.absDiff(Time.realtimeSinceStartup) > Random.Range(1f, 5f)) move = transform.position;

                    yield return null;
                }
            }
            var randomSeconds = Random.Range(0f, 1f) * Random.Range(0f, 3f) / Random.Range(1f, 3f);
            yield return new WaitForSeconds(randomSeconds * 0.25f);
        }
    }

    void MoveTowards(Vector2 target) {
        transform.position = new Vector2(
                            Mathf.Lerp(transform.position.x, target.x, moveSpeed * Time.deltaTime * Random.Range(0.5f, 1.5f)),
                            Mathf.Lerp(transform.position.y, target.y, moveSpeed * Time.deltaTime * Random.Range(0.5f, 1.5f))
                        );
    }

    Vector2 getTarget() {
        //will eventually return a target of interest (such as food, or a potential mate), or a random target if there are none in range
        //range will be determined by a stat, which will be increased by genes
        //a better range stat will make for a more fit organism because it will be able to search greater areas for food/mates
        return randomTarget();
    }

    Vector2 randomTarget() {
        return new Vector2(
            Mathf.Clamp(
                transform.position.x + Random.Range(-moveSpeed, moveSpeed),
                -60f, 60f),
            Mathf.Clamp(
                transform.position.y + Random.Range(-moveSpeed, moveSpeed),
                -33f, 33f)
            );
    }

    IEnumerator split() {
        yield return new WaitForSeconds(30);
        while (true) {
            float roll = Random.Range(0f, 1000f);
            bool reproduce = roll <= splitChance;
            while (!reproduce) {
                roll = Random.Range(0f, 1000f);
                reproduce = roll <= splitChance;

                yield return new WaitForSeconds(1);
            }
            Instantiate(gameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(30 - (30 * splitChance / 1000));
        }
    }
}

