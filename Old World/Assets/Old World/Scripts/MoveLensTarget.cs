using UnityEngine;
using System.Collections;

public class MoveLensTarget : MonoBehaviour {

    private Transform player;
    private Transform target;
    private Transform lens;
    private Transform lensLight;
    private MeshRenderer lensMesh;
    private Vector3 originalLensPos;
    [HideInInspector]
    public bool LensActivated = false;
    [HideInInspector]
    public bool LensDropped = false;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        target = GameObject.Find("LensTarget").transform;
        lens = GameObject.Find("Player/Lens").transform;
        lensLight = GameObject.Find("Player/Lens/ReflectedLensLight").transform;
        lensMesh = GameObject.Find("Player/Lens").GetComponent<MeshRenderer>();
    }
	
	//Update is called once per frame
	void LateUpdate () {
        if (LensDropped || FirstPersonViewToggle.FirstPerson)
        {
            //Show the lens
            LensActivated = true;
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
            lensLight.LookAt(target);

            //Check if LensDrop button is pressed
            if (Input.GetButtonDown("LensDrop") && !LensDropped)
            {
                LensDropped = true;

                lens.transform.parent = null;
            }
            else if (Input.GetButtonDown("LensDrop") && LensDropped)
            {
                LensDropped = false;

                lens.transform.parent = player;
                lens.transform.position = lens.transform.localPosition;
                lens.transform.rotation = lens.transform.localRotation;
            }
        }
        else
        {
            //Hide the lens
            lensMesh.enabled = false;
            LensActivated = false;
        }
    }
}
