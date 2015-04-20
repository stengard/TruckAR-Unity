using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shootout_script : MonoBehaviour {
    public GameObject theForks;
    public float moveDist;
    public Text theText;
    private float maxDist;
    Vector3 lastFrame;
    Vector3 diff;
	// Use this for initialization
	void Start () {
        lastFrame = transform.parent.eulerAngles;
        theForks.transform.position = new Vector3(theForks.transform.position.x, theForks.transform.position.y, moveDist);
        theText.text = (moveDist / 10).ToString("0") + "m";
	}

    public void changeDist(float newDist) {
        moveDist = newDist;
        theForks.transform.localPosition = new Vector3(theForks.transform.localPosition.x, theForks.transform.localPosition.y, moveDist);
        theText.text = (moveDist/10).ToString("0") + "m";
    }
	// Update is called once per frame
	void Update () {
        diff = transform.parent.eulerAngles - lastFrame;
        if (Mathf.Abs(diff.x) > 10 || Mathf.Abs(diff.y) > 10 || Mathf.Abs(diff.z) > 10) {
            //transform.parent.eulerAngles = lastFrame;
        }

        lastFrame = transform.parent.eulerAngles;
	}
}
