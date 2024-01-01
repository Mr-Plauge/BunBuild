using UnityEngine;
using System.Collections;

namespace Spidergram
{
	[RequireComponent(typeof(AudioSource))]

	public class AudioShoot : MonoBehaviour
	{
		// Spidergram Corp(c)
		// Alejandro Jauco
		/*

		 AudioShoot.js play an audio clip when Fire1 is pressed
		 attach this script to the Player.

		12/2/2022 check for isPlaying
		 */

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
			if (GetComponent<AudioSource>().isPlaying)
				return;
			GetComponent<AudioSource>().volume = volume;
			GetComponent<AudioSource>().PlayOneShot(sound);
		}

		void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				PlayOneSound(TheSound, Volume);
			}
		}

	}
}