using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//地圖生成
public class CreateBook_Boy : MonoBehaviour {

    private GameObject collectedTiles;
    private const float tileWidth = 75f;
    public GameObject tilePos;
    private float heightLevel = 0;
    private float PlayerSpeed = 10f;
    private GameObject gameLayer;
    private float timeCount = 0;
    private bool EnemySAdd = false;
    private bool EnemyBAdd = false;
    private bool MoneyAdd = false;
    private bool RainbowAdd;
    private GameObject tmpTile;
    private float startUpPosY;
    public GameObject DeadMenu;
    private float gameSpeed = 60f;
    private float MaxSpeed = 100.0f;
    private float outofbounceX;
    private float outofbounceY;
    private int blankCounter = 0;
    private int middleCounter = 0;
    private string lastTile = "right";
    private float StartTime;
    public bool isDead;
    private bool PlayerDead = false;
    private bool PlayerKilled = false;
    private GameObject _Player;
    public GameObject eventsystem;
    Animator anim;
    void Start()
    {
        gameLayer = GameObject.Find("gameLayer");

        collectedTiles = GameObject.Find("tiles");

        for (int i = 0; i < 30; i++)
        {
            GameObject tmpG1 = Instantiate(Resources.Load("book_left", typeof(GameObject))) as GameObject;
            tmpG1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
            tmpG1.transform.position = Vector3.zero;
            GameObject tmpG2 = Instantiate(Resources.Load("book_middle", typeof(GameObject))) as GameObject;
            tmpG2.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
            tmpG2.transform.position = Vector3.zero;
            GameObject tmpG3 = Instantiate(Resources.Load("book_right", typeof(GameObject))) as GameObject;
            tmpG3.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
            tmpG3.transform.position = Vector3.zero;
            GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
            tmpG4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
            tmpG4.transform.position = Vector3.zero;

        }

        tilePos = GameObject.Find("Start_Pos");
        startUpPosY = tilePos.transform.position.y;
        outofbounceX = tilePos.transform.position.x - 60f;
        outofbounceY = startUpPosY - 3.0f;
        _Player = GameObject.Find("Player");
        anim = _Player.gameObject.GetComponent<Animator>();
        DeadMenu.SetActive(false);
        fillScene();
        StartTime = Time.time;
    }



