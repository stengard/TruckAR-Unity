using metaio;
using UnityEngine;
using System;

public class StereoRenderingCallback : metaioCallback
{
	protected override void onSDKReady()
	{
		// Recommended way to load stereo calibration (in this order):
		// 1) Load your own, exact calibration (calibration XML file created with Toolbox 6.0.1 or newer),
		//    i.e. *you* as developer provide a calibration file. Note that the path to "hec.xml"
		//    doesn't actually exist in this example; it's only there to show how to apply a custom
		//    calibration file.
		// 2) Load calibration XML file from default path, i.e. in case the user has used Toolbox to
		//    calibrate (result file always stored at same path).
		// 3) Load calibration built into Metaio SDK for known devices (may not give perfect result
		//    because stereo glasses can vary).
		// Items 2) and 3) only do something on Android for the moment, as there are no supported,
		// non-Android stereo devices yet.
        //Debugga.LoggaLive("Inne i OnSDKReaady");

        string calibrationFilePath = AssetsManager.getAssetPath("hecMike.xml");
        //string calibrationFilePath = AssetsManager.getAssetPath("hecMartin2.xml");
        if ((calibrationFilePath == null || !MetaioSDKUnity.setHandEyeCalibrationFromFile(calibrationFilePath))) {
            MetaioSDKUnity.setHandEyeCalibrationByDevice();
        }
	}

    protected override void onTrackingEvent(System.Collections.Generic.List<TrackingValues> trackingValues) {
        base.onTrackingEvent(trackingValues);
        Debugga.Logga("Quality:"+trackingValues[0].quality);
        if (trackingValues[0].state == metaio.TrackingState.Found) {
            transform.FindChild("StereoCameraRight").GetComponent<NavTruckScript>().updatePath();
        }
    }

}
