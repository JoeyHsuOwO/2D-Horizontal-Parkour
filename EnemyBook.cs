using UnityEngine;
using System.Collections;
using Pathfinding;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
//敵人尋路設定
public class EnemyBook : MonoBehaviour
{
    public float speed = 6000f;
    private float timeCount = 0;
    private float timeCount1 = 0;
    public Transform target;
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb2d;

    public Path path;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;

    private bool PlayerDead;
    private GameObject _Player;
    Animator anim;
    // Use this for initialization
   
    void Start()
    {
        _Player = GameObject.Find("Player");
        anim = _Player.gameObject.GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        target = _Player.transform;
        if(target == null)
        {
            Debug.LogError("No Player Set");
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath ()
    {
        
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            anim.SetBool("Dead", true);
            killPlayer();
            
        }
        if (coll.gameObject.tag == "Brush")
        {
            rb2d.AddForce(Vector2.right * 500000);
            rb2d.AddForce(Vector2.up * 500000);
        }
        if (coll.gameObject.tag == "Ruler")
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "OP")
        {
            rb2d.AddForce(Vector2.right * 500000);
            rb2d.AddForce(Vector2.down * 500000);
        }
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
       
        
        timeCount += Time.deltaTime;
        if(timeCount >=10)
        {
            Destroy(gameObject);
        }
       if(target == null)
        {
            return;
        }
       if(path == null)
            return;

       if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        rb2d.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if(dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
    private void killPlayer()
    {
        if (PlayerDead) return;
        PlayerDead = true;
        
        GameObject.Find("Main Camera").GetComponent<ScoreHandler>().sendToHighScore();
        
    }
    
    
}