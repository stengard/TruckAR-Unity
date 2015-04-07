using UnityEngine;
using System.Collections;

public class safeZoneScript : MonoBehaviour {

    public Camera cameraLeft;
    public Camera cameraRight;

    public Color startColor;
    public Color endColor;
    public float safeRadius;

    private Vector3 cameraCentroid;
    private float distance;

    private float radius;
    private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();
        cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.up, cameraRight.transform.up });

        distance = Vector3.Distance(transform.position, cameraCentroid);
        radius = transform.localScale.x * GetComponent<CapsuleCollider>().radius;
        meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
    void Update() {
        distance = Vector3.Distance(transform.position, cameraCentroid);
        Debug.Log(distance);
        Debug.Log(radius);

        Color c = Color.Lerp(startColor, endColor, distance / (safeRadius * 1000));
        c.a = .58f;

        if (distance <= radius) {
            c = startColor;


        }
        Debugga.Logga(c.r + " " + c.g + " " + c.b);

        meshRenderer.material.color = c;
    }
}
