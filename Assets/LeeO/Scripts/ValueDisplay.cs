using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ValueDisplay : MonoBehaviour
{
    public Grid_Detector gridDetector;
    public int PieceType;
    private TextMeshProUGUI textMesh;

    void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        textMesh.text = (gridDetector.Inventory[PieceType] + gridDetector.Additives[PieceType]).ToString();
    }
}