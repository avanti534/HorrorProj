using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFunctionScript : MonoBehaviour {

    public UnityEvent targetEvent;
    public bool destroyAfterEvent = true;

    public bool DetectPlayer = true;
    public GameObject targetColliderGO = null;

    private void OnTriggerEnter(Collider other)
    {
        if (DetectPlayer)
        {
            if (other.gameObject.GetComponent<PlayerC>())
            {
                ScriptCallEvent();
            }
        } else
        {
            if (other.gameObject == targetColliderGO)
            {
                Debug.Log("Detected other obj");
                ScriptCallEvent();
            }
        }
        
    }

    void ScriptCallEvent()
    {
        targetEvent.Invoke();
        if (destroyAfterEvent)
        {
            Destroy(gameObject);
        }
    }
}
