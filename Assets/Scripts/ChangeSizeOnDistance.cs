using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to any GameObject that makes the objecct smaller the closer the user gets to it.
/// </summary>
public class ChangeSizeOnDistance : MonoBehaviour {

    public float sizeMultiplier;

    private Vector3 originalScale;

    private float distance;

    public Camera cameraLeft;
    public Camera cameraRight;

    public Vector3 cameraCentroid;

    // Use this for initialization
    void Start() {

        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();

         cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.up, cameraRight.transform.up });

        //sizeMultiplier = 1;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update() {

        //Calcculate the distance between marker and camera. Multiplied by 1000 to compensate for the distance returned is in mm.
        distance = Vector3.Distance(transform.position, cameraCentroid) / (sizeMultiplier * 1000);

        //Calulate the scalefactor to scale the attatched gameObject with. 
        float scaleFactorX = distance;
        float scaleFactorY = distance;
        float scaleFactorZ = distance;

        //If the marker is recogniized (distance != 0), scale the object with the factor scaleFactor
        if(distance != 0)
            transform.localScale = new Vector3(originalScale.x * scaleFactorX, originalScale.y * scaleFactorY, originalScale.z * scaleFactorZ);

        //Debugga.LoggaLive("Scale :" + scaleFactorX + ", " + scaleFactorY + ", " + scaleFactorZ);
        //Debugga.LoggaLive("position: " + distance);
        //Debugga.LoggaLive(originalScale.x + "");
    }
}