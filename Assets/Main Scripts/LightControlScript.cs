using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControlScript : MonoBehaviour {

    Animator anim;
    int animNum;

    public bool lightOn = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        if (lightOn)
        {
            TurnLightOn();
        } else
        {
            TurnLightOff();
        }
	}

    public void PlayLightFlicker()
    {
        int randomNum = Random.Range(1, 3);
        if (randomNum == 1)
        {
            anim.SetInteger("FlickerVal", 1);
        } else if (randomNum == 2)
        {
            anim.SetInteger("FlickerVal", 2);
        }
    }
    public void ResetFlickerVal()
    {
        anim.SetInteger("FlickerVal", 0);
    }
    public void TurnLightOff()
    {
        anim.SetBool("On", false);
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }
    public void TurnLightOn()
    {
        anim.SetBool("On", true);
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    }
}
