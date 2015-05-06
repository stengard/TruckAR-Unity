using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HologramTruck : MonoBehaviour {

    public GameObject forks, frame_1, frame_2, truck, holders;
    public Slider forkHeightSlider;

    private Vector3 forksOriginalPosition, forksOriginalRotation, frame_1OriginalPosition, frame_2OriginalPosition,truckOriginalPosition, holdersOriginalScale, originalScale;
    private float forkheightMax;
    private Camera cameraLeft, cameraRight, currentCamera;
    public Camera testCamera;
    private Vector3 cameraCentroid;

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

        holdersOriginalScale = holders.transform.localScale;

        forkheightMax = forkHeightSlider.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
        float forksPosY = forks.transform.localPosition.y/100;

        if (Camera.main) {
            cameraCentroid = Camera.main.transform.position;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.position, cameraRight.transform.position });
        }

        transform.localScale = new Vector3(originalScale.x + forksPosY, originalScale.y + forksPosY, originalScale.z + forksPosY);
        //transform.localPosition = new Vector3(truckOriginalPosition.x, truckOriginalPosition.y, truckOriginalPosition.z - forks.transform.localPosition.y);

        Debugga.Logga(forks.transform.localPosition.y + "");
	
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
