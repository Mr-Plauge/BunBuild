using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Spidergram Corp (c)
Alejandro Jauco
5/6/2020
*/
namespace Spidergram
{

    public class HitCancel : MonoBehaviour
    {
        // public string button = "Cancel";
        public KeyCode LevelExit = KeyCode.Escape;

        public int SceneNum = 0;
        // public string SceneName = "MainMenu";

        public KeyCode quitkey = KeyCode.Q;  // app  quit key

        void CheckAppQuit()
        {
            if (Input.GetKeyDown(quitkey))
            {
                Debug.Log("Quit App");
                Time.timeScale = 1.0f;
                ShowCursor();
                Application.Quit();
            }
        }

        void CheckLeaveLevel()
        {
            if (Input.GetKeyDown(LevelExit))
            {

                Debug.Log("Leave the Level");
                Time.timeScale = 1.0f;
                ShowCursor();
                PlayScene();
            }
        }


        public void PlayScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNum);

            //Debug.Log("Call Scene: " + SceneNum);
        }
        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Debug.Log("Show");
        }

        // Update is called once per frame
        void Update()
        {

            CheckAppQuit();
            CheckLeaveLevel();
            return;
            /*
            if (Input.GetButtonDown(button))
            {
                ShowCursor();
                PlayScene();
                //SceneManager.LoadScene(SceneName);
                //Debug.Log("Call Scene: " + SceneName);
            }
            */
        }
    }

}