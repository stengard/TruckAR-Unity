﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using metaio;

public class EnvironmentalOverlay : MonoBehaviour {

    public GameObject wall;

    private Vector3 originalScale;
    private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        originalScale = wall.transform.localScale;
        originalPosition = wall.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        float[] t = new float[10];

        List<TrackingValues> track = new List<TrackingValues>();


        //TrackingValues t = new TrackingValues();
        //Debugga.Logga(t.quality + "");

	}

    public void scaleX(float f) {
        wall.transform.localScale = new Vector3(originalScale.x + f, wall.transform.localScale.y ,wall.transform.localScale.z);

        wall.transform.localPosition = new Vector3(-wall.transform.localScale.x / 2, wall.transform.localPosition.y, wall.transform.localPosition.z);
    }
    
    public void scaleY(float f) {
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, originalScale.x + f, wall.transform.localScale.z);
    }

    public void translateX(float f) {
        wall.transform.localPosition = new Vector3(originalPosition.x + f, wall.transform.localPosition.y, wall.transform.localPosition.z);
    }

    public void translateY(float f) {
        wall.transform.localPosition = new Vector3(wall.transform.localPosition.x, wall.transform.localPosition.y, originalPosition.z + f);
    }
}