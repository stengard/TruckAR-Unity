using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;
using System.Collections.Generic;
public class OverlayW : MonoBehaviour {


    public GameObject overlay;

    public int numberOfOverlays;
    public float damping;

    private List<GameObject> overlayObjects, originalOverlayObjects;


	// Use this for initialization
    void Start() {
        overlayObjects = new List<GameObject>();

        for (int i = 0; i < numberOfOverlays; i++) {

            overlayObjects.Add(Instantiate(overlay));
            //overlayObjects[i].transform.parent = transform;
            overlayObjects[i].transform.position = transform.position;
           // overlayObjects[i].transform.ro
        }

        originalOverlayObjects = overlayObjects;



        
    }
	
	// Update is called once per frame
	void Update () {
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

        Debugga.Logga(transform.GetChild(0).transform.localScale.x + "");

        for (int i = 0; i < overlayObjects.Count; i++) {


            float theta = (2 * Mathf.PI / overlayObjects.Count) * i;
            float x = 200 * Mathf.Cos(theta) + transform.position.x;
            float y = 200 * Mathf.Sin(theta) + transform.position.y;
            
            float tempX = Mathf.Lerp(overlayObjects[i].transform.position.x, x, Time.deltaTime * damping);

            float tempY = Mathf.Lerp(overlayObjects[i].transform.position.y, y, Time.deltaTime * damping);

            overlayObjects[i].transform.position = new Vector3(tempX, tempY, transform.position.z);
           

        }
       
    }
}
