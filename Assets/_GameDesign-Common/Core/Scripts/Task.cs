using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spidergram
{

    /*
    Spidergram Corp (c)
    Alejandro Jauco
    5/6/2020
    */

    /*
     * 
     * Task
     * Common - everyone must do
     * Short - click button, timer
     * Long - multiple steps
     * Visual - something with an animation can see (clear asteroids)
     */


    public class Task : MonoBehaviour
    {
        public TaskType Type; // task type
        public string Name;  // name of task
        public string Description; // short description of what the player needs to do
                                   // public float Timer; // count down timer if needed
        public bool isComplete = false;

        public GameObject TaskObject; // must be a prefab. object which will be enabled when task is executed

        Canvas theCanvas;
        Text theHeader;

        void Start()
        {
            theCanvas = GetComponentInChildren<Canvas>();

            if (theCanvas == null)
            {
                return;
            }

            if (TaskObject != null)
            {
                TaskObject.SetActive(false);
            }

            if (theHeader == null)
            {


                GameObject go = null;
                var gos = theCanvas.gameObject.GetComponentsInChildren<Text>();

                foreach (Text te in gos)
                {
                    if (te.transform.name == "Header")
                    {
                        go = te.gameObject;
                        // Debug.Log("aaa " + go.transform.name);
                    }

                }
                if (go != null)
                    theHeader = go.GetComponentInChildren<Text>();
                if (theHeader != null)
                {
                    theHeader.text = Description;
                }

            }
        }

        void Update()
        {
            if (isComplete && TaskObject != null)
            {
                MeshRenderer render = gameObject.GetComponent<MeshRenderer>();
                if (render)
                    render.enabled = false;

                GameObject go = Instantiate(TaskObject, transform.position, transform.rotation);
                go.SetActive(true);


            }

        }

    }
}