    void Update()
    {
        isDead = anim.GetBool("Dead");
        if (isDead)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= 0.5)
            {
                gameSpeed = 0;
                DeadMenu.SetActive(true);
                Time.timeScale = 0f;
                eventsystem.GetComponent<AudioSource>().enabled = false;
                GetComponent<ScoreHandleLibrary>().sendToHighScore();
            }
        }
    }

    void FixedUpdate()
    {

        if (gameSpeed != 0)
        {
            if (_Player.transform.position.x < outofbounceX + 120)
            {
                _Player.transform.Translate(Vector2.right * PlayerSpeed * Time.deltaTime);
            }
            randomizeEnemyB();
            randomizeMoney();

            if (Time.time % 2 == 0)
            {
                GetComponent<ScoreHandleLibrary>().Points += 10;
            }




            if (Time.time % 10 == 0)
            {
                gameSpeed += 0.5f;
                gameSpeed = Mathf.Clamp(gameSpeed, 60, 100);
            }

            gameLayer.transform.position = new Vector2(gameLayer.transform.position.x - gameSpeed * Time.deltaTime, 0);





            foreach (Transform child in gameLayer.transform)
            {

                if (child.position.x < outofbounceX)
                {

                    switch (child.gameObject.name)
                    {

                        case "book_left(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("gLeft").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
                            break;
                        case "book_middle(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("gMiddle").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
                            break;
                        case "book_right(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("gRight").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
                            break;
                        case "blank(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("gBlank").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
                            break;
                        case "Scaner(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("EnemiesS").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("EnemiesS").transform;
                            break;
                        case "Box(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("EnemiesB").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("EnemiesB").transform;
                            break;
                        case "Money_library(Clone)":
                            child.gameObject.transform.position = collectedTiles.transform.FindChild("money").transform.position;
                            child.gameObject.transform.parent = collectedTiles.transform.FindChild("money").transform;
                            break;
                        case "Rainbow":
                            GameObject.Find("Rainbow").GetComponent<Rainbow>().inPlay = false;
                            break;
                        default:
                            Destroy(child.gameObject);
                            break;

                    }


                }




            }


            if (gameLayer.transform.childCount < 20)
                spawnTile();
            if (_Player.transform.position.y < outofbounceY)
            {
                killPlayer();
            }

            if (_Player.transform.position.x < outofbounceX + 48)
            {
                killPlayer();
            }


        }

    }

    private void killPlayer()
    {
        if (PlayerDead) return;
        PlayerDead = true;
        anim.SetBool("Dead", true);
        DeadMenu.SetActive(true);
        Time.timeScale = 0f;
        eventsystem.GetComponent<AudioSource>().enabled = false;
        GetComponent<ScoreHandleLibrary>().sendToHighScore();


    }


    private void fillScene()
    {
        //Fill start
        for (int i = 0; i < 8; i++)
        {
            setTile("middle");
        }
        setTile("right");
    }

    private void setTile(string type)
    {
        switch (type)
        {
            case "left":
                tmpTile = collectedTiles.transform.FindChild("gLeft").transform.GetChild(0).gameObject;
                break;
            case "middle":
                tmpTile = collectedTiles.transform.FindChild("gMiddle").transform.GetChild(0).gameObject;
                break;
            case "right":
                tmpTile = collectedTiles.transform.FindChild("gRight").transform.GetChild(0).gameObject;
                break;
            case "blank":
                tmpTile = collectedTiles.transform.FindChild("gBlank").transform.GetChild(0).gameObject;
                break;
        }
        tmpTile.transform.parent = gameLayer.transform;
        tmpTile.transform.position = new Vector3(tilePos.transform.position.x + (tileWidth), startUpPosY + (heightLevel * tileWidth), 0);
        tilePos = tmpTile;
        lastTile = type;
    }

    private void spawnTile()
    {

        if (blankCounter > 0)
        {
            randomizeEnemyB();
            setTile("blank");
            blankCounter--;
            return;
        }
        EnemyBAdd = false;
        if (middleCounter > 0)
        {

            randomizeEnemyS();
            randomizeMoney();
            setTile("middle");
            middleCounter--;
            return;
        }
        EnemySAdd = false;
        MoneyAdd = false;

        if (lastTile == "blank")
        {

            changeHeight();
            setTile("left");
            middleCounter = (int)Random.Range(1, 4);
        }
        else if (lastTile == "right")
        {

            blankCounter = (int)Random.Range(1, 2);
        }
        else if (lastTile == "middle")
        {
            setTile("right");
        }


    }

    private void changeHeight()
    {
        float newHeightLevel = (float)Random.Range(0, 2);
        if (newHeightLevel < heightLevel)
            heightLevel = heightLevel - 0.05f;
        else if (newHeightLevel > heightLevel)
            heightLevel = heightLevel + 0.05f;
    }

    private void randomizeEnemyS()
    {
        if (EnemySAdd)
        {
            return;
        }

        if ((int)Random.Range(1, 10) <= 2)
        {

            GameObject EnemyS = Instantiate(Resources.Load("Scaner", typeof(GameObject))) as GameObject;
            EnemyS.transform.parent = collectedTiles.transform.FindChild("EnemiesS").transform;

            EnemyS.transform.parent = gameLayer.transform;
            EnemyS.transform.position = new Vector2(tilePos.transform.position.x, tilePos.transform.position.y + 25);

            EnemySAdd = true;
        }

    }

    private void randomizeEnemyB()
    {
        if (EnemyBAdd)
        {
            return;
        }

        if ((int)Random.Range(1, 900) == 1)
        {
            GameObject EnemyB = Instantiate(Resources.Load("Box", typeof(GameObject))) as GameObject;
            EnemyB.transform.parent = collectedTiles.transform.FindChild("EnemiesB").transform;

            EnemyB.transform.parent = gameLayer.transform;
            EnemyB.transform.position = new Vector2(_Player.transform.position.x + (int)Random.Range(250, 300), _Player.transform.position.y + (int)Random.Range(90, 100));


            EnemyBAdd = true;
        }

    }
    private void randomizeMoney()
    {
        if (MoneyAdd)
        {
            return;
        }

        if ((int)Random.Range(1, 50) == 1)
        {
            GameObject money = Instantiate(Resources.Load("money_library", typeof(GameObject))) as GameObject;
            money.transform.parent = collectedTiles.transform.FindChild("MoneyBank").transform;

            money.transform.parent = gameLayer.transform;
            money.transform.position = new Vector2(_Player.transform.position.x + (float)Random.Range(200, 300), _Player.transform.position.y + (float)Random.Range(20, 22));

            MoneyAdd = true;
        }

    }
}
