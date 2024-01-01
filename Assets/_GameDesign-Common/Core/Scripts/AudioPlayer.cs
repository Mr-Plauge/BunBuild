using UnityEngine;
using System.Collections;

namespace Spidergram
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioPlayer : MonoBehaviour
	{
		// Spidergram Corp(c)
		// Alejandro Jauco

		// attach to any object.  Will play sound immediately or after startDelay

		public AudioClip TheSound;
		public float Volume = 1.0f;
		public bool destroySelf = false;
		public float destroyDelay = 0f;
		public float startDelay = 1.0f;

		private AudioSource src;
		private float timer = 0f;

		IEnumerator PlayTheSound()
		{

			src.volume = Volume;
			timer = Time.deltaTime;
			src.PlayOneShot(TheSound);
			timer = TheSound.length;

			Debug.Log("timer " + timer);

			while (timer > 0f)
			{
				// stay till it is done.
				timer -= Time.deltaTime;
				Debug.Log("timer " + timer);
			}


			if (destroySelf)
				Destroy(this.gameObject);

			//if (timer > destroyDelay)
			//		yield return null;
			//else
			yield return new WaitForSeconds(0);
			Debug.Log("Play Done" + Time.time);
		}

		void PlayIt()
		{


			//src.PlayOneShot(TheSound);
			src.Play();
			//timer = TheSound.length	;
			/*while(timer > 0f)
			{
				// stay till it is done.
				timer -= Time.deltaTime;
				Debug.Log("timer "  + timer);
			}
			*/


			if (destroySelf)
				Destroy(this.gameObject,
					(TheSound.length > destroyDelay) ? TheSound.length : destroyDelay);
		}

		public IEnumerator Start()
		{

			src = GetComponent<AudioSource>();
			src.volume = Volume;
			src.clip = TheSound;
			timer = startDelay;

			if (TheSound == null)
			{
				Debug.Log("No sound");
				yield return null;
			}
			//Debug.Log("Audio Start player " + this.name);
			//StartCoroutine(PlayTheSound());
			// PlayIt();
			//Debug.Log("Audio End player" + Time.time);
			// yield return new WaitForSeconds(src.);



			yield return null;
		}

		void Update()
		{


			if (!src.isPlaying && timer <= 0)
			{
				PlayIt();
				timer = startDelay;
			}
			timer -= Time.deltaTime;
			//Debug.Log("timer " + timer);
		}

	}
}