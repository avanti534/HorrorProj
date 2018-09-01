using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastController : MonoBehaviour {

    PlayerC pc;

    LockerController targetLC;
    BedController targetBC;

    //Transform holdingObj = null;

    Quaternion camSavedRot;
    Vector3 camSavedPos;

    bool hidingInBed = false;
    bool currentlyHiding = false;
    bool currentlyHolding = false;

    ActionTextControl atc;

    // Use this for initialization
    void Start () {
        pc = GameObject.FindObjectOfType<PlayerC>();
        atc = FindObjectOfType<ActionTextControl>();
	}

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(pc.playerCamR.position, pc.mainCam.forward, out hit, 5f))
        {
            GameObject tObj = hit.collider.gameObject;
            Debug.DrawLine(pc.playerCamR.position, hit.point);
            // Locker
            if (tObj.GetComponent<LockerController>())
            {
                if (Input.GetKey(KeyCode.F) && !currentlyHolding)
                {
                    LockerController lc = tObj.GetComponent<LockerController>();
                    if (currentlyHiding == false && lc.playerIn == true)
                    {
                        Debug.Log("Clicked on locker");
                        pc.SetPlayerHideOptions();
                        pc.transform.position = lc.startPos.position;
                        lc.LockerPlayAnim();
                        pc.PlayBoolAnim("locker", true);
                        currentlyHiding = true;
                        lc.ld.SetPlayerInLocker(true);
                        targetLC = lc;

                        Vector3 lookDir = lc.lookPos.position - pc.transform.position;
                        Quaternion lookRot = Quaternion.LookRotation(lookDir);
                        pc.transform.rotation = lookRot;
                    }
                }
            }
            else if (tObj.transform.parent != null)
            {
                // Bed
                if (tObj.transform.parent.GetComponent<BedController>())
                {
                    Transform bedObj = tObj.transform.parent;
                    if (Input.GetKey(KeyCode.F) && !currentlyHolding)
                    {
                        BedController bc = bedObj.GetComponent<BedController>();
                        if (currentlyHiding == false && bc.playerIn == true)
                        {
                            Debug.Log("Player clicked on bed");
                            pc.SetPlayerHideOptions();
                            pc.transform.position = bc.startPos.position;
                            pc.PlayBoolAnim("Moving", false);
                            pc.PlayBoolAnim("Bed", true);
                            bc.bd.SetPlayerInBed(true);
                            currentlyHiding = true;
                            hidingInBed = true;
                            targetBC = bc;

                            Vector3 lookDir = bc.lookPos.position - pc.transform.position;
                            Quaternion lookRot = Quaternion.LookRotation(lookDir);
                            pc.transform.rotation = lookRot;
                        }
                    }
                }

                else if (tObj.transform.parent.GetComponent<LanternController>())
                {
                    if (tObj.transform.parent.GetComponent<LanternController>().isProp)
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            pc.hasLantern = true;
                            pc.PlayItemSoundEffect();
                            Destroy(tObj.transform.parent.gameObject);
                        }
                    }
                }

                if (tObj.transform.parent.GetComponent<SearchController>())
                {
                    if (Input.GetKey(KeyCode.F))
                    {
                        tObj.transform.parent.GetComponent<SearchController>().PlayOpenAnim();
                    }
                }


                /*
                // Picking up Items
                if (tObj.transform.parent.GetComponent<ItemScript>())
                {
                    Debug.Log("Looking at item");
                    if (Input.GetMouseButtonDown(0) && !currentlyHolding)
                    {
                        holdingObj = tObj.transform.parent;
                        pc.GetComponent<CapsuleCollider>().radius = 1.95f;
                        // Clicked on object with item script
                        Debug.Log("Clicked item: " + tObj.name);
                        holdingObj.position = pc.pickupPos.position;
                        holdingObj.parent = pc.pickupPos;
                        holdingObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        holdingObj.GetComponent<Rigidbody>().isKinematic = true;
                        holdingObj.GetComponent<Rigidbody>().useGravity = false;
                        tObj.GetComponent<MeshCollider>().enabled = false;

                        currentlyHolding = true;
                    }
                }
                */
            }
        }
        // Exit locker
        if (Input.GetKey(KeyCode.Space))
        {   
            // Exit Locker
            if (currentlyHiding && targetLC != null)
            {
                targetLC.LockerPlayAnim();
                targetLC.ld.SetPlayerInLocker(false);
                pc.PlayBoolAnim("locker", false);
                pc.PlayBoolAnim("Moving", false);
                currentlyHiding = false;
                targetLC = null;
            }
            // Exit Bed
            if (currentlyHiding && hidingInBed)
            {
                pc.PlayBoolAnim("Bed", false);
                targetBC.bd.SetPlayerInBed(false);
                currentlyHiding = false;
                hidingInBed = false;
                targetBC = null;
            }

        }
        /*
        if (Input.GetMouseButtonDown(1))
        {
            // Drop item
            if (currentlyHolding)
            {
                pc.GetComponent<CapsuleCollider>().radius = 1;
                holdingObj.position = pc.pickupPos.position;
                holdingObj.parent = null;
                holdingObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                holdingObj.GetComponent<Rigidbody>().isKinematic = false;
                holdingObj.GetComponent<Rigidbody>().useGravity = true;
                holdingObj.GetChild(0).GetComponent<MeshCollider>().enabled = true;

                holdingObj.GetComponent<Rigidbody>().AddForce(transform.forward * 450000.0f);

                currentlyHolding = false;
                holdingObj = null;
            }
        }
        */
    }
}