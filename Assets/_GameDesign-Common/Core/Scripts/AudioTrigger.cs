using UnityEngine;
using System.Collections;

namespace Spidergram
{
    [RequireComponent(typeof(AudioSource))]

    public class AudioTrigger : MonoBehaviour
    {
        // Spidergram Corp(c)
        // Alejandro Jauco
        // Update: 12/1/2020 fixed code to enable looping. fixed exit too.

        // the object this script is attached to requires
        // a collider and IsTrigger=true

        public AudioClip TheSound;
        public float Volume = 1.0f;

        public bool loop = false;  //loop the sound on trigger stay
        public bool destroySelf = false;
        public float destroydelay = 1.0f;

        public void PlayOneSound(AudioClip sound, float volume)
        {
            // KeyCode key;

            if (sound == null)
            {
                Debug.Log("No sound");
                return;
            }
            GetComponent<AudioSource>().clip = sound;
            GetComponent<AudioSource>().volume = volume;
            GetComponent<AudioSource>().loop = loop;
            // GetComponent<AudioSource>().PlayOneShot(sound);
            GetComponent<AudioSource>().Play();
            /*
            Debug.Log("Trigger sound "
                + " " + sound.name
                + " " + volume.ToString()
                + Time.time);
                */
        }

        public IEnumerator OnTriggerEnter(Collider hit)
        {
            Debug.Log("audio trigger " + hit.name);

            if (GetComponent<AudioSource>().isPlaying)
                yield return 0;

            PlayOneSound(TheSound, Volume);
            if (destroydelay < TheSound.length)
                destroydelay = TheSound.length;

            yield return new WaitForSeconds(destroydelay);

            if (destroySelf)
                Destroy(this.gameObject);

        }

        public IEnumerator xOnTriggerStay(Collider hit)
        {
            if (loop == false)
                yield return 0;

            Debug.Log("audio trigger stay " + hit.name);
            GetComponent<AudioSource>().clip = TheSound;
            GetComponent<AudioSource>().volume = Volume;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(TheSound.length);
            if (destroySelf)
                Destroy(this.gameObject);

        }

        public IEnumerator OnTriggerExit(Collider hit)
        {
            yield return 0;

            if (GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().loop = loop;
                //while (GetComponent<AudioSource>().isPlaying)
                //    ; // stay here till it stops

                // Debug.Log("audio trigger exit " + hit.name);
                yield return new WaitForSeconds(TheSound.length);

                GetComponent<AudioSource>().Stop();


            }
        }

    }
}