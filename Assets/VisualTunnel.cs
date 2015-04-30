using UnityEngine;
using System.Collections;
using metaio;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Script that creates a Viual tunnel from the camera to an object. Using a Catmull-rom spline to interpolate a curve from a to b.
/// </summary>
public class VisualTunnel : MonoBehaviour {

    public Camera cameraLeft;
    public Camera cameraRight;

    private Camera currentCamera;

    public GameObject square;

    public int squareDensity;

    private Vector3 cameraCentroid;
    private Vector3 bending;
    private int numberOfTunnels;
    private float distance;

    public Text maxDistanceText;
    public Text densityText;

    public float maxDistance;

    private List<GameObject> tunnelObjects;
    Vector3[] catmullRomVectors;

    CRSpline catmullRom;

    // Use this for initialization
    void Start() {

        catmullRom = new CRSpline();

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
 


        //cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });

        distance = Vector3.Distance(transform.position, cameraCentroid);
        numberOfTunnels = Mathf.RoundToInt((distance / 1000) * squareDensity);

        tunnelObjects = new List<GameObject>();

        maxDistanceText.text = "Max Distance: " + maxDistance;
        densityText.text = "Square Density: " + squareDensity;

        //Instantiate objects
        addTunnels();
    }

    // Update is called once per frame
    void Update() {

        maxDistanceText.text = "Max Distance: " + maxDistance;
        densityText.text = "Square Density: " + squareDensity;

        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
        }

        distance = Vector3.Distance(transform.position, cameraCentroid);


        numberOfTunnels = Mathf.RoundToInt((distance / 1000) * squareDensity);
        Debugga.Logga("distance: " + distance);
        Debugga.Logga("cc: " + cameraCentroid);

        //Debugga.Logga("Number of tunnels: " + numberOfTunnels);

        //Position vector half the distance between the camera and the object in the cameras looking direction to ccreate the curved path.
        Vector3 vectorMid = cameraCentroid + currentCamera.transform.forward * (distance * 0.5f);


        catmullRomVectors = new Vector3[] { cameraCentroid, cameraCentroid, vectorMid, transform.position, transform.position };

        //Add vectors to CR points
        catmullRom.pts = catmullRomVectors;

        //Interpolation of the catmull-rom spline placing objects along it
        if (tunnelObjects.Count != numberOfTunnels) {
            for (int i = 0; i < tunnelObjects.Count; i++) {
                Destroy(tunnelObjects[i]);
            }

            tunnelObjects.Clear();

            if (distance < maxDistance) {
                addTunnels();

            }
            else { return; }
        }


        // tunnelObjects = new GameObject[size];
        Vector3 prevPt = catmullRom.Interp(0);
        for (int i = 1; i <= tunnelObjects.Count; i++) {
            float pm = (float)i / tunnelObjects.Count;
            Vector3 currPt = catmullRom.Interp(pm);

            tunnelObjects[i - 1].transform.position = currPt;
            tunnelObjects[i - 1].transform.rotation = currentCamera.transform.rotation;

            //if (i != tunnelObjects.Count) {
            //    //Rotate the object so that it "looks at" the previous.
            //   tunnelObjects[i - 1].transform.LookAt(tunnelObjects[i].transform.position);
            //}
            //else {
            //    //Rotate the last object to "look at" the camera
            //    tunnelObjects[i - 1].transform.LookAt(cameraCentroid);
            //}


            prevPt = currPt;
        }

    }

    private void addTunnels() {
        for (int i = 0; i < numberOfTunnels; i++) {

            tunnelObjects.Add(Instantiate(square));
            //tunnelObjects[i].transform.parent = transform.parent;
            tunnelObjects[i].tag = "L&U";

            tunnelObjects[i].name = "Ruta_" + i;
            //sq[i].SetActive(false);

        }
    }

    public void OnDrawGizmos() {
        //if (Application.isPlaying)
        //    catmullRom.GizmoDraw(3);
    }

    public void setMaxDistance(float f){
        maxDistance = f;
    }

    public void setDensity(float f) {
        squareDensity = Mathf.RoundToInt(f);
    }




}
