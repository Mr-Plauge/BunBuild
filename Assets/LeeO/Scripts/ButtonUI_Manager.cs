using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Spidergram;

public class ButtonUI_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public int Mode = 0;
    public int SceneNum = 1;
    public bool Held;
    public int actionFuse;
    public bool activated = false;
    public Blackout_Manager blackout;

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
        blackout.Toggle(true);
        activated = true;
    }

    void Update() {
        Color adjustedColor = GetComponent<RawImage>().color;
        adjustedColor.a = (Held ? 1f : 0.5f);
        GetComponent<RawImage>().color = adjustedColor;
        if (blackout.Toggled) {
            actionFuse++;
        }
        if (actionFuse == 15 && activated) {
            UIFunction(Mode);
            Debug.Log(Mode);
        }
    }

    void OnDisable() {
        Held = false;
    }

    public void UIFunction(int mode) {
        actionFuse = 0;
        if (mode == 0) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (mode == 1) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneNum);
        }
        if (mode == 2) {
            Application.Quit();
        }
    }
}