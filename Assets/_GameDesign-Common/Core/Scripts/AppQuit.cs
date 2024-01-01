using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Spidergram Corp (c)
Alejandro Jauco
5/6/2020
*/
namespace Spidergram
{

    public class AppQuit : MonoBehaviour
    {
        public KeyCode quitkey = KeyCode.Q;
        public bool useCtrl = true;

        void Update()
        {
            bool ans;

            if (useCtrl)
                ans = (useCtrl && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)));
            else
                ans = true;


            if (Input.GetKey(quitkey) && ans)
            {
                Time.timeScale = 1.0f;
                Debug.Log("App Quit");

                Application.Quit();
            }
        }
    }
}