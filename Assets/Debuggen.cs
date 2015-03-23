using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debuggen : MonoBehaviour {

    public Text debug;

	// Update is called once per frame
    void Start() {
        debug.text = "";
    }

	void Update () {

        debug.text = "";
        foreach(string s in Debugga.getLogs()){
            debug.text += s + "\n";
        }
	}
}
