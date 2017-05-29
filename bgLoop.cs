using UnityEngine;
using System.Collections;


public class bgLoop : MonoBehaviour
{
    public float speed = 0.2f;
    Animator anim;
    public GameObject _Player;
    private bool dead;
    void Start()
    {
        _Player = GameObject.Find("Player");
        anim = _Player.gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        dead = anim.GetBool("Dead");
        if(dead == false)
        {
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * speed/100, 0);
        }
        
    }
}