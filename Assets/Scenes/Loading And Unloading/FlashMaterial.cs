using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshRenderer))]
public class FlashMaterial : MonoBehaviour {

    public int flashingSpeed;

    private float currentOpacity;

    MeshRenderer meshRenderer;
    // Use this for initialization
    void Start() {
        flashingSpeed = 10;
        currentOpacity = 0;

        meshRenderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update() {

        //Change opacity of the material attached to the object
        currentOpacity = Mathf.Abs(Mathf.Sin(Time.time*flashingSpeed));
        meshRenderer.material.SetColor("_Color", new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, currentOpacity));

    }
}
