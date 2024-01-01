using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPScursor : MonoBehaviour
{
	
	private bool toggle = false;
	
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
		HideCursor();
		toggle = false;

    }

    // Update is called once per frame
    public void HideCursor()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log("Hide");
    }
	
	public void ShowCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
        //Debug.Log("Show");
    }
	
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle = !toggle;

        }
        if (toggle)
        {
            ShowCursor();
        }
        else
            HideCursor();
    }
}
