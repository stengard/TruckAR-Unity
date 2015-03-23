using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;

public class InfoHUD : MonoBehaviour {

    public Text cameraFPS;
    public Text renderingFPS;
    public Text trackingFPS;

    //Testa get camera list och kolla vad upplösningen är
	// Update is called once per frame
	void Update () {
        cameraFPS.text = MetaioSDKUnity.getCameraFrameRate().ToString();
        renderingFPS.text = MetaioSDKUnity.getRenderingFrameRate().ToString();
        trackingFPS.text = MetaioSDKUnity.getTrackingFrameRate().ToString();
    }
}
