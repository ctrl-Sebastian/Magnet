using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public static float currentTime = 0f;
    public static float startingTime = 35f;
    public TMP_Text countdownText;


    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if(currentTime >= 0f){
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.0") + " Secs";
        }
    }

    
}
