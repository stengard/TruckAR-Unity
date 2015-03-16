using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using metaio;

[RequireComponent(typeof(Camera))]
public class metaioCamera : MonoBehaviour 
{
	private bool updateProjectionMatrix;

	private static List<metaioCamera> instances = new List<metaioCamera>();

	[SerializeField]
	public CameraType cameraType;

	/// <summary>
	/// Reference to the camera object
	/// </summary>
	private Camera mCamera;

	public void Awake()
	{
		instances.Add(this);
		mCamera = GetComponent<Camera>();
	}

	public void OnDestroy()
	{
		instances.Remove(this);
	}

	void Start()
	{
		mCamera.transform.position = Vector3.zero;
		mCamera.transform.rotation = Quaternion.identity;
		updateProjectionMatrix = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!updateProjectionMatrix)
		{
			return;
		}

		float[] m = new float[16];

		// Retrieve camera projection matrix
		MetaioSDKUnity.getProjectionMatrix(m, cameraType);

		// quick test to validate projection matrix
		if (m[0] > 0)
		{
			// Create matrix, note that array returned by SDK is column-major
			
			Matrix4x4 matrix;
			
			matrix.m00 = m[0];
			matrix.m10 = m[1];
			matrix.m20 = m[2];
			matrix.m30 = m[3];
			
			matrix.m01 = m[4];
			matrix.m11 = m[5];
			matrix.m21 = m[6];
			matrix.m31 = m[7];
			
			matrix.m02 = m[8];
			matrix.m12 = m[9];
			matrix.m22 = m[10];
			matrix.m32 = m[11];
			
			matrix.m03 = m[12];
			matrix.m13 = m[13];
			matrix.m23 = m[14];
			matrix.m33 = m[15];
			
			mCamera.projectionMatrix = matrix;
			Debug.Log("Setting projection matrix: " + mCamera.projectionMatrix.ToString());
			updateProjectionMatrix = false;
		}
	}

	/// <summary>
	/// Update camera projection matrix when screen orientation changes
	/// </summary>
	public static void updateCameraProjectionMatrix()
	{
		foreach (metaioCamera camera in instances)
		{
			// Update projection matrix in next Update() call
			camera.updateProjectionMatrix = true;
		}
	}
}

