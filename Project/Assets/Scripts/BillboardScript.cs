using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour {
    Camera cam;
	
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cam.transform);
        transform.forward = -transform.forward;
        transform.localRotation = Quaternion.Euler(0,
                                  transform.localRotation.eulerAngles.y,
                                  transform.localRotation.eulerAngles.z);

	}
}
