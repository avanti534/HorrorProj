using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {


    PlayerC pc;
    public float xSens = 300f;
    public float frontOpenPosLimit = 45;
    public float backOpenPosLimit = 45;

    public GameObject frontDoorCollider;
    public GameObject backDoorCollider;
    [System.NonSerialized]
    public bool moveDoor = false;
    DoorCollision doorCollision = DoorCollision.NONE;

    //Quaternion lastRot;
    //bool canPlay = false;

    //private DoorOpenCollider openCollider;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(doorMover());
        pc = FindObjectOfType<PlayerC>();
        //openCollider = transform.parent.GetComponentInChildren<DoorOpenCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.collider.gameObject == frontDoorCollider)
                {
                    moveDoor = true;
                    //Debug.Log("Front door hit");
                    doorCollision = DoorCollision.FRONT;
                }
                else if (hitInfo.collider.gameObject == backDoorCollider)
                {
                    moveDoor = true;
                    //Debug.Log("Back door hit");
                    doorCollision = DoorCollision.BACK;
                }
                else
                {
                    doorCollision = DoorCollision.NONE;
                }
            }

        }
        
        if (Input.GetMouseButtonUp(0))
        {
            //canPlay = true;
            moveDoor = false;
            //Debug.Log("Mouse up");
        }
    }

    IEnumerator doorMover()
    {
        bool stoppedBefore = false;
        float xRot = -transform.rotation.x;

        while (true)
        {
            if (moveDoor)
            {
                pc.targetDoorScript = this;
                stoppedBefore = false;
                //Debug.Log("Moving Door");

                xRot += Input.GetAxis("Mouse X") * xSens * Time.deltaTime;


                //Check if this is front door or back
                if (doorCollision == DoorCollision.FRONT)
                {
                    float dist = Vector3.Distance(pc.transform.position, transform.position);
                    // Prevent player from interacting with door if distance is above a certain limit
                    if (dist <= 8.0f)
                    {
                        //Debug.Log("Pull Down(PULL TOWARDS)");
                        xRot = Mathf.Clamp(xRot, -frontOpenPosLimit, backOpenPosLimit);
                        //Debug.Log(yRot);
                        transform.localEulerAngles = new Vector3(0, -xRot, 0);
                        
                    } else
                    {
                        moveDoor = false;
                    }
                }
                else if (doorCollision == DoorCollision.BACK)
                {
                    //Debug.Log("Pull Up(PUSH AWAY)");
                    xRot = Mathf.Clamp(xRot, 0, backOpenPosLimit);
                    //Debug.Log(yRot);
                    transform.localEulerAngles = new Vector3(0, xRot, 0);
                }
            }
            else
            {
                if (!stoppedBefore)
                {
                    stoppedBefore = true;
                }
            }
            yield return null;
        }

    }
    enum DoorCollision
    {
        NONE, FRONT, BACK
    }

    public void PlayDoorSlamOpen()
    {
        GetComponent<Animator>().SetTrigger("SlamOpen");
    }
}
