using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FadeIn : MonoBehaviour
{   
    public Grid_Detector gridDetector;
    public AudioSource audioSource;
    public bool chase = false;
    public bool canBegin = false;
    public int initialFuse;
    public Blackout_Manager blackout;
    public CharacterControls_Innshire playerInnshire;

    void Awake() {
        audioSource.volume = 0;
    }
    void Update() {
        if (!blackout.Toggled) {
            canBegin = true;
            if (initialFuse < 100) {
                initialFuse++;
            }
        }
        else {
            canBegin = false;
            initialFuse = 0;
            audioSource.volume = 0;
        }
        if (canBegin && initialFuse >= 100 && !playerInnshire.ended) {
            if ((gridDetector.inDanger && chase == false) || (!gridDetector.inDanger && chase == true)) {
                if (audioSource.volume > 0) {
                    audioSource.volume -= 0.01f;
                }
            }
            else if ((gridDetector.inDanger && chase == true) || (!gridDetector.inDanger && chase == false)) {
                if (audioSource.volume < 1) {
                    audioSource.volume += 0.01f;
                }
            }
        }
        if (playerInnshire.ended) {
                audioSource.volume = 0;
        }
    }
}