using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

    public int cooldownTime;
    public Text seconds;
    int time;

	void Start ()
    {
        time = cooldownTime;
	}
	
	void FixedUpdate ()
    {
        if(time <= 0)
        {
            seconds.enabled = false;
            CancelInvoke("MinusTime");
            time = cooldownTime;
            gameObject.GetComponent<Button>().interactable = true;
        }
        seconds.text = time.ToString();
	}

    public void CoolDown()
    {
        seconds.enabled = true;
        InvokeRepeating("MinusTime", 1.0f, 1.0f);
    }

    void MinusTime()
    {
        time--;
    }
}
