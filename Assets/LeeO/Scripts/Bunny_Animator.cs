using UnityEngine;
using System.Collections;

public class Bunny_Animator : MonoBehaviour {
    
    Animator animator;
    public Collider cc;
    public bool isMenu = false;
    private float waveFuse;
	private float distToGround;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
		distToGround = cc.bounds.extents.y;
    }
    
    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool moving = (h != 0f || v != 0f || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow));

        animator.SetBool("Moving", moving);
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("IsAiming", IsAiming());
        animator.SetBool("IsMenu", IsMenu());

    }
	bool IsAiming() {
        return Input.GetKey(KeyCode.Mouse1);
    }
	bool IsGrounded(){
		return !Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
    bool IsMenu() {
        return isMenu;
    }
}