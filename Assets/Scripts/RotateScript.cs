using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
    Quaternion rotation;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, transform.parent.parent.rotation.y);
	}
}
