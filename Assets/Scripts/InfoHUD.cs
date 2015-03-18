using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;

public class InfoHUD : MonoBehaviour {

    private string cfpsString = "0";
    private string tfpsString = "0";
    private string rfpsString = "0";

    public Text cameraFPS;
    public Text renderingFPS;
    public Text trackingFPS;
	
	// Update is called once per frame
	void Update () {
        cameraFPS.text = MetaioSDKUnity.getCameraFrameRate().ToString();
        renderingFPS.text = MetaioSDKUnity.getRenderingFrameRate().ToString();
        trackingFPS.text = MetaioSDKUnity.getTrackingFrameRate().ToString();
	}
}
