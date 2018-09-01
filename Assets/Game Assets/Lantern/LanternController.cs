using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour {

    public Light lanternLight;
    public bool isProp = false;


    // Use this for initialization
    void Start() {
        if (!isProp)
        {
            FindObjectOfType<PlayerC>().AttachLantern(this.transform);
            TurnOffLight();
        }
	}

    public void TurnOffLight()
    {
        lanternLight.enabled = false;
    }
    public void TurnOnLight()
    {
        lanternLight.enabled = true;
    }
}
