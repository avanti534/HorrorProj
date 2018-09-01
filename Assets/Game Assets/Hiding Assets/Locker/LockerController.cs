using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerController : MonoBehaviour {

    Animator anim;
    public Transform startPos;
    public Transform lookPos;

    public bool playerIn = false;

    public LockerDetector ld;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        startPos = transform.GetChild(0);
        lookPos = transform.GetChild(1);
        ld = GetComponentInChildren<LockerDetector>();
	}

    public void LockerPlayAnim()
    {
        anim.SetTrigger("Play");

        
    }
}
