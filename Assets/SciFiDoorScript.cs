using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiDoorScript : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void OpenDoor()
    {
        anim.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        anim.SetBool("Open", false);
    }
}
