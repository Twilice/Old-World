using UnityEngine;
using System.Collections;

public class LensAnimations : MonoBehaviour
{
    private SkinnedMeshRenderer smr;
    private MoveLensTarget mlt;

    private float value = 0.0f;
    bool open = false;
    bool idleDone = false;

    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        mlt = GameObject.Find("_Camera/MainCamera").GetComponent<MoveLensTarget>();
    }

    void Update()
    {
        if (mlt.LensActivated)
        {
            if (!open)
            {
                if (value <= 100)
                {
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
                if (idleDone)
                {
                    if (value >= 50)
                    {
                        value -= Time.deltaTime * 60;
                        smr.SetBlendShapeWeight(1, value);
                    }
                    else
                    {
                        idleDone = false;
                    }
                }
                else if (!idleDone)
                {
                    if (value <= 100)
                    {
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
            open = false;
            value = 0;
            smr.SetBlendShapeWeight(1, 0);
            idleDone = true;
        }
    }
}