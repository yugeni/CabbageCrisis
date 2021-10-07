using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	public static int buttonType = 0;
    public GameObject smoke;
    public GameObject ladybird;
    public GameObject groundBeetle;
    public Button PesticideButton;
    public Button LadybirdButton;
    public Button GroundBeetleButton;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 0)
            {
                if (buttonType == 2)
                {
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10.0f));
                    if (touchPos.y >= -2.5f && touchPos.y <= 3.5f)
                    {
                        Instantiate(smoke, touchPos, Quaternion.identity);
                        PesticideButton.interactable = false;
                        PesticideButton.GetComponent<Cooldown>().CoolDown();
                        buttonType = 0;
                    }
                }
            }
        }
    }

    public void Hand()
    {
        buttonType = 0;
    }

    public void Water()
    {
        buttonType = 1;
    }

    public void Pesticide()
    {
        buttonType = 2;
    }

    public void SummonLadybird()
    {
        buttonType = 0;
        Instantiate(ladybird, transform.position, Quaternion.identity);
        LadybirdButton.interactable = false;
        LadybirdButton.GetComponent<Cooldown>().CoolDown();
    }

    public void SummonGroundBeetle()
    {
        buttonType = 0;
        Instantiate(groundBeetle, transform.position, Quaternion.identity);
        GroundBeetleButton.interactable = false;
        GroundBeetleButton.GetComponent<Cooldown>().CoolDown();
    }
}
