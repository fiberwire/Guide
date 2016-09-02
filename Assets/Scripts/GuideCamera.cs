using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class GuideCamera : MonoBehaviour {

    public float leftMargin, rightMargin, topMargin, bottomMargin;

    public float zoomSpeed;
    public float zoomRatio;

    public float left { get { return Camera.main.ViewportToWorldPoint(new Vector2(0 + leftMargin, 0)).x; } }
    public float right { get { return Camera.main.ViewportToWorldPoint(new Vector2(1 - rightMargin, 0)).x; } }
    public float top { get { return Camera.main.ViewportToWorldPoint(new Vector2(0, 1 - topMargin)).y; } }
    public float bottom { get { return Camera.main.ViewportToWorldPoint(new Vector2(0, 0 + bottomMargin)).y; } }

    public float averageOrganismSize {
        get {
            List<GameObject> orgs = (from o in GameObject.FindGameObjectsWithTag("Organism") select o).ToList();
            return (from o in orgs select o.transform.localScale).ToList().Average();
        }
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(autozoom());
    }

    IEnumerator autozoom() {
        while (true) {
            float size = averageOrganismSize;
            yield return null;

            //zoom camera based on average organism size
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, size * zoomRatio, Time.deltaTime);
            yield return null;
        }
    }
}
