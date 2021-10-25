using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//timer class was made with help from:
//https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timeRemaining = 120;

    [SerializeField]
    private float timeToDecrease = 10;

    [SerializeField]
    private Text timeText;

    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        //start timer
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            //if timer is running and still time remaining, decrease and display remaining time
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else //if timer runs out, stop timer and display appropriate text
            {
                timeRemaining = 0;
                timerIsRunning = false;
                displayFinishText("You Lose");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //if player does the wrong action, timer gets decreased
    public void decreaseTime()
    {
        timeRemaining -= timeToDecrease;
    }

    //if player solves puzzle, timer is stopped and appropriate text is displayed
    public void stopTimer()
    {
        timerIsRunning = false;
        displayFinishText("You Win");
    }

    //changes timer text to something else
    private void displayFinishText(string finishText)
    {
        timeText.text = finishText;
    }
}
