using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets.Scripts.Genes;

public class Organism : MonoBehaviour {

    public List<Gene> genes;

    //gene count
    public int genome;

    private int _genome {
        get {
            genome = genes.Count;
            return genome;
        }
    }


    //base stats
    public float baseMoveSpeed;
    public float baseSplitChance;
    public float baseSize;

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
        if (genes == null) genes = new List<Gene>();

        StartCoroutine(randomMovement());
        StartCoroutine(addGenes());
        StartCoroutine(refreshGenetics());
        StartCoroutine(split());
    }

    IEnumerator addGenes() {
        while (true) {
            genes.Add(new Fast(this));
            genes.Add(new Fertile(this));
            genes.Add(new Big(this));
            yield return new WaitForSeconds(1f);
        }

    }

    // Update is called once per frame
    void Update() {
        transform.localScale = new Vector2(size, size);
    }

    //apply genetic bonuses, whatever they may be
    void applyGenetics(List<Gene> genes) {
        genes.ForEach((gene) => {
            gene.apply();
        });
        genome = _genome;
    }

    //recalculate genetic bonuses when they change
    IEnumerator refreshGenetics() {
        List<Gene> oldGenes = genes.ToList();
        while (true) {
            if (oldGenes.SequenceEqual(genes)) {
                yield return null;
            } else {
                resetGenetics();
                applyGenetics(genes);
                oldGenes = genes.ToList();
                yield return null;
            }
        }

    }

    //zero out genetic bonuses
    private void resetGenetics() {
        geneticMoveSpeed = 0;
        geneticSplitChance = 0;
        geneticSize = 0;
    }

    IEnumerator randomMovement() {
        while (true) {
            //choose random target location
            Vector2 target = new Vector2(
            Mathf.Clamp(transform.position.x + Random.Range(-moveSpeed, moveSpeed), -60f, 60f),
            Mathf.Clamp(transform.position.y + Random.Range(-moveSpeed, moveSpeed), -33f, 33f)
            );
            //time since target chosen
            var time = Time.realtimeSinceStartup;

            //move to target
            while (Vector2.Distance(transform.position, target) > Random.Range(0.05f, 0.5f)) {
                transform.position = new Vector2(
                        Mathf.Lerp(transform.position.x, target.x, Mathf.Pow(moveSpeed, -4) * Time.deltaTime * Random.Range(0.5f, 1.5f)),
                        Mathf.Lerp(transform.position.y, target.y, Mathf.Pow(moveSpeed, -4) * Time.deltaTime * Random.Range(0.5f, 1.5f))
                    );

                //give up on target if it takes too long to get there
                if (time.absDiff(Time.realtimeSinceStartup) > Random.Range(1f, 5f)) target = transform.position;

                yield return null;
            }
            var randomSeconds = Random.Range(0f, 1f) * Random.Range(0f, 3f) / Random.Range(1f, 3f);
            //Debug.Log(string.Format("random seconds: {0}", randomSeconds));
            yield return new WaitForSeconds(randomSeconds * 0.25f);
        }
    }

    IEnumerator split() {
        yield return new WaitForSeconds(30);
        while (true) {
            float roll = Random.Range(0f, 1000f);
            bool reproduce = roll <= splitChance;
            while (!reproduce) {
                roll = Random.Range(0f, 1000f);
                reproduce = roll <= splitChance;
                //Debug.Log(roll);
                //Debug.Log(reproduce);

                yield return new WaitForSeconds(1);
            }
            Instantiate(gameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(30 - (30 * splitChance/1000));
        }
    }
}

