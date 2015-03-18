using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {



    public void loadScene(string scene){
        Debug.Log("Loading scene " + scene);
        Application.LoadLevel(scene);
    }
}
