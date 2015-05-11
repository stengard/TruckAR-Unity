using UnityEngine;
using System.Collections;
using metaio;

public class Billboard : MonoBehaviour {
    private Vector3 cameraCentroid;

    public float speed;
    public Camera cameraLeft;
    public Camera cameraRight;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

	// Use this for initialization
	void Start () {

        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();

        originalRotation = transform.rotation;
        originalPosition = transform.position;

	}
	
	// Update is called once per framez
	void Update () {


        if (Camera.main) {
            //Debugga.Logga("main camera");
            cameraCentroid = Camera.main.transform.position;

            transform.rotation = Camera.main.transform.rotation;
        }
        else {
            //Debugga.Logga("Stereo");
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });

            transform.rotation = cameraLeft.transform.rotation;
        }



        

        //Quaternion rotation = Quaternion.LookRotation(cameraCentroid);

        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

       

	}
}
