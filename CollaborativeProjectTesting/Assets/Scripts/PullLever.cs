using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    [SerializeField]
    private string selectableTag = "Lever";

    [SerializeField]
    private CorrectLeverOrder correctOrder;

    [SerializeField]
    private Timer timer; //used to call some timer functions

    private Ray ray;
    private int leverCount = 0;
    private string[] leverOrder = new string[5];
    public bool puzzleFinished = false;

    private void Awake()
    {
        selectableTag = selectableTag.Trim();
    }

    private void Update()
    {
        OnMouseOver();

        //check if player finished puzzle
        if(leverCount == 5 && puzzleFinished == false)
        {
            puzzleFinished = true;
            timer.stopTimer();
        }
    }

    // OnMouseOver is called every frame while the mouse is over the GUIElement or Collider
    private void OnMouseOver()
    {
        //check if there are still levers to pull
        if(leverCount < 5)
        {
            //clicking on lever
            if (Input.GetKeyDown(KeyCode.E))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                //get the lever that was clicked
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //add it to players lever order
                    leverOrder[leverCount] = hitInfo.transform.gameObject.name;

                    //check if the lever pulled corresponds to the correct lever order
                    if (CheckIfCorrect())
                    {
                        //if true, increment count and do something to the lever
                        leverCount++;
                        hitInfo.transform.Rotate(45, 0, 0);
                    }
                    else
                    {
                        //if false, decrease timer
                        timer.decreaseTime();
                    }
                }
            }
        }
    }

    private bool CheckIfCorrect()
    {
        //compare current lever that was pulled to the lever that was supposed to be pulled
        if (leverOrder[leverCount].Equals(correctOrder.LeverOrder[leverCount]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    






}
