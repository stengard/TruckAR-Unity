using UnityEngine;

public class amsterdamScript : MonoBehaviour {

    public float distBetween = 1000.0f;
    public float rightLeftOffset = 3000.0f;
    public int noOfLights = 10;
    public float lightScale = 350.0f;
    public float speedToDriveAt = 500.0f;
    public float distToLine = 1000;
    public float maxScale = 4.0f;

    public Color greenColor = new Color(0, 255, 0, 1);
    public Color redColor = new Color(255, 0, 0, 1);
    public Color yellowColor = new Color(255, 200, 0, 1);

    private GameObject[] trafficLights_left;
    private GameObject[] trafficLights_right;
    private float distanceMoved;
    public GameObject theCube;
    // Use this for initialization
	void Start () {
        distanceMoved = (-distBetween * noOfLights / 3) -distToLine;
        trafficLights_left = new GameObject[noOfLights];
        trafficLights_right = new GameObject[noOfLights];
        createCubes();
	}

    void createCubes() {
        for (int i = 0; i < noOfLights; i++) {
            trafficLights_left[i] = Instantiate(theCube);
            trafficLights_left[i].name = "LeftLight" + i;
            trafficLights_left[i].transform.parent = transform;
            trafficLights_left[i].transform.localPosition = new Vector3((-distBetween*noOfLights/3)+i * distBetween, lightScale / 2, -rightLeftOffset / 2);
            trafficLights_left[i].transform.localScale = new Vector3(lightScale, lightScale, lightScale);

            trafficLights_right[i] = Instantiate(theCube);
            trafficLights_right[i].name = "RightLight" + i;
            trafficLights_right[i].transform.parent = transform;
            trafficLights_right[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, lightScale / 2, rightLeftOffset / 2);
            trafficLights_right[i].transform.localScale = new Vector3(lightScale, lightScale, lightScale);
        }
    }

    void resetColor() {
        distanceMoved = (-distBetween * noOfLights / 3) -distToLine;
    }
	
	// Update is called once per frame
    void Update() {
        distanceMoved = distanceMoved + Time.deltaTime * speedToDriveAt;
        for (int i = 0; i < noOfLights; i++) {
            float scaleFactor = maxScale - (maxScale-1)*Mathf.Clamp01(Mathf.Abs(trafficLights_left[i].transform.localPosition.x - distanceMoved) / distToLine);
            float greenScale = (scaleFactor - 1) / (maxScale - 1);
            Color newColor = greenScale * greenColor + (1 - greenScale) * redColor;
            newColor.a = greenScale+0.5f;
            trafficLights_left[i].GetComponent<Renderer>().material.SetColor("_Color", newColor);
            trafficLights_left[i].transform.localPosition = new Vector3(trafficLights_left[i].transform.localPosition.x, lightScale * scaleFactor / 2, trafficLights_left[i].transform.localPosition.z);
            trafficLights_left[i].transform.localScale = new Vector3(lightScale * scaleFactor, lightScale * scaleFactor, lightScale * scaleFactor);

            trafficLights_right[i].GetComponent<Renderer>().material.SetColor("_Color", newColor);
            trafficLights_right[i].transform.localPosition = new Vector3(trafficLights_right[i].transform.localPosition.x, lightScale * scaleFactor / 2, trafficLights_right[i].transform.localPosition.z);
            trafficLights_right[i].transform.localScale = new Vector3(lightScale * scaleFactor, lightScale * scaleFactor, lightScale * scaleFactor);   
        }

        //Reached the end of the Lights
        if (distanceMoved > trafficLights_left[trafficLights_left.Length-1].transform.position.x + distToLine) {
            resetColor();
            return;
        }
    }

    public void setLightScale(float newScale) {
        lightScale = newScale;
    }

    public void setmaxScale(float newScale) {
        maxScale = newScale;
    }

    public void setDistToLine(float newDist) {
        distToLine = newDist;
    }
    public void setDistanceBetween(float newDist) {
        distBetween = newDist;
        for (int i = 0; i < noOfLights; i++) {
            trafficLights_left[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, trafficLights_left[i].transform.localPosition.y, trafficLights_left[i].transform.localPosition.z);
            trafficLights_right[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, trafficLights_right[i].transform.localPosition.y, trafficLights_right[i].transform.localPosition.z);
        }
    }
    public void setSpeed(float newSpeed) {
        speedToDriveAt = newSpeed;
    }

    public void setOffset(float newOffset) {
        rightLeftOffset = newOffset;
        for (int i = 0; i < noOfLights; i++) {
            trafficLights_left[i].transform.localPosition = new Vector3(trafficLights_left[i].transform.localPosition.x, trafficLights_left[i].transform.localPosition.y, -rightLeftOffset / 2);
            trafficLights_right[i].transform.localPosition = new Vector3(trafficLights_left[i].transform.localPosition.x, trafficLights_right[i].transform.localPosition.y, rightLeftOffset / 2);
        }
    }

    public void setNoOfLights(float newNumber) {
        for (int i = 0; i < noOfLights; i++) {
            Destroy(trafficLights_left[i]);
            Destroy(trafficLights_right[i]);
        }
        noOfLights = (int)newNumber;
        trafficLights_left = new GameObject[noOfLights];
        trafficLights_right = new GameObject[noOfLights];
        createCubes();
    }
}
