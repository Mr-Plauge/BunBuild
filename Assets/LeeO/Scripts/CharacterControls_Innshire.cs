using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls_Innshire : MonoBehaviour {
	
	public float speed = 10.0f;
	public float airVelocity = 8f;
	public float gravity = 40f;
	public float maxVelocityChange = 5f;
	public float jumpHeight = 2f;
	public float maxFallSpeed = 20f;
	public float rotateSpeed = 25f; //Speed the player rotate
	private Vector3 moveDir;
	public GameObject cam;
	public Console console;
	private Rigidbody rb;

	private float distToGround;

	private bool canMove = true; //If player is not hitted
	private bool isStuned = false;
	private bool wasStuned = false; //If player was stunned before get stunned another time
	private float pushForce;
	private Vector3 pushDir;

	public float minAngleX = -10;
	public float maxAngleX = 50; //Maximum angle that we allow the camera to reach
	public float minAngleY = -35;
	public float maxAngleY = 35;
	
	public float mouseSpeed = 2;
	private float shotCooldown = 20f;

	float smoothX;
	float smoothY;
	float smoothXvelocity;
	float smoothYvelocity;
	public float lookAngle; //Angle the camera has on the Y axis
	public float tiltAngle;

	public Transform pivot;
	public GameObject bullet;
	public GameObject shootPoint;
	public AudioSource audioSource;
	public AudioClip shootSound;
	public AudioClip boomSound;

	public Animator animator;
	public bool ended;

	public Vector3 checkPoint;
	private bool slide = false;

	void  Start (){
		// get the distance to ground
		distToGround = GetComponent<Collider>().bounds.extents.y;
	}
	
	bool IsGrounded (){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
	
	void Awake () {
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;

		checkPoint = transform.position;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked; // AMJ keep it centered
	}

	void Fire(float shotOffsetX, float shotOffsetY, float shotOffsetZ, bool big = false) {
        GameObject Bullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
		if (big) {
			Bullet.transform.localScale = new Vector3(2,2,2);
		}
		Bullet.GetComponent<Rigidbody>().velocity = new Vector3(shootPoint.transform.forward.x + shotOffsetX, 0 + shotOffsetY, shootPoint.transform.forward.z + shotOffsetZ) * 200;
        Destroy(Bullet, 3.0f);
	}
	
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Mouse1)) {
			speed = 5f;
		}
		else {
			speed = 12f;
		}
		if (canMove)
		{
			if (moveDir.x != 0 || moveDir.z != 0 || Input.GetKey(KeyCode.Mouse1))
			{
				Vector3 targetDir = moveDir; //Direction of the character

				targetDir.y = 0;
				if (targetDir == Vector3.zero)
					targetDir = transform.forward;
				Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
				Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
				if (Input.GetKey(KeyCode.Mouse1)) {
					targetRotation = pivot.rotation;
				}
				transform.rotation = targetRotation;
			}

			if (IsGrounded())
			{
			 // Calculate how fast we should be moving
				Vector3 targetVelocity = moveDir;
				targetVelocity *= speed;

				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = rb.velocity;
				if (targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
				{
					targetVelocity = velocity;
					rb.velocity /= 1.1f;
				}
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				if (!slide)
				{
					if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
						rb.AddForce(velocityChange, ForceMode.VelocityChange);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
					//Debug.Log(rb.velocity.magnitude);
				}

				// Jump
				if (IsGrounded() && Input.GetButton("Jump"))
				{
					rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				}
			}
			else
			{
				if (!slide)
				{
					Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
					Vector3 velocity = rb.velocity;
					Vector3 velocityChange = (targetVelocity - velocity);
					velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
					velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
					rb.AddForce(velocityChange, ForceMode.VelocityChange);
					if (velocity.y < -maxFallSpeed)
						rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
				}
			}
		}
		else
		{
			rb.velocity = pushDir * pushForce;
		}
		// We apply gravity manually for more tuning control
		rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
		if (shotCooldown > 0) {
			shotCooldown--;
		}
	}

	void Update()
	{
		float h = 0;
		float v = 0;
		if (!console.Toggled) {
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		}

		if (Input.GetKey(KeyCode.Mouse1) && Input.GetMouseButtonDown(0) && shotCooldown == 0) {
			audioSource.PlayOneShot(shootSound);
			audioSource.PlayOneShot(boomSound);
			animator.SetTrigger("Firing");
			Fire(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
			Fire(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
			Fire(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
			Fire(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
			Fire(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
			Fire(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
			Fire(0, 0, 0, true);
			shotCooldown = 30;
		}

		Vector3 v2 = v * pivot.transform.forward; //Vertical axis to which I want to move with respect to the camera
		Vector3 h2 = h * pivot.transform.right; //Horizontal axis to which I want to move with respect to the camera
		moveDir = (v2 + h2).normalized; //Global position to which I want to move in magnitude 1

		float targetSpeed = mouseSpeed;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
		{
			if (hit.transform.tag == "Slide")
			{
				slide = true;
			}
			else
			{
				slide = false;
			}
		}
		if (!console.Toggled && !ended) {
        	Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	public void HitPlayer(Vector3 velocityF, float time)
	{
		rb.velocity = velocityF;

		pushForce = velocityF.magnitude;
		pushDir = Vector3.Normalize(velocityF);
		StartCoroutine(Decrease(velocityF.magnitude, time));
	}

	public void LoadCheckPoint()
	{
		transform.position = checkPoint;
	}

	private IEnumerator Decrease(float value, float duration)
	{
		if (isStuned)
			wasStuned = true;
		isStuned = true;
		canMove = false;

		float delta = 0;
		delta = value / duration;

		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return null;
			if (!slide) //Reduce the force if the ground isnt slide
			{
				pushForce = pushForce - Time.deltaTime * delta;
				pushForce = pushForce < 0 ? 0 : pushForce;
				//Debug.Log(pushForce);
			}
			rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
		}

		if (wasStuned)
		{
			wasStuned = false;
		}
		else
		{
			isStuned = false;
			canMove = true;
		}
	}
}
