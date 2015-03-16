using UnityEngine;
using System.Collections;
using metaio;

public class StatsGUI : MonoBehaviour 
{
	
	public GUIStyle buttonTextStyle;
	float SizeFactor;
	public GUIStyle headlineTextStyle;

	private string cfpsString = "cfps: 0";
	private string tfpsString = "tfps: 0";
	private string rfpsString = "rfps: 0";

	// Use this for initialization
	void Start () {
		SizeFactor = GUIUtilities.SizeFactor;
	}
	
	// Update is called once per frame
	void Update () {
		SizeFactor = GUIUtilities.SizeFactor;
		cfpsString = "cfps: " + MetaioSDKUnity.getCameraFrameRate ();
		tfpsString = "tfps: " + MetaioSDKUnity.getTrackingFrameRate();
		rfpsString = "rfps: " + MetaioSDKUnity.getRenderingFrameRate();
	}
	
	void OnGUI () {
		
		if(GUIUtilities.ButtonWithText(new Rect(
			Screen.width - 200*SizeFactor,
			0,
			200*SizeFactor,
			100*SizeFactor),cfpsString,null,buttonTextStyle) ||	Input.GetKeyDown(KeyCode.Escape)) {
			PlayerPrefs.SetInt("backFromARScene", 1);
			Application.LoadLevel("MainMenu");
		}	

		if(GUIUtilities.ButtonWithText(new Rect(
			Screen.width - 200*SizeFactor, 
			110*SizeFactor,
			200*SizeFactor,
			100*SizeFactor),tfpsString,null,buttonTextStyle) ||	Input.GetKeyDown(KeyCode.Escape)) {
			PlayerPrefs.SetInt("backFromARScene", 1);
			Application.LoadLevel("MainMenu");
		}

		if(GUIUtilities.ButtonWithText(new Rect(
			Screen.width - 200*SizeFactor, 
			220*SizeFactor,
			200*SizeFactor,
			100*SizeFactor),rfpsString,null,buttonTextStyle) ||	Input.GetKeyDown(KeyCode.Escape)) {
			PlayerPrefs.SetInt("backFromARScene", 1);
			Application.LoadLevel("MainMenu");
		}

	}
}
