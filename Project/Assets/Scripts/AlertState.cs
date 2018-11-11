using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState {

    enemyAI myEnemy;
    float currentRotationTime = 0f;

    public AlertState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.yellow;
        myEnemy.myDroneLightBulb.material.color = Color.yellow;

        myEnemy.transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 * 1.0f / myEnemy.rotationTime, 0f);

        if (currentRotationTime > myEnemy.rotationTime)
        {
            currentRotationTime = 0f;
            GoToPatrolState();
        }
        else
        {
            //Si aún estamos dando vueltas lanzamos un rayo desde una altura de 0.5m desde la posicion del enemigo hacia donde mira
            RaycastHit hit;
            Vector3 rayOrigin = new Vector3(myEnemy.transform.position.x, 0.5f, myEnemy.transform.position.z);
            Vector3 rayDirection = myEnemy.transform.forward * 100f;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    GoToAttackState();
                }
            }
 
        }
        currentRotationTime += Time.deltaTime;
    }

    public void Impact()
    {
        currentRotationTime = 0f;
    }

    public void GoToPatrolState()
    {
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }

    public void GoToAlertState() { }

    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.attackState;
    }

    public void OnTriggerEnter(Collider col)
    {

    }

    public void OnTriggerStay(Collider col)
    {

    }

    public void OnTriggerExit(Collider col)
    {

    }

}
