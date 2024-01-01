using UnityEngine;
using System.Collections;

namespace Spidergram
{

	public class BulletDestroyAfterTimeLimit : MonoBehaviour
	{
		// Destroy an object after delta time elapses

		// Spidergram Corp (c)
		// Alejandro Jauco
		//

		public float tdelay = 1.0f;  // die after 1 second

		private float knt = 0;
		void Start()
		{
			knt = 0.0f;
		}

		void Update()
		{

			knt += Time.deltaTime;

			if (knt >= tdelay)
			{
				Destroy(gameObject);
				// Debug.Log("Destroyed " + gameObject.name);
			}

		}
	}
}