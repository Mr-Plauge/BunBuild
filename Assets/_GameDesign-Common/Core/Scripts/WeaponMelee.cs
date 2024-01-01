using UnityEngine;
using System.Collections;

namespace Spidergram
{
    public class WeaponMelee : MonoBehaviour
    {

        /*
        Spidergram Corp (c)
        Alejandro Jauco
        2/8/2021
        */

        // add this routine to the bullet

        public string hitTag = "destroyable";
        public string hitName = "enemy";
        public int points = 10;
        public float destroyDelay = 0.0f;
        private GameObject player;

        private AudioSource asrc;
        public AudioClip sound;  // sound when player hit
        public float volume = 1f;  // the volume 0 - 1.0
        public AnimationClip aclip; // delay till clip is done

        bool fire = true;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
                Debug.Log("BulletStats: Player not found.");

            fire = true;

        }

        void ScorePoints()
        {

            if (player)
                player.SendMessage("ScorePoints", points, SendMessageOptions.DontRequireReceiver);
            else
            {
                Debug.Log("CANNOT SCORE POINTS" + points);
            }
        }

        void HealthPoints()
        {

            if (player)
            {
                Debug.Log("Send HP ");
                player.SendMessage("HealthPoints", points, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.Log("CANNOT Health POINTS" + points);
            }
        }

        public void PlayOneSound()
        {

            if (sound == null)
            {
                Debug.Log("No sound");
                return;
            }
            AudioSource asrc = GetComponent<AudioSource>();
            if (asrc == null)
            {
                Debug.Log("Missing Audiosource");
                return;
            }

            asrc.volume = volume;
            asrc.PlayOneShot(sound);
            Debug.Log("Make a sound: " + sound.name);
        }

        void DoDestroy(GameObject go)
        {
            // var pgo= go.transform.parent;

            Destroy(go);
            Destroy(gameObject);
            ScorePoints();
        }

        void DoDeath()
        { }

        void DoMonkey(GameObject go)
        {
            if (go == null)
            {
                // Debug.Log("go null " + Time.time);
                return;

            }

            Debug.Log("Hit Monkey tag: " + go.tag);
            //Debug.Log("Player " + player.name);

            if (fire && 
                (go.CompareTag(hitTag) || (go.name == hitName)))
            {
                Debug.Log("Hit Match Tag: " + go.tag);
                NPCStats estats = go.GetComponent<NPCStats>();
                if (estats != null)
                {
                    /*
                    float pts = estats.hp - points;
                    if (pts <= 0)
                    {
                        if (aclip)
                        {
                            destroyDelay = aclip.length;
                            Debug.Log("aclip: " + destroyDelay);
                        }
                    } else
                    */
                    estats.TakeDamage(this.points);   // give object damage

                }

                if (asrc && sound)
                {
                    PlayOneSound();
                    destroyDelay = sound.length + 0.01f;
                }



                fire = false;
            }

        }

        private GameObject cgo;

        void OnTriggerEnter(Collider col)
        {


            Debug.Log("Enter " + col.name + " " + "Mask " + col.gameObject.layer + Time.time);

            if (col.gameObject.layer == 3)
            {
                cgo = col.gameObject;
                Debug.Log("Mask " + col.gameObject.layer);
                Debug.Log("b: " + (1 << 3).ToString());

            }

        }
        void OnTriggerStay(Collider col)
        {
            if (col.gameObject.layer == 3)
            {
                cgo = col.gameObject;
                Debug.Log("stay Mask " + col.gameObject.layer);
                Debug.Log("b: " + (1 << 3).ToString());

            }
            Debug.Log("Stay " + col.name + " ");
        }

        private void OnTriggerExit(Collider col)
        {
            cgo = null;
            Debug.Log("Exit " + col.name + " " + Time.time);
        }


        void OnCollisionEnter(Collision col)
        {
            // DoMonkey(col.gameObject);
        }

        private void Update()
        {
            bool gb = Input.GetButtonDown("Fire1");
            if (gb)
            {
                // Debug.Log("Swing " + Time.time);
                DoMonkey(cgo);
                cgo = null;


            }
        }
    }
}