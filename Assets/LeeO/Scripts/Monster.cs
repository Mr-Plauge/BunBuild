using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spidergram;

public class Monster : MonoBehaviour
{  
    public Grid_Detector gridDetector;
    public GameObject target;
    public GameObject head;
	public float gravity = 40f;
    private Rigidbody rb;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public bool withinRange;
    public bool dead;
    public bool hasHurt;
	
	void Awake () {
        int compNumber = 1000000;
        if (gridDetector.roomScore > 2) {
            compNumber = 10;
        }
        if (gridDetector.roomScore > 4) {
            compNumber = 8;
        }
        if (gridDetector.roomScore > 6) {
            compNumber = 7;
        }
        if (gridDetector.roomScore > 7) {
            compNumber = 6;
        }
        if (gridDetector.roomScore > 8) {
            compNumber = 5;
        }
        if (Random.Range(0, compNumber) > 1)
        {
            Destroy(gameObject);
        }
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;
        transform.Rotate(0, Random.Range(0f, 180f), 0, Space.Self);
	}

    void Update() {
        //transform.LookAt(target.transform);
        withinRange = Vector3.Distance(transform.position, target.transform.position) < 20;
        if (withinRange && !dead) {
            head.transform.LookAt(target.transform);
            gridDetector.fearCounter = 30;
            Vector3 targetDir = rb.velocity; //Direction of the character
            targetDir.y = 0;
            if (targetDir == Vector3.zero)
                targetDir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * 5); //Rotate the character little by little
            transform.rotation = targetRotation;

            rb.velocity = head.transform.forward * 8f;
        }
	    rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
    }

    public void KillMe() {
        if (!dead) {
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            dead = true;
            Debug.Log("dead");
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && !hasHurt && !dead)
        {
            hasHurt = true;
            col.gameObject.GetComponent<AudioSource>().PlayOneShot(hurtSound);
			col.gameObject.GetComponent<PlayerManager2>().TakeDamage(1);
            Destroy(gameObject);
		}

        if (col.gameObject.tag == "Player" && dead)
        {
            Destroy(gameObject);
		}
    }
}