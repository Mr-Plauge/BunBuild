using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spidergram
{

    public class FinishLine : MonoBehaviour
    {
        private GameObject Player;

        public int FinishPoints = 100;
        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                PlayerManager2 pm2 = col.gameObject.GetComponent<PlayerManager2>();

                pm2.ScorePoints(FinishPoints);
                Debug.Log("Finish line");
            }
        }
    }
}

