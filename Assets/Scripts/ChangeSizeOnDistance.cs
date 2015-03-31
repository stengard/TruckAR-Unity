using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to any GameObject that makes the objecct smaller the closer the user gets to it.
/// </summary>
public class ChangeSizeOnDistance : MonoBehaviour {

    public float sizeMultiplier;
    private float[] trackingValues;

    private Vector3 originalScale;

    // Use this for initialization
    void Start() {


        //sizeMultiplier = 1;
        trackingValues = new float[7];
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update() {

        //Check if marker with COS 1 is trackable
        float quality = metaio.MetaioSDKUnity.getTrackingValues(1, trackingValues);

        //Get Camera translation
        Vector3 cameraTranslation;
        cameraTranslation.x = trackingValues[0];
        cameraTranslation.y = trackingValues[1];
        cameraTranslation.z = trackingValues[2];

        Quaternion cameraRotation = new Quaternion();
        // get the camera rotation from the Metaio SDK
        cameraRotation.x = trackingValues[3];
        cameraRotation.x = trackingValues[4];
        cameraRotation.z = trackingValues[5];
        cameraRotation.w = trackingValues[6];
        cameraRotation = Quaternion.Inverse(cameraRotation);

        //Calculate camera position
        Vector3 cameraPosition = cameraRotation * (-cameraTranslation);

        //Calcculate the distance between marker and camera
        float distance = Vector3.Distance(Vector3.zero, cameraPosition);

        //Calulate the scalefactor to scale the attatched gameObject with. Multiplied by 1000 to compensate for the distance returned is in mm.
        float scaleFactorX = originalScale.x * (distance / (sizeMultiplier * 1000));
        float scaleFactorY = originalScale.y * (distance / (sizeMultiplier * 1000));
        float scaleFactorZ = originalScale.z * (distance / (sizeMultiplier * 1000));

        if (scaleFactorX > originalScale.x)
            scaleFactorX = originalScale.x;

        if (scaleFactorY > originalScale.y)
            scaleFactorY = originalScale.y;

        if (scaleFactorZ > originalScale.z)
            scaleFactorZ = originalScale.z;

        //If the marker is recogniized (distance != 0), scale the object with the factor scaleFactor
        if(distance != 0)
            transform.localScale = new Vector3(scaleFactorX, scaleFactorY, scaleFactorZ);

        //Debugga.LoggaLive("Scale :" + scaleFactorX + ", " + scaleFactorY + ", " + scaleFactorZ);
        //Debugga.LoggaLive(originalScale.x + "");
    }
}