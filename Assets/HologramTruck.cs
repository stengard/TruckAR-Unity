using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HologramTruck : MonoBehaviour {

    public GameObject forks, frame_1, frame_2, truck, holders;
    public Slider forkHeightSlider;

    private Vector3 originalEVERYTHINGposition, originalEVERYTHINGScale, originalWindowScale, forksOriginalPosition, forksOriginalRotation, frame_1OriginalPosition, frame_2OriginalPosition,truckOriginalPosition, holdersOriginalScale, originalScale;
    private float forkheightMax;
    private Camera cameraLeft, cameraRight, currentCamera;
    public Camera testCamera;
    private Vector3 cameraCentroid;
    public GameObject EVERYTHING, dummy, pallet, stillDummy, plane;
    public float scaleOnDistance;
    private float startDistance;

    private Renderer[] mats;
    public GameObject window;

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

        frame_2OriginalPosition = frame_2.transform.localPosition;
        frame_1OriginalPosition = frame_1.transform.localPosition;
        forksOriginalPosition = forks.transform.localPosition;
        forksOriginalRotation = forks.transform.localEulerAngles;

        truckOriginalPosition = truck.transform.localPosition;
        originalScale = transform.localScale;
        originalWindowScale = window.transform.localScale;

        originalEVERYTHINGposition = EVERYTHING.transform.localPosition;
        originalEVERYTHINGScale = EVERYTHING.transform.localScale;

        holdersOriginalScale = holders.transform.localScale;
        startDistance = Vector3.Distance(pallet.transform.position, forks.transform.position) / EVERYTHING.transform.localScale.x;
        Debugga.Logga("STARTDIST" + startDistance);
        forkheightMax = forkHeightSlider.maxValue;

        mats = truck.GetComponentsInChildren<Renderer>();


	}
	
	// Update is called once per frame
	void Update () {
        float distanceForksToPallet = -scaleOnDistance * (1 - startDistance / (Vector3.Distance(pallet.transform.position, forks.transform.position) / EVERYTHING.transform.localScale.x));

        if (distanceForksToPallet < 0)
            distanceForksToPallet = 0;

        float dist2 = Vector3.Distance(pallet.transform.position, forks.transform.position)/EVERYTHING.transform.localScale.x;

        Debugga.Logga(Vector3.Distance(pallet.transform.position, forks.transform.position)/EVERYTHING.transform.localScale.x + " dasd");
        float forksPosY = forks.transform.localPosition.y/100;

        float fullScale = distanceForksToPallet + forksPosY;
        
        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
        }

        //Material m = plane.GetComponent<Renderer>().sharedMaterial;
        //m.color = new Color(m.color.r, m.color.g, m.color.b, EVERYTHING.transform.localScale.x/10);
        Debugga.Logga("Forks" + forks.transform.localPosition);
        Debugga.Logga("Forks" + forks.transform.position);
        dummy.transform.localPosition = new Vector3(-truck.transform.localPosition.x, frame_1.transform.localPosition.x*truck.transform.localScale.x, -forks.transform.localPosition.y*truck.transform.localScale.y);
        //dummy.transform.localPosition = new Vector3(dummy.transform.localPosition.x - forks.transform.localPosition.x * EVERYTHING.transform.localScale.x - truck.transform.localPosition.x * EVERYTHING.transform.localScale.x, dummy.transform.localPosition.z, dummy.transform.localPosition.z - forks.transform.localPosition.y * EVERYTHING.transform.localScale.y / 2 - forks.transform.localPosition.y);
        //dummy.transform.localPosition = new Vector3(dummy.transform.localPosition.x - forks.transform.localPosition.x * EVERYTHING.transform.localScale.x - truck.transform.localPosition.x * EVERYTHING.transform.localScale.x, dummy.transform.localPosition.z, dummy.transform.localPosition.z - forks.transform.localPosition.y * EVERYTHING.transform.localScale.y / 2 - forks.transform.localPosition.y);
        //dummy.transform.localPosition = new Vector3(dummy.transform.localPosition.x + 5384.0f*(dist2-50)/305, dummy.transform.localPosition.y + 607.0f*(dist2-50)/305, dummy.transform.localPosition.z + 2106*(dist2-50)/305);
        EVERYTHING.transform.localEulerAngles = new Vector3(EVERYTHING.transform.localEulerAngles.x, EVERYTHING.transform.localEulerAngles.y, (50 - 90 * (dist2 - 50) / 305) / EVERYTHING.transform.localScale.x);

        EVERYTHING.transform.localScale = new Vector3(originalEVERYTHINGScale.x + forksPosY + distanceForksToPallet, originalEVERYTHINGScale.y + forksPosY + distanceForksToPallet, originalEVERYTHINGScale.z + forksPosY + distanceForksToPallet);

        foreach (Renderer component in mats) {

            Debugga.Logga(component.transform.name);
            if (component.transform.name != "fork_left" && component.transform.name != "fork_right") { 
                component.material.SetColor("_Color", new Color (component.material.color.r, component.material.color.g, component.material.color.b, 1*(dist2-50)/305));
            }
        }
    }

    public void liftForks(float h) {

        if (h >= frame_2.transform.localScale.y) {
            frame_2.transform.localPosition = new Vector3(frame_2.transform.localPosition.x, h - frame_2.transform.localScale.y/2 , frame_2.transform.localPosition.z);
            forks.transform.localPosition = new Vector3(forks.transform.localPosition.x, forksOriginalPosition.y + h, forks.transform.localPosition.z);
        }
        else {
            frame_2.transform.localPosition = frame_2OriginalPosition;
            forks.transform.localPosition = new Vector3(forks.transform.localPosition.x, forksOriginalPosition.y + h, forks.transform.localPosition.z);
        }
    }

    public void moveFrameForward(float v) {

        frame_1.transform.localPosition = new Vector3(frame_1OriginalPosition.x + v, frame_1.transform.localPosition.y, frame_1.transform.localPosition.z);

        holders.transform.localScale = new Vector3(frame_1.transform.localPosition.x , holders.transform.localScale.y, holders.transform.localScale.z);
    }

    public void sideShift(float v) {

        forks.transform.localPosition = new Vector3(forks.transform.localPosition.x, forks.transform.localPosition.y, forksOriginalPosition.z + v);
    }

    public void tiltForks(float v) {

        forks.transform.localEulerAngles = new Vector3(forks.transform.localEulerAngles.x, forks.transform.localEulerAngles.y, forksOriginalRotation.z + v);
    }

    public void rotateTruck(float v) {
        truck.transform.localEulerAngles = new Vector3(truck.transform.localEulerAngles.x, v, truck.transform.localEulerAngles.z);  
    }
}
