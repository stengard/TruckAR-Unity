using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using metaio;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class provides main behavior for the metaioSDK GameObject
/// </summary>
public class metaioSDK : MonoBehaviour
{
	// Ensure dependency DLLs can be loaded
	// (cf. http://forum.unity3d.com/threads/31083-DllNotFoundException-when-depend-on-another-dll)
	private void adjustPath()
	{
		var envPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
		var pluginsPath = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Assets"), "Plugins");

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
		// Unfortunately we cannot use Application.dataPath in the loading thread (in which this constructor is called),
		// so we have to construct the path to "XYZ_Data/Plugins" ourself. Changing the PATH later (e.g. in Awake) does
		// not seem to work.

		// Search for "Plugins" folder in subfolders of the current directory (where the executable is)
		string[] subPaths = Directory.GetFileSystemEntries(Environment.CurrentDirectory);

		foreach (var subPath in subPaths)
		{
			var fullSubPath = Path.Combine(Environment.CurrentDirectory, subPath);
			// Only look at directories
			if (!Directory.Exists(fullSubPath))
				continue;

			// Use GetFullPath to ensure conversion of path separators (slash or backslash) to native
			var potentialPluginsPath = Path.GetFullPath(Path.Combine(fullSubPath, "Plugins"));
			if (Directory.Exists(potentialPluginsPath) && !envPath.Contains(Path.PathSeparator + potentialPluginsPath))
			{
				envPath += Path.PathSeparator + potentialPluginsPath;
			}
		}
#endif

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
#if (UNITY_EDITOR_64 || UNITY_64)
		var cpuArchPluginPath = Path.Combine(pluginsPath, "x86_64");
#else
		var cpuArchPluginPath = Path.Combine(pluginsPath, "x86");
#endif
		if (pluginsPath.Length > 0 && !envPath.Contains(Path.PathSeparator + cpuArchPluginPath))
		{
			envPath += Path.PathSeparator + cpuArchPluginPath;
		}
#endif

		if (pluginsPath.Length > 0 && !envPath.Contains(Path.PathSeparator + pluginsPath))
		{
			envPath += Path.PathSeparator + pluginsPath;
		}

		Environment.SetEnvironmentVariable(
			"PATH",
			envPath,
			EnvironmentVariableTarget.Process);
	}

	public metaioSDK()
	{
		// Must be called before any calls to the metaio SDK DLL
		adjustPath();
	}


#region Public fields

	// Tracking configuration (path to file or a string)
	[SerializeField]
	public String trackingConfiguration;

	// Device camera to start
	[SerializeField]
	public int cameraFacing = MetaioCamera.FACE_UNDEFINED;

	// Whether stereo rendering is enabled (note that there is no property for see-through because that can simply be
	// achieved by disabling the metaioDeviceCamera script)
	[SerializeField]
	private bool _stereoRenderingEnabled = false;
	public bool stereoRenderingEnabled
	{
		get
		{
			return _stereoRenderingEnabled;
		}
		set
		{
			_stereoRenderingEnabled = value;

			MetaioSDKUnity.setStereoRendering(_stereoRenderingEnabled ? 1 : 0);

			metaioTracker[] trackers = (metaioTracker[])FindObjectsOfType(typeof(metaioTracker));
			foreach (metaioTracker tracker in trackers)
			{
				tracker.stereoRenderingEnabled = _stereoRenderingEnabled;
			}
		}
	}

	[SerializeField]
	private bool _seeThroughEnabled = false;
	public bool seeThroughEnabled
	{
		get
		{
			return _seeThroughEnabled;
		}
		set
		{
			_seeThroughEnabled = value;

			if (MetaioSDKUnity.deviceCamera != null)
			{
				MetaioSDKUnity.deviceCamera.seeThroughEnabled = _seeThroughEnabled;
			}

			// Trackers have references to the mono/left/right cameras. So use that class to change see-through settings.
			metaioTracker[] trackers = (metaioTracker[])FindObjectsOfType(typeof(metaioTracker));
			foreach (metaioTracker tracker in trackers)
			{
				tracker.seeThroughEnabled = _seeThroughEnabled;
			}
		}
	}

