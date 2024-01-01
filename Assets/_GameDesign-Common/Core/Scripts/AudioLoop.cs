using UnityEngine;
using System.Collections;
namespace Spidergram
{

	[RequireComponent(typeof(AudioSource))]

	public class AudioLoop : MonoBehaviour
	{
		// Spidergram Corp(c)
		// Alejandro Jauco

		/*
		AudioLoop.js plays an audio clip continuosly.  Hitting P or S will start or stop the audio clip
		*/


		public AudioClip BackSound;         // the audio file to play
		public float Volume = 0.75f;  // starting volume

		bool useLoop = true;  // play in a loop if true, otherwise play just once on Awake

		private bool togglePlay = true;


		void Awake()
		{
			if (useLoop)
				GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource>().clip = BackSound;
			GetComponent<AudioSource>().volume = Volume;
			GetComponent<AudioSource>().Play();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{

				togglePlay = !togglePlay;

				if (togglePlay)
				{
					GetComponent<AudioSource>().Play();
					Debug.Log("Playing " + BackSound.name);
				}
				else
				{

					GetComponent<AudioSource>().Stop();
					Debug.Log("Stopping " + BackSound.name);
				}

			}
		}


	}
}