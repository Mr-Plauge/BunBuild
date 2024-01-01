using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Spidergram Corp (c)
Alejandro Jauco
5/5/2020
*/

namespace Spidergram
{
    public class AudioPlayDestroy : MonoBehaviour
    {
        public AudioClip clip;



        // Start is called before the first frame update
        void Start()
        {
            Renderer rend;
            rend = GetComponent<Renderer>();
            rend.enabled = false;
            if (clip != null)
            {

                //AudioSource.PlayClipAtPoint(clip, transform.position);
                Destroy(gameObject, clip.length);

            }
        }

    }
}