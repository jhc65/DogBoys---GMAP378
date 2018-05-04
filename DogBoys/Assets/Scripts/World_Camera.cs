using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Camera : MonoBehaviour {
    [SerializeField]
    private float cameraSpeed_;
    [SerializeField]
    private float cameraZoomSpeed_;
    [SerializeField]
    private float cameraSmooth_;
    [SerializeField]
    private float cameraMovementRangeXMin_, cameraMovementRangeXMax_;
    [SerializeField]
    private float cameraMovementRangeZMin_, cameraMovementRangeZMax_;
    [SerializeField]
    private float cameraZoomRangeYMin_, cameraZoomRangeYMax_;
    [SerializeField]
    private Transform targetCameraFollow_;
    [SerializeField]
    private Vector3 cameraOffset_;
    [SerializeField]
    private bool cameraFollowing_ = false;


    private float horizontalInput_;
    private float verticalInput_;
    private float mouseScrollWheelInput_;

    private void Update()
    {
        getUserInput();
    }

    private void LateUpdate()
    {
        if (cameraFollowing_) {
            cameraFollow();
        }
        else {
            cameraFreeMovement();
        }
    }

    private void cameraFollow()
    {
        Vector3 newPosition = targetCameraFollow_.position + cameraOffset_;
        Vector3 smoothed = Vector3.Lerp(transform.position, newPosition, cameraSmooth_ * Time.deltaTime);
        targetCameraFollow_.position = newPosition;
        transform.LookAt(targetCameraFollow_);
    }

    private void cameraFreeMovement()
    {       
        float x = 0;
        float z = 0;
        if((horizontalInput_ != 0.0f) && (verticalInput_ != 0.0f))
        {
            x = (transform.position.x + (cameraSpeed_ * Time.deltaTime * horizontalInput_));
            z = (transform.position.z + (cameraSpeed_ * Time.deltaTime * verticalInput_));
            z = Mathf.Clamp(z, cameraMovementRangeZMin_, cameraMovementRangeZMax_);
            x = Mathf.Clamp(x, cameraMovementRangeXMin_, cameraMovementRangeXMax_);
            Vector3 movement = new Vector3(x, transform.position.y, z);
            transform.position = movement;

        }
        else if (horizontalInput_ != 0.0f)
        {
            x = (transform.position.x + (cameraSpeed_ * Time.deltaTime * horizontalInput_));
            x =Mathf.Clamp(x, cameraMovementRangeXMin_, cameraMovementRangeXMax_);
            Vector3 movement = new Vector3(x, transform.position.y, transform.position.z);
            transform.position = movement;
        }
        else if (verticalInput_ != 0.0f)
        {
            z = (transform.position.z + (cameraSpeed_ * Time.deltaTime * verticalInput_));
            z = Mathf.Clamp(z, cameraMovementRangeZMin_, cameraMovementRangeZMax_);
            Vector3 movement = new Vector3(transform.position.x, transform.position.y, z);
            transform.position = movement;
        }
        cameraZoom();
    }

    private void cameraZoom()
    {
        if(mouseScrollWheelInput_ != 0.0f)
        {
            float y = (transform.position.y + (cameraZoomSpeed_ * Time.deltaTime * mouseScrollWheelInput_));
            y = Mathf.Clamp(y, cameraZoomRangeYMin_, cameraZoomRangeYMax_);
            Vector3 movement = new Vector3(transform.position.x, y, transform.position.z);
            transform.position = movement;
        }
    }

    private void getUserInput()
    {
        mouseScrollWheelInput_ = Input.GetAxisRaw("Mouse ScrollWheel");
        verticalInput_ = Input.GetAxisRaw("Vertical");
        horizontalInput_ = Input.GetAxisRaw("Horizontal");
    }
}
