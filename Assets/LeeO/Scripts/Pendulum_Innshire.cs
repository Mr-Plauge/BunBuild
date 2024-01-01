using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum_Innshire : MonoBehaviour
{
	public float speed = 1.5f;
	public Vector3 limit = new Vector3(75f, 0f, 0f); //Limit in degrees of the movement
	public bool randomStart = false; //If you want to modify the start position
	private float random = 0;

	// Start is called before the first frame update
	void Awake()
    {
		if(randomStart)
			random = Random.Range(0f, 1f);
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 angle = new Vector3(limit.x * Mathf.Sin(Time.time + random * speed), limit.y * Mathf.Sin(Time.time + random * speed), limit.z * Mathf.Sin(Time.time + random * speed));
		transform.localRotation = Quaternion.Euler(angle.x, angle.y, angle.z);
        Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
