using UnityEngine;
using System.Collections;
using Pathfinding;

public class pathFinderHelperScript : MonoBehaviour {
    AstarData data;
    Vector3 initPos;
	// Use this for initialization
	void Start () {
        data = GetComponent<AstarPath>().astarData;
        initPos = data.gridGraph.center;
    }
	
	// Update is called once per frame
	void Update () {
        data.gridGraph.rotation = transform.parent.parent.rotation.eulerAngles;
        data.gridGraph.center = transform.parent.parent.position + initPos;
        data.active.Scan();
        Debugga.Logga("rota:"+data.gridGraph.rotation);
    }
}
