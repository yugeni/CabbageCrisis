using UnityEngine;
using System.Collections;
using Behave.Runtime;
using Tree = Behave.Runtime.Tree;

public class Ladybird : BANewBehaveLibrary1_NewAgentBlueprint1 {

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

    void Start ()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponent<AudioSource>();
        killCount = 0;
        target = null;
        tree = BLNewBehaveLibrary1.InstantiateTree(BLNewBehaveLibrary1.TreeType.LadybirdTree, this);
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

    public override BehaveResult TickFindAphidsAction(Tree sender)
    {
        if (GameObject.FindGameObjectWithTag("Aphid") == null)
            return BehaveResult.Failure;
        else
        {
            target = GameObject.FindGameObjectWithTag("Aphid");
            return BehaveResult.Success;
        }
    }

    public override BehaveResult TickFindCaterpillarsAction(Tree sender)
    {
        if (GameObject.FindGameObjectWithTag("Caterpillar") == null)
            return BehaveResult.Failure;
        else
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Caterpillar");
            foreach(GameObject targeted in targets)
            {
                if (targeted.GetComponent<Animal>().isDead == false)
                {
                    target = targeted;
                    break;
                }
            }
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
            if (target.tag == "Caterpillar")
            {
                if (target.GetComponent<Caterpillar>().type == 1)
                    Killed();
            }
            killCount++;
            return BehaveResult.Success;
        }
    }

    void FixedUpdate ()
    {
        if(hp > 0)
        {
            if (killCount >= 7)
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
