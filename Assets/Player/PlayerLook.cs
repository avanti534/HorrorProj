using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform mainParent;
    PlayerC pc;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;

    private void Start()
    {
        if (mainParent == null)
        {
            Debug.LogWarning("No main parent for PlayerLook script");
        } else
        {
            pc = mainParent.GetComponent<PlayerC>();
        }
    }
    void Update()
    {
        if (pc.canMove)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                
            }

            // Player rotation and camera rotation should be seperate
            if (pc.targetDoorScript != null)
            {
                // If there is a target door and player is using it, dont rotate camera
                if (!pc.targetDoorScript.moveDoor)
                {
                    // Player rotation
                    float mouseX = Input.GetAxis("Mouse X");
                    mainParent.Rotate(new Vector3(0, mouseX * sensitivityX, 0));
                }
            } else
            {
                // Player rotation
                float mouseX = Input.GetAxis("Mouse X");
                mainParent.Rotate(new Vector3(0, mouseX * sensitivityX, 0));
            }
        }
    }
}
