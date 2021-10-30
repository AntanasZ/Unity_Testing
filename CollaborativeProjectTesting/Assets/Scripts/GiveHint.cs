using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Fade in/out text was amde with help from this solution:
//https://stackoverflow.com/questions/27885201/fade-out-unity-ui-text

public class GiveHint : MonoBehaviour
{
    [SerializeField]
    private Text hintText;

    private float textTimer; //used to make text disappear after some time
    private bool timerRunning = false;

    private void Start()
    {
        hintText.CrossFadeAlpha(0, 0, false); //hide text at the start
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseOver();
        if(timerRunning)
        {
            textTimer -= Time.deltaTime;
        }

        if(timerRunning && textTimer <= 0)
        {
            timerRunning = false;
            hintText.CrossFadeAlpha(0, 1, false); //fade text out in 1 second
        }
    }

    private void OnMouseOver()
    {
        //if the E key is pressed on zoltar model, give hint
        if(Input.GetKeyDown(KeyCode.E))
        {
            hintText.text = "Maybe you should pay attention to the lever order";
            hintText.CrossFadeAlpha(1, 1, false); //fade text in, in 1 second
            textTimer = 6; //text will last for 6 seconds
            timerRunning = true; //start text timer
        }
    }
}
