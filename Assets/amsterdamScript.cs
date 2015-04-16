using UnityEngine;
using System.Collections;
using metaio;

public class amsterdamScript : MonoBehaviour {

    public float distBetween = 1000.0f;
    public float rightLeftOffset = 3000.0f;
    public int noOfLights = 10;
    public float lightScale = 350.0f;
    public float speedToDriveAt = 500.0f;

    public Color greenColor = new Color(0, 255, 0);
    public Color redColor = new Color(255, 0, 0);
    public Color yellowColor = new Color(255, 200, 0);

    private int currentLight = -1;
    private int nextLight = 0;
    private GameObject[] trafficLights_left;
    private GameObject[] trafficLights_right;
    // Use this for initialization
	void Start () {
        trafficLights_left = new GameObject[noOfLights];
        trafficLights_right = new GameObject[noOfLights];
        for (int i = 0; i < noOfLights; i++) {
            trafficLights_left[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            trafficLights_left[i].name = "LeftLight" + i;
            trafficLights_left[i].transform.parent = transform;
            trafficLights_left[i].transform.localPosition = new Vector3(i * distBetween, lightScale / 2, -rightLeftOffset / 2);
            trafficLights_left[i].transform.localScale = new Vector3(lightScale, lightScale, lightScale);

            trafficLights_right[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            trafficLights_right[i].name = "RightLight" + i;
            trafficLights_right[i].transform.parent = transform;
            trafficLights_right[i].transform.localPosition = new Vector3(i * distBetween, lightScale / 2, rightLeftOffset / 2);
            trafficLights_right[i].transform.localScale = new Vector3(lightScale, lightScale, lightScale);

            if (i < currentLight) {
                trafficLights_left[i].GetComponent<Renderer>().material.color = greenColor;
                trafficLights_right[i].GetComponent<Renderer>().material.color = greenColor;
            }
            else if (i == nextLight) {
                trafficLights_left[i].GetComponent<Renderer>().material.color = yellowColor;
                trafficLights_right[i].GetComponent<Renderer>().material.color = yellowColor;
            }
            else {
                trafficLights_left[i].GetComponent<Renderer>().material.color = redColor;
                trafficLights_right[i].GetComponent<Renderer>().material.color = redColor;
            }

        }
        InvokeRepeating("lightFunc", 0.0f, (float)(distBetween/speedToDriveAt));
	}

    void resetColor() {
        currentLight = -1;
        nextLight = 0;
        for (int i = 0; i < noOfLights; i++) {
            if (i < currentLight) {
                trafficLights_left[i].GetComponent<Renderer>().material.color = greenColor;
                trafficLights_right[i].GetComponent<Renderer>().material.color = greenColor;
            }
            else if (i == nextLight) {
                trafficLights_left[i].GetComponent<Renderer>().material.color = yellowColor;
                trafficLights_right[i].GetComponent<Renderer>().material.color = yellowColor;
            }
            else {
                trafficLights_left[i].GetComponent<Renderer>().material.color = redColor;
                trafficLights_right[i].GetComponent<Renderer>().material.color = redColor;
            }
        }
    }
	
	// Update is called once per frame
    void Update() {
    }

    void lightFunc() {
        nextLight++;
        currentLight++;
        if (currentLight < noOfLights) {
            if (currentLight > 0) {
                trafficLights_left[currentLight-1].GetComponent<Renderer>().material.color = greenColor;
                trafficLights_right[currentLight-1].GetComponent<Renderer>().material.color = greenColor;
            }
        }
        if (nextLight < noOfLights) {
            trafficLights_left[nextLight].GetComponent<Renderer>().material.color = yellowColor;
            trafficLights_right[nextLight].GetComponent<Renderer>().material.color = yellowColor;
            if (nextLight > 0) {
                trafficLights_left[nextLight-1].GetComponent<Renderer>().material.color = yellowColor;
                trafficLights_right[nextLight-1].GetComponent<Renderer>().material.color = yellowColor;
            }
        }
    }

	void FixedUpdate() {
        //Reached the end of the Lights
        if (currentLight >= noOfLights) {
            resetColor();
            return;
        }
    }
}
