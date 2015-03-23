using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    private bool loading = true;
    private Texture2D loadingTexture;
    public Sprite loadingSprite;

    void Start()
    {
        loadingTexture = new Texture2D((int)loadingSprite.rect.width, (int)loadingSprite.rect.height);

        Color[] pixels = loadingSprite.texture.GetPixels((int)loadingSprite.textureRect.x,
                                                (int)loadingSprite.textureRect.y,
                                                (int)loadingSprite.textureRect.width,
                                                (int)loadingSprite.textureRect.height);

        loadingTexture.SetPixels(pixels);
        loadingTexture.Apply();
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
        
        DebugLog.Debugga("Loading scene " + scene);
        Application.LoadLevelAsync(scene);
    }

}
