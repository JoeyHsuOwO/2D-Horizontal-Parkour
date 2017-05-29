using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


//敵人碰撞設定
public class Enemy : MonoBehaviour
{
    private float speed = 5f;
    private float timeCount = 0;
    private float timeCount1 = 0;
    public bool PlayerDead;
    private GameObject _Player;
    private Rigidbody2D rb2d;
    private bool EnemyDead;
    
    Animator anim;
    // Use this for initialization
    void Start()
    {
        _Player = GameObject.Find("Player");
        anim = _Player.gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        timeCount = 0;
        if (coll.gameObject.tag == "Player")
        {
            anim.SetBool("Dead", true);
            killPlayer();
        }
        if (coll.gameObject.tag == "Brush")
        {
            
            
            rb2d.AddForce(Vector2.right * 500000);
            EnemyDead = true;     
        }
        if(coll.gameObject.tag == "Ruler")
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "OP")
        {
            rb2d.AddForce(Vector2.right * 5000000);
            rb2d.AddForce(Vector2.down * 500000);
        }
        if(coll.gameObject.tag == "WaterPen")
        {
            
        }

    }
    // Update is called once per frame
    void Update()
    {
      
        if (EnemyDead)
        {
            timeCount += Time.deltaTime;
            if(timeCount >= 1)
            {
                Destroy(gameObject);
            }
        } 

    }
    private void killPlayer()
    {
        if (PlayerDead) return;
        PlayerDead = true;
        GameObject.Find("Main Camera").GetComponent<ScoreHandler>().sendToHighScore();
        
    }
    
    


}