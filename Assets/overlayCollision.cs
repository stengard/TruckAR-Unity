using UnityEngine;
using System.Collections;

public class overlayCollision : MonoBehaviour {


    private string taggen;
	// Use this for initialization
	void Start () {
        taggen = tag;
	}

	// Update is called once per frame
	void Update () {
       //Debugga.Logga(transform.position + "");
	}

    public void OnTriggerEnter(Collider other) {
        if (other.tag == taggen) {
            //Debugga.Logga(name + " collided with " + other.name);

           // transform.position = transform.position - transform.forward*100;
        }
    }



}
