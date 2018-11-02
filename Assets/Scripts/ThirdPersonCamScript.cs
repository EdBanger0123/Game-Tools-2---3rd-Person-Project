using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamScript : MonoBehaviour {

    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt, camTransform, Enemy;

    private Camera cam;

    private float sensitivityX = 1.0f, sensitivityY = 1.0f;
    public float currentX, currentY, distance, height;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        
    }

    /*private void Update()
    {
        currentX += (Input.GetAxis("Mouse X") * sensitivityX);
        currentY += (Input.GetAxis("Mouse Y") * sensitivityY);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }*/

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, height, - distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + dir;
        camTransform.LookAt(lookAt.position);

        /*if(GetComponent<PlayerMovement>()._lockedOn == true)
        {
            camTransform.LookAt(Enemy);
        }*/
    }
}
