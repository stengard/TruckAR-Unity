using UnityEngine;
using System.Collections;

public class NavTruckScript : MonoBehaviour {
    NavMeshAgent agent;
    LineRenderer line;
    public Transform target;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        line = GetComponent<LineRenderer>();
        StartCoroutine(getPath());
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000))
            {
                target.position = hit.point;
                StartCoroutine(getPath());
                Debugga.Logga("Agent moving..");
            }
            else
            {
                Debugga.Logga("Ray missed..");
            }
        }
        drawPath(agent.path);
        Debug.DrawLine(transform.position, target.position, Color.red);

	}

    IEnumerator getPath(){
        
        agent.SetDestination(target.position);
        while (agent.pathPending.Equals(true)) 
            yield return new WaitForFixedUpdate();
        Debugga.Logga("asd");
        drawPath(agent.path);
        agent.Stop();
    }

    void drawPath(NavMeshPath path){
        if (path.corners.Length < 2)
            return;
        line.SetPosition(0, transform.position);
        line.SetVertexCount(path.corners.Length);
        for (int i = 1; i < path.corners.Length; i++){
            
            Debugga.Logga("Loggar path nr: " + i);
            Debugga.Logga("Position corner (xyz): (" + path.corners[i].x + ", " +path.corners[i].y+", " +path.corners[i].z+")");
            line.SetPosition(i, path.corners[i]);
        }
    }
}
