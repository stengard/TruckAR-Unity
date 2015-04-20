using UnityEngine;
using System.Collections;

public class HologramTruck : MonoBehaviour {

    public GameObject forks, frame_1, frame_2, truck, holders;

    private Vector3 forksOriginalPosition, forksOriginalRotation, frame_1OriginalPosition, frame_2OriginalPosition, holdersOriginalScale;
	// Use this for initialization
	void Start () {

        frame_2OriginalPosition = frame_2.transform.localPosition;
        frame_1OriginalPosition = frame_1.transform.localPosition;
        forksOriginalPosition = forks.transform.localPosition;
        forksOriginalRotation = forks.transform.localEulerAngles;

        holdersOriginalScale = holders.transform.localScale;
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
