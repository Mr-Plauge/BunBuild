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
    * Nov 28, 2022. Convert to using SendMessage();
    * Dec 6, 2022. useStay for Lava
    */


    public enum pickupTypex
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

    public class Pickup : MonoBehaviour
    {


        public pickupType puType = pickupType.None;
        public int value = 1;
        public string playerTag = "Player";
        public string playerName = "Player";
        public bool useTrigger = true;  // force collider to trigger
        public bool useDestroy = true; // destroy this object after trigger
        public bool useHide = true;  // hide this game object after use essentially rendering the object not accessable
        public bool useStay = false; // used for standing on lava
        private bool isStay = false; // if useStay is true, isStay will be available over time


        private AudioSource asrc;
        public AudioClip sound;  // sound when player hit
        public float volume = 1f;  // the volume 0 - 1.0


        public GameObject player;


    Renderer rend;

        void Start()
        {
            GameObject go = GameObject.FindGameObjectWithTag(playerTag);
            Collider collider = GetComponent<Collider>();
            if (collider && useTrigger)
                collider.isTrigger = true;

            asrc = GetComponent<AudioSource>();
            rend = GetComponent<Renderer>();

            if (go)
                player = go;


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
                if (useHide)
                {
                    Collider col = GetComponent<Collider>();
                    col.enabled = false;  // turn off collider so it doesn't work

                    if (rend != null)
                    {
                        rend.enabled = false;
                    }

                }


                if (player)
                {
                    switch (puType)
                    {
                        case pickupType.None:
                            Debug.Log("pickupType not Set");
                            break;
                        case pickupType.Coin:
                            Debug.Log("pickup Coin");
                            player.SendMessage("ScorePoints",value);
                            break;
                        case pickupType.Health:
                            player.SendMessage("AddHealth", value);
                            break;
                        case pickupType.Key1:
                            player.SendMessage("AddKey1", value);
                            break;
                    }
                }
                // PlayOneSound();

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


        private void OnTriggerStay(Collider other)
        {
            if (useStay == false)
                return;

            // Debug.Log("TriggerStay " + other.transform.name);

            isStay = true;
        }
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("TriggerExit " + other.transform.name);

            isStay = false;
        }


        public float timeRate = 0.2f;
        private float currTime = 0f;


        void Update()
        {


            // Take away or give points over time
            if (timeRate > 0f && isStay)
            {
                currTime += Time.deltaTime;

                if (currTime >= timeRate)
                {
                    // Debug.Log("time " + currTime);
                    // GameObject player = GameObject.FindGameObjectWithTag("Player");

                    if (player != null)
                        DoPickup(player);
                    currTime = 0f;
                }
            }
        }

    }


}
