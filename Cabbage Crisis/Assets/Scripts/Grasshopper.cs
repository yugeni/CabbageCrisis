using UnityEngine;
using System.Collections;

public class Grasshopper : Animal {

    public int type;
    GameObject cabbage;
    bool reached;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Cabbage" && !reached)
        {
            anim.Play(idle.name);
            reached = true;
            InvokeRepeating("Bite", 0, 2.0f);
        }
        if (col.gameObject.tag == "Smoke")
        {
            if (type != 2)
            {
                hp = 0;
            }
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponents<AudioSource>()[0];
        audio2 = GetComponents<AudioSource>()[1];
        sR = GetComponent<SpriteRenderer>();
        reached = false;
        cabbage = GameObject.FindGameObjectWithTag("Cabbage");
    }


    void Update()
    {
        if (Time.timeScale != 0)
        {
            Death();
            Tapped();
            Move();
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
                    Damaged();
                    hp--;
                }
            }
        }
    }

    void Move()
    {
        if (cabbage != null && !reached && !isDead)
        {
            if (transform.position.x > cabbage.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.position.x < cabbage.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            }
            anim.Play(moving.name);
        }
    }

    void Bite()
    {
        if (cabbage != null && hp > 0)
        {
            audio1.Play();
            cabbage.GetComponent<Cabbage>().Bitten();
        }
    }

    void Death()
    {
        if (hp <= 0)
        {
            isDead = true;
            anim.Play(dead.name);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            Invoke("DestroySelf", 2.0f);
        }
    }
}
