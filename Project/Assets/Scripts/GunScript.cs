using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    //public GameObject bulletHolePrefab;
    public AudioSource audioSource;
    public AudioClip gunshot;

    public GameObject[] totalDecals;
    int actualDecal = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetMouseButtonDown(0))
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
                else
                {
                    totalDecals[actualDecal].transform.position = hitInfo.point + hitInfo.normal * 0.01f;
                    totalDecals[actualDecal].transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal * -1);
                    totalDecals[actualDecal].transform.SetParent(hitInfo.collider.transform);
                    actualDecal++;
                    if (actualDecal == totalDecals.Length)
                    {
                        actualDecal = 0;
                    }
                }
 
                //Debug.Log(hitInfo.collider.transform.name);
                //GameObject.Instantiate(bulletHolePrefab,
                //                       hitInfo.point + hitInfo.normal * 0.01f,
                //                       Quaternion.FromToRotation(Vector3.forward, hitInfo.normal * -1)
                //                       //hitInfo.collider.transform
                //                       );
                ////bulletHolePrefab.transform.forward = - hitInfo.normal;
                audioSource.PlayOneShot(gunshot);
                

            }



        }

    }
}
