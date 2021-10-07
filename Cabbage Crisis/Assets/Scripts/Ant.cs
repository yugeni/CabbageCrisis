using UnityEngine;
using System.Collections;

public class Ant : Animal {

    GameObject target;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Smoke")
        {
            hp = 0;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponents<AudioSource>()[0];
        audio2 = GetComponents<AudioSource>()[1];
        target = null;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (hp > 0)
            {
                Tapped();
                if (target != null)
                {
                    MoveToLadybird();
                }
                else
                {
                    FindTarget();
                    Move();
                }
            }
            else
            {
                anim.Play(dead.name);
                GetComponent<Collider2D>().enabled = false;
                Invoke("DestroySelf", 2.0f);
            }
        }
    }

    void Tapped()
    {
        if (Buttons.buttonType == 0)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10.0f));
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    audio2.Play();
                    hp = 0;
                }
            }
        }
    }

    void Move()
    {
        if(transform.position.x >= 6.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.x <= -6.0f)
        {
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        }
        anim.Play(moving.name);
        transform.Translate(new Vector2(-speed, 0));
    }

    void Kill()
    {
        if (target != null)
        {
            if (target.tag == "Ladybird" && target.GetComponent<Ladybird>().hp > 0)
            {
                audio1.Play();
                target.GetComponent<Ladybird>().Killed();
            }
            else if(target.tag == "GroundBeetle" && target.GetComponent<GroundBeetle>().hp > 0)
            {
                audio1.Play();
                target.GetComponent<GroundBeetle>().Killed();
            }
        }
    }

    void FindTarget()
    {
        GameObject[] targets1 = GameObject.FindGameObjectsWithTag("Ladybird");
        GameObject[] targets2 = GameObject.FindGameObjectsWithTag("GroundBeetle");
        int size = targets1.Length + targets2.Length;
        GameObject[] targets = new GameObject[size];
        int i = 0;
        if (targets1.Length > 0)
        {
            foreach (GameObject targeted in targets1)
            {
                targets[i] = targeted;
                i++;
            }
        }
        if (targets2.Length > 0)
        {
            foreach (GameObject targeted in targets2)
            {
                targets[i] = targeted;
                i++;
            }
        }
        if (targets.Length > 0)
        {
            foreach (GameObject targeted in targets)
            {
                if (targeted.tag == "Ladybird")
                {
                    if (targeted.GetComponent<Ladybird>().hp > 0)
                    {
                        target = targeted;
                        break;
                    }
                }
                else
                {
                    if (targeted.GetComponent<GroundBeetle>().hp > 0)
                    {
                        target = targeted;
                        break;
                    }
                }
            }
        }
    }

    void MoveToLadybird()
    {
        if (target.tag == "Ladybird")
        {
            if (target.GetComponent<Ladybird>().hp > 0)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > 0.1f)
                {
                    if (transform.position.x > target.transform.position.x)
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (transform.position.x < target.transform.position.x)
                        transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                    anim.Play(moving.name);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * 2.0f);
                }
                else
                {
                    anim.Play(idle.name);
                    Kill();
                }
            }
            else
            {
                target = null;
            }
        }
        else
        {
            if (target.GetComponent<GroundBeetle>().hp > 0)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > 0.1f)
                {
                    if (transform.position.x > target.transform.position.x)
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (transform.position.x < target.transform.position.x)
                        transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                    anim.Play(moving.name);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * 2.0f);
                }
                else
                {
                    anim.Play(idle.name);
                    Kill();
                }
            }
            else
            {
                target = null;
            }
        }
    }
}
