using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spidergram;
using TMPro;

public class Descriptor_Manager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public PlayerManager2 player;
    public CharacterControls_Innshire playerInnshire;
    bool playNoise = false;

    void Update() {
        if (player.hp > 0) {
            GetComponent<TextMeshProUGUI>().text = "ESCAPED";
        }
        else {
            GetComponent<TextMeshProUGUI>().text = "PERISHED";
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerInnshire.ended = true;
        if (playerInnshire.ended && playNoise == false) {
            audioSource.PlayOneShot(audioClip);
            playNoise = true;
        }
    }
}
