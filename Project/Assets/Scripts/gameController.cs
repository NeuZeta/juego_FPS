﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

    public GameObject[] enemies;
    public static gameController instance;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    [HideInInspector]
    public int enemiesDead = 0;
    [HideInInspector]
    public bool winGame = false;
    [HideInInspector]
    public bool LoseGame = false;

    private void Awake()
    {
        instance = this;
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);


    }
    void Update () {
		if (enemiesDead == enemies.Length)
        {
            victoryPanel.SetActive(true);
            winGame = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (LoseGame)
        {
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

	}






}//gameController
