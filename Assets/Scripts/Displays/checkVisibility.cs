using UnityEngine;
using System.Collections;

public class checkVisibility : MonoBehaviour {
    bool visible = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Menu))
            {
                if(visible){
                    this.gameObject.SetActive(false);
                    visible = false;
                }
                else
                {
                    this.gameObject.SetActive(true);
                    visible = true;
                }
                return;
            }
        }
	}
}
