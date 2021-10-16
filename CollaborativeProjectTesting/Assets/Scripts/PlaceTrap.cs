using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InvokeRepeating documentation can be found here:
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html

public class PlaceTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject bearTrapPrefab;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private Transform trapperPosition;

    private float trapOffset = -0.01f; //offset trap from ground

    private GameObject[] bearTraps = new GameObject[10]; //10 bear traps can be placed at a time
    private int rayCastDepth = 10;
    private int trapCount = 0; //keeps track of how many traps are in level

    // Start is called before the first frame update
    void Start()
    {
        //after 'n' seconds, call method every 'n' seconds
        InvokeRepeating("PlaceBearTrap", 5f, 5f);
    }

    //this gets called every 'n' seconds to attempt to place a bear trap
    void PlaceBearTrap()
    {
        if(trapCount <= 10) //check if there are less than 10 traps
        {
            if (Random.Range(0, 100) > 50) //roll to see if trapper will place a trap(50% chance)
            {
                //instanciate a trap object and place it under trappers current position
                RaycastHit hitInfo;
                GameObject currentTrap = Instantiate(bearTrapPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

                if (Physics.Raycast(trapperPosition.position, -trapperPosition.up, out hitInfo, rayCastDepth, groundLayerMask))
                {
                    currentTrap.transform.position = trapperPosition.position - new Vector3(0, hitInfo.distance - trapOffset, 0);
                    currentTrap.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    currentTrap.SetActive(true);
                    bearTraps[0] = currentTrap;
                    trapCount++;
                }
            }       
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
