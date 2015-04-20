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
    public float spread;

    private List<GameObject> overlayObjects, originalOverlayObjects;

    float canvasWidth;
    float canvasHeight;
    public bool useCircle;

	// Use this for initialization
    void Start() {

        overlayObjects = new List<GameObject>();

        instantiateOverlays(numberOfOverlays);

        originalOverlayObjects = new List<GameObject>();

        for (int i = 0; i < overlayObjects.Count; i++) {

            originalOverlayObjects.Add(overlayObjects[i]);
        }

        numberOfColumns = Mathf.RoundToInt(Mathf.Sqrt(overlayObjects.Count));
        numberOfRows = Mathf.RoundToInt(Mathf.Ceil(overlayObjects.Count / numberOfColumns));

        canvasWidth = overlayObjects[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        canvasHeight = overlayObjects[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

        useCircle = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (useCircle) {
            PlaceOnCircle();
        }
        else { 
            PlaceOnGrid();
        }

        if (GetComponent<ChangeSizeOnDistance>().enabled) {

        }
    }

    public void reInstantiateOvelays(float n) {
        n = Mathf.RoundToInt(n);

        for (int i = 0; i < overlayObjects.Count; i++) {
            Destroy(overlayObjects[i]);
        }

        overlayObjects.Clear();

        instantiateOverlays(Mathf.RoundToInt(n));
        
    }

    private void instantiateOverlays(int n) {

        for (int i = 0; i < n; i++) {

            overlayObjects.Add(Instantiate(overlay));
            overlayObjects[i].transform.parent = transform;
            overlayObjects[i].transform.position = transform.position;
            overlayObjects[i].transform.rotation = transform.rotation;
            overlayObjects[i].name = overlay.name + "_" + i;
        }

        if (useCircle) {

            PlaceOnCircle();
        }
        else {

            PlaceOnGrid();
        }
    }

    private void PlaceOnGrid() {

        numberOfColumns = Mathf.RoundToInt(Mathf.Sqrt(overlayObjects.Count));
        numberOfRows = Mathf.RoundToInt(Mathf.Ceil(overlayObjects.Count / numberOfColumns));

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

            x = offsetVector.x + (canvasWidth / 2 + spread) * (i % numberOfColumns) * overlayObjects[i].transform.localScale.x * transform.localScale.x;
            y = offsetVector.y - (canvasHeight / 2 + spread) * counter * overlayObjects[i].transform.localScale.x * transform.localScale.y;
            

            float tempX = Mathf.Lerp(overlayObjects[i].transform.position.x, x, Time.deltaTime * damping);

            float tempY = Mathf.Lerp(overlayObjects[i].transform.position.y, y, Time.deltaTime * damping);

            overlayObjects[i].transform.position = new Vector3(tempX, tempY, transform.position.z);

        }
    }

    private void PlaceOnCircle() {

        numberOfColumns = Mathf.RoundToInt(Mathf.Sqrt(overlayObjects.Count));
        numberOfRows = Mathf.RoundToInt(Mathf.Ceil(overlayObjects.Count / numberOfColumns));

        for (int i = 0; i < overlayObjects.Count; i++) {

            float canvasWidth = overlayObjects[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            float canvasHeight = overlayObjects[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;

            float theta = (2 * Mathf.PI / overlayObjects.Count) * i;

            float x = 0;
            float y = 0;

            x = overlayObjects[i].transform.localScale.x * transform.localScale.x * (canvasWidth / 2 + spread * overlayObjects.Count) * Mathf.Cos(theta) + transform.position.x;
            y = overlayObjects[i].transform.localScale.x * transform.localScale.y * (canvasHeight / 2 + spread * overlayObjects.Count) * Mathf.Sin(theta) + transform.position.y;

            float tempX = Mathf.Lerp(overlayObjects[i].transform.position.x, x, Time.deltaTime * damping);

            float tempY = Mathf.Lerp(overlayObjects[i].transform.position.y, y, Time.deltaTime * damping);

            overlayObjects[i].transform.position = new Vector3(tempX, tempY, transform.position.z);
        }
    }

    public void arrangementToggle(bool t) {
        useCircle = t;
    }

    public void billboardToggle(bool t) {
        GetComponent<Billboard>().enabled = t;

        if (!t) {
            for (int i = 0; i < numberOfOverlays; i++) {

                overlayObjects[i].transform.rotation = transform.rotation;
                overlayObjects[i].name = overlay.name + "_" + i;
            }
        }
    }

    public void dynamicSize(bool t) {
        GetComponent<ChangeSizeOnDistance>().enabled = t;
    }

    public void resizeOverlays(float s) {

        Debugga.Logga(originalOverlayObjects[0].transform.localScale + "");

        for (int i = 0; i < overlayObjects.Count; i++) {

            overlayObjects[i].transform.localScale = originalOverlayObjects[i].transform.localScale + new Vector3(s, s, s);
        }
    }
}
