using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class HologramMapHelper : MonoBehaviour {
    Seeker seeker;
    public GameObject theTarget;
    Vector3 targetPos;
    public Vector3 position;
    public Path path;
    int currentWaypoint;
    public float nextWaypointDistance = 50;
    public GameObject dirOriginal;
    public GameObject circleOriginal;
    public List<GameObject> clonedDirs;
	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        targetPos = theTarget.transform.position;
        //position = transform.localPosition;
        position.y = 0;
        targetPos.y = 0;
        updatePath();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void updatePath() {
        Debugga.Logga("target"+targetPos);
        Debugga.Logga("pos:" + transform.position);
        Debugga.Logga("localpos:" + position);
        Debugga.Logga("parentpos:" + transform.parent.position);
        Debugga.Logga("parentlocalpos:" + transform.parent.localPosition);
        seeker.StartPath(position, targetPos, completed);
    }

    void completed(Path p) {
        Debugga.Logga("PATH searched atleast?");
        if (!p.error) {
            path = p;
            currentWaypoint = 0;

            for (int i = 0; i < clonedDirs.Count; i++) {
                Destroy(clonedDirs[i]);
            }
            clonedDirs.Clear();
            for (int i = 0; i < path.vectorPath.Count; i++) {
                Vector3 pos = path.vectorPath[i];
                pos.y = -999.0f;

                if ((i + 1) < path.vectorPath.Count) {
                    clonedDirs.Insert(i, Instantiate(dirOriginal));
                    clonedDirs[i].transform.parent = transform;
                    clonedDirs[i].transform.localPosition = new Vector3((pos.x - 5599)/ transform.localScale.x, pos.y / transform.localScale.x, pos.z / transform.localScale.x);
                    clonedDirs[i].transform.LookAt(path.vectorPath[i + 1]);
                }
                else {
                    clonedDirs.Insert(i, Instantiate(circleOriginal));
                    clonedDirs[i].transform.parent = transform;
                    clonedDirs[i].transform.localPosition = new Vector3((pos.x - 5599) / transform.localScale.x, pos.y / transform.localScale.x, pos.z / transform.localScale.x);
                }
                Debugga.Logga("clonedpos:" + clonedDirs[0].transform.position);
                Debugga.Logga("clonedLocalpos:" + clonedDirs[0].transform.localPosition);
                clonedDirs[i].transform.Rotate(90, -90, 0);
                clonedDirs[i].SetActive(true);
            }
        }
    }
    public void FixedUpdate() {
        if (path == null) {
            //We have no path to move after ye
            return;
        }
                if (currentWaypoint >= path.vectorPath.Count) {
            //Reached the end of the path
            return;
        }

        //Direction to the next waypoint
        position = transform.position;
        position.y = 0;

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        float dist = Vector3.Distance(position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance) {
            Destroy(clonedDirs[currentWaypoint]);
            currentWaypoint++;
            return;
        }

        //If we are have taken another route around the first waypoint.
        if (path.vectorPath.Count > currentWaypoint + 1)
            if (dist > Vector3.Distance(position, path.vectorPath[currentWaypoint + 1])) {
                if(seeker.IsDone())
                    updatePath();
            }
    }

}
