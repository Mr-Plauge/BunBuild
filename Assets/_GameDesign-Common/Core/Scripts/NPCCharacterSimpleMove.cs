using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class NPCCharacterSimpleMove : MonoBehaviour {

    /*
    Spidergram Corp(c)
    Alejandro Jauco
    5/6/2020
    Updated 4/21/2021 added movedirection.y = 0 on line 
    */



    public Transform[] waypoint;  // waypoints.  must be at least 2
    public  float speed = 7f;  // speed in meters per second
    public float walkSpeed = 7f;
    private int currentWaypoint = 0;
    public  bool  loop = true;
    public float tgtDelta = 1f; // close enough distance to trget

    public string playerTag = "Player";

    public  Transform player;
    public float inRange = 4f;  // distance agro
    private CharacterController character;

    public  Vector3 m_GroundNormal;
    public  float m_GroundCheckDistance = 0.1f;

    public 	bool m_IsGrounded = true;
    public Animator anim;


    void Start()
    {
        character = GetComponent<CharacterController>();

        if (anim == null)
            anim = GetComponent<Animator>();
        // speed = 0f;  // idle
        if (anim)
        {
            anim.SetFloat("Speed", speed);
        }


        GameObject go;
        go = GameObject.FindGameObjectWithTag(playerTag);
        if (go)
            player = go.transform;
    }

    void Update()
    {
        float dist;
        dist = inRange;
        if (player != null)
        {
            dist = Vector3.Distance(player.transform.position, transform.position);
        }

        if (dist < inRange)
            Chase();
        else
            Wander();
    }

    void Chase()
    {
        Vector3 trget = player.transform.position;
        trget.y = transform.position.y; // keep waypoint at character's height
        Vector3 moveDirection = trget - transform.position;
        moveDirection.y = 0;

        if (moveDirection.magnitude > tgtDelta)
        {


            // Move toward trget
            transform.LookAt(trget);
            CheckGroundStatus();
            character.SimpleMove(moveDirection.normalized * speed); // does not need *Time.deltaTime.  character.Move() does

        }
    }
 
 void Wander (){
        if (waypoint.Length == 0)
            return;

     if(currentWaypoint < waypoint.Length)
     {
         Vector3 trget = waypoint[currentWaypoint].position;

            if (waypoint.Length > 0)
                trget = waypoint[currentWaypoint].position;
            else
                trget = player.position;


            trget.y = transform.position.y; // keep waypoint at character's height
         Vector3 moveDirection = trget - transform.position;
            moveDirection.y = 0;
         if(moveDirection.magnitude < 1)
         {
             transform.position = trget; // force character to waypoint position
             currentWaypoint++;
         }
         else
         {
             transform.LookAt(trget);
             CheckGroundStatus();
             character.SimpleMove(moveDirection.normalized * speed);
         }
     }
     else
     {
         if(loop)
         {
             currentWaypoint=0;
         }
     }
 }

    private bool test = true;

     void CheckGroundStatus (){
	    RaycastHit hitInfo ;
    #if UNITY_EDITOR
	    // helper to visualise the ground check ray in the scene view
	    Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
    #endif
	    // 0.1ff is a small offset to start the ray from inside the character
	    // it is also good to note that the transform position in the sample assets is at the base of the character
	    if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
	    {
		    m_GroundNormal = hitInfo.normal;
		    m_IsGrounded = true;
	    }
	    else
	    {
		    m_IsGrounded = false;
		    m_GroundNormal = Vector3.up;
	    }

        if (m_IsGrounded != test)
        {
            test = m_IsGrounded;
            Debug.Log("Grounded is " + test.ToString());
        }
    }
}