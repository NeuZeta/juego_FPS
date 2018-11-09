using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class enemyAI : MonoBehaviour {

    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public Light myLight;
    public Renderer myDroneLightBulb;
    public float life = 100f;
    public float timeBetweenShoots = 1.0f;
    public float damageForce = 10f;
    public float rotationTime = 3.0f;
    public float shootHeight = 0.5f;
    public Transform[] waypoints;
    public AudioSource audioSource;
    public AudioClip gunshot;


    private Vector3 healthScale;
    public GameObject healthBar;

    private void Awake()
    {
        this.enabled = true;
        // Getting the intial scale of the healthbar (whilst the player has full health).
        healthScale = healthBar.transform.localScale;
        UpdateHealthBar();
    }

    void Start () {
        //creamos los estados de nuestra IA
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        //le decimos que inicialmente empezará patrullando
        currentState = patrolState;

        //guardamos la referencia de nuestro NavMesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Update () {

        if (gameController.instance.winGame || gameController.instance.LoseGame)
        {
            this.enabled = false;
        }

        //Como nuestros estados no heredan de MonoBehaviour, no se llama a update automáticamente, y nos encargamos nosotros de llamarlo a cada frame
        currentState.UpdateState();

        //Morir
        if (life < 0)
        {
            gameObject.SetActive(false);
            gameController.instance.enemiesDead++;
        }
        
	}

    public void Hit(float damage)
    {
        life -= damage;

        currentState.Impact();

        if (life > 0f)
        {
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        healthBar.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, 1 - life * 0.01f);

        // Set the scale of the health bar to be proportional to the player's health.
        healthBar.transform.localScale = new Vector3(healthScale.x * life * 0.01f, 0.15f, 1);
    }

    //Ya que nuestros states no heredan de Monobehaviour, tendremos que avisarles cuando algo entra, está o sale de nuestro trigger
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

}
