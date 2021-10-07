using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {

    public float hp;
    public float speed;
    public AnimationClip idle;
    public AnimationClip moving;
    public AnimationClip dead;
    public bool isDead;
    protected Animator anim;
    protected AudioSource audio1;
    protected AudioSource audio2;
    protected SpriteRenderer sR;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    void Fade()
    {
        Color c = sR.color;
        c.a = 0.5f;
        sR.color = c;
    }

    void Original()
    {
        Color c = sR.color;
        c.a = 1.0f;
        sR.color = c;
    } 

    public void Damaged()
    {
        Fade();
        Invoke("Original", 0.1f);
    }

}
