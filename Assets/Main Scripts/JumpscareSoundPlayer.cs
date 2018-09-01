using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareSoundPlayer : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip[] jumpSoundClips;
    public int playIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpscareSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = jumpSoundClips[playIndex];
            audioSource.Play();
            playIndex++;
        }
    }
}
