using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Monster_Animator : MonoBehaviour
{
    void Awake() {
        GetComponent<Animator>().SetBool("Monster", true);
    }
    
    void Update() {
        if (transform.parent.gameObject.GetComponent<Monster>().dead) {
            GetComponent<Animator>().SetBool("Dead", true);
        }
    }
}