	// Near clipping plane limit (default 50mm)
	[SerializeField]
	public float nearClippingPlaneLimit = 50f;

	// Far clipping plane limit (default 1000Km)
	[SerializeField]
	public float farClippingPlaneLimit = 1e+9f;

#endregion

#region Private fields
	/// <summary>
	/// Whether a GUI label should be shown, indicating that the application was not started with "-force-opengl".
	/// </summary>
	private bool showWrongRendererError = false;

#endregion

#region Editor script fields

	public static String[] trackingAssets = {"None", "DUMMY", "GPS", "ORIENTATION", "LLA", "CODE", "QRCODE", "FACE", "StreamingAssets...", "Absolute Path or String...", "Generated"};

	[HideInInspector]
	[SerializeField]
	public int trackingAssetIndex;

	[HideInInspector]
	[SerializeField]
	public UnityEngine.Object trackingAsset = null;

#endregion

	void Awake()
	{
		// Validate that renderer is OpenGL
		showWrongRendererError = !SystemInfo.graphicsDeviceVersion.Contains("OpenGL");

#if !UNITY_EDITOR && (UNITY_IPHONE)
		if (!showWrongRendererError)
		{
			// validate OpenGL ES version
			showWrongRendererError = !SystemInfo.graphicsDeviceVersion.Contains("OpenGL ES 2.");
		}
#endif

		if (showWrongRendererError)
		{
			Debugga.LoggaFel("#######################\n" +
			               "Metaio SDK only works with OpenGL renderer. The current renderer is "+SystemInfo.graphicsDeviceVersion+".\n"+
#if !UNITY_EDITOR && (UNITY_IPHONE)
			               "Please choose OpenGL ES 2.x Graphics API from the Player Settings.\n" +
#elif (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
			               "Please pass \"-force-opengl\" to the Windows executable to enforce running with OpenGL.\n" +
#endif
			               "#######################");
		}

		AssetsManager.extractAssets(true);
	}

	void OnGUI()
	{
		if (showWrongRendererError)
		{
			Color backup = GUI.contentColor;
			GUI.contentColor = new Color(1, 0, 0); // red
			GUI.Label(new Rect(0, 0, Screen.width, 100), "Metaio SDK camera stream rendering can only work with OpenGL.");
#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
			GUI.Label(new Rect(0, 110, Screen.width, 100), "Please choose OpenGL ES 2.x Graphics API from the Player Settings.");
#elif (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
			GUI.Label(new Rect(0, 110, Screen.width, 100), "Please pass \"-force-opengl\" to the Windows executable to enforce running with OpenGL.");
#endif
			GUI.contentColor = backup;
		}
	}

	/// <summary>
	/// Parses the application signature from the file StreamingAssets/MetaioSDKLicense.xml.
	/// </summary>
	/// <returns>
	/// Signature as string, or empty string if signature not found or signature file does not exist. Never returns NULL.
	/// </returns>
	public string parseApplicationSignature()
	{
#if UNITY_EDITOR
		string licenseFilePath = Path.Combine(Application.streamingAssetsPath, "MetaioSDKLicense.xml");
#else
		string licenseFilePath = AssetsManager.getAssetPath("MetaioSDKLicense.xml");
#endif
		FileInfo licenseFileInfo = new FileInfo(licenseFilePath);

		if (licenseFileInfo.Exists)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(licenseFilePath);

			// Same structure as for native Windows applications with Metaio SDK (appKeys.xml), except that <AppID>
			// is unused, so we only need <SignatureKey> for Unity
			XmlNodeList rootList = doc.GetElementsByTagName("Keys");
			if (rootList.Count != 1)
			{
				Debugga.LoggaFel("MetaioSDKLicense.xml has wrong format");
			}
			else
			{
				string signatureKey = null;

				XmlNode root = rootList[0];

				foreach (XmlNode childNode in root.ChildNodes)
				{
					if (childNode.Name == "SignatureKey")
					{
						signatureKey = childNode.InnerText;
					}
				}

				if (string.IsNullOrEmpty(signatureKey))
				{
					// On Android/iOS, you *must* register the application and enter a signature (even for free
					// license), while on Windows/Mac, the SDK runs with free license if no signature given.
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
					Debugga.LoggaFel("Missing application signature");
#else
					Debugga.Logga("Missing application signature");
#endif
				}
				else
				{
					return signatureKey.Trim();
				}
			}
		}
		else
		{
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
					Debugga.LoggaFel("No file MetaioSDKLicense.xml found in StreamingAssets");
#else
					Debugga.Logga("No file MetaioSDKLicense.xml found in StreamingAssets");
#endif
		}

