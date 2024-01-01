using UnityEngine;
using System.Collections;


namespace Spidergram
{


    public class BulletStats : MonoBehaviour
    {

        /*
        Spidergram Corp (c)
        Alejandro Jauco
        5/6/2020
        4/20/2022  use a taglist
        */

        // add this routine to the bullet

        public string[] destroyTaglist = { "destroyable", "Enemy" };
        public string hitName = "enemy";
        public int points = 10;
        public float destroyDelay = 0.0f;
        private GameObject player;

        private AudioSource asrc;
        public AudioClip sound;  // sound when player hit
        public float volume = 1f;  // the volume 0 - 1.0



        void Start()
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
                Debug.Log("BulletStats: Player not found.");

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
            if (player == null)
            {
                Debug.Log("Player is null");

            }

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

        void DoMonkey(GameObject go)
        {
            Debug.Log("Hit AAA " + go.tag);
            //Debug.Log("Player " + player.name);


            if (go.tag == player.gameObject.tag)
            {
                HealthPoints();
                Debug.Log("Hit " + go.tag);
                Destroy(gameObject, destroyDelay);
                return;
            }

            foreach (var hitTag in destroyTaglist)
            {
                if (go.CompareTag(hitTag) || (go.name == hitName))
                {
                    Debug.Log("Hit Tag " + go.tag);
                    NPCStats estats = go.GetComponent<NPCStats>();
                    if (estats != null)
                        estats.TakeDamage(this.points);   // give object damage
                    if (asrc && sound)
                    {
                        PlayOneSound();
                        destroyDelay = sound.length + 0.01f;
                    }

                }
            }



            Destroy(this.gameObject, destroyDelay);

        }

        void OnTriggerEnter(Collider col)
        {
            DoMonkey(col.gameObject);
        }

        void OnCollisionEnter(Collision col)
        {
            DoMonkey(col.gameObject);
        }
    }
}