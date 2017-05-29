using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Loading : MonoBehaviour {

     public Scrollbar programBar;
     public Text displayText;
     public int displayProgress = 0;
     int toProgress = 0;
     AsyncOperation op;

     // Use this for initialization
     void Start () {
         StartCoroutine("StartLoading" , "NewTitle");
     }

     // Update is called once per frame
     void Update () {

     }


     private IEnumerator StartLoading(string sceneName)
     {



         op = SceneManager.LoadSceneAsync(sceneName);
         op.allowSceneActivation = false;
         while (op.progress < 0.9f)
         {
             if (displayProgress < (int)(op.progress * 100))
             {
                 ++displayProgress;
             }
                 SetLoadingPercentage(displayProgress);
                 yield return new WaitForEndOfFrame();
        }


             toProgress = 100;
         while (displayProgress < toProgress)
         {
             ++displayProgress;
             SetLoadingPercentage(displayProgress);
            displayText.text = displayProgress.ToString() + "%";
             yield return new WaitForEndOfFrame();
         }

         op.allowSceneActivation = true;
     }
     private void SetLoadingPercentage(float value)
     {
        programBar.value = value/100;
     }
  
}
