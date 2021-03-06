using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This class handles enemy movement using nav meshes and nav mesh agents
//main patrol code can be found in unity documentation: https://docs.unity3d.com/Manual/nav-AgentPatrol.html

//code for finding a player and chasing them was made 
//with help from CodeMonkeys guide: https://www.youtube.com/watch?v=db0KWYaWfeM&ab_channel=CodeMonkey
public class Patrol : MonoBehaviour
{
    public enum State
    {
        Patrol,
        ChaseTarget
    }

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField]
    private Transform player;
    public State state = State.Patrol;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //adjust enemy speed based on what enemy they are
        //trapper uses default speed
        if(agent.tag.Equals("ChaserEnemy"))
        {
            agent.speed = 5f;
        }
        else if(agent.tag.Equals("ThrowerEnemy"))
        {
            agent.speed = 2.5f;
        }

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        HandleMovement();
    }

    void HandleMovement()
    {
        if(state == State.Patrol) //patrolling
        {
            //returns if no points have been set up
            if (points.Length == 0)
                return;

            //set agent to go to the currently selected destination
            agent.destination = points[destPoint].position;

            //choose the next point in the array as the destination randomly
            destPoint = Random.Range(0, points.Length - 1);
        }
        else //chasing player
        {
            agent.destination = player.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //look for player
        FindTarget();

        //choose the next destination point when the agent gets
        //close to the current one
        //if (agent.pathPending && agent.remainingDistance < 0.5f)
        if (agent.remainingDistance < 0.5f && state == State.Patrol)
            HandleMovement();
    }

    private void FindTarget()
    {
        float targetRange = 10f;

        if(Vector3.Distance(transform.position, player.position) < targetRange)
        {
            state = State.ChaseTarget;
            HandleMovement();
        }
        else if(Vector3.Distance(transform.position, player.position) > targetRange)
        {
            state = State.Patrol;
        }
    }
}
