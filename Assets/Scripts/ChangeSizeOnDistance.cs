using UnityEngine;
using System.Collections;

public class ChangeSizeOnDistance : MonoBehaviour {

    public float sizeMultiplier;
    private float[] trackingValues;

    // Use this for initialization
    void Start() {
        sizeMultiplier = 1;
        trackingValues = new float[7];
    }

    // Update is called once per frame
    void Update() {

        float quality = metaio.MetaioSDKUnity.getTrackingValues(1, trackingValues);

        Vector3 cameraTranslation;
        cameraTranslation.x = trackingValues[0];
        cameraTranslation.y = trackingValues[1];
        cameraTranslation.z = trackingValues[2];

        Quaternion cameraRotation = new Quaternion();
        // get the camera rotation from the SDK
        cameraRotation.x = trackingValues[3];
        cameraRotation.x = trackingValues[4];
        cameraRotation.z = trackingValues[5];
        cameraRotation.w = trackingValues[6];
        cameraRotation = Quaternion.Inverse(cameraRotation);

        Vector3 cameraPosition = cameraRotation * (-cameraTranslation);

        float distance = Vector3.Distance(Vector3.zero, cameraPosition);

        //transform.localScale = transform.localScale * (distance/sizeMultiplier);
        
        Debugga.LoggaLive("Distance: " + distance);

    }
}