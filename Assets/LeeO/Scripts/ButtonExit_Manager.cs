using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spidergram;

public class ButtonExit_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Grid_Detector gridDetector;
    public Console console;
    public GameObject[] PieceIndex = new GameObject[12];

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
        ExitConsole();
    }

    void Update() {
        Color adjustedColor = GetComponent<RawImage>().color;
        adjustedColor.a = (Held ? 1f : 0.5f);
        GetComponent<RawImage>().color = adjustedColor;
    }

    void OnDisable() {
        Held = false;
    }

    public void ExitConsole() {
        console.Toggle(false);
    }
}