using UnityEngine;
using System.Collections;

public class MoveLensTarget : MonoBehaviour {

    private Transform target;
    private Animator anim;
    private Transform lens;
    private MeshRenderer lensMesh;

    void Awake()
    {
        target = GameObject.Find("LensTarget").transform;
        anim = GameObject.Find("Player").GetComponent<Animator>();
        lens = GameObject.Find("Player/Lens/ReflectedLensLight").transform;
        lensMesh = GameObject.Find("Player/Lens").GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (FirstPersonViewToggle.FirstPerson)
        {
            //Show the lens
            lensMesh.enabled = true;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 500))
            {
                target.position = hit.point;
            }
            else
            {
                target.position = transform.position + transform.TransformDirection(Vector3.forward) * 10;
            }
            lens.LookAt(target);
        }
        else
        {
            //Hide the lens
            lensMesh.enabled = false;
        }
    }
}
