using UnityEngine;
using System.Collections;

public class MoneyScript : MonoBehaviour {

    private float maxY;
    private float minY;
    public bool inPlay = true;
    private bool releaseMoney = false;

    // Use this for initialization
    void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.gameObject.tag == "Player")
        {
            GameObject.Find("Main Camera").GetComponent<ScoreHandler>().Points += 87;
            Destroy(gameObject);
        }
        if (Coll.gameObject.tag == "OP")
        {
            GameObject.Find("Main Camera").GetComponent<ScoreHandler>().Points += 87;
            Destroy(gameObject);
        }


    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
