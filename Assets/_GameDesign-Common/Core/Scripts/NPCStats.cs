using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Spidergram Corporation
 * Alejandro Jauco
 * Dec 3, 2019
 * May 5, 2020 instantiate deathObject before dying.  Add when hit
 */


namespace Spidergram
{


    [RequireComponent(typeof(AudioSource))]

    public class NPCStats : MonoBehaviour
    {


        public float hp = 30;  // 0 = dead
        public float maxHP = 200;
        public int killpts = 10;  // amount of points given to the player
        public string playerTag = "Player";
        public Text Healthtext;
        public string losetext = "";
        private GameObject player;
        private Animator anim;
        private AudioSource asrc;  // The audiosource to play the sound
        public bool useDestroy = true;
        public float destroyDelay = 0f; // delay the destroy
        public AudioClip hitclip;  // NPC got hit, play a sound
        public float volume = 1f;
        public AudioClip deathclip;
        public GameObject deathObject; // if destroy, instantiate this death object before destroy()


        void Restart()
        {

            if (Healthtext)
                Healthtext.text = " ";
        }

        void Start()
        {

            player = GameObject.FindGameObjectWithTag(playerTag);
            asrc = GetComponent<AudioSource>();
            anim = GetComponent<Animator>();

            Restart();
            if (anim == null)
                anim = GetComponent<Animator>();
        }

        public void TakeDamage(int inpoints)
        {

            hp = hp - inpoints;
            if (hp < 0)
            {
                hp = 0;
                gameObject.tag = "Untagged";

            }

            if (asrc && hitclip)
            {
                asrc.PlayOneShot(hitclip);
            }

            if (Healthtext)
            {
                if (hp <= 0)
                {
                    Healthtext.text = losetext;

                }

                Healthtext.text = " " + hp;
            }
            // Debug.Log("TakeDamage: " + transform.name + " HP " + inpoints);
            return;
        }

        void DoDeath()
        {
            Debug.Log("DoDeath");
            Collider[] cs = GetComponentsInChildren<Collider>();

            foreach (Collider coll in cs)
                coll.enabled = false;

            if (deathObject != null)
            {
                Instantiate(deathObject, transform.position, transform.rotation);
            }
            if (deathclip != null && asrc != null)
            {
                // AudioSource.PlayClipAtPoint(deathclip, transform.position, volume);
                asrc.volume = volume;
                asrc.PlayOneShot(deathclip);
                Debug.Log("Play Death Clip");
            }

            if (useDestroy)
            {
                Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rs)
                    r.enabled = false;

                if (deathclip != null && destroyDelay < deathclip.length)
                    destroyDelay = deathclip.length;

                Destroy(this.gameObject, destroyDelay);
                Debug.Log("Destroyed " + this.transform.name);
            }
        }

        public void AddHealth(int inpoints)
        {
            if (hp <= 0)
                return;

            hp = hp + inpoints;
            hp = Mathf.Min(hp, maxHP);

            if (hp <= 0)
            {
                hp = 0;
                gameObject.tag = "Untagged";
            }

            if (Healthtext)
            {
                Healthtext.text = " " + hp;
                if (hp <= 0)
                {
                    Healthtext.text = losetext;
                }

            }
            Debug.Log("Player: HP " + inpoints);
        }


        public void HealthPoints(int inpoints)
        {
            TakeDamage(inpoints);
        }


        public void xHealthPoints(int inpoints)
        {
            hp = hp - inpoints;

            if (Healthtext)
            {

                if (hp <= 0)
                {
                    hp = 0;
                    gameObject.tag = "Untagged";
                    Healthtext.text = transform.name + " is dead";
                    if (player)
                    {
                        player.SendMessage("ScorePoints", killpts);
                    }
                }
                else
                    Healthtext.text = transform.name + " hp " + hp;
            }
            Debug.Log("HP " + hp);
        }


        void OnDestroy()
        {

            // Debug.Log("OnDestroy " );
        }

        bool done = false;

        void Update()
        {

            if (done)
                return;

            if (hp <= 0)
            {

                done = true;
                // Debug.Log("AAA we done");

                if (anim)
                {
                    anim.SetTrigger("dead");
                }
                // Debug.Log("NPCStats Score me: " + transform.name);

                if (player != null)
                {
                    Debug.Log("NPCStats Score me: " + transform.name + " killpts " + killpts);

                    player.SendMessage("ScorePoints", killpts, SendMessageOptions.DontRequireReceiver);


                    /*
                    PlayerPoints5 pp = player.GetComponent<PlayerPoints5>();
                    if (pp != null)
                    {
                        pp.ScorePoints(killpts);
                    }
                    */

                }

                DoDeath();

            }

        }


    }

}