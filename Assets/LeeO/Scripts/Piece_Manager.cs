using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece_Manager : MonoBehaviour
{
    public int PieceType = 1;
    public Grid_Detector gridDetector;
    public Vector3 GridLocation = new Vector3(0,0,0);
    public bool Instantiated = false;
    public GameObject[] Doors = new GameObject[4];

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Grid_Detector>().GridIndex = new Vector3((int)GridLocation.x + 50, 0, (int)GridLocation.z + 50);
        }
    }

    void Update()
    {
        Doors[0] = transform.GetChild(2).gameObject;
        Doors[1] = transform.GetChild(3).gameObject;
        Doors[2] = transform.GetChild(4).gameObject;
        Doors[3] = transform.GetChild(5).gameObject;
        int[] cardinalsX = {49, 51, 50, 50};
        int[] cardinalsZ = {50, 50, 49, 51};
        for (int i = 0; i < 4; i++) {
            if (gridDetector.WorldGrid[((int)GridLocation.x + cardinalsX[i]), ((int)GridLocation.z + cardinalsZ[i])] != null && gridDetector.WorldGrid[((int)GridLocation.x + cardinalsX[i]), ((int)GridLocation.z + cardinalsZ[i])].Instantiated) {
                Doors[i].SetActive(false);
            }
            else {
                Doors[i].SetActive(true);
            }
        }
        gridDetector.WorldGrid[(int)GridLocation.x + 50, (int)GridLocation.z + 50] = this;
    }
}