using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

	void Start ()
    {
        Invoke("DestroySelf", 0.67f);
	}

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
