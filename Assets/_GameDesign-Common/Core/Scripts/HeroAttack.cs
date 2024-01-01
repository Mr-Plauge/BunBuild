using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by Alejandro Jauco
 * Update: 11/28/2021 Added Audio when swinging sword
 * 11/28/2022 removed reference to PM
 * 11/28/2022 made to work with Obstacle PC using can Attack and on trigger stay
 */

namespace Spidergram
{

    public class HeroAttack : MonoBehaviour
    {
        public Transform player;
        public int damagePoints = 5;
        public Animator anim;
        public float CooldownDelay = 1f; // 2 secs between swings
        public string[] destroyTaglist = { "destroyable", "Enemy" };
        float delay;
        public bool canAttack;

        public bool useTrigger = true;

        // Start is called before the first frame update
        void Start()
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");

            if (go != null)
            {
                player = go.transform;

            }

            if (anim == null)
                anim = GetComponent<Animator>();

            delay = .3f;

            Collider collider = GetComponent<Collider>();
            if (collider)
                collider.isTrigger = useTrigger;
            canAttack = false;
        }

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


        // Update is called once per frame
        void Update()
        {
            /*
             * 
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
            {
                Debug.Log("Attacking " + Time.time);
            }
            */

                delay -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && delay <= 0f)
            {
                delay = CooldownDelay;
                if (anim && anim.GetBool("isAttacking") == false)
                {
                    Debug.Log("Attack ");
                    anim.SetTrigger("isAttacking");
                    if (TheSound != null)
                        PlayOneSound(TheSound, Volume);
                }
                canAttack = true;
            }

        }


        private void OnTriggerEnter(Collider col)
        {
            if (canAttack == false)
                return;

            Debug.Log("TTT " + col.name + " Tag: " + col.tag);

            foreach (var destroyTag in destroyTaglist)
            {

                if (col.CompareTag(destroyTag))
                {
                    NPCStats stats;

                    stats = col.GetComponent<NPCStats>();

                    if (stats)
                    {
                        stats.TakeDamage(damagePoints);
                    }
                    Debug.Log("Enemy Hit " + Time.time);
                    canAttack = false;
                }
            }

        }

        private void OnTriggerStay(Collider col)
        {
            if (canAttack == false)
                return;

            foreach (var destroyTag in destroyTaglist)
            {

                if (col.CompareTag(destroyTag))
                {
                    NPCStats stats;

                    stats = col.GetComponent<NPCStats>();

                    if (stats)
                    {
                        stats.TakeDamage(damagePoints);
                    }
                    Debug.Log("Enemy Hit " + Time.time);
                    canAttack = false;

                }
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }

    }
}