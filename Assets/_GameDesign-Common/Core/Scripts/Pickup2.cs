using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spidergram
{
    // modified Apr 15, 2020
    /*
    *  Spidergram Corp (c)
    * Alejandro Jauco
    * 
    * Apr 15, 2020
    * Updated: May 5, 2020 added audio
    * Aug 1, 2020. extend pickupTypes
    * Feb 9, 2021. fixed disable collider to only happen when hit object matches
    * Oct 21, 2021.  Added Object pickups and moved keys to top
    */
    public enum pickupType
    {
        None,  // default
        Coin,
        Health,
        Key1,
        /*
    Object1,
    Object2,
    Object3,

    Key2,
    Key3,
    Key4,

    Weapon1,
    Weapon2,
    Weapon3,
    Ammo1,
    Ammo2,
    Ammo3,
    Shield1,
    Shield2,
    Shield3,
    Armor1,
    Armor2,
    Armor3,
    Helmet1,
    Helmet2,
    Helmet3,
    Fruit1,
    Fruit2,
    Fruit3,
    Buff,
    Debuff,
    Portal,
    Star,
    Potion1,
    Potion2,
    Potion3,
    Secret1,
    Secret2,
    Secret3,
    Speed,
    Strength,
    Agility,

    */
        Unknown
    }

    [RequireComponent(typeof(AudioSource))]

    public class Pickup2 : MonoBehaviour
    {


        public pickupType puType = pickupType.None;
        public int value = 1;
        public string playerTag = "Player";
        public string playerName = "Player";
        public bool useTrigger = true;
        public bool useDestroy = true;

        private AudioSource asrc;
        public AudioClip sound;  // sound when player hit
        public float volume = 1f;  // the volume 0 - 1.0

        Renderer rend;

        void Start()
        {
            Collider collider = GetComponent<Collider>();
            if (collider && useTrigger)
                collider.isTrigger = true;

            asrc = GetComponent<AudioSource>();

            rend = GetComponent<Renderer>();

        }

        public void PlayOneSound()
        {

            if (sound == null)
            {
                Debug.Log("No sound");
                return;
            }
            // AudioSource asrc = GetComponent<AudioSource>();
            if (asrc == null)
            {
                Debug.Log("Missing Audiosource");
                return;
            }

            asrc.volume = volume;
            asrc.PlayOneShot(sound);
            Debug.Log("Make a sound: " + sound.name);
        }

        IEnumerator PlayIt()
        {
            float len;

            if (useDestroy)
            {
                Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rs)
                    r.enabled = false;
                // gameObject.GetComponent<MeshRenderer>().enabled = false; // make it look like it disappeared

            }

            if (sound == null || asrc == null)
            {
                Debug.Log("No Audio");
                yield return null;
            }
            else
            {
                asrc.volume = volume;
                len = sound.length;
                asrc.PlayOneShot(sound);
                yield return new WaitForSeconds(len + 0.1f);
                // Debug.Log("Make a sound: " + sound.name);
            }

            if (useDestroy)
            {

                Destroy(gameObject);
            }




        }

        void DoPickup(GameObject go)
        {


            // Debug.Log("name " + go.name);

            if (go.CompareTag(playerTag) || go.name == playerName)
            {
                Collider col = GetComponent<Collider>();
                col.enabled = false;  // turn off collider so it doesn't work

                PlayerManager1 pp = go.GetComponent<PlayerManager1>();
                if (pp == null)
                {
                    Debug.Log("NO PlayerManager found");
                }

                if (pp)
                {
                    switch (puType)
                    {
                        case pickupType.None:
                            Debug.Log("pickupType not Set");

                            // pp.HealthPoints(value);  // HealthPoints takes away.  Legacy
                            break;
                        case pickupType.Coin:
                            Debug.Log("pickup Coin");
                            pp.ScorePoints(value);
                            break;
                        case pickupType.Health:
                            pp.AddHealth(value);
                            break;
                        case pickupType.Key1:
                            pp.AddKey1(value);
                            break;
                    }
                }
                // PlayOneSound();

                if (rend != null)
                {
                    rend.enabled = false;
                }
                StartCoroutine(PlayIt());


                return;

                /*

                if (useDestroy)
                {
                    Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in rs)
                        r.enabled = false;
                    // gameObject.GetComponent<MeshRenderer>().enabled = false; // make it look like it disappeared

                    float v;

                    v = 0.0f;
                    if (sound != null)
                        v += sound.length;
                    Destroy(gameObject, v);
                }
                */



            }

        }


        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("CollisionEnter " + other.transform.name);
            DoPickup(other.gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("TriggerEnter " + other.transform.name);
            DoPickup(other.gameObject);
        }

    }
}
