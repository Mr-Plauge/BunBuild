using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Bunny_Marker : MonoBehaviour
{
    public Transform playerRotation;
    public Transform console;

    void Update() {
        transform.eulerAngles = new Vector3(0.0f, 0, -playerRotation.eulerAngles.y) + console.eulerAngles;
    }
}