using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonsterWall : MonoBehaviour
{
    public GameObject monster;
    void Update() {
        float OffsetX = Time.time * 0.8f;
        float OffsetY = Time.time * 0.8f;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
        //transform.eulerAngles = new Vector3(0, Random.Range(0,2) == 1 ? 0 : 180, 0);
        transform.position = monster.transform.position;
    }
}