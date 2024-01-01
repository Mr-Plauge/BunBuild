
// #define USEMODULARFPC

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;



namespace Spidergram
{

    [RequireComponent(typeof(AudioSource))]

    public class PlayerManager1 : MonoBehaviour
    {
        /*
         *  Spidergram Corp (c)
         * Alejandro Jauco
         * 
         * Updated: Apr 8, 2020
         * Updated: Dec 3 2019 added enemy attack with NPCstats
         * Updated: Nov 2 2020 play win or lose clips when win or lose
         * Updated: 12/7/2020 added portal
         * Updated: 9/13/2021 fixed restartkey string to show correct letter
         * Updated: 10/21/2021 added a show the time when timer is <= ShowTimeValue
         * Updated: 11/3/2021 added a showCursor toggle
         * Updated: 4/13/2022 disable FirstPersonController
         * Updated: 11/28/2022 pre-processor define for modular FirstPersonController
         */
        //

        public bool usePortal = false;  // change this if you want to require player to get out of level with portal

        public bool useDisableFPC = false;  //turn off the FPC

        public float hp = 100;  // 0 = dead
        public float maxHP = 200;
        public float winpoints = 100f;   // score this and you are a winner
        public float startpoints = 0; // start with something greater than lose value

        // private float losevalue = -1;

        /// GUIText scoretext;  // Old Gui system
        public Text Scoretext;
        public Text Healthtext;
        public Text statusText;
        public Text timeText;
        public float timeShowTimeValue = 10f;  // 10 secs, display time left once time is nearing lose condition
        public float timeLimit = 180f;  // must finish all before time runs out!
        public float timeCountdown;

        public bool areTasksDone;


        public string timeString = "Timeout. You Lose";  // timer goes to zero what happens? Do you win or lose?

        public string wintext = "WINNER";
        public string losetext = "LOSER";

        public int winlevelnum = -1;
        public int loselevelnum = -1;

        // public string pickupTag = "Pick Up";
        //public string pickupName = "Coin";

        private AudioSource asrc;
        public AudioClip hitclip;  // sound when player hit
        public AudioClip winclip;  // sound when player wins
        public AudioClip loseclip;  // sound when player loses


        public float volume = 1f;  // the volume 0 - 1.0
        public bool showCursor = false;  // set to true to force showing the cursor.


        static float winstatus = 0; //  1=win, -1=lose, 0=nothing

        public float total = 0;
        public KeyCode restartKey = KeyCode.T;

        void CursorReset()
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1.0f;

        }

        void ResetPlayer()
        {
            winstatus = 0;
            total = 0;
            ResetPoints();
            ScorePoints(0);
            if (Healthtext)
                Healthtext.text = " " + hp;

            timeCountdown = timeLimit;
            if (timeText != null)
                timeText.text = "";
            if (statusText != null)
                statusText.text = "";
            if (showCursor)
                CursorReset();
        }

