using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState {

    enemyAI myEnemy;
    float actualTimeBetweenShoots = 0f;

    public AttackState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
        myEnemy.myDroneLightBulb.material.color = Color.red;
        actualTimeBetweenShoots += Time.deltaTime;
    }

    public void Impact() { }

    public void GoToPatrolState() { }

    public void GoToAlertState()
    {
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToAttackState() { }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        //Estaremos mirando siempre al player
        Vector3 lookInDirection = col.transform.position - myEnemy.transform.position;

        //rotando solamente en el eje Y
        myEnemy.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookInDirection.x, 0, lookInDirection.z));

        //le toca volver a disparar
        if (actualTimeBetweenShoots > myEnemy.timeBetweenShoots)
        {
            actualTimeBetweenShoots = 0f;
            col.gameObject.GetComponent<PlayerLife>().Hit(myEnemy.damageForce);
            myEnemy.audioSource.PlayOneShot(myEnemy.gunshot);
        }


    }

    public void OnTriggerExit(Collider col)
    {
        GoToAlertState();
    }

}
