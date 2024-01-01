using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spidergram
{


    public class PausePlayer : MonoBehaviour
    {
        private GameObject Player;

        private CursorLockMode cursorState;
        private bool cursorVisible;


        // Update is called once per frame
        void Awake()
        {

            cursorVisible = Cursor.visible;  // current cursor visibility state
            cursorState = Cursor.lockState; // current cursor Lock state


            if (PlayerPrefs.GetString("AMJ-Menu") != "AMJ")
            {
                PlayerPrefs.SetString("AMJ-Menu", "AMJ");
            }
            Player = GameObject.FindGameObjectWithTag("Player");



        }

        private void OnEnable()
        {
            if (Player == null)
            {
                return;
            }
            Player.SetActive(false);
            Cursor.visible = true;  // make sure it is visible
            Cursor.lockState = CursorLockMode.None; // lock cursor to the game screen
            Time.timeScale = 0; // stop time
            Debug.Log("OnDisable");
        }

        private void Reset()
        {
            if (Player == null)
            {
                return;
            }

            // Debug.Log("Reset");
            Cursor.lockState = cursorState;
            Cursor.visible = cursorVisible;
            Time.timeScale = 1.0f; // ensure game can play again
                                   // Debug.Log("timeScale: " + Time.timeScale);

        }

        void OnDisable()
        {
            if (Player != null)
                Player.SetActive(true);
            Reset();
            this.gameObject.SetActive(false);

        }

        private void OnDestroy()
        {
            Reset();
        }
    }


}