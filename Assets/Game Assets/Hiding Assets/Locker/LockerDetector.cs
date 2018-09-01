using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDetector : MonoBehaviour {

    LockerController lc;
    ActionTextControl atc;

    bool playerInLocker = false;

	// Use this for initialization
	void Start () {
        lc = GetComponentInParent<LockerController>();
        atc = GameObject.FindObjectOfType<ActionTextControl>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerC>())
        {
            if (!playerInLocker)
                atc.SetActionText("Press [F] to enter locker");
            Debug.Log("Player enter");
            lc.playerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerC>())
        {
            atc.SetActionText("");
            Debug.Log("Player Exit");
            lc.playerIn = false;
        }
    }

    public void SetPlayerInLocker(bool val)
    {
        playerInLocker = val;
        atc.SetActionText("");
    }
}
