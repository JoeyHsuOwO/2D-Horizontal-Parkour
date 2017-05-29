using UnityEngine;
using System.Collections;

//道具效果以及地圖偵測
public class Rainbow : MonoBehaviour {
    private float maxY;
    private float minY;
    private int direction = 1;
    private float RainbowTime = 0f;
    private GameObject _Player;
    public bool inPlay = true;
    private bool RainbowWrite;
    private bool releaseRainbow = false;


    private SpriteRenderer RainbowRender;

    // Use this for initialization
    void Start()
    {
        _Player = GameObject.Find("Player");
        maxY = this.transform.position.y + 10f;
        minY = maxY - 10.0f;

        RainbowRender = this.transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RainbowWrite)
        {
            GameObject RainbowPen = Instantiate(Resources.Load("RainbowPen", typeof(GameObject))) as GameObject;
            RainbowPen.transform.position = new Vector2(_Player.transform.position.x-1, _Player.transform.position.y);
            RainbowTime += Time.deltaTime;
        }
        else
        {
            RainbowTime = 0;
        }
        if(RainbowTime >= 5)
        {
            RainbowWrite = false;

        }
       
        
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + (direction * 0.5f));
        if (this.transform.position.y > maxY)
            direction = -1;
        if (this.transform.position.y < minY)
            direction = 1;

        if (!inPlay && !releaseRainbow)
            respawn();

    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {

            RainbowWrite = true;
            inPlay = false;
            this.transform.position = new Vector2(this.transform.position.x - 50f, this.transform.position.y + 100.0f);
        }
        else if (coll.gameObject.tag == "OP")
        {

            RainbowWrite = true;
            inPlay = false;
            this.transform.position = new Vector2(this.transform.position.x - 50f, this.transform.position.y + 100.0f);
        }
        
        
       
    }

    void respawn()
    {

        releaseRainbow = true;
        Invoke("placeRainbow", (float)Random.Range(3, 10));
    }

    void placeRainbow()
    {

        inPlay = true;
        releaseRainbow = false;

        this.transform.position = new Vector2(_Player.transform.position.x + (float)Random.Range(400, 500), _Player.transform.position.y + 2f);
        maxY = this.transform.position.y + 10f;
        minY = maxY - 10f;
    }
}
