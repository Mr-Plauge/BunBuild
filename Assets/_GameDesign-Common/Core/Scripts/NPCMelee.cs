using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
Spidergram Corp (c)
Alejandro Jauco
5/6/2020
*/

public class NPCMelee : MonoBehaviour
{
    public string playerTag = "Player";
    public string hitTag = "Player";
    // public Slider healthbar;
    public float points = 10;
    public float deltaTime = 2f;  // every 2 secs send an hp 
    private GameObject player;
    private float timer, etime;

    Animator anim;

    public void OnTriggerEnter(Collider other)
    {

        // Debug.Log("Enter");

        if (other.gameObject.tag != playerTag) return;

        player.SendMessage("HealthPoints", points);


        timer = Time.time;
        etime = timer + deltaTime;

       /*
        if (healthbar)
        {

            healthbar.value -= points;

            if (healthbar.value <= 0 && anim != null)
                anim.SetBool("isDead", true);


        }
        */
    }

   public void OnTriggerStay(Collider other)
    {
        // Debug.Log("Stay");
        if (other.gameObject.tag != playerTag) return;
        if (timer >= etime)
        {
            player.SendMessage("HealthPoints", points);
            timer = Time.time;
            etime = timer + deltaTime;
            // Debug.Log("time " + timer);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Debug.Log("Melee: Exit");
    }

    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        anim = GetComponent<Animator>();
        timer = 0;

    }
    
    void Update()
    {
        timer += Time.deltaTime;
    }
}
