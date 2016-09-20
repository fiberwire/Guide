using UnityEngine;
using System.Collections;

public class Organism : MonoBehaviour {

    public delegate void LifeCycleEvent();

    public event LifeCycleEvent OnDeath;
    public event LifeCycleEvent OnDamage;

    GuideCamera cam;

    //stats
    public float maxHealth;
    public float health;
    public float maxEnergy;
    public float energy;

    public bool initializedHealthAndEnergy = false;
    public bool regenDisabled = false;

    public Genome genome;
    public Stats stats;
    public Genetics genetics;
    public delegate void GenomeChanged(Genome gen);
    public event GenomeChanged genomeChanged;

    public int genomeCount;

    // Use this for initialization
    void Start() {

        OnDeath += () => {
            Destroy(gameObject);
        };
        
        cam = Camera.main.GetComponent<GuideCamera>();

        if (genome == null) genome = new Genome(this);
        genomeChanged += genetics.apply;

        StartCoroutine(move());
        StartCoroutine(regenHealth());
        genomeChanged(genome);
    }

    IEnumerator regenHealth() {
        while (true) {
            if (regenDisabled) yield return null;

            var regen = stats.HealthRegen;

            if (regen <= 0) yield return null;

            if (health + regen <= maxHealth) {
                health += regen;
                Debug.Log("Regenerated " + regen + " health.");
            }

            yield return new WaitForSeconds(1);
        }
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
                            Mathf.Lerp(transform.position.x, target.x, stats.MoveSpeed * Time.deltaTime * Random.Range(0.5f, 1.5f)),
                            Mathf.Lerp(transform.position.y, target.y, stats.MoveSpeed * Time.deltaTime * Random.Range(0.5f, 1.5f))
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
                transform.position.x + Random.Range(-stats.MoveSpeed, stats.MoveSpeed),
                cam.left, cam.right),
            Mathf.Clamp(
                transform.position.y + Random.Range(-stats.MoveSpeed, stats.MoveSpeed),
                cam.bottom, cam.top)
            );
    }

    public void DoDamage(float damage){
        if (!initializedHealthAndEnergy) return;

        if (health > damage){ //if damage won't kill organism
            health -= damage;
            if (OnDamage != null) {
                OnDamage();
            }
        }
        else { //if damage will kill organism
            health = 0;
            if (OnDeath != null) {
                OnDeath();
            }
        }
    }
}

