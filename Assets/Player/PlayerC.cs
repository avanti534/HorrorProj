using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour {

    [System.NonSerialized]
    public Transform playerCamR;
    [System.NonSerialized]
    public Transform mainCam;

    public float playerSpeed = 7.0f;
    
    Rigidbody rb;
    public Animator anim;
    

    public Transform pickupPos;

    public DoorScript targetDoorScript = null;

    [System.NonSerialized]
    public bool playerHiding = false;

    [System.NonSerialized]
    public bool canMove = true;
    // Used for moving player
    private Transform tpLocation;

    private AudioSource audioSource;
    public AudioClip[] footStepSounds;
    public AudioClip itemSoundEffect;

    private MenuUIController menuController;
    [System.NonSerialized]
    public bool inMenu = false;
    
    public bool hasLantern = false;
    public Rigidbody lanternPos;
    bool holdingLantern = false;
    LanternController targetLantern;
    public BoxCollider lanternCollider;

    // Use this for initialization
    void Start () {
        canMove = false;

        playerCamR = transform.GetChild(0);
        mainCam = transform.GetChild(0).GetChild(0).transform;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        menuController = FindObjectOfType<MenuUIController>();
        lanternCollider.enabled = false;
        
        pickupPos = playerCamR.Find("PickupPos");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            PlayBoolAnim("LeanLeft", true);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            PlayBoolAnim("LeanRight", true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            PlayBoolAnim("LeanLeft", false);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            PlayBoolAnim("LeanRight", false);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Moving", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Moving", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Moving", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Moving", false);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            anim.SetBool("Crouch", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inMenu)
            {
                menuController.OpenMenuUI();
                canMove = false;
                inMenu = true;
            } else
            {
                menuController.CloseMenuUI();
                canMove = true;
                inMenu = false;
            }
        }
        if (hasLantern)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!holdingLantern)
                {
                    anim.SetBool("Lantern", true);
                    lanternCollider.enabled = true;
                }
                else
                {
                    anim.SetBool("Lantern", false);
                    lanternCollider.enabled = false;
                }

                holdingLantern = !holdingLantern;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Keyboard
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(transform.forward * playerSpeed * Time.deltaTime, Space.World);
                anim.SetBool("Moving", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(transform.forward * -playerSpeed * Time.deltaTime, Space.World);
                anim.SetBool("Moving", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(transform.right * -playerSpeed * Time.deltaTime, Space.World);
                anim.SetBool("Moving", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * playerSpeed * Time.deltaTime, Space.World);
                anim.SetBool("Moving", true);
            }
            if (Input.GetKey(KeyCode.C))
            {
                anim.SetBool("Crouch", true);
            }
        }
    }


    // Animations
    public void PlayBoolAnim(string name, bool val)
    {
        anim.SetBool(name, val);
    }
    public void SetPlayerHideOptions()
    {
        playerHiding = true;
        //GetComponent<MeshRenderer>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        ResetCameraProperties();
    }
    public void ResetPlayerHideOptions()
    {
        playerHiding = false;
        Debug.Log("Reset player hide");
        //GetComponent<MeshRenderer>().enabled = true;
        canMove = true;
        rb.isKinematic = false;
       // mainCam.transform.position = new Vector3(0, 0, 0);
        ResetCameraProperties();
    }

    public void ResetCameraProperties()
    {
        playerCamR.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        mainCam.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        playerCamR.position = new Vector3(0.0f, 1.2f, 0.0f);
        //mainCam.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void ResetAnimVals()
    {
        anim.SetBool("Moving", false);
        anim.SetBool("LeanLeft", false);
        anim.SetBool("LeanRight", false);
        anim.SetBool("Crouch", false);
        holdingLantern = false;
        anim.SetBool("Lantern", false);
    }

    public void EndPlayer()
    {
        canMove = false;
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("Death");
        ResetAnimVals();
        anim.SetTrigger("Death");
    }

    // Used in animations
    public void DisablePlayerMove()
    {
        canMove = false;
    }
    public void EnablePlayerMove()
    {
        canMove = true;
    }

    public void PlayFootStepSound()
    {
        if (footStepSounds.Length != 0)
        {
            int randomNum = Random.Range(0, footStepSounds.Length);
            AudioClip targetClip = footStepSounds[randomNum];
            if (!audioSource.isPlaying)
            {
                audioSource.clip = targetClip;
                audioSource.Play();
            }
        }
    }

    public void AttachLantern(Transform lanternParent)
    {
        lanternParent.transform.position = lanternPos.position;
        lanternParent.GetComponent<HingeJoint>().connectedBody = lanternPos;
        targetLantern = lanternParent.GetComponent<LanternController>();
    }
    public void TurnOffLantern()
    {
        targetLantern.TurnOffLight();
    }
    public void TurnLanternOn()
    {
        targetLantern.TurnOnLight();
    }

    public void PlayItemSoundEffect()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = itemSoundEffect;
        audioSource.Play();
    }
}
