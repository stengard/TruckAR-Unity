using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;

public class stateManager : MonoBehaviour {
    private string currentState;
    public GameObject HUDObject;
    public GameObject GAZEObject;
    public GameObject MARKERObject;
    private GameObject[] allActiveObjs;
	// Use this for initialization
	void Start () {
        changeState("STATE_HUD");
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void changeState(string state)
    {
        currentState = state;
        switch (currentState)
        {
            case "STATE_HUD":
                changeInit(HUDObject);
                break;
            case "STATE_GAZE":
                changeInit(GAZEObject);
                Debug.Log("Gaze initiated");
                break;
            case "STATE_MARKER":
                changeInit(MARKERObject);
                Debug.Log("Marker Initiated");
                break;
            default:
                break;
        }
    }

    void changeInit(GameObject obj){
        allActiveObjs = GameObject.FindGameObjectsWithTag("Displays");
        for(int nObjs=0;nObjs<allActiveObjs.Length;nObjs++){
            Destroy(allActiveObjs[nObjs].gameObject);  
        }
        Debug.Log("l = " + allActiveObjs.Length);
        Instantiate(obj);
    }

}
