using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chair_Vanish : MonoBehaviour
{
    public bool shouldRotate = true;
    public void Awake() {
        if (Random.Range(0, 10) > 1)
        {
            Destroy(gameObject);
        }
        if (shouldRotate)
        {
            transform.Rotate(0, 0, Random.Range(0f, 180f), Space.Self);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Chair_Vanish>() != null)
        {
            col.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}
