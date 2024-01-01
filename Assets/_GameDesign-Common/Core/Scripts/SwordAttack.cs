using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spidergram
{
    /*
     * 11/28/2022 made to work with Send Message and not player manager specific
     */

    public class SwordAttack : MonoBehaviour
    {
        public GameObject player;
        public int damagePoints = 5;
        private float timer = 0f;
        public float timeDelta = 0.1f;
        private bool bAttack = false;
        public AudioClip hitclip;  // sound to play when striking
        private AudioSource asrc;


        // Start is called before the first frame update
        void Start()
        {
            asrc = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag("Player");

            timer = timeDelta;
            bAttack = false;

        }


        private void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player") && bAttack == false)
            {
                timer = timeDelta;
                bAttack = true;
                // Debug.Log("Player Hit " + Time.time);
            }

        }

        void
        OnTriggerStay(Collider col)
        {
            // bAttack = true;
        }

        private void FixedUpdate()
        {
            if (bAttack == true)
            {

                timer -= Time.fixedDeltaTime;

                if (timer <= 0f)
                {
                    player.SendMessage("TakeDamage", damagePoints);
                    timer = timeDelta;
                    bAttack = false;
                    if (asrc && hitclip)
                    {
                        // Debug.Log("HIT Sound");
                        if (asrc.isPlaying != true)
                            asrc.PlayOneShot(hitclip);
                    }
                }

            }
        }

        private void Update()
        {
            
        }
    }
}