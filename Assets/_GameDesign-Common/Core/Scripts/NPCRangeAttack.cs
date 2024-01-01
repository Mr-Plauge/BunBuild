using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class NPCRangeAttack : MonoBehaviour {

/*
Spidergram Corp (c)
Alejandro Jauco
*/

private GameObject player;
    public string Player_tag = "Player";
//string Enemy_tag = "Enemy";
    public float player_inrange = 30.0f;  // Fire if within this range
    public float lookSpeed = 2.0f;  // how fast the swivel will turn
    public Transform Shootpoint;
    public Rigidbody Bullet;
    public float Bullet_force = 20.0f;
    public string projectile_tag = "Bullet";
private float shoot_delay = 1.0f;  // the counter
    public float cool_down = 2.0f;
// float bullet_destroy_time = 2.0f;
// private bool  dead;
     private Vector3 curLoc;
     private Vector3 prevLoc;
    private AudioSource asrc;
    public AudioClip clip;

	public bool useClosest = false;

    public Transform swivel;

Vector3 trans;

void Start (){

        if (swivel == null)
            swivel = this.transform;

	player = GameObject.FindGameObjectWithTag(Player_tag);
	if (!player)
		Debug.Log("Player NOT found");

	if (!Shootpoint)
		Debug.Log("" + this.name +": Shootpoint NOT set");
	if (!Bullet)
		Debug.Log("" + this.name + ": Bullet Prefab NOT set");
	prevLoc = player.transform.position;
	curLoc = prevLoc;

        asrc = GetComponent<AudioSource>();

}
    public GameObject ClosestUnit()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(Player_tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        // Debug.Log("AMJ found num: " + gos.Length);

        if (gos.Length == 0)
        {
            Debug.Log("No Troop Found");
            return null;
        }

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Update (){

		if (useClosest)
        {
            player = ClosestUnit();
        }

// player = GameObject.FindGameObjectWithTag(Player_tag);
if (!player) // player doesn't exist
	return;  

	float dist= Vector3.Distance(transform.position, player.transform.position);
	if (dist >= player_inrange)
 		return;
 	
// Debug.Log("Test " + Time.time);
	prevLoc = curLoc;
    curLoc = player.transform.position;

     transform.rotation = Quaternion.Slerp(transform.rotation,
    	Quaternion.LookRotation(player.transform.position - transform.position), lookSpeed*Time.deltaTime);

    trans = transform.rotation.eulerAngles;
	shoot_delay -= Time.deltaTime;
	if(shoot_delay <= 0 && Shootpoint != null)
	{
	    shoot_delay = cool_down;
	    Rigidbody clone;
	    clone = Instantiate(Bullet, Shootpoint.position, Shootpoint.rotation);
	    clone.GetComponent<Rigidbody>().velocity = Shootpoint.transform.forward * Bullet_force;
            if (asrc != null  && clip != null)
            {
                // Debug.Log("Playsound");
                asrc.PlayOneShot(clip);
            }
	}

//else if(gameObject.tag == projectile_tag)
//{
//transform.Translate(Vector3.forward * (Time.deltaTime * projectile_speed));
//bullet_destroy_time -= Time.deltaTime;
//
//	if(bullet_destroy_time <= 0)
//	{
//	Destroy (gameObject);
//	}
//}
//

}

void OnTriggerEnter ( Collider collision  ){

	if(gameObject.tag == projectile_tag)
	{
		Debug.Log("Collision Detected");
		if(collision.gameObject.tag == Player_tag)
		{
			Debug.Log("Collided With Player");
			//if (collision.gameObject.GetComponent<PlayerDeath>())
			//    collision.gameObject.GetComponent<PlayerDeath>().dead = true; 
			//Time.timeScale = 1.0f;
			Destroy (gameObject);
		}
		Destroy (gameObject);
	}

}

}