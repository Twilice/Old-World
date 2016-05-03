using UnityEngine;
using System.Collections;

public class LensAnimations : MonoBehaviour
{
    private SkinnedMeshRenderer smr;
    private MoveLensTarget mlt;

    private float value = 0.0f;
    private float sinValue = - Mathf.PI / 2;

    private bool open = false;

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
                if (value <= 90)
                {
                    //Opens the lens for the first time
                    value = (Mathf.Sin(sinValue) + 1) * 50;
                    smr.SetBlendShapeWeight(1, value);
                    sinValue += 2f * Time.deltaTime;
                }
                else
                {
                    open = true;
                    sinValue = Mathf.PI / 2;
                }
            }
            else if (open)
            {
                //Plays the idle animation
                value = (Mathf.Sin(sinValue) + 1) * 30 + 40;
                smr.SetBlendShapeWeight(1, value);
                sinValue += 2f * Time.deltaTime;
            }
        }
        else
        {
            //Resets the anim once the orb is deactivated
            open = false;                        
            value = 0.0f;
            smr.SetBlendShapeWeight(1, 0);
            sinValue = -Mathf.PI / 2;
        }
    }
}