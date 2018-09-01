using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOutsideScript : MonoBehaviour {

    AmbientSoundControl asc;

    private void Start()
    {
        asc = FindObjectOfType<AmbientSoundControl>();
    }

    public void PlayAmbientSounds()
    {
        asc.StartAmbientSound();
    }

    public void StopAmbientSounds()
    {
        asc.StopAmbientSound();
    }
}
