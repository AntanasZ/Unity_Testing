using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform shootingTarget;

    private bool minigameActive = true;
    private bool minigameFinished = false;
    private bool targetActive = false;
    private Vector3 showPosition;
    private Vector3 hidePosition;

    private void Awake()
    {
        //check if player started minigame
        if(minigameActive)
        {
            //start showing/hiding targets
            InvokeRepeating("HideShowTarget", 1, 3);
        }

        //check if minigame is finished
        if(minigameFinished)
        {
            //stop showing/hiding targets
            CancelInvoke("HideShowTarget");
        }
    }
    private void Start()
    {
        //set show/hide positions of current target instance
        showPosition = shootingTarget.transform.position;
        hidePosition = new Vector3(shootingTarget.transform.position.x, shootingTarget.transform.position.y - 1, shootingTarget.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //check if minigame is playing
        if(minigameActive && !minigameFinished)
        {
            //move target to show/hide position 
            if (targetActive)
            {
                shootingTarget.transform.position = showPosition;
                //shootingTarget.transform.position = Vector3.Lerp(showPosition, hidePosition, 3);
            }
            else
            {
                shootingTarget.transform.position = hidePosition;
                //shootingTarget.transform.position = Vector3.Lerp(hidePosition, showPosition, 3);
            }
        }
    }

    private void HideShowTarget()
    {
        //roll to see if the target will show/hide
        //if roll is above 50, target will show
        if (Random.Range(0, 100) > 50)
        {
            targetActive = true;
        }
        else
        {
            targetActive = false;
        }
    }
}
