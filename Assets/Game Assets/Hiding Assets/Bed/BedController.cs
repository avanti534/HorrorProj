using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour {

    public Transform startPos;
    public Transform lookPos;

    public bool playerIn = false;

    public BedDetector bd;

	// Use this for initialization
	void Start () {
        startPos = transform.Find("StartPos");
        lookPos = transform.Find("LookPos");
        bd = GetComponentInChildren<BedDetector>();
	}
}
