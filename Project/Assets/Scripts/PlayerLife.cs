using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour {

    public Slider healthBar;
    public Slider armorBar;

    private Vector3 healthScale;
    private Vector3 armorScale;

    float totalLife = 150f;
    float life = 150f;
    float armor = 100f;
    float totalArmor = 100f;
    float healing = 50f;
    float armorUp = 40f;

    public AudioSource audioSource;
    public AudioClip armorSound, healingSound;
    public GameObject bloodPanel;


    private void Awake()
    {
        bloodPanel.SetActive(false);
        healthScale = healthBar.transform.localScale;
        armorScale = armorBar.transform.localScale;
    }


    private void Update()
    {
        if (life <= 0)
        {
            gameController.instance.LoseGame = true;
        }
    }

    public void Hit(float damage)
    {
        armor -= damage;
        if(armor < 0)
        {
            life += armor;
            armor = 0f;
        }
        UpdateArmorBar();
        UpdateHealthBar();
        bloodPanel.SetActive(true);
        Invoke("HideBloodPanel", 0.325f);
    }

    void HideBloodPanel()
    {
        bloodPanel.SetActive(false);
    }

    void UpdateHealthBar()
    {
        // Set the scale of the health bar to be proportional to the player's health.
        healthBar.value = life;
    }

    void UpdateArmorBar()
    {
        armorBar.value = armor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "healer")
        {
            if (life < totalLife)
            {
                life += healing;
                if (life > totalLife)
                {
                    life = totalLife;
                }
                UpdateHealthBar();
                audioSource.PlayOneShot(healingSound);
                other.gameObject.SetActive(false);
            }
        }

        if (other.gameObject.tag == "Armor")
        {
            if (armor < totalArmor)
            {
                armor += armorUp;
                if (armor > totalArmor)
                {
                    armor = totalArmor;
                }
                UpdateArmorBar();
                audioSource.PlayOneShot(armorSound);
                other.gameObject.SetActive(false);
            }
            
        }

    }



    

}//PlayerLife
