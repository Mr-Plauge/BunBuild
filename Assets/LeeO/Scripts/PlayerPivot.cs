using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPivot : MonoBehaviour
{
	public float mouseSpeed = 2;
    float turnSmoothing = 0.1f;
	float smoothX;
	float smoothY;
	float smoothXvelocity;
	float smoothYvelocity;
	public float lookAngle; //Angle the camera has on the Y axis
	public float tiltAngle;
    public GameObject player;
    public Console console;
	public Transform doorRot;
	public Quaternion storedRotation;

	private void Update()
	{
		float mouseH = Input.GetAxis("Mouse X");
		float mouseV = Input.GetAxis("Mouse Y");

        transform.position = player.transform.position;
        
		float targetSpeed = mouseSpeed;
		if (!console.Toggled) {
			HandleRotations(Time.deltaTime, 0, mouseH, targetSpeed);
			storedRotation = transform.rotation;
		}
	}

	void HandleRotations(float d, float v, float h, float targetSpeed)
	{ //Function that rotates the camera correctly
		if (turnSmoothing > 0)
		{
			smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, turnSmoothing);
			smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, turnSmoothing);
		}
		else
		{
			smoothX = h;
			smoothY = v;
		}
		Quaternion targetRotation;
		lookAngle += smoothX * targetSpeed;
		tiltAngle += -smoothY * targetSpeed;
		//lookAngle = Mathf.Clamp(lookAngle, minAngleY, maxAngleY);
		//tiltAngle = Mathf.Clamp(tiltAngle, minAngleX, maxAngleX);
		targetRotation = Quaternion.Euler(tiltAngle, lookAngle, 0);
		transform.rotation = targetRotation;

	}
}