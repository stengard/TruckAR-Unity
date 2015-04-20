using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;
using System.Collections.Generic;
public class OverlayW : MonoBehaviour {


    public GameObject overlay;

    public int numberOfOverlays;
    public float damping;

    private int numberOfColumns;
    private int numberOfRows;

    private List<GameObject> overlayObjects, originalOverlayObjects;

    float canvasWidth;
    float canvasHeight;
    public bool useCircle;

	// Use this for initialization
    void Start() {

        overlayObjects = new List<GameObject>();

        for (int i = 0; i < numberOfOverlays; i++) {

            overlayObjects.Add(Instantiate(overlay));
            overlayObjects[i].transform.parent = transform;
            overlayObjects[i].transform.position = transform.position;
            overlayObjects[i].name = overlay.name + "_" + i;
        }

        originalOverlayObjects = overlayObjects;

        numberOfColumns = Mathf.RoundToInt(Mathf.Sqrt(overlayObjects.Count));
        numberOfRows = Mathf.RoundToInt(Mathf.Ceil(overlayObjects.Count / numberOfColumns));

        canvasWidth = overlayObjects[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        canvasHeight = overlayObjects[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.O)) {
            useCircle = !useCircle;
        }

        if (useCircle) {
            PlaceInCircle();
        }
        else { 
            PlaceInGrid();
        }
        

       
    }

    private void PlaceInGrid() {

        Vector3 offsetVector = transform.position;

        if (numberOfColumns > 1)
            offsetVector.x -= canvasWidth * (numberOfColumns-1)/4;

        if (numberOfRows > 1) {
            offsetVector.y += canvasHeight * (numberOfRows-1)/4;
        }

 
        int counter = -1;
        for (int i = 0; i < overlayObjects.Count; i++) {

            if (i % numberOfColumns == 0)
                counter++;

            canvasWidth = overlayObjects[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            canvasHeight = overlayObjects[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;

            float x = 0;
            float y = 0;

            x = offsetVector.x + (canvasWidth/2 + 15) * (i % numberOfColumns);
            y = offsetVector.y - (canvasHeight/2 + 15) * counter;
            

            float tempX = Mathf.Lerp(overlayObjects[i].transform.position.x, x, Time.deltaTime * damping);

            float tempY = Mathf.Lerp(overlayObjects[i].transform.position.y, y, Time.deltaTime * damping);

            overlayObjects[i].transform.position = new Vector3(tempX, tempY, transform.position.z);
        }
    }

    private void PlaceInCircle() {

        for (int i = 0; i < overlayObjects.Count; i++) {

            float canvasWidth = overlayObjects[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            float canvasHeight = overlayObjects[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;

            float theta = (2 * Mathf.PI / overlayObjects.Count) * i;

            float x = 0;
            float y = 0;

            x = (canvasWidth / 2) * Mathf.Cos(theta) + transform.position.x;
            y = (canvasHeight / 2) * Mathf.Sin(theta) + transform.position.y;

            float tempX = Mathf.Lerp(overlayObjects[i].transform.position.x, x, Time.deltaTime * damping);

            float tempY = Mathf.Lerp(overlayObjects[i].transform.position.y, y, Time.deltaTime * damping);

            overlayObjects[i].transform.position = new Vector3(tempX, tempY, transform.position.z);
        }
    }
}
