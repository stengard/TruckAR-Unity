using UnityEngine;
using System.Collections;

public class CorrectPosition : MonoBehaviour {


    Vector3 originalPosition;
	// Use this for initialization
	void Start () {
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void translateInX(float f) {
        transform.position = originalPosition + new Vector3(f, transform.position.y, transform.position.z);
    }
    public void translateInZ(float f) {
        transform.position =  originalPosition + new Vector3(transform.position.x, f, transform.position.z);
    }
}
