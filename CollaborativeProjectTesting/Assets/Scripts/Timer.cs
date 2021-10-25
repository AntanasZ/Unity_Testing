using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
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

    public void decreaseTime()
    {
        timeRemaining -= timeToDecrease;
    }

    public void stopTimer()
    {
        timerIsRunning = false;
        displayFinishText("You Win");
    }

    private void displayFinishText(string finishText)
    {
        timeText.text = finishText;
    }
}
