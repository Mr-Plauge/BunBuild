using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid_Detector : MonoBehaviour
{
    public Piece_Manager[,] WorldGrid = new Piece_Manager[100,100];
    public Piece_Manager[,] SimulatedGrid = new Piece_Manager[100,100];
    public Vector3 GridIndex = new Vector3(50,0,50);
    public int SelectedPiece = 0;
    public Texture[] PieceTexture = new Texture[12];
    public GameObject[] PieceIndex = new GameObject[12];
    public bool holdingAny;
    public ButtonGenerate_Manager buttonGenerateManager;
    public int[] Inventory = new int[12];
    public int[] Additives = new int[12];

    public int[] AdjGroup_Up = { 1,2,4,5,8,9,10 };
    public int[] AdjGroup_Down = { 1,3,4,5,6,7,10 };
    public int[] AdjGroup_Left = { 1,2,3,4,6,8,11 };
    public int[] AdjGroup_Right = { 1,2,3,5,7,9,11 };

    public bool inDanger;
    public int fearCounter;

    private bool initialGenerate = false;

    public int roomScore;
    public Text roomScoreText;

    public Tip_Manager tipManager;

    public bool GaveCollectTip = false;
    public bool GaveDoorTip = false;
    public bool GaveGridInterfaceTip = false;
    public bool GaveGenerateTip = false;
    public bool GaveShotgunTip = false;
    public bool GaveObjectiveTip = false;

    public int InitDelay = 80;

    void Awake() {
        InitDelay = 80;
    }

    void Update() {
        roomScoreText.text = roomScore.ToString();
        if (fearCounter > 0) {
            inDanger = true;
            ShotgunTip();
            fearCounter--;
        }
        else {
            inDanger = false;
        }
        if (InitDelay > 0) {
            InitDelay--;
        }
        if (InitDelay == 0) {
            CollectTip();
        }
    }
    void LateUpdate()
    {
        if (!initialGenerate) {
            buttonGenerateManager.GeneratePieces();
            initialGenerate = true;
        }
        if (!Input.GetKey(KeyCode.Mouse0)) {
            SelectedPiece = 0;
        }
    }

    public void CollectTip()
    {
        if (!GaveCollectTip) {
            tipManager.Tip("Tip: Collect puzzle pieces");
            GaveCollectTip = true;
        }
    }

    public void DoorTip()
    {
        if (!GaveDoorTip) {
            tipManager.Tip("Tip: Interact with doors to build");
            GaveDoorTip = true;
        }
    }

    public void GridInterfaceTip()
    {
        if (!GaveGridInterfaceTip) {
            tipManager.Tip("Tip: Drag and arrange puzzle pieces onto the grid");
            GaveGridInterfaceTip = true;
        }
    }

    public void GenerateTip()
    {
        if (!GaveGenerateTip) {
            tipManager.Tip("Tip: When ready, press Generate");
            GaveGenerateTip = true;
        }
    }

    public void ShotgunTip()
    {
        if (!GaveShotgunTip) {
            tipManager.Tip("Tip: Right click to aim, left click to shoot");
            GaveShotgunTip = true;
        }
    }

    public void ObjectiveTip()
    {
        if (!GaveObjectiveTip) {
            tipManager.Tip("Tip: Build outwards to reach the finish line!");
            GaveObjectiveTip = true;
        }
    }
}