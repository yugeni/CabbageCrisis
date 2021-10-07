using UnityEngine;
using System.Collections.Generic;

public class CSpawner : MonoBehaviour {

    public GameObject caterpillar;
    public int totalCount;
    public float startTime;
    public float spawnInterval;
    public float repeatRate;
    public bool normalLevel;
    public float destroyTime;
    int count;

    void Spawn()
    {
        if (caterpillar.tag != "Aphid")
        {
            List<Vector3> locations = new List<Vector3>();
            for (float i = -2.0f; i <= 3.0f; i = i + 1.0f)
            {
                Vector3 location1 = new Vector3(-9.0f, i, i);
                locations.Add(location1);
                Vector3 location2 = new Vector3(9.0f, i, i);
                locations.Add(location2);
            }
            int random = Random.Range(0, locations.Count);
            Instantiate(caterpillar, locations[random], Quaternion.identity);
            count++;
        }
        else
        {
            if(GameObject.FindGameObjectWithTag("Cabbage") != null)
            {
                GameObject cabbage = GameObject.FindGameObjectWithTag("Cabbage");
                Instantiate(caterpillar, new Vector3(cabbage.transform.position.x, cabbage.transform.position.y, cabbage.transform.position.z + 1.0f), Quaternion.identity);
                count++;
            }
        }
    }


	void Start ()
    {
        count = 0;
        InvokeRepeating("Spawn", startTime, repeatRate);
        if (normalLevel)
            Invoke("StopSpawning", destroyTime);
	}
	
	void FixedUpdate ()
    {
        if (GameObject.FindGameObjectWithTag("Cabbage") != null)
        {
            if (count >= totalCount)
            {
                CancelInvoke("Spawn");
                InvokeRepeating("Spawn", spawnInterval, repeatRate);
                count = 0;
            }
        }
        else
        {
            CancelInvoke("Spawn");
        }
	}

    void StopSpawning()
    {
        CancelInvoke("Spawn");
        Destroy(gameObject);
    }

}
