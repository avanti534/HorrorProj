using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParent : MonoBehaviour {

    // Camera view location
    [System.NonSerialized]
    public Transform viewPoint;
    // Camera look rotation
    [System.NonSerialized]
    public Transform lookPoint;

	// Use this for initialization
	void Start () {
        viewPoint = transform.Find("ViewPoint");
        lookPoint = transform.Find("LookPoint");
    }
}
