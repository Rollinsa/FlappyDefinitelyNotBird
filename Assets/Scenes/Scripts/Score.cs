using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    private float startDelay;
    private Spawner spawner;
    private bool didSetActive;

    void Awake()
    {
        score = GetComponent<Text>();
        score.enabled = false;
        startDelay = FindObjectOfType<Countdown>().getStartDelay();
        spawner = FindObjectOfType<Spawner>();
    }
 
    void Update () {
        if (Time.time < startDelay)
        {
            return;
        }
        if (!didSetActive)
        {
            gameObject.SetActive(true);
            score.enabled = true;
            didSetActive = true;
        }
        score.text = "Score: " + spawner.getWallsPassed();
     }

     public float getStartDelay()
     {
        return startDelay;
     }
}
