using UnityEngine;
using System.Collections;

public class Cabbage : MonoBehaviour {

    public GameObject waterDrop;
    public Sprite original;
    public Sprite damaged;
    public float thirstyTime;
    float startTime;
    int hp;
    bool thirsty;
    bool timer;

    void Start () 
    {
        hp = 10;
        thirsty = false;
        timer = false;
        if (thirstyTime != 0)
        {
            Invoke("Thirsty", thirstyTime);
        }
	}
	

	void Update () 
    {
        if (Time.timeScale != 0)
        {
            if (hp <= 0)
                Destroy(gameObject);
            else
            {
                Tapped();
                if (thirsty && !timer)
                {
                    startTime = Time.time;
                    timer = true;
                }
                if (timer)
                {
                    if (startTime <= Time.time - 3.0f)
                    {
                        hp--;
                        startTime = Time.time;
                    }
                }
            }
        }
	}

    void Thirsty()
    {
        if (!thirsty)
        {
            waterDrop.SetActive(true);
            thirsty = true;
        }
    }

    void Quenched()
    {
        waterDrop.SetActive(false);
        thirsty = false;
        timer = false;
        Invoke("Thirsty", thirstyTime);
    }

    void OriginalSprite()
    {
        GetComponent<SpriteRenderer>().sprite = original;
    }

    void DamagedSprite()
    {
        GetComponent<SpriteRenderer>().sprite = damaged;
    }

    void Damaged()
    {
        DamagedSprite();
        Invoke("OriginalSprite", 0.1f);
    }

    public void Bitten()
    {
        Damaged();
        hp--;
    }

    void Tapped()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10.0f));
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && Buttons.buttonType == 1 && thirsty)
            {
                GetComponent<AudioSource>().Play();
                Quenched();
                Buttons.buttonType = 0;
            }
        }
    }
}
