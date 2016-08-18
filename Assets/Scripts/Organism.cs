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

    //genetic bonuses
    public float geneticMoveSpeed;

    //final stats properties
    public float moveSpeed { get { return baseMoveSpeed + geneticMoveSpeed; } }

    // Use this for initialization
    void Start() {
        genes = new List<Gene>();

        5.times(i => {
            genes.Add(new Fast(this));
        });
        applyGenetics(genes);

        StartCoroutine(randomMovement());
        StartCoroutine(refreshGenetics());
        StartCoroutine(addFast());
    }

    IEnumerator addFast() {
        while (true) {
            genes.Add(new Fast(this));
            yield return new WaitForSeconds(1f);
        }
        
    }

    // Update is called once per frame
    void Update() {
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
    }

    IEnumerator randomMovement() {
        while (true) {
            Vector2 target = new Vector2(
            transform.position.x + Random.Range(-moveSpeed, moveSpeed),
            transform.position.y + Random.Range(-moveSpeed, moveSpeed));

            while (Vector2.Distance(transform.position, target) > Random.Range(0.05f, 0.5f)) {
                transform.position = new Vector2(
                        Mathf.Lerp(transform.position.x, target.x, Mathf.Sqrt(moveSpeed) * Time.deltaTime * Random.Range(0.5f, 1.5f)),
                        Mathf.Lerp(transform.position.y, target.y, Mathf.Sqrt(moveSpeed) * Time.deltaTime * Random.Range(0.5f, 1.5f))
                    );
                yield return null;
            }
            var randomSeconds = Random.Range(0f, 1f) * Random.Range(0f, 3f) / Random.Range(1f, 3f);
            Debug.Log(string.Format("random seconds: {0}", randomSeconds));
            yield return new WaitForSeconds(randomSeconds * 0.25f);
        }
    }
}

