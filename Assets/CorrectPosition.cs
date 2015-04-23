using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CorrectPosition : MonoBehaviour {


    Vector3 originalPosition;

    public Text textPos;
    string startText;


	// Use this for initialization
	void Start () {

        startText = textPos.text;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.right*500, Color.green);
        Debug.DrawRay(transform.position, transform.forward * 500, Color.red);
        textPos.text = startText + " " + transform.localPosition.x + ", " + transform.localPosition.y + ", " + transform.localPosition.z;

    }

    public void translateInX(float f) {

        //transform.localPosition = Vector3.right * f;

        transform.localPosition = new Vector3(f, transform.localPosition.y, transform.localPosition.z);
    }
    public void translateInZ(float f) {
        //transform.localPosition = Vector3.forward * f;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -f);
    }
}
