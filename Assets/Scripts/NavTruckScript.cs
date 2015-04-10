using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class NavTruckScript : MonoBehaviour {
    Seeker seeker;
    LineRenderer line;
    Vector3 target;
    Vector3 position;
    public GameObject dirOriginal;
    public GameObject circleOriginal;
    public List<GameObject> clonedDirs;
    public Path path;
    //The AI's speed per second
    public float speed = 1000;
    private CharacterController controller;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 50;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        line = GetComponent<LineRenderer>();
        controller = GetComponent<CharacterController>();
        position = transform.position;
        position.y = 0;
        //seeker.StartPath(position, target.position, onPathComplete);
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 25000))
            {
                position = transform.position;
                position.y = 0;
                target = hit.point;
                seeker.StartPath(position, target, onPathComplete);
            }
            else
            {
                Debugga.Logga("Ray missed..");
            }
        }

	}

    void onPathComplete(Path p)
    {
        Debugga.Logga("Path aquired, amount:" + p.vectorPath.Count);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
            for (int i = 0; i < clonedDirs.Count; i++)
            {
                Destroy(clonedDirs[i]);
            }
            clonedDirs.Clear();
            for (int i = 0; i < path.vectorPath.Count; i++)
            {
                Vector3 pos = path.vectorPath[i];
                pos.y = 0.1f;
                
                if ((i + 1) < path.vectorPath.Count)
                {
                    clonedDirs.Insert(i, Instantiate(dirOriginal));
                    clonedDirs[i].transform.position = pos;
                    clonedDirs[i].transform.LookAt(path.vectorPath[i + 1]);
                }
                else
                {
                    clonedDirs.Insert(i, Instantiate(circleOriginal));
                    clonedDirs[i].transform.position = pos;
                }
                clonedDirs[i].transform.Rotate(90, -90, 0);
                clonedDirs[i].transform.position.Set(clonedDirs[i].transform.position.x, 20f, clonedDirs[i].transform.position.z);
                clonedDirs[i].SetActive(true);
            }
        }
    }

    public void FixedUpdate()
    {
        if (path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            return;
        }

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove(dir);
        position = transform.position;
        position.y = 0;

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            Destroy(clonedDirs[currentWaypoint]);
            currentWaypoint++;
            return;
        }
       
    }

}
