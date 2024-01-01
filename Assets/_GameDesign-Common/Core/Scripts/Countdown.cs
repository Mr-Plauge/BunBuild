using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
Spidergram Corp (c)
Alejandro Jauco
5/6/2020
*/
namespace Spidergram
{


    public class Countdown : MonoBehaviour
    {

        // time variables
        public float timeInterval = 10f;  // 10 seconds
        GameObject theObject;
        private bool taskComplete = false;

        private Canvas theCanvas;
        private GameObject theButton;

        // the timeText to update the time to
        private Text timeText;
        public string TaskDone = "Task Complete";

        public float currentTime = 0;

        private bool onTimer;

        public IEnumerator DoTimer()
        {

            onTimer = true;
            // while there are seconds left
            while (currentTime > 0)
            {
                // wait for 1 second
                yield return new WaitForSeconds(1);

                // reduce the time
                currentTime--;
                Debug.Log("Timer " + currentTime);

                UpdateTimerText();
            }
            // game over
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                // Time.timeScale = 0.0f;
                if (timeText != null)
                    timeText.text = TaskDone;
                if (theObject != null)
                    theObject.SetActive(true);
                Debug.Log("Timer end");

                taskComplete = true;
                this.GetComponentInParent<Task>().isComplete = true;

                onTimer = false;

            }


        }

        public void MyReset()
        {
            currentTime = timeInterval; // reset the time if disabled before end
            UpdateTimerText();
            StopCoroutine("DoTimer");
        }

        public void StartTimer()
        {
            MyReset();
            theButton.SetActive(false);

            currentTime = timeInterval - Time.deltaTime;
            // start the timer ticking
            StartCoroutine("DoTimer", 0);

        }

        public float TimeLeft()
        {
            return currentTime;
        }

        private void Start()
        {
            theCanvas = GetComponentInChildren<Canvas>();
            theButton = theCanvas.gameObject.GetComponentInChildren<Button>().gameObject;

            if (timeText == null)
            {


                GameObject go = null; // = theCanvas.gameObject.transform.Find("TimerText").gameObject;
                var gos = theCanvas.gameObject.GetComponentsInChildren<Text>();

                foreach (Text te in gos)
                {
                    if (te.transform.name == "TimerText")
                    {
                        go = te.gameObject;
                        // Debug.Log("aaa " + go.transform.name);
                    }

                }
                if (go != null)
                    timeText = go.GetComponentInChildren<Text>();

            }

            taskComplete = false;
        }


        void Awake()
        {        // retrieve the Component and set the text


            if (theObject != null)
            {

                theObject.SetActive(false); // disable the object
                Debug.Log("Awake false");
            }

            MyReset();

        }

        void UpdateTimerText()
        {
            if (timeText != null)
                timeText.text = currentTime.ToString("#");
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.T))
            {
                StartTimer();
            }
        }

        private void LateUpdate()
        {

            if (taskComplete == true)
                return; // all ready completed

            if (currentTime == timeInterval && theButton.activeSelf == false)
                theButton.SetActive(true);

            if (this.enabled == false)
                Debug.Log("not enabled");
            if (this.isActiveAndEnabled == false)
                Debug.Log("not active");
            if (theCanvas.isActiveAndEnabled == false && onTimer == true)
            {
                onTimer = false;
                MyReset();
                Debug.Log("DisableBBB");
            }


        }


    }
}