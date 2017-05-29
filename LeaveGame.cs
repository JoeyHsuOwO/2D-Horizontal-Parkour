using UnityEngine;
using System.Collections;

public class LeaveGame : MonoBehaviour {
    public GameObject leaveScreen;
    public GameObject widget;
    private bool leave;
	// Use this for initialization
	void Start () {
        leaveScreen.SetActive(false);
        widget.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_STANDALONE
        MouseInput();   // 滑鼠偵測
#elif UNITY_ANDROID
		MobileInput();  // 觸碰偵測
#endif
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (leave)
            {
                leaveScreen.SetActive(false);
                leave = false;
                widget.SetActive(false);
            }
            else
            {
                leaveScreen.SetActive(true);
                leave = true;
                widget.SetActive(true);
            }
        }
        }

    void MouseInput()
    {
       
        
        if (Input.GetMouseButton(0))
        {

           


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
                


            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
               
            }
            

            //手指離開螢幕
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

            }

        }
    }
    public void yButton()
    {
        Application.Quit();
    }
    public void nButton()
    {
        leave = false;
        leaveScreen.SetActive(false);
        widget.SetActive(false);
    }
}
