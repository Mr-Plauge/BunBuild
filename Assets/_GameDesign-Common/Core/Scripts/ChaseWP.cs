using UnityEngine;
using UnityEngine.AI;
using System.Collections;
/*
 * 9/22/2021 AMJ updated to include navmesh attacking and animations
 * 12/8/2021 AMJ remove rigidbody and fixed rotSpeed
 * 2/23/2022 AMJ added tagName variable
 * 11/28/2022 AMJ streamline public/private vars
 */


namespace Spidergram
{
// [RequireComponent((typeof(Rigidbody)))]
[RequireComponent((typeof(NavMeshAgent)))]

    public class ChaseWP : MonoBehaviour
    {
        public string tagName = "Player";

        [HideInInspector]
        public Transform player;
        public Transform head;
        public Animator anim;
        NavMeshAgent agent;
        // Rigidbody rb;


        public string state = "patrol";
        int currentWP = 0;
        public float rotSpeed = 720f;  // angular speed in degrees/sec.  This is influenced by max accel and agent approach
        private float speed = 0f;
        public float walkSpeed = 1.0f;
        public float runSpeed = 3f;
        public float attackDist = 2f;
        public float stoppingDist = 1.5f;

        // walk speed of 1
        // run speed of 2

        public GameObject[] waypoints;
        public float WPdist = 1.0f;
        public float chaseRadius = 5f;  // chase if within this radius


        // Use this for initialization
        void Start()
        {
            if (anim == null)
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
                Debug.Log("Requires component: NavMeshAgent");

            GameObject go=null;

            if (player == null)
                go = GameObject.FindGameObjectWithTag(tagName);

            if (go != null)
            {
                player = go.transform;

            }
            if (player == null)
            {
                Debug.LogError("" + tagName + ". not found");
            }
            if (head == null)
            {
                head = player;
            }
            speed = 0f;  // idle
            if (anim)
            {
                anim.SetFloat("Speed", speed);
            }
            agent.autoBraking = false;
            agent.stoppingDistance = stoppingDist;
            agent.angularSpeed = rotSpeed;
        }

        // Update is called once per frame
        void Update()
        {

            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            direction.Normalize();
            float angle = Vector3.Angle(direction, head.up);
            // Debug.Log("Angle " + angle);

            // Debug.Log("State " + state);
            if (state == "patrol" && waypoints.Length < 2)
            {
                speed = 0f; // idle
                if (anim)
                {
                    // anim.SetBool("isIdle", true);
                    //anim.SetBool("isWalking", false);
                    anim.SetFloat("Speed", speed);
                    agent.speed = speed;

                }

            }

            // patrol with waypoints
            if (state == "patrol" && waypoints.Length >= 2)
            {

                if (waypoints[currentWP] == null)  // do nothing
                {
                    Debug.LogError("Null Waypoint Node");

                }

                speed = walkSpeed;
                agent.speed = speed;
                if (anim)
                {

                    // anim.SetBool("isIdle", false);
                    // anim.SetBool("isWalking", true);
                    anim.SetFloat("Speed", speed);
                }

                float dist = Vector3.Distance(waypoints[currentWP].transform.position, transform.position);
                // Debug.Log("Dist:" + dist.ToString("0.00"));


                if (dist < WPdist)
                {

                    currentWP = Random.Range(0, waypoints.Length);

                    //currentWP++;
                    //if(currentWP >= waypoints.Length)
                    //{
                    //	currentWP = 0;
                    //}	
                }

                //rotate towards waypoint
                direction = waypoints[currentWP].transform.position - transform.position;
                //this.transform.rotation = Quaternion.Slerp(transform.rotation,
                //	                 Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                //this.transform.Translate(0, 0, Time.deltaTime * speed);
                agent.SetDestination(waypoints[currentWP].transform.position);
                agent.stoppingDistance = 123f;  // set it to reach the WP

                anim.SetFloat("Speed", speed);
                Debug.Log("WP Dest  " + waypoints[currentWP].transform.position);
            }

            angle = 0f;


            // chase the player
            float distDelta = Vector3.Distance(player.position, this.transform.position);
            if ((distDelta < chaseRadius && (angle < 30 || state == "pursuing"))
                || state == "isAttacking")
            {

                state = "pursuing";
                speed = runSpeed;

                agent.SetDestination(player.position);
                // agent.stoppingDistance = stoppingDist;
                agent.speed = speed;

                //this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                //							Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

                if
                    // (direction.magnitude > 2f)
                    (distDelta > attackDist)
                {
                    // chase the player

                    //this.transform.Translate(0,0,Time.deltaTime * speed);
                    if (anim)
                    {
                        //anim.SetBool("isWalking", true);
                        //anim.SetBool("isAttacking", false);
                        anim.SetFloat("Speed", speed);  // this will make it run/chase after player
                    }
                }
                else
                {
                    // attack the player
                    state = "isAttacking";
                    if (anim 
                        // && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")
                       )
                    {
                        // Debug.Log("Attack ");
                        // anim.SetBool("isAttacking", true);
                        // anim.SetBool("isWalking", false);
                        anim.SetFloat("Speed", 0f);

                        anim.SetTrigger("isAttacking");
                        //agent.speed = 0f;

                    }
                }

            }
            else
            {

                state = "patrol";
                if (anim)
                {

                    // anim.SetBool("isWalking", true);
                    // anim.SetBool("isAttacking", false);
                    anim.SetFloat("Speed", speed);
                }
            }


        }


    }



}

