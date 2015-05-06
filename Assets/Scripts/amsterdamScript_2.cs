using UnityEngine;

public class amsterdamScript_2 : MonoBehaviour {

    public float distBetween = 1000.0f;
    public int noOfLights = 10;
    public float lightScale = 350.0f;
    public float speedToDriveAt = 500.0f;
    public float distToLine = 1000;
    public float maxScale = 4.0f;

    public Color greenColor = new Color(0, 255, 0, 1);
    public Color redColor = new Color(255, 0, 0, 1);

    private GameObject[] trafficLights_go;
    private GameObject[] trafficLights_stop;
    private float distanceMoved;
    public GameObject greenLight;
    public GameObject redLight;
    // Use this for initialization
    void Start() {
        distanceMoved = (-distBetween * noOfLights / 3) - distToLine;
        trafficLights_go = new GameObject[noOfLights];
        trafficLights_stop = new GameObject[noOfLights];
        createCubes();
    }

    void createCubes() {
        for (int i = 0; i < noOfLights; i++) {
            trafficLights_go[i] = Instantiate(greenLight);
            trafficLights_go[i].name = "GreenLight" + i;
            trafficLights_go[i].transform.parent = transform;
            trafficLights_go[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, lightScale / 2, 0.0f);
            trafficLights_go[i].transform.localScale = new Vector3(lightScale, lightScale, lightScale);

            trafficLights_stop[i] = Instantiate(redLight);
            trafficLights_stop[i].name = "RedLight" + i;
            trafficLights_stop[i].transform.parent = transform;
            trafficLights_stop[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, lightScale / 2, 0.0f);
            trafficLights_stop[i].transform.localScale = new Vector3(lightScale, lightScale, lightScale);
        }
    }

    void resetColor() {
        distanceMoved = (-distBetween * noOfLights / 3) - distToLine;
    }

    // Update is called once per frame
    void Update() {
        distanceMoved = distanceMoved + Time.deltaTime * speedToDriveAt;
        for (int i = 0; i < noOfLights; i++) {
            float scaleFactor = maxScale - (maxScale - 1) * Mathf.Clamp01(Mathf.Abs(trafficLights_go[i].transform.localPosition.x - distanceMoved) / distToLine);
            if (i == 1)
                Debugga.Logga("Scalefactor:" + scaleFactor);
            //Sätt A - Röda förhåller sig till distanceMoved
            float greenScale = 1 - (scaleFactor - 1) / (maxScale - 1);
            //Sätt B - Gröna förhåller sig till distanceMoved
            //float greenScale = (scaleFactor - 1) / (maxScale - 1);
            Color newColor = greenScale * greenColor + (1 - greenScale) * redColor;
            trafficLights_go[i].GetComponent<Renderer>().material.SetColor("_Color", newColor);
            trafficLights_go[i].transform.localPosition = new Vector3(trafficLights_go[i].transform.localPosition.x, trafficLights_go[i].transform.localPosition.y, trafficLights_go[i].transform.localPosition.z);
            //A - För att få gröna stora / B för att få röda stora:
            trafficLights_go[i].transform.localScale = new Vector3(lightScale * (maxScale - (scaleFactor - 1)), lightScale * (maxScale - (scaleFactor - 1)), lightScale * (maxScale - (scaleFactor - 1)));
            //B - För att få gröna stora / A för att få röda stora:
            //trafficLights_go[i].transform.localScale = new Vector3(lightScale * scaleFactor, lightScale * scaleFactor, lightScale * scaleFactor); 

            Color stopColor = new Color(1.0f, 1.0f, 1.0f, 1.0f - (2 * greenScale));
            trafficLights_stop[i].GetComponent<Renderer>().material.SetColor("_Color", stopColor);
            trafficLights_stop[i].transform.localPosition = new Vector3(trafficLights_stop[i].transform.localPosition.x, trafficLights_stop[i].transform.localPosition.y, trafficLights_stop[i].transform.localPosition.z);
            //A - som innan
            trafficLights_stop[i].transform.localScale = new Vector3(lightScale * (maxScale - (scaleFactor - 1)), lightScale * (maxScale - (scaleFactor - 1)), lightScale * (maxScale - (scaleFactor - 1)));            
            //B - som innan
            //trafficLights_stop[i].transform.localScale = new Vector3(lightScale * scaleFactor, lightScale * scaleFactor, lightScale * scaleFactor);   
        }

        //Reached the end of the Lights
        if (distanceMoved > trafficLights_go[trafficLights_go.Length - 1].transform.position.x + distToLine) {
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
            trafficLights_go[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, trafficLights_go[i].transform.localPosition.y, trafficLights_go[i].transform.localPosition.z);
            trafficLights_stop[i].transform.localPosition = new Vector3((-distBetween * noOfLights / 3) + i * distBetween, trafficLights_stop[i].transform.localPosition.y, trafficLights_stop[i].transform.localPosition.z);
        }
    }
    public void setSpeed(float newSpeed) {
        speedToDriveAt = newSpeed;
    }

    public void setNoOfLights(float newNumber) {
        for (int i = 0; i < noOfLights; i++) {
            Destroy(trafficLights_go[i]);
            Destroy(trafficLights_stop[i]);
        }
        noOfLights = (int)newNumber;
        trafficLights_go = new GameObject[noOfLights];
        trafficLights_stop = new GameObject[noOfLights];
        createCubes();
    }
}
