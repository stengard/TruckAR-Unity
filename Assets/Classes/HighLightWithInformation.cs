using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighlightWithInformation {

    private Vector3 scale;
    private Vector3 position;
    private Color color;

    private Quaternion rotation;
    private GameObject parent;
    private GameObject overlayCube;

    private List<GameObject> informationObjects;



    /// <summary>
    /// Create a highlight with information
    /// </summary>
    /// <param name="p"> The parent object</param>
    public HighlightWithInformation(GameObject p) {

        parent = p;
        position = Vector3.zero;
        scale = new Vector3(200, 1, 100);
        rotation = Quaternion.identity;

        overlayCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Instantiate(overlayCube);
        overlayCube.transform.localScale = scale;
        overlayCube.transform.parent = parent.transform;

        overlayCube.GetComponent<MeshRenderer>().material.SetColor("_color", Color.white);

    }

    public void instantiateOverlay() {

        //GameObject.Instantiate(overlayCube, position, rotation);  
    }

    public void setColor(Color c){
        MeshRenderer mr = overlayCube.GetComponent<MeshRenderer>();
        mr.material.SetColor("_Color", c);
        color = c;
    }

    public void addInformationObject(GameObject info) {
        //informationObjects.Add(info);
        //info.transform.parent = parent.transform;


        //foreach (GameObject g in informationObjects) {
        //    Debugga.Logga("hej " + g.name);
        //    //GameObject.Instantiate(g);
        //    g.transform.parent = parent.transform;
            
        //}

    }

    public Transform getParent(){
        return parent.transform;
    }



    public void setScale(Vector3 s) {
        overlayCube.transform.localScale = s;
        scale = s;
    }

    public void setPosition(Vector3 p) {
        overlayCube.transform.position = p;
        position = p;
    }

    public Vector3 getPosition() {
        return position;
    }

}
