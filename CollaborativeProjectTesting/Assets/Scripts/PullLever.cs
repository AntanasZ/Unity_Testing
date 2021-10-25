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
    private Timer timer;

    private Ray ray;
    private int leverCount = 0;
    private string[] leverOrder = new string[5];
    public bool puzzleFinished = false;
    


    private void Awake()
    {
        selectableTag = selectableTag.Trim();
    }

    //private void Start()
    //{
        
    //}

    private void Update()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hitInfo = new RaycastHit();
        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    var currentSelection = hitInfo.transform;
        //    if(currentSelection.CompareTag(selectableTag))
        //    {

        //    }
        //}
        OnMouseOver();

        if(leverCount == 5 && puzzleFinished == false)
        {
            puzzleFinished = true;
            timer.stopTimer();
        }
    }

    // OnMouseOver is called every frame while the mouse is over the GUIElement or Collider
    private void OnMouseOver()
    {
        if(leverCount < 5)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    leverOrder[leverCount] = hitInfo.transform.gameObject.name;

                    if (CheckIfCorrect())
                    {
                        leverCount++;
                        hitInfo.transform.Rotate(45, 0, 0);
                    }
                    else
                    {
                        //decrease timer
                        timer.decreaseTime();
                    }
                }
            }
        }
    }

    private bool CheckIfCorrect()
    {
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
