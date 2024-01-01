using UnityEngine;
using System.Collections;
namespace Spidergram
{


    public class BulletDamageHealth : MonoBehaviour
    {
        /*
        // Spidergram Corporation (c)
        // Alejandro Jauco

        // add this routine to the bullet
        
         * 4/20/2022 update to taglist
        */

        public string[] destroyTaglist = { "destroyable", "Enemy" };
        public string hitName = "enemy";
        public float points = 10;

        private GameObject go;


        void DamagePoints()
        {

            if (go)
                go.SendMessage("HealthPoints", points);
            else
            {
                Debug.Log("CANNOT Health POINTS " + points);
            }
            Destroy(this.gameObject);
        }

        void OnTriggerEnter(Collider col)
        {
            go = null;
            foreach (var hitTag in destroyTaglist)
            {
                if ((col.gameObject.tag == hitTag) || (col.gameObject.name == hitName))
                {
                    go = col.gameObject;
                    DamagePoints();
                    Debug.Log("Trigger " + col.gameObject.tag);
                }
            }


        }


        void OnCollisionEnter(Collision col)
        {
            go = null;
            foreach (var hitTag in destroyTaglist)
            {
                if ((col.gameObject.tag == hitTag) || (col.gameObject.name == hitName))
                {
                    go = col.gameObject;
                    DamagePoints();

                    Debug.Log("Collision " + col.gameObject.tag);
                }
            }
        }
    }
}