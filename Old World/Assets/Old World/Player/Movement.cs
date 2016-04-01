using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //turning
        //jumping
        //move
        Move();
	}

    void Move()
    {
        anim.SetFloat("Forward", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("Sideways", Input.GetAxisRaw("Horizontal"));
    }
}
