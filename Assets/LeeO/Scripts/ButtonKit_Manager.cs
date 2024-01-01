using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonKit_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public bool Held;
    public int PieceType = 1;
    public bool Empty = true;
    public RawImage rawImage;
    public Grid_Detector gridDetector;

    void Awake()
    {
        Held = false;
        rawImage = GetComponent<RawImage>();
        rawImage.texture = gridDetector.PieceTexture[PieceType];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gridDetector.Inventory[PieceType] + gridDetector.Additives[PieceType] > 0) {
            Held = true;
            gridDetector.holdingAny = true;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Held = false;
        gridDetector.holdingAny = false;
    }

    void Update() {
        Color adjustedColor = GetComponent<RawImage>().color;
        adjustedColor.a = (Held ? 1f : 0.5f);
        GetComponent<RawImage>().color = adjustedColor;
        if (Held && Input.GetMouseButtonDown(0)) {
            gridDetector.SelectedPiece = PieceType;
            audioSource.PlayOneShot(audioClip);
        }
    }

    void OnDisable() {
        Held = false;
    }
}
