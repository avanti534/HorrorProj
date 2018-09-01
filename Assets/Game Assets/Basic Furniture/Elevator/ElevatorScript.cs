using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

    Animator anim;
    public Transform targetLocation;

    AudioSource audioSource;
    public AudioClip dingSound;

    float speed = 3.0f;

    [System.NonSerialized]
    public bool beginMove = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        beginMove = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (beginMove)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, step);

            float dist = Vector3.Distance(transform.position, targetLocation.position);
            if (dist == 0.0f)
            {
                Debug.Log("Elevator reached point");
                SetDoorBool(true);
                beginMove = false;
                //Destroy(this);
            }
        }
	}

    public void SetDoorBool(bool openVal)
    {
        anim.SetBool("Open", openVal);
    }
    
    public void PlayDingSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = dingSound;
            audioSource.Play();
        }
    }

}
