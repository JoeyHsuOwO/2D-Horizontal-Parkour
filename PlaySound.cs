using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
    private AudioSource[] _audiSource;
    // Use this for initialization
    void Start()
    {
        _audiSource = this.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSound(string type)
    {

        switch (type)
        {

            case "select":
                _audiSource[0].Play();
                break;
            

        }
    }
}