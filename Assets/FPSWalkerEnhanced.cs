using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FPSWalkerEnhanced : MonoBehaviour {

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;

    public float movingSpeed;

    public CursorLockMode lockMode;

    Quaternion originalRotation;

    public Camera cam;
    Vector3 movement; //stores player speed
    public void Start() {
        originalRotation = transform.localRotation;

        Cursor.lockState = lockMode;

    }





    public void Update() {

        if (axes == RotationAxes.MouseXAndY) {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX) {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.position += transform.forward * movingSpeed;
        }

        if (Input.GetKey(KeyCode.S)) {
            transform.position -= transform.forward * movingSpeed;
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.position -= transform.right * movingSpeed;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position += transform.right * movingSpeed;
        }

        if (Input.GetKey(KeyCode.Space)) {
            transform.position += Vector3.up * movingSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            transform.position -= Vector3.up * movingSpeed;
        }
    }


    public static float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;

        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
   
