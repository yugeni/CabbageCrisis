using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Modes : MonoBehaviour {

    public Button[] buttons;

	void Start ()
    {
        buttons[0].GetComponentInChildren<Text>().text = "Beat Lv 15 to unlock";
        buttons[1].GetComponentInChildren<Text>().text = "Beat Lv 19 to unlock";
        buttons[2].GetComponentInChildren<Text>().text = "Beat Lv 24 to unlock";
    }
	
	void Update ()
    {
        UnlockMode();
	}

    void UnlockMode()
    {
        if(PlayerPrefs.GetInt("Level") >= 15)
        {
            buttons[0].interactable = true;
            buttons[0].GetComponentInChildren<Text>().text = "1";
        }
        if (PlayerPrefs.GetInt("Level") >= 19)
        {
            buttons[1].interactable = true;
            buttons[1].GetComponentInChildren<Text>().text = "2";
        }
        if (PlayerPrefs.GetInt("Level") >= 24)
        {
            buttons[2].interactable = true;
            buttons[2].GetComponentInChildren<Text>().text = "3";
        }
    }
}
