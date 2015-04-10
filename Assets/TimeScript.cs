using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {
    Text theText;
    string[] monthArray;
	// Use this for initialization
	void Start () {
        monthArray = new string[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        theText = GetComponent<Text>();
        theText.text = monthArray[System.DateTime.Now.Month-1]+" "+System.DateTime.Now.Day.ToString("00")+" "+System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00");
	}
	
	// Update is called once per frame
	void Update () {
        theText.text = monthArray[System.DateTime.Now.Month - 1] + " " + System.DateTime.Now.Day.ToString("00")+" "+System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00");
	}
}
