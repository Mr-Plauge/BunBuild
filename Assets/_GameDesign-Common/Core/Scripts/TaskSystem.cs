using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Spidergram Corp (c)
Alejandro Jauco
5/6/2020
12/8/2020 bug fix for null tasks
*/
namespace Spidergram
{

    public class TaskSystem : MonoBehaviour
    {
        [SerializeField]
        List<Task> tasks;

        [SerializeField]
        KeyCode key = KeyCode.Tab;

        int taskknt;
        PlayerManager1 playermgr;
        int winpoints = 100000;

        public Canvas taskCanvas;
        public Text taskText;

        public class TStat
        {
            public string name;
            public string desc;
        }

        List<TStat> tstats;

        void GetTaskStats()
        {
            tstats = new List<TStat>();

            foreach (Task task in tasks)
            {
                TStat tstat;
                tstat = new TStat();

                if (task == null)
                    continue;

                tstat.name = task.Name;
                tstat.desc = task.Description;

                tstats.Add(tstat);


            }
        }

        // Start is called before the first frame update
        void Start()
        {
            taskknt = 0;
            playermgr = GetComponent<PlayerManager1>();

            if (taskCanvas != null)
                taskCanvas.gameObject.SetActive(false);
            GetTaskStats();

            if (tasks.Count == 0)
            {
                Debug.Log("NO TASKS LISTED");
            }
        }

        // Update is called once per frame
        void Update()
        {
            int knt = 0;
            foreach (Task task in tasks)
            {
                if (task == null)
                    continue;

                if (task.isComplete == true)
                    knt++;
            }
            taskknt = knt;
            if (taskknt == tasks.Count)
            {
                playermgr.ScorePoints(winpoints);
                playermgr.TasksCompleted();
            }

            if (Input.GetKeyDown(key))
            {
                string stext = "";
                if (taskCanvas != null)
                    taskCanvas.gameObject.SetActive(true);

                foreach (TStat stat in tstats)
                {
                    // string str = "<b>" + stat.name + "</size></b>" + stat.desc;
                    // stext += str + "</br>";
                    string str = stat.name + ": " + stat.desc;
                    stext += str + "\n";


                    //stext += stat.name + ": " + stat.desc + "</br>";

                    Debug.Log("name:" + stat.name);
                    Debug.Log("desc:" + stat.desc);
                }
                if (taskText != null)
                {
                    taskText.text = stext;
                    Debug.Log("AAAA " + stext);

                }
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (taskCanvas != null)
                    taskCanvas.gameObject.SetActive(false);
            }
        }
    }
}