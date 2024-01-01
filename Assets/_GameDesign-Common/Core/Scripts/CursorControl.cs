using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spidergram
{

    public class CursorControl : MonoBehaviour
    {
	
        private bool cursorState = false;  // hide = false
	
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1.0f;

            cursorState = false;  // hide the cursor at start
            HideCursor();

        }

        // Update is called once per frame
        public void HideCursor()
        {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Hide");
        }
	
	    public void ShowCursor()
	    {
		    Cursor.visible = true;
		    Cursor.lockState = CursorLockMode.None;
            Debug.Log("Show");
        }

        private void DoCursor()
        {

            if (cursorState)
            {
                ShowCursor();
            }
            else
                HideCursor();
        }
	
	    void Update()
	    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                cursorState = !cursorState;
            }

            DoCursor();

        }
        private void LateUpdate()
        {

        }
    }

}
