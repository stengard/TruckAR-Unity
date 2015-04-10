using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    private float x;
    private float y;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        x = -Input.GetAxis("Horizontal");
        y = -Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        pos.x+= x*100;
        pos.z += y*100;

        transform.position = pos;
	}
}
