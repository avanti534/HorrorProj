using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundControl : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        StopAmbientSound();
	}
	
	public void StartAmbientSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    public void StopAmbientSound()
    {
        audioSource.Stop();
    }
}
