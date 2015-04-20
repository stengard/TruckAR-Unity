using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class predictivePath : MonoBehaviour {

    public GameObject pivotPoint, backWheel, frontWheelLeft, frontWheelRight, pallet, midPoint;


    private Vector3 normalVector, normalVectorWheel, normalVectorPivotPoint;

    private LineRenderer lineRenderer, lineRendererWheel, lineRendererPivotPoint;

    public GameObject lineObject;

    public int numberOfLineObjects;

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
        float angle = Vector3.Angle(normalVector, normalVectorWheel);
        if (angle == 0) angle = 0.001f;

        Vector3 eulerAngles = originalRotation;

        //Calculate normal vectors 
        normalVectorWheel = Vector3.Normalize(backWheel.transform.forward);
        normalVector = pivotPoint.transform.position - backWheel.transform.position;
        normalVectorPivotPoint = Vector3.Normalize(pivotPoint.transform.forward);


        if (backWheel.transform.localEulerAngles.y < 180 && backWheel.transform.localEulerAngles.y > 0) {
            eulerAngles.y = eulerAngles.y + angle;
        }
        else {
            eulerAngles.y = eulerAngles.y - angle;
        }

        pivotPoint.transform.localEulerAngles = eulerAngles;

        //reinstantiate lineobjects
        //if (leftLineObjects.Count != numberOfLineObjects) {
        //    for (int i = 0; i < leftLineObjects.Count; i++) {
        //        Destroy(leftLineObjects[i]);
        //        Destroy(rightLineObjects[i]);
        //    }

        //    rightLineObjects.Clear();
        //    leftLineObjects.Clear();
        //    addLeftLines();
        //    addRightLines();
        //}

        float distance = Vector3.Distance(pivotPoint.transform.position, backWheel.transform.position);

        Vector3 vectorMidLeft = frontWheelLeft.transform.position + pivotPoint.transform.forward * (distance * 0.5f);
        Vector3 vectorMidRight = frontWheelRight.transform.position + pivotPoint.transform.forward * (distance * 0.5f);


        float turningRadius = distance / Mathf.Tan(Mathf.Deg2Rad * angle);
        Vector3 turningMid = pivotPoint.transform.position + transform.right * turningRadius;

        midPoint.transform.position = turningMid;

        Vector3[] pointsOnTurningCircleLeft = new Vector3[numberOfLineObjects];
        Vector3[] pointsOnTurningCircleRight = new Vector3[numberOfLineObjects];

        float slice = 0.5f*Mathf.PI/ numberOfLineObjects;

        for (int i = 0; i < numberOfLineObjects; i++) {

            float stepAngle = slice * i;

            float x = turningRadius * Mathf.Cos(stepAngle);
            float z = turningRadius * Mathf.Sin(stepAngle);

            pointsOnTurningCircleLeft[i].x = x - turningRadius;
            pointsOnTurningCircleLeft[i].y = 0;
            pointsOnTurningCircleLeft[i].z = z;

            pointsOnTurningCircleRight[i].x = x - turningRadius;
            pointsOnTurningCircleRight[i].y = 0;
            pointsOnTurningCircleRight[i].z = z;

            leftLineObjects[i].transform.localPosition =  pointsOnTurningCircleLeft[i];
            rightLineObjects[i].transform.localPosition = pointsOnTurningCircleRight[i];
        }

        //Show some helplines
        lineRendererWheel.SetPosition(0, backWheel.transform.position);
        lineRendererWheel.SetPosition(1, normalVectorWheel * 10);

        lineRendererPivotPoint.SetPosition(0, pivotPoint.transform.position);
        lineRendererPivotPoint.SetPosition(1, normalVectorPivotPoint * 10);

        lineRenderer.SetPosition(0, backWheel.transform.position);
        lineRenderer.SetPosition(1, pivotPoint.transform.position);



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
}
