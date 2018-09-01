using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedDetector : MonoBehaviour {

    BedController bc;
    ActionTextControl atc;

    bool playerInBed = false;

    // Use this for initialization
    void Start()
    {
        bc = GetComponentInParent<BedController>();
        atc = GameObject.FindObjectOfType<ActionTextControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerC>())
        {
            if (!playerInBed)
                atc.SetActionText("Press [F] to enter bed");
            Debug.Log("Player enter");
            bc.playerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerC>())
        {
            atc.SetActionText("");
            Debug.Log("Player Exit");
            bc.playerIn = false;
        }
    }

    public void SetPlayerInBed(bool val)
    {
        playerInBed = val;
        atc.SetActionText("");
    }
}
