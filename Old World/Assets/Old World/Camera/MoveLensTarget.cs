using UnityEngine;
using System.Collections;

public class MoveLensTarget : MonoBehaviour
{

    private Transform target;
    private Transform lens;
    private Transform lensLight;
    private Transform targetLens;
    private MeshRenderer lensMesh;
    private Collider collision;
    private LensReflect lensScript;
    private Vector3 originalLocalLensPos;
    private Quaternion originalLocalLensRot;
    private Vector3 originalLocalLightPos;
    private Quaternion originalLocalLightRos;
    private Vector3 originalLensPos;
    private Quaternion originalLensRot;
    [HideInInspector]
    public bool LensActivated = false;
    [HideInInspector]
    public bool LensDropped = false;

    private int layerMask;
    void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("LightShaft");
        layerMask += 1 << LayerMask.NameToLayer("Player");
        layerMask = ~layerMask;
        target = GameObject.Find("LensTarget").transform;
        lens = GameObject.Find("Player/Lens").transform;
        lensLight = GameObject.Find("Player/Lens/ReflectedLensLight").transform;
        lensMesh = lens.GetComponent<MeshRenderer>();
        collision = lens.GetComponent<Collider>();
        lensScript = lens.GetComponent<LensReflect>();
        targetLens = GameObject.Find("Player/TargetLens").transform;
    }

    //Update is called once per frame
    void LateUpdate()
    {
        if (StateController.menuOpen == false)
        {
            if (LensDropped || FirstPersonViewToggle.FirstPerson)
            {
                //Show the lens
                LensActivated = true;
                lensMesh.enabled = true;
                collision.enabled = true;

                if (LensDropped == false)
                {
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
                }

                //Check if lens is dropped
                if (Input.GetButtonDown("LensDrop") && !LensDropped)
                {
                    //The lens is dropped
                    LensDropped = true;

                    //Save lens original local position
                    originalLocalLensPos = lens.transform.localPosition;
                    originalLocalLensRot = lens.transform.localRotation;

                    //Save lens original world position
                    originalLensPos = lens.transform.position;
                    originalLensRot = lens.transform.rotation;

                    //Save light original local position
                    originalLocalLightPos = lensLight.transform.localPosition;
                    originalLocalLightRos = lensLight.transform.localRotation;

                    //De-parent light from lens
                    lensLight.transform.parent = null;
                }
                //Check if lens is getting returned
                else if (Input.GetButtonDown("LensDrop") && LensDropped)
                {
                    targetLens.LookAt(lens);
                    RaycastHit hit;


                    if (Physics.Raycast(targetLens.transform.position, targetLens.transform.forward, out hit, 500, layerMask, QueryTriggerInteraction.Collide))
                    {
                        if (hit.collider.transform.CompareTag("Prism"))
                        {
                            //Not dropped anymore
                            LensDropped = false;

                            //Hide the lens
                            lensMesh.enabled = false;
                            collision.enabled = false;
                            LensActivated = false;
                            lensScript.inLight = false;
                            //Give back the light's parent, the lens
                            lensLight.transform.parent = lens;

                            //Move back the light to the lens
                            lensLight.transform.localPosition = originalLocalLightPos;
                            lensLight.transform.localRotation = originalLocalLightRos;

                            //Move the lens back to the player
                            lens.transform.localPosition = originalLocalLensPos;
                            lens.transform.localRotation = originalLocalLensRot;
                        }
                    }
                }
            }
            else
            {
                //Hide the lens
                lensMesh.enabled = false;
                collision.enabled = false;
                LensActivated = false;
                lensScript.inLight = false;
            }
            if (LensDropped)
            {
                lens.transform.position = originalLensPos;
                lens.transform.rotation = originalLensRot;
            }
        }
    }
}
