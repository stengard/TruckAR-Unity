using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tilt_script : MonoBehaviour {
    public GameObject TiltForks;
    public GameObject TiltArrowUp;
    public GameObject TiltArrowDown;
    public Text theText;
    public float currentAngle = 10;
    private float maxAngle = 24;
	// Use this for initialization
	void Start () {
        currentAngle = (maxAngle < currentAngle) ? maxAngle : (-maxAngle > currentAngle) ? -maxAngle : currentAngle;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -currentAngle, transform.eulerAngles.z);
        TiltArrowUp.GetComponent<Renderer>().material.color = new Color((currentAngle > 0) ? 0 : 1, (currentAngle > 0) ? currentAngle / maxAngle : 1, (currentAngle > 0) ? 0 : 1);
        TiltArrowDown.GetComponent<Renderer>().material.color = new Color((currentAngle < 0) ? 0 : 1, (currentAngle < 0) ? -currentAngle / maxAngle : 1, (currentAngle < 0) ? 0 : 1);
        theText.text = (currentAngle < 0) ? "-" : "+" + currentAngle + "°";
	}
    public void changeAngle(float angle) {
        currentAngle = angle;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -currentAngle, transform.localEulerAngles.z);
        TiltArrowUp.GetComponent<Renderer>().material.color = new Color((currentAngle > 0) ? 0 : 1, 1, (currentAngle > 0) ? 0 : 1);
        TiltArrowDown.GetComponent<Renderer>().material.color = new Color((currentAngle < 0) ? 0 : 1, 1, (currentAngle < 0) ? 0 : 1);
        theText.text = (currentAngle < 0) ? currentAngle.ToString("0") + "°" : "+" + currentAngle.ToString("0") + "°";
    }
	// Update is called once per frame
	void Update () {
	}
}
