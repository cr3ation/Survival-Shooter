#pragma strict

var rotate_amount : float;

function Start () {

}

function Update ()
 {
 	// Change the rotate_amount value to change the range of how far the camera needs to rotate.

 	transform.rotation = Quaternion.Euler(transform.eulerAngles.x,(Mathf.Sin(Time.realtimeSinceStartup) * rotate_amount) + transform.eulerAngles.y, transform.eulerAngles.z); 
 }