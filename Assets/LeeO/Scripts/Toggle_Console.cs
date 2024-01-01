using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toggle_Console : MonoBehaviour
{
    public Console console;
    public PlayerPivot pivot;
    public GameObject raySource1;
    public GameObject raySource2;
    public GameObject doorNotifier;
    
    public void Awake() {
        console.Toggle(false, false);
    }
    
    void Update() {
		RaycastHit hit1;
		RaycastHit hit2;
        bool lookingAtDoor = false;
        if (Physics.Raycast(raySource1.transform.position, raySource1.transform.TransformDirection(Vector3.forward), out hit1, 15) && hit1.collider.gameObject.GetComponent<Door_Manager>() && !console.Toggled && Physics.Raycast(raySource2.transform.position, raySource2.transform.TransformDirection(Vector3.forward), out hit2, 15) && hit2.collider.gameObject.GetComponent<Door_Manager>() && !console.Toggled) {
            pivot.doorRot = hit1.transform;
            lookingAtDoor = true;
        }
        doorNotifier.SetActive(lookingAtDoor);
        if (Input.GetKeyDown(KeyCode.E) && (console.Toggled || lookingAtDoor))
        {
            console.Toggle(!console.Toggled);
        }
    }
}
