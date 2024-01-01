using UnityEngine;
using System.Collections;
/*
Spidergram
Alejandro Jauco
*/

namespace Spidergram
{
	[RequireComponent(typeof(AudioSource))]

	public class AudioCollision : MonoBehaviour
	{


		/*
		AudioCollision will play a sound when a collision occurs
		
		12/2/2022 check for isPlaying
		*/

		public AudioClip TheSound;
		public float Volume = 1.0f;
		public AudioSource aus;

        private void Start()
        {
			aus = GetComponent<AudioSource>();
        }

        public void PlayOneSound(AudioClip sound, float volume)
		{
			// KeyCode key;

			if (sound == null)
			{
				Debug.Log("No sound");
				return;
			}
			if (aus.isPlaying)
				return;

			aus.volume = volume;
			aus.PlayOneShot(sound);
		}

		public void OnCollisionEnter(Collision hit)
		{
			//	Debug.Log("collide " + hit.collider.name + " " + Time.time);
			PlayOneSound(TheSound, Volume);
		}

	}
}