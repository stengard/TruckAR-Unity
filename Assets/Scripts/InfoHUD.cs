using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;

public class InfoHUD : MonoBehaviour {

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