        void Restart()
        {
            ResetPlayer();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        void Start()
        {
            areTasksDone = false;
            asrc = GetComponent<AudioSource>();
            ResetPlayer();
            Debug.Log("Win Points is " + winpoints);
        }


        public void ScorePoints(int inpoints)
        {
            total += inpoints;
            /*
            if (scoretext)
            {
                scoretext.text = " " + total;
                if (total >= winpoints)
                  scoretext.text = wintext;
            }
            */

            if (Scoretext)
            {
                Scoretext.text = " " + total;
                if (total >= winpoints)
                {
                    statusText.text = wintext;
                    total = winpoints;
                }
            }
            // Debug.Log("Player: Score " + inpoints + " Total " + total);

        }

        public void TakeDamage(int inpoints)
        {
            hp = hp - inpoints;

            if (Healthtext)
            {
                Healthtext.text = " " + hp;
                if (hp <= 0)
                {
                    if (statusText != null)
                        statusText.text = losetext;
                    hp = 0;
                    Healthtext.text = " " + hp;
                    gameObject.tag = "Untagged";
                }
            }
            if (asrc && hitclip)
            {
                Debug.Log("HIT Sound");
                asrc.PlayOneShot(hitclip);
            }
            Debug.Log("Player: HP " + inpoints);
            return;
        }


#if USEMODULARFPC

        public void disableFPC()
        {
            FirstPersonController fpc = GetComponent<FirstPersonController>();
            if (fpc)
            {
                fpc.enabled = false;
            }
        }

        public void enableFPC()
        {
            FirstPersonController fpc = GetComponent<FirstPersonController>();
            if (fpc)
            {
                fpc.enabled = true;
            }
        }

#endif

        public void AddHealth(int inpoints)
        {
            hp = hp + inpoints;
            hp = Mathf.Min(hp, maxHP);

            if (Healthtext)
            {
                Healthtext.text = " " + hp;
                if (hp <= 0)
                {
                    if (statusText != null)
                        statusText.text = losetext;
                    hp = 0;
                    gameObject.tag = "Untagged";
                }
            }
            Debug.Log("Player: HP " + inpoints);
        }


        public void HealthPoints(int inpoints)
        {
            TakeDamage(inpoints);
        }

        public void AddKey1(int points)
        {

        }

        public void ResetPoints()
        {
            total = startpoints;
        }

        void PlayWinSound()
        {
            if (asrc && winclip)
            {
                Debug.Log("WIN Sound");
                asrc.PlayOneShot(winclip);
            }
        }

        void PlayLoseSound()
        {
            if (asrc && loseclip)
            {
                Debug.Log("Lose Sound");
                asrc.PlayOneShot(loseclip);
            }
        }

        void CheckWin()
        {

            // if ((total >= winpoints) || areTasksDone)
            if (total >= winpoints)
            {
                Debug.Log("YOU WIN ");
                Debug.Log("T " + total + " " + winpoints);
                /*
                if (scoretext)
                    scoretext.text = "YOU WIN";
                    */
                PlayWinSound();

#if USEMODULARFPC
                if (useDisableFPC)
                    disableFPC();
#endif

                winstatus = 1;

                if (winlevelnum >= 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(winlevelnum);

            }
        }

        void CheckLose()
        {
            // if (total <= losevalue)

            if (timeLimit > 0 && timeCountdown <= 0)
            {
                winstatus = -1;  // lose
                Debug.Log("Time ran out");

                if (Scoretext)
                    statusText.text = losetext;

                PlayLoseSound();

#if USEMODULARFPC
                if (useDisableFPC)
                    disableFPC();
#endif

                if (loselevelnum >= 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(loselevelnum);
            }

            if (hp <= 0)
            {
                Debug.Log("YOU Lose");
                winstatus = -1;
                if (loselevelnum >= 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(loselevelnum);
            }
        }

        void OnDestroy()
        {
            Time.timeScale = 1.0f;  // always reset the timeScale
                                    // Debug.Log("OnDestroy timeScale=" + Time.timeScale);
        }

        void Update()
        {
            if (Input.GetKeyDown(restartKey))
            {
                Restart();
            }
            if (winstatus != 0)
                return;


            CheckWin();
            CheckLose();
        }

        private void FixedUpdate()
        {

            if (timeCountdown > 0f)
                timeCountdown -= Time.fixedDeltaTime;

            if (timeCountdown <= 0f)
            {
                timeCountdown = 0f;
                if (statusText)
                    statusText.text = timeString;
                return;
            }
            if (timeText && timeCountdown <= timeShowTimeValue + 0.01f)
                timeText.text = Mathf.FloorToInt(timeCountdown + 0.5f).ToString();
        }

        void OnGUI()
        {
            if (winstatus != 0 && !usePortal)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.01f;

#if USEMODULARFPC
                if (useDisableFPC)
                    disableFPC();
#endif

                if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 20, 200, 30), restartKey.ToString() + " to Restart"))
                {
#if USEMODULARFPC
                if (useDisableFPC)
                    enableFPC();
#endif

                    Time.timeScale = 1.0f;
                    // Screen.lockCursor = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //Application.LoadLevel(0);  // restart the game
                    if (winlevelnum >= 0)
                        UnityEngine.SceneManagement.SceneManager.LoadScene(winlevelnum);
                    else
                        Restart();
                    Debug.Log("Restart");
                }
            }
        }

        void OnCollisionEnter(Collision col)
        {

            if (col.gameObject.tag == "Bullet")
            {
                Debug.Log("Collision " + col.gameObject.tag);
            }
        }

        public void TasksCompleted()
        {
            areTasksDone = true;
        }

    }
}