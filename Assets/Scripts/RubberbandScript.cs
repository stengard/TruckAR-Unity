using UnityEngine;
using System.Collections;

public class RubberbandScript : MonoBehaviour {
    private Vector3 cameraCentroid;
    private Camera cam;
    Vector3 position;
    public Camera cameraLeft;
    public Camera cameraRight;
    public float Dampening = 10;
	// Use this for initialization
	void Start () {
        position = transform.position;
        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main)
        {
            cameraCentroid = Camera.main.transform.up;
            cam = Camera.main;
        }
        else
        {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.up, cameraRight.transform.up });
            cam = cameraRight;
        }
        Vector3 screenWorld = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, transform.position.z));
        float offsetX = (transform.parent.position.x - screenWorld.x) / 2;
        float offsetY = (transform.parent.position.y - screenWorld.y) / 2;
        float tempX = Mathf.Lerp(transform.position.x, screenWorld.x + offsetX, Time.deltaTime * Dampening);
        float tempY = Mathf.Lerp(transform.position.y, screenWorld.y + offsetY, Time.deltaTime * Dampening);
        screenWorld.x = tempX;
        screenWorld.y = tempY;
        transform.position = screenWorld;
        
	}
}
