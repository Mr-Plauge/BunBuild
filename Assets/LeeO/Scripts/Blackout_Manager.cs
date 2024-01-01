using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Spidergram;

public class Blackout_Manager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioSource music;
    public bool Toggled;
    public int reactivationFuse = 0;
    public bool staying = false;

    void Update() {
        if (reactivationFuse < 5 && staying == false) {
            reactivationFuse++;
        }
        if (reactivationFuse >= 5) {
            Toggle(false);
        }
    }

    public void Toggle(bool value) {
        staying = true;
        reactivationFuse = 0;
        Toggled = value;
        audioSource.PlayOneShot(audioClip);
        gameObject.SetActive(value);
    }
}