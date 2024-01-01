using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Spidergram Corp (c)
Alejandro Jauco
5/6/2020
*/



public class ShowMessage : MonoBehaviour
{
    [SerializeField]
    Canvas messageCanvas;

    void Start()
    {
        messageCanvas = GetComponentInChildren<Canvas>();
        messageCanvas.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TurnOnMessage();
        }
    }

    private void TurnOnMessage()
    {
        messageCanvas.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TurnOffMessage();
        }
    }

    private void TurnOffMessage()
    {
        messageCanvas.enabled = false;
    }
    void LateUpdate()
    {
        messageCanvas.transform.rotation = Camera.main.transform.rotation;
    }
}