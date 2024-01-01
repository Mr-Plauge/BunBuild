using UnityEngine;
using System.Collections;

namespace Spidergram
{


    public class BulletDestroyAndScoreHealth : MonoBehaviour
    {
        /* Spidergram Corporation (c)
         * Alejandro Jauco
         * add this routine to the bullet
         * Update: May 5, 2020. Added Audio sound when collided with
         * 4/20/2022 update to taglist
        */

        public string[] destroyTaglist = { "destroyable", "Enemy" };
        public string hitName = "enemy";
        public float points = 10;

        private AudioSource asrc;
        public AudioClip sound;  // sound when player hit
        public float vol;  // the volume 0 - 1.0

        private GameObject player;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            asrc = GetComponent<AudioSource>();
        }

        void ScorePoints()
        {
            var go = player;

            if (go)
                go.SendMessage("ScorePoints", points, SendMessageOptions.DontRequireReceiver);
            else
            {
                Debug.Log("CANNOT SCORE POINTS" + points);
            }
        }

        void HealthPoints()
        {
            var go = player;

            if (go)
            {
                Debug.Log("Send HP ");
                go.SendMessage("HealthPoints", points, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.Log("CANNOT Health POINTS" + points);
            }
        }

        void DoDestroy(GameObject go)
        {
            // var pgo= go.transform.parent;

            Destroy(go);
            Destroy(gameObject);
            ScorePoints();
        }

        public void PlayOneSound(AudioClip sound, float volume)
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
        }

        void OnTriggerEnter(Collider col)
        {

            PlayOneSound(sound, vol);

            if (col.gameObject.tag == player.gameObject.tag)
            {
                HealthPoints();
                Debug.Log("Hit " + col.gameObject.tag);
                Destroy(gameObject, 0.01f);
                return;
            }
            foreach (var hitTag in destroyTaglist)
            {
                if ((col.gameObject.tag == hitTag) || (col.gameObject.name == hitName))
                {
                    DoDestroy(col.gameObject);
                    Debug.Log("Trigger " + col.gameObject.tag);
                }
            }


        }

        void OnCollisionEnter(Collision col)
        {

            PlayOneSound(sound, vol);

            if (col.gameObject.name == player.name)
            {
                HealthPoints();
                Debug.Log("Hit Player");
                return;
            }


            foreach (var hitTag in destroyTaglist)
            {
                if ((col.gameObject.tag == hitTag) || (col.gameObject.name == hitName))
                {
                    //		Destroy(col.gameObject);
                    //		Destroy(gameObject);
                    //		ScorePoints();

                    DoDestroy(col.gameObject);

                    Debug.Log("Collision " + col.gameObject.tag);
                }
            }
        }
    }
}