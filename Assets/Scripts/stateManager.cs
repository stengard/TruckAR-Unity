using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class stateManager : MonoBehaviour {
    private string currentState;
    public List<GameObject> HUDObject = new List<GameObject>();
    public List<GameObject> GAZEObject = new List<GameObject>();
    public List<GameObject> MARKERObject = new List<GameObject>();
    public List<GameObject> OVERLAYObject = new List<GameObject>();
    public List<GameObject> SAFEZONEObject = new List<GameObject>();
    public List<GameObject> TUNNELObject = new List<GameObject>();
    public List<GameObject> PREDICTIVEObject = new List<GameObject>();
    public List<GameObject> MULTIPLEObject = new List<GameObject>();
    public List<GameObject> HOLOGRAMTRUCKObject = new List<GameObject>();
    public List<GameObject> XRAYTOOLTIPObject = new List<GameObject>();
    public List<GameObject> XRAYObject = new List<GameObject>();
    public List<GameObject> ENVIRONMENTALObject = new List<GameObject>();

    public List<GameObject> HANSObject = new List<GameObject>();
    public List<GameObject> PALLETINFOObject = new List<GameObject>();
    public List<GameObject> PALLETXTNDObject = new List<GameObject>();
    public List<GameObject> HOLOGRAMMAPObject = new List<GameObject>();
    public List<GameObject> AMSTERDAMObject = new List<GameObject>();


    public List<GameObject> HUD;
    public string startState;
    private GameObject[] allActiveObjs;
    private bool isActive;
    public string taggen;
	// Use this for initialization
	void Start () {
        isActive = HUD[0].activeInHierarchy;
        changeState(startState);
	}
	
	// Update is called once per frame
    void Update()
    {
        if ((Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Menu)) || Input.GetKeyDown("up"))
        {
            isActive = !isActive;
            for (int i = 0; i < HUD.Count; i++) {
                Canvas[] canvasComponents = HUD[i].GetComponentsInChildren<Canvas>();
                foreach (Canvas component in canvasComponents) {
                    component.enabled = isActive;
                }
            }
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
            case "STATE_OVERLAY":
                changeInit(OVERLAYObject);
                break;
            case "STATE_SAFE_ZONE":
                changeInit(SAFEZONEObject);
                break;
            case "STATE_TUNNEL":
                changeInit(TUNNELObject);
                break;
            case "STATE_MULTIPLE":
                changeInit(MULTIPLEObject);
                break;
            case "STATE_PREDICTIVE":
                changeInit(PREDICTIVEObject);
                break;
            case "STATE_HANS":
                changeInit(HANSObject);
                break;
            case "STATE_PALLET_INFO":
                changeInit(PALLETINFOObject);
                break;
            case "STATE_PALLET_EXTENDED":
                changeInit(PALLETXTNDObject);
                break;
            case "STATE_HOLOGRAM_MAP":
                changeInit(HOLOGRAMMAPObject);
                break;
            case "STATE_AMSTERDAM":
                changeInit(AMSTERDAMObject);
                break;
            case "STATE_HOLOGRAM_TRUCK":
                changeInit(HOLOGRAMTRUCKObject);
                break;
            case "STATE_XRAY_TOOLTIP":
                changeInit(XRAYTOOLTIPObject);
                break;
            case "STATE_XRAY":
                changeInit(XRAYObject);
                break;
            case "STATE_ENVIRONMENTAL_OVERLAY":
                changeInit(ENVIRONMENTALObject);
                break;
            default:
                break;
        }
    }

    void changeInit(List<GameObject> obj)
    {
        allActiveObjs = GameObject.FindGameObjectsWithTag(taggen);
        for(int nObjs=0;nObjs<allActiveObjs.Length;nObjs++){
            allActiveObjs[nObjs].gameObject.SetActive(false);  
        }

        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);
        }
    }

}
