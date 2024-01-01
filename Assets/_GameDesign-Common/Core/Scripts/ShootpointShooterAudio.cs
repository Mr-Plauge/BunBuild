using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class ShootpointShooterAudio : MonoBehaviour {
// Spidergram Corp (c)
// Alejandro Jauco
// Update 4/17/2020  user selectable shoot key

// Time delay before next shot
// bullet must have a rigidbody for this script to work

public Rigidbody bullet;
public float bulletForce = 80;

    public int button = 0; // the fire mouse button.  0 = LMB, 1 = RMB
public float CoolDownDelay = 0.5f;  // 2 seconds before a shot can be fired
public Text CoolDownText;  // Cool down. next time to shoot

private bool  oktoshoot;

private float nexttime = 0.0f;

    public AudioClip TheSound;
    public float Volume = 1.0f;
    public void PlayOneSound(AudioClip sound, float volume)
    {
        // KeyCode key;

        if (sound == null)
        {
            Debug.Log("No sound");
            return;
        }
        GetComponent<AudioSource>().volume = volume;
        GetComponent<AudioSource>().PlayOneShot(sound);
    }

    void ShootBullet (){
	// if (Time.timeScale == 0.0f)
	//	return;
		
	// create the object	
	Rigidbody clone= Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;

	// apply the force to the newly created object

	// clone.rigidbody.velocity = transform.TransformDirection(transform.forward * bulletForce);
	clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        PlayOneSound(TheSound, Volume);
    }

void UpdateTime (){

	if (CoolDownText)
	{	float t= (nexttime - Time.time);
		float s;
		s = t *100; t = s/100.0f;  // for 2 digit sig fig
		if (t <= 0)
			t = 0.0f;
		CoolDownText.text = "Next " + t;
	}

}

// Called every rendered frame

void Update (){
	if (Time.time >= nexttime){
// 		Debug.Log("Time reached " + nexttime);		
		oktoshoot = true;

	}
        if
        // (Input.GetButtonDown("Fire1") && oktoshoot)
         (Input.GetMouseButtonDown(button) && oktoshoot)
        {
            ShootBullet();
            // set the timer to next 	
            oktoshoot = false;
            nexttime = Time.time + CoolDownDelay;

        }
        UpdateTime();
}

void Awake (){
	// ok to shoot at start
	oktoshoot = true; nexttime = 0f; // Time.time;
	// for a delay first use -> oktoshoot = false; nexttime = Time.time + CoolDownDelay;
	if (CoolDownText)
	{
		CoolDownText.text = "Next " + (nexttime-Time.time);
	}
}

}