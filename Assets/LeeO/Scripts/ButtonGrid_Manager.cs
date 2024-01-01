using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonGrid_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public bool Held;
    public int PieceType = 1;
    public bool Empty = true;
    public RawImage rawImage;
    public Grid_Detector gridDetector;
    public Vector3 RelativeGridIndex = new Vector3(0,0,0);

    void Awake()
    {
        Held = false;
        rawImage = GetComponent<RawImage>();
        rawImage.texture = gridDetector.PieceTexture[0];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Held = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Held = false;
    }

    void Update() {
        Color adjustedColor = GetComponent<RawImage>().color;
        adjustedColor.a = (Held ? 1f : 0.5f);
        GetComponent<RawImage>().color = adjustedColor;

        if (gridDetector.SimulatedGrid[(int)gridDetector.GridIndex.x + (int)RelativeGridIndex.x, (int)gridDetector.GridIndex.z + (int)RelativeGridIndex.z] != null) {
            rawImage.texture = gridDetector.PieceTexture[gridDetector.SimulatedGrid[(int)gridDetector.GridIndex.x + (int)RelativeGridIndex.x, (int)gridDetector.GridIndex.z + (int)RelativeGridIndex.z].PieceType];
            Empty = false;
        }
        else {
            rawImage.texture = gridDetector.PieceTexture[0];
            Empty = true;
        }
        if (Held && Empty && Input.GetMouseButtonUp(0))
        {
            gridDetector.SimulatedGrid[(int)gridDetector.GridIndex.x + (int)RelativeGridIndex.x, (int)gridDetector.GridIndex.z + (int)RelativeGridIndex.z] = gridDetector.SelectedPiece == 0 ? null : gridDetector.PieceIndex[gridDetector.SelectedPiece].GetComponent<Piece_Manager>();
            gridDetector.Additives[gridDetector.SelectedPiece] -= 1;
            audioSource.PlayOneShot(audioClip);
            gridDetector.GenerateTip();
        }
    }

    void OnDisable() {
        Held = false;
    }
}
