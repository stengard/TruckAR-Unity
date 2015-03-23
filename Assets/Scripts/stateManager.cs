using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using metaio;

public class stateManager : MonoBehaviour {
    private string currentState;
    public GameObject HUDObject;
    public GameObject GAZEObject;
    public GameObject MARKERObject;
    public GameObject HUD;
    private GameObject[] allActiveObjs;
	// Use this for initialization
	void Start () {
        changeState("STATE_HUD");
	}
	
	// Update is called once per frame
    void Update()
    {
        if ((Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Menu)) || Input.GetKeyDown("up"))
        {
            HUD.SetActive(!HUD.activeInHierarchy);

        }
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
                Debugga.Logga("Gaze initiated");
                break;
            case "STATE_MARKER":
                changeInit(MARKERObject);
                Debugga.Logga("Marker Initiated");
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
        Debugga.Logga("l = " + allActiveObjs.Length);
        Instantiate(obj);
    }

}
