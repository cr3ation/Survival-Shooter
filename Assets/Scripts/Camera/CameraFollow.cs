using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.
    Vector3 zoom_offset;

    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset + zoom_offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        // Zoom the camera in when the user presses "n"
        if (Input.GetKey("n") && zoom_offset.magnitude < 15)
        {
            zoom_offset += transform.forward / 3;
        }
        // Zoom the camera out when the user presses "m"
        else if (Input.GetKey("m") && zoom_offset.magnitude > 0.1)
        {
            zoom_offset -= transform.forward / 3;
        }
        // NOT WORKING PROPERLY Zoom the camera according to scroll-wheel
        /*else if(zoom_offset.magnitude < 15 && zoom_offset.magnitude > 0.1)
        {
            Vector3 temp = transform.forward * Input.mouseScrollDelta.y;
            if(temp.magnitude < 15 && temp.magnitude > 0.1)
                zoom_offset += temp;
            print("1: " + transform.forward);
            print("2: " + Input.mouseScrollDelta.y);
            print("3: " + transform.forward * Input.mouseScrollDelta.y);
        }*/

        // Adjust the nearClipPlane so that it is large when zoomed out and small when close.
        // (this is to clip away the palm-leaves when zoomed out)
        Camera.main.nearClipPlane = Mathf.Max(0.1f, 15 - zoom_offset.magnitude);
        //print(Input.mouseScrollDelta.y);
    }
}