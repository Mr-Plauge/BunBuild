using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Spidergram;

public class Console : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public ConsoleMouse consoleMouse;
    public Grid_Detector gridDetector;
    public PlayerManager2 playerManager2;
    public GameObject pivot;
    public Blackout_Manager blackout;
    public bool Toggled;
    
    public void Awake() {
        consoleMouse.Toggle(false);
    }

    void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && (gridDetector.holdingAny || consoleMouse.Toggled))
        {
            consoleMouse.Toggle(true);
        }
        else {
            consoleMouse.Toggle(false);
        }
        if (Toggled) {
            playerManager2.showCursor = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            playerManager2.showCursor = false;
            Cursor.visible = false;
		    Cursor.lockState = CursorLockMode.Locked;
        }
        Toggled = gameObject.activeSelf;
    }

    public void Toggle(bool value, bool playSound = true) {
        Toggled = value;
        if (playSound) {
            if (value == true) {
                audioSource.PlayOneShot(audioClip1);
                gridDetector.GridInterfaceTip();
            }
            else {
                audioSource.PlayOneShot(audioClip2);
            }
        }
        gameObject.SetActive(value);
        consoleMouse.Toggle(false);
		pivot.transform.rotation = pivot.GetComponent<PlayerPivot>().storedRotation;
        
        Array.Clear(gridDetector.SimulatedGrid, 0, gridDetector.SimulatedGrid.Length);
        gridDetector.SimulatedGrid = gridDetector.WorldGrid;
        
        Array.Clear(gridDetector.Additives, 0, gridDetector.Additives.Length);
    }
}
