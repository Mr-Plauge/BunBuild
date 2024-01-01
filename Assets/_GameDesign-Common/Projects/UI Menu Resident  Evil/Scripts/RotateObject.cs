using UnityEngine;


public class RotateObject : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    public GameObject cube1, cube2;


    void Update()
    {
        cube1.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        cube2.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }
}