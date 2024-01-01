using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Piece : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;
    public int PieceType;
    public Grid_Detector gridDetector;
    private GameObject[] displayPiece = new GameObject[12];
    private bool shrinking = false;

    void Awake() {
        PieceType = Random.Range(2,12);
        for (int i = 0; i < 11; i++) {
            displayPiece[i] = transform.GetChild(i).gameObject;
            displayPiece[i].SetActive(false);
        }
        displayPiece[PieceType - 1].SetActive(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            gridDetector.Inventory[PieceType] += 1;
            audioSource.PlayOneShot(audioClip);
            transform.localScale *= 1.5f;
            shrinking = true;
            gridDetector.DoorTip();
        }
    }

    void Update() {
        if (shrinking) {
            float shrinkCurve = 0.2f;
            shrinkCurve += 0.2f;
            transform.localScale = new Vector3(transform.localScale.x - shrinkCurve, transform.localScale.y - shrinkCurve, transform.localScale.z - shrinkCurve);
            if (transform.localScale.x < 0.2f) {
                Destroy(gameObject);
            }
        }
    }
}
