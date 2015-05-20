using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer)) ]
public class safeZoneScript : MonoBehaviour {

    public Camera cameraLeft;
    public Camera cameraRight;
    private Camera currentCamera;

    public Color safeColor;
    public Color unsafeColor;
    public float safeRadius, secondSafeRadius;

    public float heightOfUser;

    private Gradient gradient;
    private GradientColorKey[] gck;
    private GradientAlphaKey[] gak;

    private Vector3 cameraCentroid;
    private float distance;


    public GameObject lineObject;
    private List<GameObject> listOfLineObjects;

    private float radius;


    public int size;                    //Total number of points in circle

    private Material lineMaterial;

    private float currentOpacity;
    public float flashingSpeed;

	// Use this for initialization
	void Start () {

        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();

        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
            currentCamera = Camera.main;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
            currentCamera = cameraLeft;
        }

        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();

        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
            currentCamera = Camera.main;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
            currentCamera = cameraLeft;
        }

        //cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.up, cameraRight.transform.up });

        distance = Vector3.Distance(transform.position, cameraCentroid);


        radius = transform.localScale.x * GetComponent<CapsuleCollider>().radius;

        gradient = new Gradient();

        gck = new GradientColorKey[3];
        gck[0].color = unsafeColor;
        gck[0].time = 0.0f;
        gck[1].color = unsafeColor;
        gck[1].time = safeRadius/secondSafeRadius;
        gck[2].color = safeColor;
        gck[2].time = 1.0f;

        gak = new GradientAlphaKey[1];
        gak[0].alpha = 1;
        gak[0].time = 0.0f;

        gradient.SetKeys(gck, gak);

        lineMaterial = lineObject.GetComponent<Renderer>().sharedMaterial;

        lineMaterial.color = safeColor;

        listOfLineObjects = new List<GameObject>();

        for (int i = 0; i < size; i++) {

            listOfLineObjects.Add(Instantiate(lineObject));
            listOfLineObjects[i].transform.parent = transform;
            listOfLineObjects[i].transform.position = transform.position;
            listOfLineObjects[i].transform.rotation = transform.rotation;
            listOfLineObjects[i].name = lineObject.name + "_" + i;
        }

	}


	
	// Update is called once per frame
    void Update() {

        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
        }

        distance = Vector3.Distance(transform.position, cameraCentroid);

        distance = Mathf.Sqrt(distance*distance - heightOfUser*heightOfUser);

        CreatePoints();

    }

    private void CreatePoints() {



        lineMaterial.color = gradient.Evaluate(distance/secondSafeRadius);

        if (distance < secondSafeRadius && distance > safeRadius) {
            //currentOpacity = Mathf.Abs(Mathf.Sin(Time.time * flashingSpeed));
            currentOpacity = 1;
            lineMaterial.color = new Color(lineMaterial.color.r, lineMaterial.color.g, lineMaterial.color.b, currentOpacity);
        }
        else {
            currentOpacity = 1;
            lineMaterial.color = new Color(lineMaterial.color.r, lineMaterial.color.g, lineMaterial.color.b, currentOpacity);
        }

        for (int i = 0; i < (size); i++) {

            float theta =  -(Mathf.PI / (size)) * i;


            float x = transform.position.x + safeRadius * Mathf.Cos(theta) * transform.right.x + safeRadius * Mathf.Sin(theta) * transform.forward.x;
            float y = transform.position.y + safeRadius * Mathf.Cos(theta) * transform.right.y + safeRadius * Mathf.Sin(theta) * transform.forward.y;
            float z = transform.position.z + safeRadius * Mathf.Cos(theta) * transform.right.z + safeRadius * Mathf.Sin(theta) * transform.forward.z;

            listOfLineObjects[i].transform.position = new Vector3(x, y, z);

        }

    }
}
