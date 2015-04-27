using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SideShift_script : MonoBehaviour {
    public GameObject SideshiftForks;
    public Text theText;
    public float ShiftPercent = 0;
    private float maxShift = 15;
	// Use this for initialization
	void Start () {
        SideshiftForks.transform.position = new Vector3(SideshiftForks.transform.position.x+(0.01f*ShiftPercent*maxShift), SideshiftForks.transform.position.y, SideshiftForks.transform.position.z);
        theText.text = ShiftPercent + "cm";
    }
    public void changeShift(float newShift) {
        ShiftPercent = newShift;
        SideshiftForks.transform.localPosition = new Vector3(-1 + (0.01f * ShiftPercent * maxShift), SideshiftForks.transform.localPosition.y, SideshiftForks.transform.localPosition.z);
        theText.text = Mathf.Round(Mathf.Abs(ShiftPercent)) + "cm";
    }
	// Update is called once per frame
	void Update () {      
    }
}
