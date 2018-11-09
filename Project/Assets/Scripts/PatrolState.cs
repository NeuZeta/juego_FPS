using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {
    enemyAI myEnemy;
    private int nextWayPoint = 0;


    public PatrolState (enemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.green;
        myEnemy.myDroneLightBulb.material.color = Color.green;

        myEnemy.navMeshAgent.destination = myEnemy.waypoints[nextWayPoint].position;
        //si hemos llegado al destino cambiamos la referencia al siguiente waypoint
        if(myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)
        {
            nextWayPoint = (nextWayPoint + 1) % myEnemy.waypoints.Length;
        }

    }

    public void Impact()
    {
        if (myEnemy) GoToAlertState();
    }

    public void GoToPatrolState() { }

    public void GoToAlertState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToAttackState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.attackState;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerExit(Collider col)
    {

    }
}//PatrolState
