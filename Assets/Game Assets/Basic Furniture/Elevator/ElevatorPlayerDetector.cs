using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlayerDetector : MonoBehaviour {

    ElevatorScript es;
    public bool playerInEnter = false;

	// Use this for initialization
	void Start () {
        es = transform.parent.GetComponent<ElevatorScript>();
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.GetComponent<PlayerC>())
        {
            if (playerInEnter)
            {
                es.SetDoorBool(false);
                es.beginMove = true;
            }
            else
            {
                Destroy(this);
                es.SetDoorBool(true);
            }
        }
    }
}