		return string.Empty;
	}

	public void writeApplicationSignature(string signatureKey)
	{
		// Code not needed in deployed application
#if UNITY_EDITOR
		string licenseFilePath = Path.Combine(Application.streamingAssetsPath, "MetaioSDKLicense.xml");

		DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
		if (!dir.Exists)
		{
			try
			{
				dir.Create();
			}
			catch (Exception)
			{
			}
		}

		XmlDocument doc = new XmlDocument();
		doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
		XmlNode root = doc.AppendChild(doc.CreateElement("Keys"));
		XmlNode signatureKeyElement = root.AppendChild(doc.CreateElement("SignatureKey"));
		signatureKeyElement.AppendChild(doc.CreateTextNode(signatureKey));
		try
		{
			doc.Save(licenseFilePath);
		}
		catch (Exception e)
		{
			//Debugga.LoggaFel("Failed to write MetaioSDKLicense.xml ("+signatureKey+"): " + e);
		}
#endif
	}

	void Start ()
	{
		int result = MetaioSDKUnity.createMetaioSDKUnity(parseApplicationSignature());
		if (result == 0)
			Debugga.Logga("metaio SDK created successfully");
		else
			Debugga.LoggaFel("Failed to create metaio SDK!");

		bool mustRestoreAutoRotation = false;
		if (Screen.orientation == ScreenOrientation.Unknown)
		{
			// In this case we know that auto-rotation was active, because else Unity would immediately set a certain
			// default orientation (as defined in player settings).
			mustRestoreAutoRotation = true;

			Debugga.Logga("Fixing unknown orientation problem");

			switch (Input.deviceOrientation)
			{
				case DeviceOrientation.PortraitUpsideDown:
					Screen.orientation = ScreenOrientation.PortraitUpsideDown;
					break;
				case DeviceOrientation.LandscapeLeft:
					Screen.orientation = ScreenOrientation.LandscapeLeft;
					break;
				case DeviceOrientation.LandscapeRight:
					Screen.orientation = ScreenOrientation.LandscapeRight;
					break;
				case DeviceOrientation.FaceDown:
				case DeviceOrientation.FaceUp:
				case DeviceOrientation.Portrait:
				case DeviceOrientation.Unknown:
				default:
					Screen.orientation = ScreenOrientation.Portrait;
					break;
			}
		}

		MetaioSDKUnity.updateScreenOrientation(Screen.orientation);

		if (mustRestoreAutoRotation)
		{
			Screen.orientation = ScreenOrientation.AutoRotation;
		}



		Debugga.Logga("Starting the default camera with facing: "+cameraFacing);
		MetaioSDKUnity.startCamera(cameraFacing);

		// Load tracking configuration
		if (String.IsNullOrEmpty(trackingConfiguration))
		{
			Debugga.Logga("No tracking configuration specified");

			result = MetaioSDKUnity.setTrackingConfiguration("", 0);
		}
		else
		{
			result = MetaioSDKUnity.setTrackingConfigurationFromAssets(trackingConfiguration);

			if (result == 0)
				Debugga.LoggaFel("Start: failed to load tracking configuration: "+trackingConfiguration);
			else
				Debugga.Logga("Loaded tracking configuration: "+trackingConfiguration);
		}

		// Set LLA objects' rendering limits
		MetaioSDKUnity.setLLAObjectRenderingLimits(10, 1000);

		// Set renderer clipping plane limits
		MetaioSDKUnity.setRendererClippingPlaneLimits(nearClippingPlaneLimit, farClippingPlaneLimit);

		// Apply initial settings for mono/stereo and (non-)see-through mode
		stereoRenderingEnabled = _stereoRenderingEnabled;
		seeThroughEnabled = _seeThroughEnabled;


        //Setting the calibration from the file in /mnt/sdcard/metaio/hec/hec.xml
        //MetaioSDKUnity.setHandEyeCalibrationByDevice();
        StartCoroutine(CountdownThenTweakCameraSettings());
	}

	void OnDisable()
	{
		Debugga.Logga("OnDisable: deleting metaio sdk...");

		// stop camera before deleting the instance
		MetaioSDKUnity.stopCamera();

		// delete the instance
		MetaioSDKUnity.deleteMetaioSDKUnity();
	}

	void OnApplicationPause(bool pause)
	{
		Debugga.Logga("OnApplicationPause: "+pause);

		if (pause)
		{
			MetaioSDKUnity.onPauseApplication();
		}
		else
		{
#if UNITY_IPHONE
			System.Threading.Thread.Sleep(1000);
#endif
			MetaioSDKUnity.onResumeApplication();
		}
	}

    IEnumerator CountdownThenTweakCameraSettings() {

        // Wait a bit as this doesn�t work if you do it straight from start, obviously I probably just want to wait a frame rather then seconds in the final thing but 5 seconds was a good amount of time to test with. 
        yield return new WaitForSeconds(1.0f);

        // Get the cameras
        List<MetaioCamera> cameras = MetaioSDKUnity.getCameraList();

        // the first camera in the array is the back camera or it is for iPad3 anyway.
        MetaioCamera camera = cameras[0];

        // Not necessary but I found knowing the original settings of the iPad camera very useful
        Debugga.Logga("-- TINKERING STUFF --");
        Debugga.Logga("Camera: " + camera.index + " Camera Name: " + camera.friendlyName);
        Debugga.Logga("Downsampling: " + camera.downsample);
        Debugga.Logga("FPS: x=" + camera.fps.x + " y=" + camera.fps.y);
        Debugga.Logga("Resolution: x=" + camera.resolution.x + " y=" + camera.resolution.y);
        Debugga.Logga("-- End of Tinkering Stuff --");

        // Defining the new camera settings , I encountered a bug where setting resolution without setting FPS caused the FPS to be set to the resolution's values, to avoid this I�m stating the FPS as well.  
        camera.fps.x = 30;
        camera.fps.y = 30;
        camera.resolution.x = 320;
        camera.resolution.y = 240;
        // this bits probably unnecessary now but meh, testing on device takes so long that I can�t be bothered to check for bugs post its removal. 
        camera.downsample = 1;

        // Apply the new camera settings 
        MetaioSDKUnity.startCamera(camera);

        // Waiting some more then reporting back the camera settings so I could check they�d been successfully changed.
        yield return new WaitForSeconds(1.0f);

        Debugga.Logga("-- TINKERING STUFF --");
        Debugga.Logga("Camera: " + camera.index + " Camera Name: " + camera.friendlyName);
        Debugga.Logga("Downsampling: " + camera.downsample);
        Debugga.Logga("FPS: x=" + camera.fps.x + " y=" + camera.fps.y);
        Debugga.Logga("Resolution: x=" + camera.resolution.x + " y=" + camera.resolution.y);
        Debugga.Logga("-- End of Tinkering Stuff --");

    }

}
