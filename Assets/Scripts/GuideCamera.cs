using UnityEngine;
using System.Collections;

public class GuideCamera : MonoBehaviour {

    public float leftMargin, rightMargin, topMargin, bottomMargin;

    public float left { get { return Camera.main.ViewportToWorldPoint(new Vector2(0 + leftMargin,0)).x; } }
    public float right { get { return Camera.main.ViewportToWorldPoint(new Vector2(1 - rightMargin, 0)).x; } }
    public float top { get { return Camera.main.ViewportToWorldPoint(new Vector2(0, 1 - topMargin)).y; } }
    public float bottom { get { return Camera.main.ViewportToWorldPoint(new Vector2(0, 0 + bottomMargin)).y; } }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
