using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tip_Manager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public int stayDuration;

    public void Tip(string tipText) {
        stayDuration = 600;
        audioSource.PlayOneShot(audioClip);
        gameObject.SetActive(true);
        text.text = tipText;
    }

    public void Update() {
        if (stayDuration > 0) {
            stayDuration--;
        }
        if (stayDuration == 0) {
            audioSource.PlayOneShot(audioClip);
            gameObject.SetActive(false);
        }
    }
}