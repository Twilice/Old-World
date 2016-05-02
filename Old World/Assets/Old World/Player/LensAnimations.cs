using UnityEngine;
using System.Collections;

public class LensAnimations : MonoBehaviour
{
    private SkinnedMeshRenderer smr;
    private MoveLensTarget mlt;

    private float value = 0.0f;
    private bool open = false;
    private bool idleDone = false;

    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        mlt = GameObject.Find("_Camera/MainCamera").GetComponent<MoveLensTarget>();
    }

    void Update()
    {
        //Check if lens is activated
        if (mlt.LensActivated)
        {
            //Checks if lens is fully opened in the animation
            if (!open)
            {
                if (value <= 100)
                {
                    //Opens the lens for the first time
                    value += Time.deltaTime * 120;
                    smr.SetBlendShapeWeight(1, value);
                }
                else
                {
                    open = true;
                }
            }
            else if (open)
            {
                //Checks if idle anim is done
                if (idleDone)
                {
                    if (value >= 50)
                    {
                        //Closes the lens
                        value -= Time.deltaTime * 60;
                        smr.SetBlendShapeWeight(1, value);
                    }
                    else
                    {
                        idleDone = false;
                    }
                }
                //Checks if it should start the idle anim
                else if (!idleDone)
                {
                    if (value <= 100)
                    {
                        //Opens lens
                        value += Time.deltaTime * 60;
                        smr.SetBlendShapeWeight(1, value);
                    }
                    else
                    {
                        idleDone = true;
                    }

                }
            }
        }
        else
        {
            //Resets the anim once the orb is gone
            open = false;
            value = 0;
            smr.SetBlendShapeWeight(1, 0);
            idleDone = true;
        }
    }
}