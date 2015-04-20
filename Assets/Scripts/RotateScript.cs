using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
    Quaternion rotation;
    public float rotateBy;
	// Use this for initialization
	void Start () {
	}

    public void doRotate(float rot) {
        rotateBy = rot;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotateBy, transform.localEulerAngles.z);
    }
	// Update is called once per frame
	void Update () {
	}
}
