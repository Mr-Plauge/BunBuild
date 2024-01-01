using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Spidergram
{

    public class PlayMenuScene : MonoBehaviour
    {


        public int SceneNum = 1;

        public void PlayScene()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneNum);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void xRestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RestartLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void NextLevel()
        {
            int curr = SceneManager.GetActiveScene().buildIndex;
            int next;

            next = (curr + 1 < SceneManager.sceneCount) ? curr + 1 : 0;
            Debug.Log("Next Scene is " + next);
            SceneManager.LoadScene(next);
        }

    }
}