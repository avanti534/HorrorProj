using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenCollider : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip openSound;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.transform.parent != null)
        {
            if (target.transform.parent.GetComponent<DoorScript>())
            {
                // Target door script
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = openSound;
                    audioSource.Play();
                }
            }
        }
    }
}
