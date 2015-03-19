using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    private bool loading = true;
    public Texture loadingTexture;

    void Start()
    {
    }

    void Awake () {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Application.isLoadingLevel){
            loading = true;
        }
        else
            loading = false;
    }

    void OnGUI()
    {
        if (loading)
        {
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loadingTexture, ScaleMode.ScaleAndCrop, false, 0.0f);
            }
        }
    }

    public void loadScene(string scene)
    {
        Debug.Log("Loading scene " + scene);
        Application.LoadLevelAsync(scene);
    }

}
