using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class NavTruckScript : MonoBehaviour {
    Seeker seeker;
    public GameObject targetTransform;
    Vector3 target;
    Vector3 position;
    public GameObject dirOriginal;
    public GameObject circleOriginal;
    public List<GameObject> clonedDirs;
    public Path path;
    public GameObject theParent;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 50;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    // Use this for initialization
    void Start() {
        seeker = GetComponent<Seeker>();
        if (GetComponent<Camera>()) {
            Debugga.Logga("Kamera");
            position = GetComponent<Camera>().transform.position;
        }
        else
            position = transform.position;
        position.y = 0;

        target = targetTransform.transform.position;
        target.y = 0;
        updatePath();
    }


    // Update is called once per frame
    void Update() {
    }

    public void updatePath() {
        seeker.StartPath(position, target, onPathComplete);
    }

    void onPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;

            for (int i = 0; i < clonedDirs.Count; i++) {
                Destroy(clonedDirs[i]);
            }
            clonedDirs.Clear();
            for (int i = 0; i < path.vectorPath.Count; i++) {
                Vector3 pos = path.vectorPath[i];
                pos.y = 0.1f;

                if ((i + 1) < path.vectorPath.Count) {
                    clonedDirs.Insert(i, Instantiate(dirOriginal));
                    clonedDirs[i].transform.position = pos;
                    clonedDirs[i].transform.LookAt(path.vectorPath[i + 1]);
                }
                else {
                    clonedDirs.Insert(i, Instantiate(circleOriginal));
                    clonedDirs[i].transform.position = pos;
                }
                clonedDirs[i].transform.Rotate(90, -90, 0);
                clonedDirs[i].transform.position.Set(clonedDirs[i].transform.position.x, 20f, clonedDirs[i].transform.position.z);
                clonedDirs[i].transform.parent = theParent.transform;
                clonedDirs[i].SetActive(true);
            }
        }
    }

    public void FixedUpdate() {
        if (path == null) {
            //We have no path to move after yet
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
