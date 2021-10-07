using UnityEngine;
using System.Collections;

public class Aphid : Animal {

    GameObject cabbage;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Smoke")
        {
            hp = 0;
        }
    }

    void Start ()
    {
        cabbage = GameObject.FindGameObjectWithTag("Cabbage");
        InvokeRepeating("Bite", 3.0f, 2.0f);
    }
	

	void FixedUpdate ()
    {
        if(hp <= 0)
        {
            DestroySelf();
        }
	}

    void Bite()
    {
        if (cabbage != null)
        {
            cabbage.GetComponent<Cabbage>().Bitten();
        }
    }
}
