using UnityEngine;
using System.Collections;

public class XRay : MonoBehaviour {

    public GameObject canvas;
    private Vector3 originalScale;
    private Vector3 originalPosition;

	// Use this for initialization
	void Start () {

        originalScale = canvas.transform.localScale;
        originalPosition = canvas.transform.localPosition;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void scaleX(float f) {
        canvas.transform.localScale = new Vector3(originalScale.x + f, canvas.transform.localScale.y, canvas.transform.localScale.z);
    }

    public void scaleY(float f) {
        canvas.transform.localScale = new Vector3(canvas.transform.localScale.x, originalScale.x + f, canvas.transform.localScale.z);
    }

    public void translateX(float f) {
        canvas.transform.localPosition = new Vector3(originalPosition.x + f, canvas.transform.localPosition.y, canvas.transform.localPosition.z);
    }

    public void translateY(float f) {
        canvas.transform.localPosition = new Vector3(canvas.transform.localPosition.x, originalPosition.y + f, canvas.transform.localPosition.z);
    }
}
