using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spidergram;

public class ButtonReset_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Grid_Detector gridDetector;

    public bool Held;

    void Awake()
    {
        Held = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Held = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Held = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClip);
        ResetPieces();
    }

    void Update() {
        Color adjustedColor = GetComponent<RawImage>().color;
        adjustedColor.a = (Held ? 1f : 0.5f);
        GetComponent<RawImage>().color = adjustedColor;
    }

    public void ResetPieces() {
        Array.Clear(gridDetector.SimulatedGrid, 0, gridDetector.SimulatedGrid.Length);
        gridDetector.SimulatedGrid = gridDetector.WorldGrid;
        
        Array.Clear(gridDetector.Additives, 0, gridDetector.Additives.Length);
    }

    void OnDisable() {
        Held = false;
    }
}