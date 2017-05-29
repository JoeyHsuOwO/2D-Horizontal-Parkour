using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//觸控操作及畫筆切換
public class Touch : MonoBehaviour
{

    //紀錄手指觸碰位置
    private Vector2 m_screenPos = new Vector2();
    private GameObject _Player;
    float d;//距離
    float x1, y1;
    float x, y;
    public GameObject Pen_Create;
    private float speed = 100f;
    private bool RulerAdd;
    private float RulerTime = 0;
    public string penName = "pencil";
    public GameObject BrushI;
    private float BT;
    private bool BI;
    public GameObject PencilI;
    private float PT;
    private bool PI;
    public GameObject PenI;
    private float PeT;
    private bool PeI;
    public GameObject CrayonI;
    private float CT;
    private bool CI;
    public GameObject WaterI;
    private float WT;
    private bool WI;
    private float time_count = 5f;
    private bool fly;
    private float flyTime;

    Animator anim;

    private float outofbounceX;
    void Start()
    {        
        _Player = GameObject.Find("Player");
        anim = _Player.gameObject.GetComponent<Animator>();
        BrushI.SetActive(false);
        PencilI.SetActive(false);
        PenI.SetActive(false);
        CrayonI.SetActive(false);
        WaterI.SetActive(false);
    }
    
    void Update()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        MouseInput();   // 滑鼠偵測
#elif UNITY_ANDROID
		MobileInput();  // 觸碰偵測
#endif

        if(BI)
        {
            BT += Time.deltaTime;
        }
        else
        {
            BT = 0;
        }
        if(BT >= 1)
        {
            BrushI.SetActive(false);
            BI = false;
        }

        if (PI)
        {
            PT += Time.deltaTime;
        }
        else
        {
            PT = 0;
        }
        if (PT >= 1)
        {
            PencilI.SetActive(false);
            PI = false;
        }

        if (PeI)
        {
            PeT += Time.deltaTime;
        }
        else
        {
            PeT = 0;
        }
        if (PeT >= 1)
        {
            PenI.SetActive(false);
            PeI = false;
        }

        if (CI)
        {
            CT += Time.deltaTime;
        }
        else
        {
            CT = 0;
        }
        if (CT >= 1)
        {
            CrayonI.SetActive(false);
            CI = false;
        }

        if (WI)
        {
            WT += Time.deltaTime;
        }
        else
        {
            WT = 0;
        }
        if (WT >= 1)
        {
            WaterI.SetActive(false);
            WI = false;
        }

        if(RulerAdd)
        {
            GameObject Ruler = Instantiate(Resources.Load("Ruler", typeof(GameObject))) as GameObject;
            Ruler.transform.position = new Vector2(_Player.transform.position.x + 5, _Player.transform.position.y);
            RulerAdd = false;
        }

        if(fly)
        {
            flyTime += Time.deltaTime;

            _Player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            _Player.transform.gameObject.tag = "OP";
        }
        else
        {
            flyTime = 0;
            
        }
        if(flyTime >= 10)
        {
            fly = false;
            _Player.transform.gameObject.tag = "Player";
            anim.SetBool("Fly", false);
            _Player.GetComponent<Rigidbody2D>().gravityScale = 50f;
        }
       
    }
   
    public void ChangPen(string pen)
    {

        pen = "Brush";
        penName = pen;
        BrushI.SetActive(true);
        BI = true;
    }
    public void ChangPen2(string pen)
    {
        
        pen = "pencil";
        penName = pen;
        PencilI.SetActive(true);
        PI = true;
    }
    public void ChangPen3(string pen)
    {
        
        pen = "Pen";
        penName = pen;
        PenI.SetActive(true);
        PeI = true;

    }
    public void ChangPen4(string pen)
    {
        
        pen = "crayon";
        penName = pen;
        CrayonI.SetActive(true);
        CI = true;
    }
    public void ChangPen5(string pen)
    {

        pen = "WaterColorPen";
        penName = pen;
        WaterI.SetActive(true);
        WI = true;
    }
    public void RulerAttack()
    {
        RulerAdd = true;       
        
    }


    public void Eraser()
    {
       GameObject[] BrushC = GameObject.FindGameObjectsWithTag("Brush");
        for(int i = 0; i< BrushC.Length; i++)
        {
            Destroy(BrushC[i]);
        }
        GameObject[] PenC = GameObject.FindGameObjectsWithTag("Pen");
        for (int i = 0; i < PenC.Length; i++)
        {
            Destroy(PenC[i]);
        }       
    }

    public void Fly()
    {
        anim.SetBool("Fly", true);
        fly = true;
        _Player.transform.position = new Vector3(_Player.transform.position.x, _Player.transform.position.y + 25 , _Player.transform.position.z);
    }

    void MouseInput()
    {
        
        if (Input.GetMouseButton(0))
        {
            
            Pen_Create = Resources.Load(penName, typeof(GameObject)) as GameObject;
            m_screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            x = Input.mousePosition.x;
            y = Input.mousePosition.y;
            Vector3 Math_Point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 40));
           Instantiate(Pen_Create, Math_Point, Quaternion.identity);
            
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            
      
        
        }
        
        
        
    }


    void MobileInput()
    {
        if (Input.touchCount <= 0)
            return;

        //1個手指觸碰螢幕
        if (Input.touchCount == 1)
        {

            //開始觸碰
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                m_screenPos = Input.touches[0].position;


            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Pen_Create = Resources.Load(penName, typeof(GameObject)) as GameObject;
                x = Input.mousePosition.x;
                y = Input.mousePosition.y; 
                Vector3 Math_Point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 40));
                Instantiate(Pen_Create, Math_Point, Quaternion.identity);
            }


            //手指離開螢幕
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

            }

        }
    }
}