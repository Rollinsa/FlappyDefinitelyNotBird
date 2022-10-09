using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private float startDelay;
    public Text timer;
    private int startDelayInt;
    void Awake()
    {
        timer = GetComponent<Text>();
        startDelayInt = (int)startDelay;
    }
 
    void Update () {
        int timeSeconds = (int)Time.time;

        timer.text = (startDelayInt - timeSeconds).ToString();

        if (Time.time > startDelay)
        {
            Destroy(gameObject);
        }
     }

     public float getStartDelay()
     {
        return startDelay;
     }
}
