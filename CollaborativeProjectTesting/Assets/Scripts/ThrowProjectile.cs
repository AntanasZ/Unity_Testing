using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform throwerEnemy;
    private GameObject projectileInstance;
    private Patrol enemyState;
    private bool throwingProjectile = false;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private GameObject projectilePrefab;

    private void Awake()
    {
        //instatiate projectile object
        projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //used to get state of the enemy
        enemyState = throwerEnemy.GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if enemy is chasing player
        if(enemyState.state == Patrol.State.ChaseTarget && throwingProjectile == false)
        {
            //start throwing projectiles
            throwingProjectile = true;
            InvokeRepeating("UpdateProjectilePosition", 5f, 5f);
        }

        //update projectile position while chasing player
        if(enemyState.state == Patrol.State.ChaseTarget && throwingProjectile == true)
        {
            projectileInstance.transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }
        
        //check if enemy is patrolling
        if(enemyState.state == Patrol.State.Patrol && throwingProjectile == true)
        {
            //stop throwing projectiles and deactivate last thrown projectile
            throwingProjectile = false;
            CancelInvoke("UpdateProjectilePosition");
            DeactivateProjectile();
        }
    }

    void UpdateProjectilePosition()
    {
        //update projectile position and rotation based on the thrower enemy
        projectileInstance.transform.position = throwerEnemy.position;
        projectileInstance.transform.rotation = throwerEnemy.rotation;

        if (!projectileInstance.activeSelf) //check if projectile is not active
        {
            ActivateProjectile();
        }
    }

    void ActivateProjectile()
    {
        projectileInstance.SetActive(true);
    }
    void DeactivateProjectile()
    {
        projectileInstance.SetActive(false);
    }
}
