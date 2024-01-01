using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spidergram;

public class ButtonGenerate_Manager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;
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
        GeneratePieces();
        audioSource.PlayOneShot(audioClip);
        gridDetector.ObjectiveTip();
    }

    void Update() {
        Color adjustedColor = GetComponent<RawImage>().color;
        adjustedColor.a = (Held ? 1f : 0.5f);
        GetComponent<RawImage>().color = adjustedColor;
    }

    public void GeneratePieces() {
        gridDetector.SimulatedGrid = gridDetector.WorldGrid;
        gridDetector.WorldGrid = gridDetector.SimulatedGrid;
        for (int i = 0; i < 12; i++) {
            gridDetector.Inventory[i] += gridDetector.Additives[i];
        }
        Array.Clear(gridDetector.Additives, 0, gridDetector.Additives.Length);
        for (int x = -6; x < 6; x++) {
            for (int z = -6; z < 6; z++) {
                if (gridDetector.WorldGrid[(int)gridDetector.GridIndex.x + x, (int)gridDetector.GridIndex.z + z] != null && !gridDetector.WorldGrid[(int)gridDetector.GridIndex.x + x, (int)gridDetector.GridIndex.z + z].Instantiated) {
                    Vector3 InitialGrid = new Vector3(-50 + gridDetector.GridIndex.x + x, 0, -50 + gridDetector.GridIndex.z + z);
                    Vector3 AdjustedGrid = new Vector3(InitialGrid.x * 54.69f, 0, InitialGrid.z * 54.69f);
                    GameObject PieceClone = Instantiate(PieceIndex[gridDetector.WorldGrid[(int)gridDetector.GridIndex.x + x, (int)gridDetector.GridIndex.z + z].PieceType], AdjustedGrid, new Quaternion(0, 0, 0, 0));
                    PieceClone.GetComponent<Piece_Manager>().Instantiated = true;
                    PieceClone.GetComponent<Piece_Manager>().GridLocation = InitialGrid;
                    PieceClone.SetActive(true);
                    gridDetector.roomScore += 1;
                }
            }
        }
        console.Toggle(false, false);
    }

    void OnDisable() {
        Held = false;
    }
}