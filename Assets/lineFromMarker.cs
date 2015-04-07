using UnityEngine;
using System.Collections;

public class lineFromMarker : MonoBehaviour {
    LineRenderer line;
    public Material lineMaterial;
    public Color lineColor = Color.white;
	// Use this for initialization
	void Start () {
        line = gameObject.AddComponent<LineRenderer>();
        line.material = lineMaterial;
        line.SetColors(lineColor, lineColor);
        line.SetWidth(1.0f, 1.0f);
        line.SetVertexCount(2);
    }
	
	// Update is called once per frame
	void Update () {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.parent.position);
	}
}
