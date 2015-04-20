﻿using UnityEngine;
using System.Collections;
using metaio;

public class Billboard : MonoBehaviour {
    private Vector3 cameraCentroid;

    public float speed;
    public Camera cameraLeft;
    public Camera cameraRight;

	// Use this for initialization
	void Start () {

        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();

	}
	
	// Update is called once per framez
	void Update () {


        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
        }

        Quaternion rotation = Quaternion.LookRotation(cameraCentroid);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

       

	}
}