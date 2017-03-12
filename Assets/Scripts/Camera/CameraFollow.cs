using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.

    float minFov = 12f;
    float maxFov = 24f;
    float sensitivity = 5f;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();       //Contains stored input keys+--+´´

    void Start()
    {
        // Key bindings
        keys.Add("ZoomIn", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ZoomIn", "Alpha9")));
        keys.Add("ZoomOut", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ZoomOut", "Alpha0")));

        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        // Zoom in/out using the camera fov
        float fov = Camera.main.fieldOfView;
        // Zoom the camera in when the user presses "n"
        if (Input.GetKey("n") || Input.GetKey(keys["ZoomIn"]))
        {
            fov -= 0.3f;
        }
        // Zoom the camera out when the user presses "m"
        else if (Input.GetKey("m") || Input.GetKey(keys["ZoomOut"]))
        {
            fov += 0.3f;
        }
        // Zoom the camera according to scroll-wheel
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}