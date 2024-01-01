using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsoleMouse : MonoBehaviour
{
    private Canvas canvas;
    private Camera mainCamera;
    public RawImage rawImage;
    public Grid_Detector gridDetector;
    public bool Toggled;
    
    public void Awake() {
        rawImage.texture = gridDetector.PieceTexture[0];
        canvas = transform.root.GetComponent<Canvas>();
        mainCamera = Camera.main;
        Toggle(false);
    }

    void Update() {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
        rawImage.texture = gridDetector.PieceTexture[gridDetector.SelectedPiece];
    }

    public void Toggle(bool value) {
        if (value == false) {
            transform.position = new Vector2(-999, -999);
        }
        Toggled = value;
        gameObject.SetActive(value);
    }
}
