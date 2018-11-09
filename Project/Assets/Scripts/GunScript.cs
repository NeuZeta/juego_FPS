using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunScript : MonoBehaviour {

    //public GameObject bulletHolePrefab;
    public AudioSource audioSource;
    public AudioClip gunshot;
    public AudioClip outOfAmmo;
    public AudioClip ammoPick;
    public float damage = 10f;
    public Text ammoText;

    public GameObject[] totalDecals;
    int actualDecal = 0;

    int ammo;
    int totalAmmo = 25;
    int ammoReload = 20;
   

    // Use this for initialization
    void Start () {
        ammo = totalAmmo;
        ammoText.text = ammo.ToString();
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            {
                Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                Vector3 rayDirection = Camera.main.transform.forward;
                RaycastHit hitInfo;

                if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
                {
                    if (hitInfo.collider.name == "FPSController")
                    {
                        Vector3 newRayOrigin = hitInfo.point;
                        if (Physics.Raycast(newRayOrigin, rayDirection, out hitInfo))
                        {
                            if (!totalDecals[actualDecal].activeSelf)
                            {
                                totalDecals[actualDecal].SetActive(true);
                            }
                            totalDecals[actualDecal].transform.position = hitInfo.point + hitInfo.normal * 0.01f;
                            totalDecals[actualDecal].transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal * -1);
                            totalDecals[actualDecal].transform.SetParent(hitInfo.collider.transform);
                            actualDecal++;
                            if (actualDecal == totalDecals.Length)
                            {
                                actualDecal = 0;
                            }
                        }
                    }
                    else if (hitInfo.collider.tag == "Enemy")
                    {
                        hitInfo.collider.gameObject.GetComponentInParent<enemyAI>().Hit(damage);
                        if (!totalDecals[actualDecal].activeSelf)
                        {
                            totalDecals[actualDecal].SetActive(true);
                        }
                        totalDecals[actualDecal].transform.position = hitInfo.point + hitInfo.normal * 0.01f;
                        totalDecals[actualDecal].transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal * -1);
                        totalDecals[actualDecal].transform.SetParent(hitInfo.collider.transform);
                        actualDecal++;
                        if (actualDecal == totalDecals.Length)
                        {
                            actualDecal = 0;
                        }
                    }
                    else
                    {
                        if (!totalDecals[actualDecal].activeSelf)
                        {
                            totalDecals[actualDecal].SetActive(true);
                        }
                        totalDecals[actualDecal].transform.position = hitInfo.point + hitInfo.normal * 0.01f;
                        totalDecals[actualDecal].transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal * -1);
                        totalDecals[actualDecal].transform.SetParent(hitInfo.collider.transform);
                        actualDecal++;
                        if (actualDecal == totalDecals.Length)
                        {
                            actualDecal = 0;
                        }
                    }

                    audioSource.PlayOneShot(gunshot);
                    ammo--;
                    if (ammo < 0) ammo = 0;
                    ammoText.text = ammo.ToString();

                }



            }
            else
            {
                audioSource.PlayOneShot(outOfAmmo);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            if (ammo < totalAmmo)
            {
                ammo += ammoReload;
                if (ammo > totalAmmo)
                {
                    ammo = totalAmmo;
                }
                audioSource.PlayOneShot(ammoPick);
                other.gameObject.SetActive(false);
                ammoText.text = ammo.ToString();
            }
        
        }
    }

}//GunScript
