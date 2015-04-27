using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class predictivePath : MonoBehaviour {

    public GameObject pivotPoint, backWheel, frontWheelLeft, frontWheelRight, pallet, midPoint;

    private Vector3 normalVector, normalVectorWheel, normalVectorPivotPoint;

    private LineRenderer lineRenderer, lineRendererWheel, lineRendererPivotPoint;

    public GameObject lineObject;

    public int numberOfLineObjects;
    public float lengthOfLines;

    public bool showHelpLines;

    private List<GameObject> leftLineObjects, rightLineObjects;

    private Vector3 originalRotation;

    // Use this for initialization
    void Start() {
        if (!pivotPoint)
            pivotPoint = (GameObject)transform.FindChild("PivotPoint").gameObject;

        if (!backWheel)
            backWheel = (GameObject)transform.FindChild("BackWheel").gameObject;

        if (!frontWheelLeft)
            frontWheelLeft = (GameObject)transform.FindChild("FronWheelLeft").gameObject;

        if (!frontWheelRight)
            frontWheelRight = (GameObject)transform.FindChild("FrontWheelRight").gameObject;

        if (!pallet)
            pallet = (GameObject)transform.FindChild("Pallet").gameObject;

        normalVector = pivotPoint.transform.position - backWheel.transform.position;

        gameObject.AddComponent<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        backWheel.gameObject.AddComponent<LineRenderer>();
        lineRendererWheel = backWheel.GetComponent<LineRenderer>();
        lineRendererWheel.SetWidth(1f, 1f);

        pivotPoint.gameObject.AddComponent<LineRenderer>();
        lineRendererPivotPoint = pivotPoint.GetComponent<LineRenderer>();
        lineRendererPivotPoint.SetWidth(1f, 1f);

        originalRotation = pivotPoint.transform.localEulerAngles;

        leftLineObjects = new List<GameObject>();
        rightLineObjects = new List<GameObject>();

        midPoint = Instantiate(midPoint);
        midPoint.transform.parent = transform;
        midPoint.transform.localScale += new Vector3(10, 10, 10);

        addLeftLines();
        addRightLines();
    }



    // Update is called once per frame
    void Update() {

        //Calculate the angle  between backWheel forward vector and middle plane and  rotate the pivot point accordingly.


        Vector3 eulerAngles = originalRotation;
        //Calculate normal vectors 
        normalVectorWheel = Vector3.Normalize(backWheel.transform.forward);
        normalVector = pivotPoint.transform.position - backWheel.transform.position;
        normalVectorPivotPoint = Vector3.Normalize(pivotPoint.transform.forward);

        float angle = Vector3.Angle(normalVector, normalVectorWheel);
        if (angle == 0) angle = 0.001f;


        if (backWheel.transform.localEulerAngles.y < 180.0f && backWheel.transform.localEulerAngles.y > 0) {
            eulerAngles.y = eulerAngles.y + angle;
        }
        else {
            eulerAngles.y = eulerAngles.y - angle;
        }

        pivotPoint.transform.localEulerAngles = eulerAngles;

        float distance = Vector3.Distance(pivotPoint.transform.position, backWheel.transform.position);

        float turningRadius = distance / Mathf.Tan(Mathf.Deg2Rad * angle);

        Vector3 turningMid = pivotPoint.transform.position + transform.right * turningRadius;

        if (pivotPoint.transform.localEulerAngles.y > 180) {
            turningMid = pivotPoint.transform.position - transform.right * turningRadius;
        }

        midPoint.transform.position = turningMid;

        float turningRadiusLeftWheel = 1 / (Mathf.Sin(Mathf.Deg2Rad * angle) / Vector3.Distance(frontWheelLeft.transform.position, turningMid));
        float turningRadiusRightWheel = 1 / (Mathf.Sin(Mathf.Deg2Rad * angle) / Vector3.Distance(frontWheelRight.transform.position, turningMid));

        Vector3[] pointsOnTurningCircleLeft = new Vector3[numberOfLineObjects];
        Vector3[] pointsOnTurningCircleRight = new Vector3[numberOfLineObjects];

        float slice_L = (lengthOfLines / turningRadiusLeftWheel) * Mathf.PI / numberOfLineObjects;
        float slice_R = (lengthOfLines / turningRadiusRightWheel) * Mathf.PI / numberOfLineObjects;

        float translateDirection_left_X = 1;
        float translateDirection_left_Z = 1;
        float translateDirection_right_Z = 1;
        float translateDirection_right_X = 1;

        if (eulerAngles.y > 0 && eulerAngles.y < 64) {
            translateDirection_left_X = -1;
            translateDirection_left_Z = -1;
            translateDirection_right_X = -1;
            translateDirection_right_Z = -1;
        }
        else if(eulerAngles.y >= 64 && eulerAngles.y < 115){
            translateDirection_left_X = -1;
            translateDirection_left_Z = -1;
            translateDirection_right_X = 1;
            translateDirection_right_Z = 1;
        }
        else if (eulerAngles.y >= 115 && eulerAngles.y < 180) {
            translateDirection_left_X = 1;
            translateDirection_left_Z = 1;
            translateDirection_right_X = 1;
            translateDirection_right_Z = 1;
        }
        else if (eulerAngles.y >= 180 && eulerAngles.y < 244) {
            translateDirection_left_X = -1;
            translateDirection_left_Z = 1;
            translateDirection_right_X = -1;
            translateDirection_right_Z = 1;
        }
        else if (eulerAngles.y >= 244 && eulerAngles.y < 295) {
            translateDirection_left_X = -1;
            translateDirection_left_Z = 1;
            translateDirection_right_X = 1;
            translateDirection_right_Z = -1;
        }
        else if (eulerAngles.y >= 295 && eulerAngles.y <= 360) {
            translateDirection_left_X = 1;
            translateDirection_left_Z = -1;
            translateDirection_right_X = 1;
            translateDirection_right_Z = -1;
        }

        for (int i = 0; i < numberOfLineObjects; i++) {

            float stepAngle_L = slice_L * i;
            float stepAngle_R = slice_R * i;

            float x_L = translateDirection_left_X*turningRadiusLeftWheel * Mathf.Cos(stepAngle_L);
            float z_L = turningRadiusLeftWheel * Mathf.Sin(stepAngle_L);

            float x_R = translateDirection_right_X * turningRadiusRightWheel * Mathf.Cos(stepAngle_R);
            float z_R = turningRadiusRightWheel * Mathf.Sin(stepAngle_R);

            pointsOnTurningCircleLeft[i].x = x_L - translateDirection_left_X * turningRadiusLeftWheel;
            pointsOnTurningCircleLeft[i].y = 0;
            pointsOnTurningCircleLeft[i].z = translateDirection_left_Z * z_L;

            pointsOnTurningCircleRight[i].x = x_R - translateDirection_right_X * turningRadiusRightWheel;
            pointsOnTurningCircleRight[i].y = 0;
            pointsOnTurningCircleRight[i].z = translateDirection_right_Z * z_R;

            leftLineObjects[i].transform.localPosition = pointsOnTurningCircleLeft[i] / frontWheelLeft.transform.localScale.x;
            rightLineObjects[i].transform.localPosition = pointsOnTurningCircleRight[i] / frontWheelLeft.transform.localScale.x;

            if (i != 0) {
                //Rotate the object so that it "looks at" the previous.
                leftLineObjects[i - 1].transform.LookAt(leftLineObjects[i].transform.position);
                rightLineObjects[i - 1].transform.LookAt(rightLineObjects[i].transform.position);

                rightLineObjects[i - 1].transform.localEulerAngles = new Vector3(0, rightLineObjects[i - 1].transform.localEulerAngles.y, 0);
                leftLineObjects[i - 1].transform.localEulerAngles = new Vector3(0, leftLineObjects[i - 1].transform.localEulerAngles.y, 0);
            
            }

        }

        //Show some helplines
        if (showHelpLines) {
            lineRendererWheel.SetPosition(0, backWheel.transform.position);
            lineRendererWheel.SetPosition(1, normalVectorWheel * 10);

            lineRendererPivotPoint.SetPosition(0, pivotPoint.transform.position);
            lineRendererPivotPoint.SetPosition(1, normalVectorPivotPoint * 10);

            lineRenderer.SetPosition(0, backWheel.transform.position);
            lineRenderer.SetPosition(1, pivotPoint.transform.position);
        }



    }

    private void addLeftLines() {
        for (int i = 0; i < numberOfLineObjects; i++) {

            leftLineObjects.Add(Instantiate(lineObject));
            leftLineObjects[i].transform.parent = frontWheelLeft.transform;
            //leftLineObjects[i].transform.localPosition= midPoint.transform.position;

            leftLineObjects[i].name = "left_line_" + i;
            //sq[i].SetActive(false);

        }
    }

    private void addRightLines() {
        for (int i = 0; i < numberOfLineObjects; i++) {

            rightLineObjects.Add(Instantiate(lineObject));
            rightLineObjects[i].transform.parent = frontWheelRight.transform;
            //rightLineObjects[i].transform.position = transform.position;

            rightLineObjects[i].name = "right_line_" + i;
            //sq[i].SetActive(false);

        }
    }

    public void rotateBackWheel(float value) {

        backWheel.transform.localEulerAngles = new Vector3(backWheel.transform.localEulerAngles.x, value, backWheel.transform.localEulerAngles.z);
    
    }
}
