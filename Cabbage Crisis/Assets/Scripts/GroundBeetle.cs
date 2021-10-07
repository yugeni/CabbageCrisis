using UnityEngine;
using System.Collections;
using Behave.Runtime;
using Tree = Behave.Runtime.Tree;

public class GroundBeetle : BANewBehaveLibrary1_NewAgentBlueprint1 {

    public float hp;
    public float speed;
    public AnimationClip idle;
    public AnimationClip moving;
    public AnimationClip dead;
    public AnimationClip scared;
    GameObject target;
    Animator anim;
    AudioSource audio1;
    Tree tree;
    int killCount;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Smoke")
        {
            Killed();
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponent<AudioSource>();
        killCount = 0;
        target = null;
        tree = BLNewBehaveLibrary1.InstantiateTree(BLNewBehaveLibrary1.TreeType.GroundBeetleTree, this);
    }

    public override BehaveResult TickCheckAntsAction(Tree sender)
    {
        target = null;
        if (AliveAnts())
        {
            if (Vector2.Distance(transform.position, new Vector3(0, 0.7f, transform.position.z)) <= 0.1f)
            {
                anim.Play(scared.name);
                return BehaveResult.Failure;
            }
            else
            {
                if (transform.position.x > 0)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (transform.position.x < 0)
                    transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                anim.Play(moving.name);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0.7f, transform.position.z), speed);
                return BehaveResult.Running;
            }
        }
        else
        {
            anim.Play(idle.name);
            return BehaveResult.Success;
        }
    }

    public override BehaveResult TickFindPreyAction(Tree sender)
    {
        GameObject[] preys1 = GameObject.FindGameObjectsWithTag("Aphid");
        GameObject[] preys2 = GameObject.FindGameObjectsWithTag("Caterpillar");
        GameObject[] preys3 = GameObject.FindGameObjectsWithTag("Snail");
        GameObject[] preys4 = GameObject.FindGameObjectsWithTag("Grasshopper");
        int size = preys1.Length + preys2.Length + preys3.Length + preys4.Length;
        GameObject[] preys = new GameObject[size];
        int i = 0;
        foreach(GameObject prey in preys1)
        {
            preys[i] = prey;
            i++;
        }
        foreach (GameObject prey in preys2)
        {
            preys[i] = prey;
            i++;
        }
        foreach (GameObject prey in preys3)
        {
            preys[i] = prey;
            i++;
        }
        foreach (GameObject prey in preys4)
        {
            preys[i] = prey;
            i++;
        }

        if (size <= 0)
            return BehaveResult.Failure;
        else
        {
            int random = Random.Range(0, size);
            target = preys[random];
            return BehaveResult.Success;
        }
    }

    public override BehaveResult TickMoveToPreyAction(Tree sender)
    {
        if (target == null || target.GetComponent<Animal>().isDead || AliveAnts())
        {
            return BehaveResult.Failure;
        }
        else
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
            {
                return BehaveResult.Success;
            }
            else
            {
                anim.Play(moving.name);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
                return BehaveResult.Running;
            }
        }
    }

    public override BehaveResult TickKillPreyAction(Tree sender)
    {
        if (target == null || target.GetComponent<Animal>().isDead || AliveAnts())
        {
            return BehaveResult.Failure;
        }
        else
        {
            anim.Play(idle.name);
            audio1.Play();
            target.GetComponent<Animal>().hp = 0;
            killCount++;
            return BehaveResult.Success;
        }
    }

    void FixedUpdate()
    {
        if (hp > 0)
        {
            if (killCount >= 12)
                Destroy(gameObject);
            if (tree != null)
            {
                tree.Tick();
            }
            if (target != null)
            {
                if (transform.position.x > target.transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (transform.position.x < target.transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            }
        }
        else
        {
            anim.Play(dead.name);
            Invoke("DestroySelf", 2.0f);
        }
    }

    public void Killed()
    {
        hp = 0;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    bool AliveAnts()
    {
        if (GameObject.FindGameObjectWithTag("Ant") != null)
        {
            GameObject[] ants = GameObject.FindGameObjectsWithTag("Ant");
            foreach (GameObject ant in ants)
            {
                if (ant.GetComponent<Animal>().hp > 0)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
