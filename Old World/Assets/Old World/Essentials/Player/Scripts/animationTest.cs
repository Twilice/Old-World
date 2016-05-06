using UnityEngine;
using System.Collections;

public class animationTest : MonoBehaviour {

    Animator m_Animator;
    bool FPV = false;
    // Use this for initialization
    void Start () {

        m_Animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        m_Animator.SetFloat("Forward", Input.GetAxis("Vertical"), 0.1f, Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.K))
        {
            FPV = !FPV;
            m_Animator.SetBool("FirstPerson", FPV);
        }
    }
}
