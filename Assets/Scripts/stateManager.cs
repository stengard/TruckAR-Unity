using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using metaio;

public class stateManager : MonoBehaviour {
    private string currentState;
    public List<GameObject> HUDObject = new List<GameObject>();
    public List<GameObject> GAZEObject = new List<GameObject>();
    public List<GameObject> MARKERObject = new List<GameObject>();
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
                break;
            case "STATE_MARKER":
                changeInit(MARKERObject);
                break;
            default:
                break;
        }
    }

    void changeInit(List<GameObject> obj)
    {
        allActiveObjs = GameObject.FindGameObjectsWithTag("Displays");
        for(int nObjs=0;nObjs<allActiveObjs.Length;nObjs++){
            allActiveObjs[nObjs].gameObject.SetActive(false);  
        }

        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);
        }
    }

}
