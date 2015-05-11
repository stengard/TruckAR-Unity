using UnityEngine;
using System.Collections;

public class OffscreenPointer : MonoBehaviour {
    private Vector3 cameraCentroid;
    private Camera cam;
    public Camera cameraLeft;
    public Camera cameraRight;
    public GameObject theArrow;
    RectTransform theTrans;
    // Use this for initialization
    void Start() {
        cameraLeft = (Camera)GameObject.Find("StereoCameraLeft").GetComponent<Camera>();
        cameraRight = (Camera)GameObject.Find("StereoCameraRight").GetComponent<Camera>();
        theArrow = Instantiate(theArrow);
        theTrans = theArrow.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        UnityEngine.UI.Image someImage = GetComponentInChildren<UnityEngine.UI.Image>();
        if (!someImage.enabled) {
            theArrow.SetActive(false);
            return;
        }
        else
            theArrow.SetActive(true);

        if (Camera.main) {
            cameraCentroid = Camera.main.transform.up;
            cam = Camera.main;
        }
        else {
            cameraCentroid = Vector3Helper.CenterOfVectors(new Vector3[] { cameraLeft.transform.up, cameraRight.transform.up });
            cam = cameraRight;
        }
        //Get vector to object from camera in screen space.
        Vector3 screenpoint = cam.WorldToScreenPoint(transform.position);
        if (25 < screenpoint.x && screenpoint.x < Screen.width - 75 && 25 < screenpoint.y && screenpoint.y < Screen.height - 50) {
            theArrow.SetActive(false);
            return;
        }
        else{
            theArrow.SetActive(true);
        }
        float angle = Mathf.Atan((screenpoint.y-Screen.height/2) / (screenpoint.x-Screen.width/2));
        theTrans.anchoredPosition = new Vector2(Mathf.Clamp(screenpoint.x, 75, Screen.width-75), Mathf.Clamp(screenpoint.y, 75, Screen.height-75));
        
        if (screenpoint.x - Screen.width / 2 < 0) {
            theTrans.localEulerAngles = new Vector3(theTrans.eulerAngles.x, theTrans.eulerAngles.y, 180+Mathf.Rad2Deg * angle);
        }
        else {
            theTrans.localEulerAngles = new Vector3(theTrans.eulerAngles.x, theTrans.eulerAngles.y, Mathf.Rad2Deg * angle);
        }
    }

    void OnEnable() {
        if (theArrow)
            theArrow.SetActive(true);
    }
    void OnDisable() {
        if (theArrow)
            theArrow.SetActive(false);
    }
    

}
