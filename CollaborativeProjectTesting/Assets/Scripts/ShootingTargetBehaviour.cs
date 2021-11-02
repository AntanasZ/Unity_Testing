using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform shootingTarget;

    private Ray ray;
    private bool minigameActive = true;
    private bool minigameFinished = false;
    private bool showTarget = false;

    [SerializeField]
    private bool targetShot = false;
    
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
        //targetMaterial = shootingTarget.gameObject.GetComponent<Renderer>().material;

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
            if (showTarget)
            {
                shootingTarget.transform.position = showPosition;
                //shootingTarget.transform.position = Vector3.Lerp(showPosition, hidePosition, 3);
            }
            else
            {
                shootingTarget.transform.position = hidePosition;
                //shootingTarget.transform.position = Vector3.Lerp(hidePosition, showPosition, 3);
            }

            OnMouseOver();
        }
    }

    // OnMouseOver is called every frame while the mouse is over the GUIElement or Collider
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            //get the target that was clicked and change material and disable target
            if (Physics.Raycast(ray, out hitInfo))
            {
                hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                targetShot = true;
                showTarget = false;
            }
        }
    }



    private void HideShowTarget()
    {
        if(targetShot != true)
        {
            //roll to see if the target will show/hide
            //if roll is above 50, target will show
            if (Random.Range(0, 100) > 50)
            {
                showTarget = true;
            }
            else
            {
                showTarget = false;
            }
        }
    }
}
