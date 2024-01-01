using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spidergram
{


    public class NPCFireball : MonoBehaviour
    {

        // Spidergram Corp (c)
        // Alejandro Jauco

        // Put this script on the shootpoint

        // Time delay before next shot
        // bullet must have a rigidbody for this script to work

        private bool inRange = false;
        public float tgtDist = 10f; // must be within this distance to shoot

        public Rigidbody bullet;
        public float bulletForce = 30;

        public float timedelay = 0.5f;  // x seconds before a shot can be fired

        private bool oktoshoot;

        private float nexttime = 0.0f;

        public string playerTag = "Player";
        Transform player;

        private AudioSource asrc;
        public AudioClip clip;

        public Animator anim;

        public NPCStats stats;

        private void Start()
        {

            asrc = GetComponent<AudioSource>();
            if (anim)
            {
                Debug.Log("Anim is set");
            }

            if (stats == null)
                stats = gameObject.GetComponent<NPCStats>();
        }

        void ShootBullet()
        {
            // if (Time.timeScale == 0.0f)
            //	return;

            // create the object	
            Rigidbody clone = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;

            // apply the force to the newly created object

            // clone.rigidbody.velocity = transform.TransformDirection(transform.forward * bulletForce);
            Rigidbody rb = clone.GetComponent<Rigidbody>();

            if (rb.velocity == Vector3.zero)
                rb.velocity = transform.forward * bulletForce;

            if (asrc != null && clip != null)
            {
                asrc.PlayOneShot(clip);
            }



        }


        // Called every rendered frame

        void Update()
        {
            if (stats)
            {
                if (stats.hp <= 0)
                    return;
            }

            if (Time.time >= nexttime)
            {
                // 		Debug.Log("Time reached " + nexttime);		
                oktoshoot = true;

            }
            float d = Vector3.Distance(transform.position, player.position);

            inRange = tgtDist > d;
            if (inRange && oktoshoot)
            {
                if (anim)
                {
                    anim.SetTrigger("attack2");

                    // Debug.Log("attack1 " + Time.time);
                }
                ShootBullet();
                // set the timer to next 	
                oktoshoot = false;
                nexttime = Time.time + timedelay;

            }
        }

        void Awake()
        {
            GameObject go;
            go = GameObject.FindGameObjectWithTag(playerTag);
            if (go)
                player = go.transform;

            // ok to shoot at start
            oktoshoot = true; nexttime = 0f; // Time.time;
        }

    }
}