using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour {

    public Button[] buttons;
    public int page;

	void Update ()
    {
        Unlocker();
	}

    void Unlocker()
    {
        for(int i = 0; i <= (PlayerPrefs.GetInt("Level") - page); i++)
        {
            if (i < buttons.Length)
                buttons[i].interactable = true;
        }
    }
}
